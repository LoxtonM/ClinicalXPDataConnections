using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;

namespace ClinicalXPDataConnections.Meta
{
    public interface IExternalClinicianDataAsync
    {
        public Task<string> GetCCDetails(ExternalClinician referrer);
        public Task<ExternalClinician> GetClinicianDetails(string sref);
        public Task<List<ExternalCliniciansAndFacilities>> GetClinicianList();
        public Task<ExternalCliniciansAndFacilities> GetPatientGPReferrer(int mpi);
        public Task<List<ExternalClinician>> GetAllCliniciansList();
        public Task<List<ExternalClinician>> GetGPList();
        public Task<List<string>> GetClinicianTypeList();
        public Task<List<ExternalCliniciansAndFacilities>> GetExternalCliniciansByType(string type);
    }
    public class ExternalClinicianDataAsync : IExternalClinicianDataAsync
    {
        private readonly ClinicalContext _clinContext;
        
        public ExternalClinicianDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
                
        public async Task<string> GetCCDetails(ExternalClinician referrer) //Get details of CC address
        {
            string cc = "";
            if (referrer.FACILITY != null) //believe it or not, there are actually some nulls!!!
            {
                ExternalFacility facility = await _clinContext.ExternalFacility.FirstAsync(f => f.MasterFacilityCode == referrer.FACILITY);

                cc = cc + Environment.NewLine + facility.NAME + Environment.NewLine + facility.ADDRESS + Environment.NewLine
                    + facility.CITY + Environment.NewLine + facility.STATE + Environment.NewLine + facility.ZIP;
            }
            return cc;
        }

        public async Task<ExternalClinician> GetClinicianDetails(string sref) //Get details of external/referring clinician
        {
            ExternalClinician item = await _clinContext.ExternalClinician.FirstAsync(f => f.MasterClinicianCode == sref);

            return item;
        }

        public async Task<List<ExternalCliniciansAndFacilities>> GetClinicianList() //Get list of all external/referring clinicians with their facilities (no GPs)
        {
            IQueryable<ExternalCliniciansAndFacilities> clinicians = from rf in _clinContext.ExternalCliniciansAndFacilities
                             where rf.NON_ACTIVE == 0 & rf.Is_GP == 0
                             orderby rf.LAST_NAME
                             select rf;
            
            return await clinicians.Distinct().ToListAsync();
        }

        public async Task<ExternalCliniciansAndFacilities> GetPatientGPReferrer(int mpi) //Get the patient's GP and facility
        {
            Patient patient = await _clinContext.Patients.FirstAsync(p => p.MPI == mpi);

            ExternalCliniciansAndFacilities gp = await _clinContext.ExternalCliniciansAndFacilities.FirstAsync(c => c.MasterClinicianCode == patient.GP_Code);

            return gp;
        }

        public async Task<List<ExternalClinician>> GetAllCliniciansList() //Get list of all external/referring clinicians
        {
            IQueryable<ExternalClinician> clinicians = from rf in _clinContext.ExternalClinician
                                                                //where rf.NON_ACTIVE == 0
                                                                 orderby rf.NAME
                                                                 select rf;

            return await clinicians.Distinct().ToListAsync();
        }

        public async Task<List<ExternalClinician>> GetGPList() //Get list of all external/referring GPs
        {
            IQueryable<ExternalClinician> clinicians = from rf in _clinContext.ExternalClinician
                             where rf.NON_ACTIVE == 0 & rf.Is_Gp == -1
                             orderby rf.NAME
                             select rf;

            return await clinicians.Distinct().ToListAsync();
        }

        public async Task<List<string>> GetClinicianTypeList() //Get list of all external clinician specialities
        {

            var specialties = await _clinContext.ExternalClinician
                    .AsNoTracking()
                    .Where(rf => rf.NON_ACTIVE == 0
                                 && rf.SPECIALITY != null
                                 && rf.POSITION != null
                                 && !EF.Functions.Like(rf.SPECIALITY, "%Family%")) // DB-side LIKE; case-insensitive in many collations
                    .Select(rf => rf.SPECIALITY)
                    .Distinct()
                    .OrderBy(s => s) // alphabetic order
                    .ToListAsync();

            return specialties;
        }

        public async Task<List<ExternalCliniciansAndFacilities>> GetExternalCliniciansByType(string type)
        {
            IQueryable<ExternalCliniciansAndFacilities> clins = _clinContext.ExternalCliniciansAndFacilities.Where(c => c.SPECIALITY != null);

            clins = clins.Where(c => c.SPECIALITY.Contains(type));


            return await clins.ToListAsync();
        }
    }
}
