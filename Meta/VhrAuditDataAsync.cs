using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{

    public interface IVHRAuditDataAsync
    {
        public Task<List<VhrAudit>> GetVHRAuditList();
        public Task<VhrAudit> GetVHRAuditDetails(int WMFACSID);
        public Task<string?> GetBreastScreeningService(string gpFacility);
        public Task<VhrAudit> GetVHRAuditDetailsByCguno(string cguno);

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

        public async Task<VhrAudit> GetVHRAuditDetails(int WMFACSID)
        {
            return await _clinContext.VhrAudits.AsNoTracking()
               .Where(i => i.WMFACSID == WMFACSID && i.DiaryRec == null)
               .OrderByDescending(i => i.DiaryID)
               .FirstOrDefaultAsync();
        }

        public async Task<string?> GetBreastScreeningService(string gpFacility)
        { 
            if (string.IsNullOrEmpty(gpFacility))
            {
                return null;
            }
            return await (from gp in _clinContext.ListScreeningServiceGPCodes.AsNoTracking()
                          join s in _clinContext.ListScreeningServiceDetails.AsNoTracking()
                            on gp.ScreeningOfficeCode equals s.ScreeningOfficeCode
                          where gp.GPCode == gpFacility
                          select s.BreastScreeningService)
                       .FirstOrDefaultAsync();

        }

        public async Task<VhrAudit> GetVHRAuditDetailsByCguno(string cguno)
        {
            return await _clinContext.VhrAudits.AsNoTracking()
               .Where(i => i.CGU_No == cguno)
               .OrderByDescending(i => i.DiaryDate)
               .FirstOrDefaultAsync();
        }

    }
}
