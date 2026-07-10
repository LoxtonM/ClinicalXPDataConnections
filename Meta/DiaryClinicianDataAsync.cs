using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IDiaryClinicianDataAsync
    {        
        public Task<List<DiaryClinician>> GetDiaryClinicians();
    }

    public class DiaryClinicianDataAsync : IDiaryClinicianDataAsync
    {
        private readonly ClinicalContext _clinicalContext;

        public DiaryClinicianDataAsync(ClinicalContext clinicalContext)
        {
            _clinicalContext = clinicalContext;
        }

        public async Task<List<DiaryClinician>> GetDiaryClinicians()
        {
            IQueryable<DiaryClinician> clinicians = _clinicalContext.DiaryClinician.Where(d => d.Name != null);

            return await clinicians.ToListAsync();
        }
    }
}
