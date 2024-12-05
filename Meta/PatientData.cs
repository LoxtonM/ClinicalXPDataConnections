using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;


namespace ClinicalXPDataConnections.Meta
{
    public interface IPatientData
    {
        public Patient GetPatientDetails(int id);
        public Patient GetPatientDetailsByWMFACSID(int id);
        public Patient GetPatientDetailsByDemographicData(string firstname, string lastname, string nhsno, DateTime dob);
        public Patient GetPatientDetailsByIntID(int intID);
        public Patient GetPatientDetailsByCGUNo(string cguNo);
        public List<Patient> GetFamilyMembers(int mpi);       

    }
    public class PatientData : IPatientData 
    {
        private readonly ClinicalContext _clinContext;       

        public PatientData(ClinicalContext context)
        {
            _clinContext = context;
        }       
        
        public Patient GetPatientDetails(int id)
        {
            Patient patient = _clinContext.Patients.FirstOrDefault(i => i.MPI == id);
            return patient;
        } //Get patient details from MPI

        public Patient GetPatientDetailsByWMFACSID(int id)
        {
            Patient patient = _clinContext.Patients.FirstOrDefault(i => i.WMFACSID == id);
            return patient;
        } //Get patient details from WMFACSID               

        public Patient GetPatientDetailsByDemographicData(string firstname, string lastname, string nhsno, DateTime dob)
        {
            Patient patient = _clinContext.Patients.FirstOrDefault(i => i.FIRSTNAME == firstname && i.LASTNAME == lastname &&
                                                        i.SOCIAL_SECURITY == nhsno && i.DOB == dob);
            return patient;
        }

        public Patient GetPatientDetailsByIntID(int intID)
        {
            Patient pt = _clinContext.Patients.FirstOrDefault(p => p.INTID == intID);
            return pt;
        }

        public Patient GetPatientDetailsByCGUNo(string cguNo)
        {
            Patient pt = _clinContext.Patients.FirstOrDefault(p => p.CGU_No == cguNo);
            return pt;
        }

        public List<Patient> GetFamilyMembers(int mpi)
        {
            Patient patient = _clinContext.Patients.FirstOrDefault(p => p.MPI == mpi);
            IQueryable<Patient> pts = _clinContext.Patients.Where(p => p.PEDNO == patient.PEDNO & p.MPI != patient.MPI).OrderBy(p => p.MPI);
            return pts.ToList();
        }

                
    }
}
