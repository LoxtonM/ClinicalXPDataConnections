using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRefReasonDataAsync
    {
        public Task<List<ReferralReason>> GetRefReasonList();
    }
    public class RefReasonDataAsync : IRefReasonDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public RefReasonDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<List<ReferralReason>> GetRefReasonList()
        {
            IQueryable<ReferralReason> refreason = from p in _clinContext.referralReasons
                          
                         select p;

            return await refreason.ToListAsync();
        }

        
    }
}
