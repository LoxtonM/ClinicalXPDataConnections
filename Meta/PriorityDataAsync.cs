using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IPriorityDataAsync
    {
        public Task<List<Priority>> GetPriorityList();
    }
    public class PriorityDataAsync : IPriorityDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public PriorityDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }       

        public async Task<List<Priority>> GetPriorityList()
        {
            IQueryable<Priority> priority = from p in _clinContext.Priority
                          where p.IsActive == true
                          orderby p.PriorityLevel
                         select p;

            return await priority.ToListAsync();
        }

        
    }
}
