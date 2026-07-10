using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{

    public interface IRelativeDiaryDataAsync
    {
        public Task<List<RelativeDiary>> GetRelativeDiaryList(int relID);
        public Task<RelativeDiary> GetRelativeDiaryDetails(int diaryID);
        public Task<RelativeDiary> GetLatestDiaryByDocCode(int relID, string docCode);
    }
    public class RelativeDiaryDataAsync : IRelativeDiaryDataAsync
    {
        private readonly ClinicalContext _context;

        public RelativeDiaryDataAsync(ClinicalContext context)
        {
            _context = context;
        }

        public async Task<List<RelativeDiary>> GetRelativeDiaryList(int relID)
        {
            IQueryable<RelativeDiary> diaryList = _context.RelativesDiary.Where(d => d.RelsID == relID);

            return await diaryList.ToListAsync();
        }

        public async Task<RelativeDiary> GetRelativeDiaryDetails(int diaryID)
        {
            RelativeDiary rd = await _context.RelativesDiary.FirstOrDefaultAsync(d => d.DiaryID == diaryID);

            return rd;
        }

        public async Task<RelativeDiary> GetLatestDiaryByDocCode(int relID, string docCode)
        {
            RelativeDiary latestDiary = await _context.RelativesDiary
                        .Where(d => d.RelsID == relID && d.DocCode == docCode)
                        .OrderByDescending(d => d.DiaryDate)
                        .FirstOrDefaultAsync();
            
            return latestDiary;
        }
    }
}
