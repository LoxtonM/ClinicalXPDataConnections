using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITotalTriageData
    {
        public List<TriageTotal> GetAllTriages(string staffCode, DateTime? startDate, DateTime? endDate);
    }

    public class TotalTriageData : ITotalTriageData
    {
        private readonly ClinicalContext _context;

        public TotalTriageData(ClinicalContext context)
        {
            _context = context;
        }

        public List<TriageTotal> GetAllTriages(string? staffCode, DateTime? startDate, DateTime? endDate)
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

            return triages.ToList();
        }
    }
}
