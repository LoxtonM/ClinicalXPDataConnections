using ClinicalXPDataConnections.Models;
using ClinicalXPDataConnections.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRelativeDiarySourceDataAsync
    {
        public Task<List<RelativeDiarySource>> GetRelativeDiarySourceList();        
    }
    public class RelativeDiarySourceDataAsync : IRelativeDiarySourceDataAsync
    {
        private readonly ClinicalContext _context;

        public RelativeDiarySourceDataAsync(ClinicalContext context)
        {
            _context = context;
        }
        public async Task<List<RelativeDiarySource>> GetRelativeDiarySourceList()
        {            
            IQueryable<RelativeDiarySource> diarySourceList = _context.RelativeDiarySource;
                
            
            return await diarySourceList.ToListAsync();            
        }        
    }
}
