using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IStudyData
    {
        public List<Study> GetStudiesList(int mpi);
        
    }
    public class StudyData : IStudyData
    {
        private readonly ClinicalContext _clinContext;
        
        public StudyData(ClinicalContext context)
        {
            _clinContext = context;            
        }

        public List<Study> GetStudiesList(int mpi)
        {
            IQueryable<Study> item = from i in _clinContext.Study
                                     where i.MPI == mpi
                                         select i;

            return item.ToList();
        }

        

    }
}
