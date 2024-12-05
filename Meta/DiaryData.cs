using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IDiaryData
    {
        public List<Diary> GetDiaryList(int id);
        public List<Diary> GetDiaryListByRefID(int refID);
        public Diary GetLatestDiaryByRefID(int refID, string? docCode = "");
    }
    public class DiaryData : IDiaryData
    {
        private readonly ClinicalContext _clinContext;

        public DiaryData(ClinicalContext context)
        {
            _clinContext = context;
        }
        
        public List<Diary> GetDiaryList(int id) //Get list of diary entries for patient by MPI
        {
            Patient pat = _clinContext.Patients.FirstOrDefault(p => p.MPI == id);

            IQueryable<Diary> diary = from d in _clinContext.Diary
                        where d.WMFACSID == pat.WMFACSID
                        orderby d.DiaryDate
                        select d;

            return diary.ToList();
        }

        public List<Diary> GetDiaryListByRefID(int refID)
        {
            IQueryable<Diary> diaryList = _clinContext.Diary.Where(d => d.RefID == refID).OrderBy(l => l.DiaryDate);

            return diaryList.ToList();
        }

        public Diary GetLatestDiaryByRefID(int refID, string? docCode = "")
        {
            Diary diary = _clinContext.Diary.FirstOrDefault(d => d.RefID == refID && d.DocCode == docCode);

            return diary;
        }
    }
}
