using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IOutcomeDataAsync
    {
        public Task<List<Outcome>> GetOutcomeList();
    }
    public class OutcomeDataAsync : IOutcomeDataAsync
    {
        private readonly ClinicalContext _context;
        public OutcomeDataAsync(ClinicalContext context)
        {
            _context = context;
        }
       
        public async Task<List<Outcome>> GetOutcomeList() 
        {
            IQueryable<Outcome> oc = _context.Outcomes.Where(o => o.DEFAULT_CLINIC_STATUS == "Active");

            return await oc.ToListAsync();
        }
    }
}
