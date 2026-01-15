using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IICPActionDataAsync
    {        
        public Task<List<ICPAction>> GetICPCancerActionsList();
        public Task<List<ICPGeneralAction>> GetICPGeneralActionsList();
        public Task<List<ICPGeneralAction2>> GetICPGeneralActionsList2();
        public Task<List<ICPCancerReviewAction>> GetICPCancerReviewActionsList();
        public Task<ICPCancerReviewAction> GetICPCancerAction(int id);
        public Task<ICPAction> GetCancerReferralAction(int id);
        public Task<ICPGeneralAction> GetGeneralReferralAction1(int id);
        public Task<ICPGeneralAction2> GetGeneralReferralAction2(int id);

    }
    public class ICPActionDataAsync : IICPActionDataAsync
    {
        private readonly ClinicalContext _clinContext;
        
        public ICPActionDataAsync(ClinicalContext context)
        {
            _clinContext = context;            
        }
        
        public async Task<List<ICPAction>> GetICPCancerActionsList() //Get list of all triage actions for Cancer ICPs
        {
            IQueryable<ICPAction> actions = from a in _clinContext.ICPCancerActionsList
                         where a.InUse == true
                         orderby a.ID
                         select a;
           
            return actions.ToList();
        }

        public async Task<List<ICPGeneralAction>> GetICPGeneralActionsList() //Get list of all "treatpath" items for General ICPs
        {
            IQueryable<ICPGeneralAction> actions = from a in _clinContext.ICPGeneralActionsList
                         where a.InUse == true
                         orderby a.ID
                         select a;
           
            return await actions.ToListAsync();
        }

        public async Task<List<ICPGeneralAction2>> GetICPGeneralActionsList2() //Get list of all "treatpath2" items for General ICPs
        {
            IQueryable<ICPGeneralAction2> actions = from a in _clinContext.ICPGeneralActionsList2
                         where a.InUse == true
                         orderby a.ID
                         select a;
       
            return await actions.ToListAsync();
        }

        public async Task<List<ICPCancerReviewAction>> GetICPCancerReviewActionsList() //Get list of all "treatpath2" items for General ICPs
        {
            IQueryable<ICPCancerReviewAction> actions = from a in _clinContext.ICPCancerReviewActionsList
                          where a.InUse == true
                          orderby a.ListOrder
                          select a;

            return await actions.ToListAsync();
        }

        public async Task<ICPCancerReviewAction> GetICPCancerAction(int id)
        {
            ICPCancerReviewAction action = await _clinContext.ICPCancerReviewActionsList.FirstOrDefaultAsync(a => a.ID == id);

            return action;
        }

        public async Task<ICPAction> GetCancerReferralAction(int id)
        {
            ICPAction action = await _clinContext.ICPCancerActionsList.FirstOrDefaultAsync(a => a.ID == id);

            return action;
        }

        public async Task<ICPGeneralAction> GetGeneralReferralAction1(int id)
        {
            ICPGeneralAction action = await _clinContext.ICPGeneralActionsList.FirstOrDefaultAsync(a => a.ID == id);

            return action;
        }

        public async Task<ICPGeneralAction2> GetGeneralReferralAction2(int id)
        {
            ICPGeneralAction2 action = await _clinContext.ICPGeneralActionsList2.FirstOrDefaultAsync(a => a.ID == id);

            return action;
        }


    }
}
