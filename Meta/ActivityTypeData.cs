using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IActivityTypeData
    {
        public List<ActivityType> GetReferralTypes();
        public List<ActivityType> GetApptTypes();
    }
    public class ActivityTypeData : IActivityTypeData
    {
        private readonly ClinicalContext _clinContext;
        

        public ActivityTypeData(ClinicalContext context)
        {
            _clinContext = context;           
        }

        public List<ActivityType> GetReferralTypes()
        {
            IQueryable<ActivityType> apptypes = _clinContext.ActivityType.Where(t => (t.NON_ACTIVE == 0 && t.ISREFERRAL == true) || t.APP_TYPE.Contains("Temp"));
            return apptypes.ToList();
        }
        public List<ActivityType> GetApptTypes()
        {
            IQueryable<ActivityType> apptypes = _clinContext.ActivityType.Where(t => t.NON_ACTIVE == 0 && t.ISAPPT == true);
            return apptypes.ToList();
        }
    }
}