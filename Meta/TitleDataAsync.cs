using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITitleDataAsync
    {
        public Task<List<PatientTitle>> GetTitlesList();
    }
    public class TitleDataAsync : ITitleDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public TitleDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        public async Task<List<PatientTitle>> GetTitlesList()
        {
            IQueryable<PatientTitle> titles = from e in _clinContext.PatientTitles
                                              select e;

            return await titles.ToListAsync();
        }
    }
}
