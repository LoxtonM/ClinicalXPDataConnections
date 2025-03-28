using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;


namespace ClinicalXPDataConnections.Meta
{
    public interface IPatientSearchData
    {        
        public List<Patient> GetPatientsListByCGUNo(string? cguNo);
        public List<Patient> GetPatientsListByName(string? firstname, string? lastname);
        public List<Patient> GetPatientsListByNHS(string? nhsNo);
        public List<Patient> GetPatientsListByDOB(DateTime dob);
        public List<Patient> GetPatientsListByPostCode(string postCode);

        public List<Patient> GetPatientsListByStaffCode(string staffCode);
        //the reason for multiple "GetPatientsLists", and not one with multiple parameters, is because in order to do that,
        //the "patients" list would have to be created first and then narrowed by criteria.
        //This would result in very long loading times, as there are a LOT of patients, and I don't really want to select them all
        //only to have to filter them.
    }
    public class PatientSearchData : IPatientSearchData
    {
        private readonly ClinicalContext _clinContext;       

        public PatientSearchData(ClinicalContext context)
        {
            _clinContext = context;
        }       
               
        
        public List<Patient> GetPatientsListByCGUNo(string cguNo)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.CGU_No.Contains(cguNo));            
            
            return patients.ToList();
        }
        public List<Patient> GetPatientsListByName(string? firstname, string? lastname)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.FIRSTNAME.Contains(firstname) || p.LASTNAME.Contains(lastname));
            
            return patients.ToList();
        }

        public List<Patient> GetPatientsListByNHS(string nhsNo)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.SOCIAL_SECURITY.Contains(nhsNo));

            return patients.ToList();
        }

        public List<Patient> GetPatientsListByDOB(DateTime dob)        
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.DOB == dob);

            return patients.ToList();
        }

        public List<Patient> GetPatientsListByPostCode(string postCode)
        {
            IQueryable<Patient> patients = _clinContext.Patients.Where(p => p.POSTCODE.Contains(postCode));

            return patients.ToList();
        }

        public List<Patient> GetPatientsListByStaffCode(string staffCode)
        {
            List<Patient> patients = new List<Patient>();
            PatientData pData = new PatientData(_clinContext);

            List<Referral> referrals = _clinContext.Referrals.Where(r => r.PATIENT_TYPE_CODE == staffCode || r.GC_CODE == staffCode).ToList();

            foreach (Referral referral in referrals)
            {
                Patient patient = pData.GetPatientDetails(referral.MPI);
                patients.Add(patient);
            }

            return patients;
        }
    }
}
