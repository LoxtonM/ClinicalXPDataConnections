using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{

    public interface IVHRAuditDataAsync
    {
        public Task<List<VhrAudit>> GetVHRAuditList();
    }
    public class VhrAuditDataAsync : IVHRAuditDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public VhrAuditDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<List<VhrAudit>> GetVHRAuditList()
        {
            List<VhrAudit> vhrAuditList = await _clinContext.VhrAudits.ToListAsync();
            return vhrAuditList;
        }

    }
}
