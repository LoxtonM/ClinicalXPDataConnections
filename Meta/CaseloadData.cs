using System.Data;
using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface ICaseloadData
    {
        public List<Caseload> GetCaseloadList(string staffCode);
    }
    public class CaseloadData : ICaseloadData
    {
        private readonly ClinicalContext _clinContext;       

        public CaseloadData(ClinicalContext context)
        {
            _clinContext = context;
        }
        
        public List<Caseload> GetCaseloadList(string staffCode) //Get caseload for clinician
        {
            IQueryable<Caseload> caseload = from c in _clinContext.Caseload
                           where c.StaffCode == staffCode
                           select c;

            return caseload.ToList();
        }

    }
}
