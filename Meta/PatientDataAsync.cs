using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicalXPDataConnections.Meta
{
    public interface IPatientDataAsync
    {
        public Task<Patient> GetPatientDetails(int id);
        public Task<Patient> GetPatientDetailsByWMFACSID(int id);
        public Task<Patient> GetPatientDetailsByDemographicData(string firstname, string lastname, string nhsno, DateTime dob);
        public Task<Patient> GetPatientDetailsByIntID(int intID);
        public Task<Patient> GetPatientDetailsByCGUNo(string cguNo);
        public Task<List<Patient>> GetFamilyMembers(int mpi);
        public Task<List<Patient>> GetPatientsInPedigree(string pedno);
        public Task<List<Patient>> GetPatientsWithoutCGUNumbers();
    }
    public class PatientDataAsync : IPatientDataAsync 
    {
        private readonly ClinicalContext _clinContext;       

        public PatientDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }       
        
        public async Task<Patient> GetPatientDetails(int id)
        {
            Patient patient = await _clinContext.Patients.FirstAsync(i => i.MPI == id);
            
            return patient;
        } //Get patient details from MPI

        public async Task<Patient> GetPatientDetailsByWMFACSID(int id)
        {
            Patient patient = await _clinContext.Patients.FirstAsync(i => i.WMFACSID == id);
            return patient;
        } //Get patient details from WMFACSID               

        public async Task<Patient> GetPatientDetailsByDemographicData(string firstname, string lastname, string nhsno, DateTime dob)
        {
            Patient patient = await _clinContext.Patients.FirstAsync(i => i.FIRSTNAME == firstname && i.LASTNAME == lastname &&
                                                        i.SOCIAL_SECURITY == nhsno && i.DOB == dob);
            return patient;
        }

        public async Task<Patient> GetPatientDetailsByIntID(int intID)
        {
            Patient pt = await _clinContext.Patients.FirstAsync(p => p.INTID == intID);
            return pt;
        }

        public async Task<Patient> GetPatientDetailsByCGUNo(string cguNo)
        {
            Patient pt = await _clinContext.Patients.FirstOrDefaultAsync(p => p.CGU_No == cguNo);

            return pt;
        }

        public async Task<List<Patient>> GetFamilyMembers(int mpi)
        {
            Patient patient = await _clinContext.Patients.FirstAsync(p => p.MPI == mpi);
            IQueryable<Patient> pts = _clinContext.Patients.Where(p => p.PEDNO == patient.PEDNO & p.MPI != patient.MPI).OrderBy(p => p.MPI);
            
            return await pts.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsInPedigree(string pedno)
        {
            IQueryable<Patient> pts = _clinContext.Patients.Where(p => p.PEDNO == pedno).OrderBy(p => p.MPI);
            
            return await pts.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsWithoutCGUNumbers()
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.ExternalID != null && (p.CGU_No == "." || p.CGU_No == null));

            return await patients.ToListAsync();
        }
    }
}
