using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IDiaryActionDataAsync
    {
        public Task<DiaryAction> GetDiaryActionDetails(string actionCode);
        public Task<List<DiaryAction>> GetDiaryActions();        
    }
    public class DiaryActionDataAsync : IDiaryActionDataAsync
    {
        private readonly ClinicalContext _clinicalContext;

        public DiaryActionDataAsync(ClinicalContext clinicalContext)
        {
            _clinicalContext = clinicalContext;
        }

        public async Task<DiaryAction> GetDiaryActionDetails(string actionCode)
        {
            DiaryAction action = await _clinicalContext.DiaryAction.FirstOrDefaultAsync(a => a.ActionCode == actionCode);

            return action;
        }

        public async Task<List<DiaryAction>> GetDiaryActions()
        {
            IQueryable<DiaryAction> actions = _clinicalContext.DiaryAction;
            
            return await actions.ToListAsync();
        }
    }
}