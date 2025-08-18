using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IPathwayData
    {
        public PatientPathway GetPathwayDetails(int id);
        public List<PatientPathway> GetPathways(int id);
        public List<Pathway> GetPathwayList();
        public List<SubPathway> GetSubPathwayList();
    }
    public class PathwayData : IPathwayData
    {
        private readonly ClinicalContext _clinContext;

        public PathwayData(ClinicalContext context)
        {
            _clinContext = context;
        }

        public PatientPathway GetPathwayDetails(int id)
        {
            PatientPathway pathway = _clinContext.PatientPathway.OrderBy(i => i.REFERRAL_DATE).FirstOrDefault(i => i.MPI == id);
            return pathway;
        } //Get earliest active pathway for patient by MPI

        public List<PatientPathway> GetPathways(int id)
        {
            IQueryable<PatientPathway> pathways = _clinContext.PatientPathway.Where(i => i.MPI == id).OrderBy(i => i.REFERRAL_DATE);

            return pathways.ToList();
        }

        public List<Pathway> GetPathwayList()
        {
            IQueryable<Pathway> pathway = from p in _clinContext.Pathways                         
                         select p;

            return pathway.ToList();
        }

        public List<SubPathway> GetSubPathwayList()
        {
            IQueryable<SubPathway> spathway = from p in _clinContext.SubPathways
                                          where p.InUse == true
                                          select p;

            return spathway.ToList();
        }

        
    }
}
