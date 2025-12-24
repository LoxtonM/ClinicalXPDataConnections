using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IActivityTypeDataAsync
    {
        public Task<List<ActivityType>> GetReferralTypes();
        public Task<List<ActivityType>> GetApptTypes();
    }
    public class ActivityTypeDataAsync : IActivityTypeDataAsync
    {
        private readonly ClinicalContext _clinContext;
        

        public ActivityTypeDataAsync(ClinicalContext context)
        {
            _clinContext = context;           
        }

        public async Task<List<ActivityType>> GetReferralTypes()
        {
            IQueryable<ActivityType> apptypes = _clinContext.ActivityType.Where(t => (t.NON_ACTIVE == 0 && t.ISREFERRAL == true) || t.APP_TYPE.Contains("Temp"));
            
            return await apptypes.ToListAsync();
        }
        public async Task<List<ActivityType>> GetApptTypes()
        {
            IQueryable<ActivityType> apptypes = _clinContext.ActivityType.Where(t => t.NON_ACTIVE == 0 && t.ISAPPT == true);
            
            return await apptypes.ToListAsync();
        }
    }
}