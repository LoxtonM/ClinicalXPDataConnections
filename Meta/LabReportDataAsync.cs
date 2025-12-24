using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace ClinicalXPDataConnections.Meta
{
    public interface ILabDataAsync
    {
        public Task<List<LabPatient>> GetPatients(string? firstname, string? lastname, string? nhsno, string? postcode, DateTime? dob);
        public Task<LabPatient> GetPatientDetails(int intID);
        public Task<List<LabLab>> GetCytoReportsList(int intID);
        public Task<LabLab> GetCytoReport(string labNo);
        public Task<List<LabDNALab>> GetDNAReportsList(int intID);
        public Task<LabDNALab> GetDNAReport(string labNo);
        public Task<List<LabDNALabData>> GetDNALabDataList(int intID);
        public Task<LabDNAReport> GetDNAReportDetails(string labNo, string indication, string reason);
    }
    public class LabReportDataAsync : ILabDataAsync
    {
        private readonly LabContext _labContext;

        public LabReportDataAsync(LabContext context)
        {
            _labContext = context;
        }
        
        public async Task<List<LabPatient>> GetPatients(string? firstname, string? lastname, string? nhsno, string? postcode, DateTime? dob)
        {
            IQueryable<LabPatient> patients = from p in _labContext.labPatient               //_labContext.labPatient;
                                              where (firstname == null || p.FIRSTNAME == firstname)
                                              && (lastname == null || p.LASTNAME == lastname)
                                              && (nhsno == null || p.SOCIAL_SECURITY == nhsno)
                                              && (dob == null || p.DOB == dob)

                                              select p;
            return await patients.ToListAsync();
        }

        public async Task<LabPatient> GetPatientDetails(int intID)
        {
            LabPatient patient = await _labContext.labPatient.FirstAsync(p => p.INTID == intID);

            return patient;
        }

        public async Task<List<LabLab>> GetCytoReportsList(int intID)
        {
            IQueryable<LabLab> reports = from r in _labContext.labLab               
                                              where r.INTID == intID
                                              select r;
            return await reports.ToListAsync();
        }

        public async Task<LabLab> GetCytoReport(string labNo)
        {
            LabLab report = await _labContext.labLab.FirstAsync(r => r.LABNO == labNo);

            return report;
        }

        public async Task<List<LabDNALab>> GetDNAReportsList(int intID)
        {
            IQueryable<LabDNALab> reports = from r in _labContext.labDNALab
                                            where r.INTID == intID
                                         select r;
            return await reports.ToListAsync();
        }

        public async Task<LabDNALab> GetDNAReport(string labNo)
        {
            LabDNALab report = await _labContext.labDNALab.FirstAsync(r => r.LABNO == labNo);

            return report;
        }

        public async Task<List<LabDNALabData>> GetDNALabDataList(int intID) //we can't make a view in shire_data, and DNALAB doesn't have
        {                                                     //all the data, so we need to build it this way.
            var items = await _labContext.labDNALab
                        .AsNoTracking()
                        .Where(l => l.INTID == intID)
                        .Join(_labContext.labDNAReport.AsNoTracking(),
                              l => l.LABNO,
                              r => r.LABNO,
                              (l, r) => new { l, r })
                        .Join(_labContext.labDNAIndication.AsNoTracking(),
                              lr => new { lr.r.INDICATION, lr.r.REASON, lr.r.LABNO },
                              i => new { i.INDICATION, i.REASON, i.LABNO },
                              (lr, i) => new LabDNALabData
                              {
                                  LABNO = lr.l.LABNO,
                                  INTID = lr.l.INTID,
                                  INDICATION = i.INDICATION,
                                  REASON = i.REASON,
                                  SEQ = lr.r.SEQ,
                                  DIAGNOSIS_REPORT = lr.r.DIAGNOSIS_REPORT,
                                  REPORT_STATUS = lr.r.REPORT_STATUS,
                                  DATEREQUESTED = i.DATEREQUESTED,
                                  REPORT_DATE = lr.r.REPORT_DATE
                              })
                        .OrderBy(x => x.LABNO)
                        .ToListAsync();

            return items;
        }

        public async Task<LabDNAReport> GetDNAReportDetails(string labNo, string indication, string reason)
        {
            LabDNAReport report = await _labContext.labDNAReport.FirstAsync(r => r.LABNO == labNo && r.INDICATION == indication && r.REASON == reason);

            return report;
        }
    }
}
