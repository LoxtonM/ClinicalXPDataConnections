using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicalXPDataConnections.Meta
{
    public interface ISocialServicePathwayDataAsync
    {
        public Task<List<SocialServicePathway>> GetAllSocialServicePathways();
        public Task<List<SocialServicePathway>> GetSocialServicePathwayList(int icpID);
        public Task<SocialServicePathway> GetSocialServicePathwayById(int id);
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
            IQueryable<SocialServicePathway> ssp = _clinContext.SocialServicePathway.Where(s => s.ICPID == icpID);

            return await ssp.ToListAsync();
        }
    }
}
