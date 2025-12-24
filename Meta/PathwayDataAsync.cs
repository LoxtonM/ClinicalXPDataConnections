using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IPathwayDataAsync
    {
        public Task<PatientPathway> GetPathwayDetails(int id);
        public Task<List<PatientPathway>> GetPathways(int id);
        public Task<List<Pathway>> GetPathwayList();
        public Task<List<SubPathway>> GetSubPathwayList();
    }
    public class PathwayDataAsync : IPathwayDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public PathwayDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<PatientPathway> GetPathwayDetails(int id)
        {
            PatientPathway pathway = await _clinContext.PatientPathway.Where(i => i.MPI == id).OrderBy(i => i.REFERRAL_DATE).FirstOrDefaultAsync();
            
            return pathway;
        } //Get earliest active pathway for patient by MPI

        public async Task<List<PatientPathway>> GetPathways(int id)
        {
            IQueryable<PatientPathway> pathways = _clinContext.PatientPathway.Where(i => i.MPI == id).OrderBy(i => i.REFERRAL_DATE);

            return await pathways.ToListAsync();
        }

        public async Task<List<Pathway>> GetPathwayList()
        {
            IQueryable<Pathway> pathway = from p in _clinContext.Pathways                         
                         select p;

            return await pathway.ToListAsync();
        }

        public async Task<List<SubPathway>> GetSubPathwayList()
        {
            IQueryable<SubPathway> spathway = from p in _clinContext.SubPathways
                                          where p.InUse == true
                                          select p;

            return await spathway.ToListAsync();
        }
    }
}
