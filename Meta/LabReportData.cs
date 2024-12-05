using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;


namespace ClinicalXPDataConnections.Meta
{
    public interface ILabData
    {
        public List<LabPatient> GetPatients(string? firstname, string? lastname, string? nhsno, string? postcode, DateTime? dob);
        public LabPatient GetPatientDetails(int intID);
        public List<LabLab> GetCytoReportsList(int intID);
        public LabLab GetCytoReport(string labNo);
        public List<LabDNALab> GetDNAReportsList(int intID);
        public LabDNALab GetDNAReport(string labNo);
        public List<LabDNALabData> GetDNALabDataList(int intID);
        public LabDNAReport GetDNAReportDetails(string labNo, string indication, string reason);
    }
    public class LabReportData : ILabData
    {
        private readonly LabContext _labContext;

        public LabReportData(LabContext context)
        {
            _labContext = context;
        }
        
        public List<LabPatient> GetPatients(string? firstname, string? lastname, string? nhsno, string? postcode, DateTime? dob)
        {
            IQueryable<LabPatient> patients = from p in _labContext.labPatient               //_labContext.labPatient;
                                              where (firstname == null || p.FIRSTNAME == firstname)
                                              && (lastname == null || p.LASTNAME == lastname)
                                              && (nhsno == null || p.SOCIAL_SECURITY == nhsno)
                                              && (dob == null || p.DOB == dob)

                                              select p;
            return patients.ToList();
        }

        public LabPatient GetPatientDetails(int intID)
        {
            LabPatient patient = _labContext.labPatient.FirstOrDefault(p => p.INTID == intID);

            return patient;
        }

        public List<LabLab> GetCytoReportsList(int intID)
        {
            IQueryable<LabLab> reports = from r in _labContext.labLab               
                                              where r.INTID == intID
                                              select r;
            return reports.ToList();
        }

        public LabLab GetCytoReport(string labNo)
        {
            LabLab report = _labContext.labLab.FirstOrDefault(r => r.LABNO == labNo);

            return report;
        }

        public List<LabDNALab> GetDNAReportsList(int intID)
        {
            IQueryable<LabDNALab> reports = from r in _labContext.labDNALab
                                            where r.INTID == intID
                                         select r;
            return reports.ToList();
        }

        public LabDNALab GetDNAReport(string labNo)
        {
            LabDNALab report = _labContext.labDNALab.FirstOrDefault(r => r.LABNO == labNo);

            return report;
        }

        public List<LabDNALabData> GetDNALabDataList(int intID) //we can't make a view in shire_data, and DNALAB doesn't have
        {                                                       //all the data, so we need to build it this way.
            List<LabDNALabData> labData = new List<LabDNALabData>();

            List<LabDNALab> lab = GetDNAReportsList(intID);

            foreach (LabDNALab item in lab)
            {
                List<LabDNAReport> reports = _labContext.labDNAReport.Where(r => r.LABNO==item.LABNO).ToList();
                
                foreach (LabDNAReport item2 in reports)
                {
                    List<LabDNAIndication> indication = _labContext.labDNAIndication.Where(i => i.INDICATION == item2.INDICATION).ToList();

                    foreach (LabDNAIndication item3 in indication)
                    {
                        if (item3.REASON == item2.REASON && item2.LABNO == item3.LABNO)
                        {
                            labData.Add(new LabDNALabData
                            {
                                LABNO = item.LABNO,
                                INTID = item.INTID,
                                INDICATION = item3.INDICATION,
                                REASON = item3.REASON,
                                SEQ = item2.SEQ,
                                DIAGNOSIS_REPORT = item2.DIAGNOSIS_REPORT,
                                REPORT_STATUS = item2.REPORT_STATUS,
                                DATEREQUESTED = item3.DATEREQUESTED,
                                REPORT_DATE = item2.REPORT_DATE
                            });
                        }
                    }
                }
            }            

            return labData;
        }

        public LabDNAReport GetDNAReportDetails(string labNo, string indication, string reason)
        {
            LabDNAReport report = _labContext.labDNAReport.FirstOrDefault(r => r.LABNO == labNo && r.INDICATION == indication && r.REASON == reason);

            return report;
        }
    }
}
