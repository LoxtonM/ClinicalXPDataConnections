using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;


namespace ClinicalXPDataConnections.Meta
{
    public interface ISocialServicePathwayDataAsync
    {
        public Task<List<SocialServicePathway>> GetAllSocialServicePathways();
        public Task<List<SocialServicePathway>> GetSocialServicePathwayList(int icpID);
        public Task<SocialServicePathway> GetSocialServicePathwayById(int id);
        public Task<List<SocialServicePathwayOutcome>> GetSocialServicePathwayOutcomes();
        public Task<List<SocialServicePathwayStatus>> GetSocialServicePathwayStatuses();
        public Task<List<SocialService>> GetSocialServices();
        public Task<List<SocialWorker>> GetSocialWorkers();                
    }
    public class SocialServicePathwayDataAsync : ISocialServicePathwayDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public SocialServicePathwayDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        
        public async Task<List<SocialServicePathway>> GetAllSocialServicePathways()
        {
            return await _clinContext.SocialServicePathway.ToListAsync();
        }

        public async Task<SocialServicePathway> GetSocialServicePathwayById(int id)
        {
            return await _clinContext.SocialServicePathway.FindAsync(id);
        }
        public async Task<List<SocialServicePathway>> GetSocialServicePathwayList(int icpID)
        {
            IQueryable<SocialServicePathway> SocialServicePathway = _clinContext.SocialServicePathway.Where(s => s.ICPID == icpID);

            return await SocialServicePathway.ToListAsync();
        }

        public async Task<List<SocialServicePathwayOutcome>> GetSocialServicePathwayOutcomes()
        {
            List<SocialServicePathwayOutcome> outcomeSocialServicePathways = await _clinContext.SocialServicePathwayOutcome.OrderBy(a => a.ID).ToListAsync();

            return outcomeSocialServicePathways;
        }

        public async Task<List<SocialServicePathwayStatus>> GetSocialServicePathwayStatuses()
        {
            List<SocialServicePathwayStatus> statusSocialServicePathways = await _clinContext.SocialServicePathwayStatus.OrderBy(a => a.SSPStatusID).ToListAsync();

            return statusSocialServicePathways;
        }

        public async Task<List<SocialService>> GetSocialServices()
        {
            List<SocialService> ss = await _clinContext.SocialService.OrderBy(a => a.SocialServicesID).ToListAsync();

            return ss;
        }
        public async Task<List<SocialWorker>> GetSocialWorkers()
        {
            List<SocialWorker> sw = await _clinContext.SocialWorker.OrderBy(a => a.SocialWorkerID).ToListAsync();

            return sw;
        }

        public async Task<SocialServicePathway> GetSocialServicePathwayDetails(int id)
        {
            SocialServicePathway SocialServicePathway = await _clinContext.SocialServicePathway.FirstOrDefaultAsync(s => s.SSPID == id);

            return SocialServicePathway;
        }

        public async Task<List<SocialServicePathway>> GetSocialServicePathwayList()
        {
            List<SocialServicePathway> SocialServicePathways = await _clinContext.SocialServicePathway.OrderBy(s => s.SSPID).Where(s => s.IsActive == true).ToListAsync();

            return SocialServicePathways;
        }
    }
}
