using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IExternalClinicianData
    {
        public string GetCCDetails(ExternalClinician referrer);
        public ExternalClinician GetClinicianDetails(string sref);
        public List<ExternalCliniciansAndFacilities> GetClinicianList();
        public ExternalCliniciansAndFacilities GetPatientGPReferrer(int mpi);
        public List<ExternalClinician> GetAllCliniciansList();
        public List<ExternalClinician> GetGPList();
        public List<string> GetClinicianTypeList();
    }
    public class ExternalClinicianData : IExternalClinicianData
    {
        private readonly ClinicalContext _clinContext;
        
        public ExternalClinicianData(ClinicalContext context)
        {
            _clinContext = context;
        }
                
        public string GetCCDetails(ExternalClinician referrer) //Get details of CC address
        {
            string cc = "";
            if (referrer.FACILITY != null) //believe it or not, there are actually some nulls!!!
            {
                ExternalFacility facility = _clinContext.ExternalFacility.FirstOrDefault(f => f.MasterFacilityCode == referrer.FACILITY);

                cc = cc + Environment.NewLine + facility.NAME + Environment.NewLine + facility.ADDRESS + Environment.NewLine
                    + facility.CITY + Environment.NewLine + facility.STATE + Environment.NewLine + facility.ZIP;
            }
            return cc;
        }

        public ExternalClinician GetClinicianDetails(string sref) //Get details of external/referring clinician
        {
            ExternalClinician item = _clinContext.ExternalClinician.FirstOrDefault(f => f.MasterClinicianCode == sref);
            return item;
        }

        public List<ExternalCliniciansAndFacilities> GetClinicianList() //Get list of all external/referring clinicians with their facilities (no GPs)
        {
            IQueryable<ExternalCliniciansAndFacilities> clinicians = from rf in _clinContext.ExternalCliniciansAndFacilities
                             where rf.NON_ACTIVE == 0 & rf.Is_GP == 0
                             orderby rf.LAST_NAME
                             select rf;
            
            return clinicians.Distinct().ToList();
        }

        public ExternalCliniciansAndFacilities GetPatientGPReferrer(int mpi) //Get the patient's GP and facility
        {
            Patient patient = _clinContext.Patients.FirstOrDefault(p => p.MPI == mpi);

            ExternalCliniciansAndFacilities gp = _clinContext.ExternalCliniciansAndFacilities.FirstOrDefault(c => c.MasterClinicianCode == patient.GP_Code);

            return gp;
        }

        public List<ExternalClinician> GetAllCliniciansList() //Get list of all external/referring clinicians
        {
            IQueryable<ExternalClinician> clinicians = from rf in _clinContext.ExternalClinician
                                                                //where rf.NON_ACTIVE == 0
                                                                 orderby rf.NAME
                                                                 select rf;

            return clinicians.Distinct().ToList();
        }

        public List<ExternalClinician> GetGPList() //Get list of all external/referring GPs
        {
            IQueryable<ExternalClinician> clinicians = from rf in _clinContext.ExternalClinician
                             where rf.NON_ACTIVE == 0 & rf.Is_Gp == -1
                             orderby rf.NAME
                             select rf;

            return clinicians.Distinct().ToList();
        }

        public List<string> GetClinicianTypeList() //Get list of all external clinician specialities
        {
            IQueryable<ExternalClinician> clinicians = from rf in _clinContext.ExternalClinician
                             where rf.NON_ACTIVE == 0 & rf.SPECIALITY != null & rf.POSITION != null & !rf.SPECIALITY.Contains("Family") 
                             orderby rf.SPECIALITY
                             select rf;

            List<string> specialties = new List<string>();
            
            foreach (var item in clinicians)
            {
                specialties.Add(item.SPECIALITY);
            }

            return specialties.Distinct().ToList();
        }
    }
}
