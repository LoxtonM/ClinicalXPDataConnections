using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITriageDataAsync
    {
        public Task<ICP> GetICPDetails(int icpID);
        public Task<ICP> GetICPDetailsByRefID(int refID);
        public Task<Triage> GetTriageDetails(int? icpID);
        public Task<List<Triage>> GetTriageList(string username);
        public Task<List<Triage>> GetTriageListFull();
        public Task<List<ICPCancer>> GetCancerICPList(string username);
        public Task<List<ICPCancer>> GetCancerICPListForPatient(int mpi);
        public Task<List<ClinicVenue>> GetClinicalFacilitiesList();
        public Task<ICPGeneral> GetGeneralICPDetails(int? icpID);
        public Task<ICPGeneral> GetGeneralICPDetailsByICPID(int? icpID);
        public Task<ICPCancer> GetCancerICPDetails(int? icpID);
        public Task<ICPCancer> GetCancerICPDetailsByICPID(int? icpID);
        public Task<int> GetGeneralICPCountByICPID(int id);
        public Task<int> GetCancerICPCountByICPID(int id);
    }
    public class TriageDataAsync : ITriageDataAsync
    {
        private readonly ClinicalContext _clinContext;
        
        public TriageDataAsync(ClinicalContext context)
        {
            _clinContext = context;           
        }
        
                
        public async Task<ICP> GetICPDetails(int icpID)
        {
            ICP icp = await _clinContext.ICP.FirstOrDefaultAsync(i => i.ICPID == icpID);

            return icp;
        }

        public async Task<ICP> GetICPDetailsByRefID(int refID)
        {
            ICP icp = await _clinContext.ICP.FirstOrDefaultAsync(i => i.REFID == refID);

            return icp;
        }
        public async Task<Triage> GetTriageDetails(int? icpID) //Get details of ICP from the IcpID
        {
            Triage icp = await _clinContext.Triages.FirstOrDefaultAsync(i => i.ICPID == icpID);

            return icp;
        }

        public async Task<List<Triage>> GetTriageList(string username) //Get list of all outstanding triages for a specific user (by login name)
        {
            IQueryable<Triage> triages = from t in _clinContext.Triages
                         where t.LoginDetails == username
                         orderby t.RefDate descending
                         select t;           

            return await triages.ToListAsync();
        }

        public async Task<List<Triage>> GetTriageListFull() //Get list of all outstanding triages for a specific user (by login name)
        {
            IQueryable<Triage> triages = from t in _clinContext.Triages
                                         orderby t.RefDate descending
                                         select t;

            return await triages.ToListAsync();
        }

        public async Task<List<ICPCancer>> GetCancerICPList(string username) //Get list of all open Cancer ICP Reviews for a specific user (by login name)
        {
            StaffMember user = await _clinContext.StaffMembers.FirstOrDefaultAsync(s => s.EMPLOYEE_NUMBER == username);
            string staffCode = user.STAFF_CODE;

            IQueryable<ICPCancer> icps = from i in _clinContext.ICPCancer
                       where i.ActOnRefBy != null && i.FinalReviewed == null && (i.GC_CODE == staffCode || i.ToBeReviewedby.ToUpper() == username.ToUpper())
                      && i.Status_Admin == "Review"
                      && i.COMPLETE == "Active"
                        orderby i.REFERRAL_DATE
                       select i;
            
            return await icps.ToListAsync();
        }

        public async Task<List<ICPCancer>> GetCancerICPListForPatient(int mpi)
        {
            IQueryable<ICPCancer> icps = from i in _clinContext.ICPCancer
                                         where i.MPI == mpi
                                         orderby i.REFERRAL_DATE
                                         select i;

            return await icps.ToListAsync();
        }


        public async Task<List<ClinicVenue>> GetClinicalFacilitiesList() //Get list of all clinic facilities where we hold clinics
        {
            IQueryable<ClinicVenue> facs = from f in _clinContext.ClinicalFacilities
                      where f.NON_ACTIVE == 0
                      select f;
        
            return await facs.ToListAsync();
        }

        public async Task<ICPGeneral> GetGeneralICPDetails(int? icpID) //Get details of a general ICP by the IcpID
        {
            ICPGeneral icp = await _clinContext.ICPGeneral.FirstOrDefaultAsync(c => c.ICP_General_ID == icpID);
            
            return icp;
        }

        public async Task<ICPGeneral> GetGeneralICPDetailsByICPID(int? icpID) //Get details of a general ICP by the IcpID
        {
            ICPGeneral icp = await _clinContext.ICPGeneral.FirstOrDefaultAsync(c => c.ICPID == icpID);

            return icp;
        }

        public async Task<ICPCancer> GetCancerICPDetails(int? icpID) //Get details of a cancer ICP by the Cancer ID
        {
            ICPCancer icp = await _clinContext.ICPCancer.FirstOrDefaultAsync(c => c.ICP_Cancer_ID == icpID);

            return icp;
        }

        public async Task<ICPCancer> GetCancerICPDetailsByICPID(int? icpID) //Get details of a cancer ICP by the IcpID
        {
            ICPCancer icp = await _clinContext.ICPCancer.FirstOrDefaultAsync(c => c.ICPID == icpID);

            return icp;
        }

        public async Task<int> GetGeneralICPCountByICPID(int id)
        {
            IQueryable<ICPGeneral> item = from i in _clinContext.ICPGeneral
                       where i.ICPID == id
                       select i;

            return await item.CountAsync();
        }

        public async Task<int> GetCancerICPCountByICPID(int id)
        {
            IQueryable<ICPCancer> item = from i in _clinContext.ICPCancer
                       where i.ICPID == id
                       select i;

            return await item.CountAsync();
        }
    }
}
