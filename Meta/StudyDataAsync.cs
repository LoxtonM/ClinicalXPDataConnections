using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IStudyDataAsync
    {
        public Task<List<Study>> GetStudiesList(int mpi);
        
    }
    public class StudyDataAsync : IStudyDataAsync
    {
        private readonly ClinicalContext _clinContext;
        
        public StudyDataAsync(ClinicalContext context)
        {
            _clinContext = context;            
        }

        public async Task<List<Study>> GetStudiesList(int mpi)
        {
            IQueryable<Study> item = from i in _clinContext.Study
                                     where i.MPI == mpi
                                         select i;

            return await item.ToListAsync();
        }

        

    }
}
