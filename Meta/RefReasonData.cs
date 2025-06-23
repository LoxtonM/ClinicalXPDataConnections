using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRefReasonData
    {
        public List<ReferralReason> GetRefReasonList();
    }
    public class RefReasonData : IRefReasonData
    {
        private readonly ClinicalContext _clinContext;

        public RefReasonData(ClinicalContext context)
        {
            _clinContext = context;
        }

        public List<ReferralReason> GetRefReasonList()
        {
            IQueryable<ReferralReason> refreason = from p in _clinContext.referralReasons
                          
                         select p;

            return refreason.ToList();
        }

        
    }
}
