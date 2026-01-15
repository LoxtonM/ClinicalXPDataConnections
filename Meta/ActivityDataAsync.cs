using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicalXPDataConnections.Meta
{
    public interface IActivityDataAsync
    {
        public Task<ActivityItem> GetActivityDetails(int id);
        public Task<List<ActivityItem>> GetClinicDetailsList(int refID);
        public Task<List<ActivityItem>> GetActivityList(int mpi);
        public Task<List<ActivityItem>> GetActivityListByClinicno(string clinicNo);
        public Task<List<ActivityItem>> GetActiveReferralList(int mpi);
    }
    public class ActivityDataAsync : IActivityDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public ActivityDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        
        public async Task<ActivityItem> GetActivityDetails(int id) //Get details of any activity item by RefID
        {
            ActivityItem referral = await _clinContext.ActivityItems.FirstOrDefaultAsync(i => i.RefID == id); //because null
            return referral;
        }

        public async Task<List<ActivityItem>> GetActivityList(int mpi) //Get all activity for a specific patient
        {
            IQueryable<ActivityItem> cl = from c in _clinContext.ActivityItems
                                          where c.MPI == mpi
                                          orderby c.DATE_SCHEDULED
                                          select c;
            
            return await cl.ToListAsync();
        }

        public async Task<List<ActivityItem>> GetActivityListByClinicno(string clinicNo)
        {
            IQueryable<ActivityItem> ActivityList = from r in _clinContext.ActivityItems
                                                where r.REFERRAL_CLINICNO == clinicNo
                                                select r;

            return await ActivityList.ToListAsync();
        }

        public async Task<List<ActivityItem>> GetActiveReferralList(int mpi) //Get all active referrals for a specific patient
        {
            IQueryable<ActivityItem> cl = from c in _clinContext.ActivityItems
                                          where c.MPI == mpi && c.TYPE.Contains("Ref") && c.COMPLETE == "Active"
                                          orderby c.DATE_SCHEDULED
                                          select c;

            return await cl.ToListAsync();
        }

        public async Task<List<ActivityItem>> GetClinicDetailsList(int refID) //Get details of an appointment by the RefID for editing
        {
           var clinics = await _clinContext.ActivityItems.Where(c => c.RefID == refID)
                .Select(c => new ActivityItem
                {
                    RefID = c.RefID,
                    BOOKED_DATE = c.BOOKED_DATE,
                    BOOKED_TIME = c.BOOKED_TIME,
                    TYPE = c.TYPE,
                    STAFF_CODE_1 = c.STAFF_CODE_1,
                    FACILITY = c.FACILITY,
                    COUNSELED = c.COUNSELED != null ? c.COUNSELED.Replace(" ", "_") : null,
                    SEEN_BY = c.SEEN_BY,
                    NOPATIENTS_SEEN = c.NOPATIENTS_SEEN,
                    LetterReq = c.LetterReq != null ? c.LetterReq.Replace(" ", "_") : null,
                    ARRIVAL_TIME = c.ARRIVAL_TIME,
                    ClockStop = c.ClockStop
                }).ToListAsync();

            return clinics;
        }
    }
}
