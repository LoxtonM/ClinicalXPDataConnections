using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITotalTriageDataAsync
    {
        public Task<List<TriageTotal>> GetAllTriages(string staffCode, DateTime? startDate, DateTime? endDate);
    }

    public class TotalTriageDataAsync : ITotalTriageDataAsync
    {
        private readonly ClinicalContext _context;

        public TotalTriageDataAsync(ClinicalContext context)
        {
            _context = context;
        }

        public async Task<List<TriageTotal>> GetAllTriages(string? staffCode, DateTime? startDate, DateTime? endDate)
        {
            var triages = _context.TriageTotal.Where(t => t.Triaged == true && t.LogicalDelete == false);

            if (staffCode != null)
            {
                triages = triages.Where(t => t.TriagedBy == staffCode);
            }

            if (startDate != null)
            {
                triages = triages.Where(t => t.TriagedDate >= startDate);
            }

            if (endDate != null)
            {
                triages = triages.Where(t => t.TriagedDate <= endDate);
            }

            triages = triages.OrderBy(t => t.TriagedDate);

            return await triages.ToListAsync();
        }
    }
}
