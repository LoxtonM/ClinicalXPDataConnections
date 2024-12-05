using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IPriorityData
    {
        public List<Priority> GetPriorityList();
    }
    public class PriorityData : IPriorityData
    {
        private readonly ClinicalContext _clinContext;

        public PriorityData(ClinicalContext context)
        {
            _clinContext = context;
        }       

        public List<Priority> GetPriorityList()
        {
            IQueryable<Priority> priority = from p in _clinContext.Priority
                          where p.IsActive == true
                          orderby p.PriorityLevel
                         select p;

            return priority.ToList();
        }

        
    }
}
