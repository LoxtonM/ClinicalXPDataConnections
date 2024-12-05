using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface IOutcomeData
    {
        public List<Outcome> GetOutcomeList();
    }
    public class OutcomeData : IOutcomeData
    {
        private readonly ClinicalContext _context;
        public OutcomeData(ClinicalContext context)
        {
            _context = context;
        }
       
        public List<Outcome> GetOutcomeList() 
        {
            IQueryable<Outcome> oc = _context.Outcomes.Where(o => o.DEFAULT_CLINIC_STATUS == "Active");

            return oc.ToList();
        }
    }
}
