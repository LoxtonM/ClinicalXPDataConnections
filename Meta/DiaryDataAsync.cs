using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IDiaryDataAsync
    {
        public Task<List<Diary>> GetDiaryList(int id);
        public Task<List<Diary>> GetDiaryListByRefID(int refID);
        public Task<Diary> GetLatestDiaryByRefID(int refID, string? docCode = "");
        public Task<Diary> GetDiaryEntry(int diaryID);
    }
    public class DiaryDataAsync : IDiaryDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public DiaryDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        
        public async Task<List<Diary>> GetDiaryList(int id) //Get list of diary entries for patient by MPI
        {
            Patient pat = await _clinContext.Patients.FirstOrDefaultAsync(p => p.MPI == id);

            IQueryable<Diary> diary = from d in _clinContext.Diary
                        where d.WMFACSID == pat.WMFACSID
                        orderby d.DiaryDate
                        select d;

            return await diary.ToListAsync();
        }

        public async Task<List<Diary>> GetDiaryListByRefID(int refID)
        {
            IQueryable<Diary> diaryList = _clinContext.Diary.Where(d => d.RefID == refID).OrderBy(l => l.DiaryDate);

            return await diaryList.ToListAsync();
        }

        public async Task<Diary> GetLatestDiaryByRefID(int refID, string? docCode = "")
        {
            //List<Diary> diaryList = await GetDiaryListByRefID(refID);
            //Diary diary = _clinContext.Diary.FirstOrDefault(d => d.RefID == refID && d.DocCode == docCode);
            IQueryable<Diary> diaryList = _clinContext.Diary.Where(d => d.DocCode == docCode && d.RefID == refID);

            Diary diary = await diaryList.OrderByDescending(d => d.DiaryID).FirstOrDefaultAsync();

            return diary;
        }

        public async Task<Diary> GetDiaryEntry(int diaryID)
        {
            Diary diary = await _clinContext.Diary.FirstOrDefaultAsync(d => d.DiaryID == diaryID);

            return diary;
        }
    }
}
