using System.Data;
using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ICaseloadDataAsync
    {
        public Task<List<Caseload>> GetCaseloadList(string staffCode);        
    }
    public class CaseloadDataAsync : ICaseloadDataAsync
    {
        private readonly ClinicalContext _clinContext;       

        public CaseloadDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        
        public async Task<List<Caseload>> GetCaseloadList(string staffCode) //Get caseload for clinician
        {
            IQueryable<Caseload> caseload = from c in _clinContext.Caseload
                           where c.StaffCode == staffCode
                           orderby c.BookedDate, c.BookedTime                           
                           select c;

            return await caseload.ToListAsync();
        }
    }
}
