using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ISupervisorDataAsync
    {
        public Task<bool> GetIsGCSupervisor(string staffCode);
        public Task<bool> GetIsConsSupervisor(string staffCode);
    }
    public class SupervisorDataAsync : ISupervisorDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public SupervisorDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<bool> GetIsGCSupervisor(string staffCode)
        {
            Supervisors sup = await _clinContext.Supervisors.FirstOrDefaultAsync(s => s.StaffCode == staffCode);
            
            return sup.isGCSupervisor;
        }

        public async Task<bool> GetIsConsSupervisor(string staffCode)
        {
            Supervisors sup = await _clinContext.Supervisors.FirstOrDefaultAsync(s => s.StaffCode == staffCode);
            
            return sup.isConsSupervisor;
        }
    }
}
