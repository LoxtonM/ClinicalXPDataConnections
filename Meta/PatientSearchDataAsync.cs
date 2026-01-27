using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IPatientSearchDataAsync
    {        
        public Task<List<Patient>> GetPatientsListByCGUNo(string? cguNo);
        public Task<List<Patient>> GetPatientsListByPedNo(string? pedNo);
        public Task<List<Patient>> GetPatientsListByName(string? firstname, string? lastname);
        public Task<List<Patient>> GetPatientsListByNHS(string? nhsNo);
        public Task<List<Patient>> GetPatientsListByDOB(DateTime dob);
        public Task<List<Patient>> GetPatientsListByPostCode(string postCode);
        public Task<List<Patient>> GetPatientsListByStaffCode(string staffCode);
        public Task<List<Patient>> GetPatientsListByTelNo(string telNo);
        public Task<List<Patient>> GetPatientsListByEmail(string email);
        //the reason for multiple "GetPatientsLists", and not one with multiple parameters, is because in order to do that,
        //the "patients" list would have to be created first and then narrowed by criteria.
        //This would result in very long loading times, as there are a LOT of patients, and I don't really want to select them all
        //only to have to filter them.
    }
    public class PatientSearchDataAsync : IPatientSearchDataAsync
    {
        private readonly ClinicalContext _clinContext;       

        public PatientSearchDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }       
               
        
        public async Task<List<Patient>> GetPatientsListByCGUNo(string cguNo)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.CGU_No.Contains(cguNo));            
            
            return await patients.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsListByPedNo(string pedNo)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.PEDNO == pedNo);

            return await patients.ToListAsync();
        }
        public async Task<List<Patient>> GetPatientsListByName(string? firstname, string? lastname)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.FIRSTNAME.Contains(firstname) || p.LASTNAME.Contains(lastname));
            
            return await patients.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsListByNHS(string nhsNo)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.SOCIAL_SECURITY.Contains(nhsNo));

            return await patients.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsListByDOB(DateTime dob)        
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.DOB == dob);

            return await patients.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsListByPostCode(string postCode)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.POSTCODE.Contains(postCode));

            return await patients.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsListByStaffCode(string staffCode)
        {
            var patients = from patient in _clinContext.Patients
                           join referral in _clinContext.Referrals
                           on patient.MPI equals referral.MPI
                           where referral.PATIENT_TYPE_CODE == staffCode || referral.GC_CODE == staffCode
                           select patient;

            return await patients.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsListByTelNo(string telNo)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.TEL.Contains(telNo) || p.WORKTEL.Contains(telNo) || p.PtTelMobile.Contains(telNo));

            return await patients.ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsListByEmail(string email)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.EmailAddress.Contains(email));

            return await patients.ToListAsync();
        }
    }
}
