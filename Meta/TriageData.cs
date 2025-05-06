﻿using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITriageData
    {
        public ICP GetICPDetails(int icpID);
        public ICP GetICPDetailsByRefID(int refID);
        public Triage GetTriageDetails(int? icpID);
        public List<Triage> GetTriageList(string username);
        public List<Triage> GetTriageListFull();
        public List<ICPCancer> GetCancerICPList(string username);
        public List<ICPCancer> GetCancerICPListForPatient(int mpi);
        public List<ClinicVenue> GetClinicalFacilitiesList();
        public ICPGeneral GetGeneralICPDetails(int? icpID);
        public ICPGeneral GetGeneralICPDetailsByICPID(int? icpID);
        public ICPCancer GetCancerICPDetails(int? icpID);
        public ICPCancer GetCancerICPDetailsByICPID(int? icpID);
        public int GetGeneralICPCountByICPID(int id);
        public int GetCancerICPCountByICPID(int id);
    }
    public class TriageData : ITriageData
    {
        private readonly ClinicalContext _clinContext;
        
        public TriageData(ClinicalContext context)
        {
            _clinContext = context;           
        }
        
                
        public ICP GetICPDetails(int icpID)
        {
            ICP icp = _clinContext.ICP.FirstOrDefault(i => i.ICPID == icpID);
            return icp;
        }

        public ICP GetICPDetailsByRefID(int refID)
        {
            ICP icp = _clinContext.ICP.FirstOrDefault(i => i.REFID == refID);
            return icp;
        }
        public Triage GetTriageDetails(int? icpID) //Get details of ICP from the IcpID
        {
            Triage icp = _clinContext.Triages.FirstOrDefault(i => i.ICPID == icpID);
            return icp;
        }

        public List<Triage> GetTriageList(string username) //Get list of all outstanding triages for a specific user (by login name)
        {
            IQueryable<Triage> triages = from t in _clinContext.Triages
                         where t.LoginDetails == username
                         orderby t.RefDate descending
                         select t;           

            return triages.ToList();
        }

        public List<Triage> GetTriageListFull() //Get list of all outstanding triages for a specific user (by login name)
        {
            IQueryable<Triage> triages = from t in _clinContext.Triages
                                         orderby t.RefDate descending
                                         select t;

            return triages.ToList();
        }

        public List<ICPCancer> GetCancerICPList(string username) //Get list of all open Cancer ICP Reviews for a specific user (by login name)
        {
            StaffMember user = _clinContext.StaffMembers.FirstOrDefault(s => s.EMPLOYEE_NUMBER == username);
            string staffCode = user.STAFF_CODE;

            IQueryable<ICPCancer> icps = from i in _clinContext.ICPCancer
                       where i.ActOnRefBy != null && i.FinalReviewed == null && (i.GC_CODE == staffCode || i.ToBeReviewedby.ToUpper() == username.ToUpper())
                      && i.Status_Admin == "Review"
                      && i.COMPLETE == "Active"
                        orderby i.REFERRAL_DATE
                       select i;
            
            return icps.ToList();
        }

        public List<ICPCancer> GetCancerICPListForPatient(int mpi)
        {
            

            IQueryable<ICPCancer> icps = from i in _clinContext.ICPCancer
                                         where i.MPI == mpi
                                         orderby i.REFERRAL_DATE
                                         select i;

            return icps.ToList();
        }


        public List<ClinicVenue> GetClinicalFacilitiesList() //Get list of all clinic facilities where we hold clinics
        {
            IQueryable<ClinicVenue> facs = from f in _clinContext.ClinicalFacilities
                      where f.NON_ACTIVE == 0
                      select f;
        
            return facs.ToList();
        }

        public ICPGeneral GetGeneralICPDetails(int? icpID) //Get details of a general ICP by the IcpID
        {
            ICPGeneral icp = _clinContext.ICPGeneral.FirstOrDefault(c => c.ICP_General_ID == icpID);
            return icp;
        }

        public ICPGeneral GetGeneralICPDetailsByICPID(int? icpID) //Get details of a general ICP by the IcpID
        {
            ICPGeneral icp = _clinContext.ICPGeneral.FirstOrDefault(c => c.ICPID == icpID);
            return icp;
        }

        public ICPCancer GetCancerICPDetails(int? icpID) //Get details of a cancer ICP by the Cancer ID
        {
            ICPCancer icp = _clinContext.ICPCancer.FirstOrDefault(c => c.ICP_Cancer_ID == icpID);
            return icp;
        }

        public ICPCancer GetCancerICPDetailsByICPID(int? icpID) //Get details of a cancer ICP by the IcpID
        {
            ICPCancer icp = _clinContext.ICPCancer.FirstOrDefault(c => c.ICPID == icpID);
            return icp;
        }

        public int GetGeneralICPCountByICPID(int id)
        {
            IQueryable<ICPGeneral> item = from i in _clinContext.ICPGeneral
                       where i.ICPID == id
                       select i;

            return item.Count();
        }

        public int GetCancerICPCountByICPID(int id)
        {
            IQueryable<ICPCancer> item = from i in _clinContext.ICPCancer
                       where i.ICPID == id
                       select i;

            return item.Count();
        }

    }
}
