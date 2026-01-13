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
            bool isSupervisor = false;

            Supervisors sup = await _clinContext.Supervisors.FirstOrDefaultAsync(s => s.StaffCode == staffCode);

            if (sup != null)
            {
                isSupervisor = sup.isGCSupervisor;
            }

            return isSupervisor;
        }

        public async Task<bool> GetIsConsSupervisor(string staffCode)
        {
            bool isSupervisor = false;

            Supervisors sup = await _clinContext.Supervisors.FirstOrDefaultAsync(s => s.StaffCode == staffCode);

            if (sup != null)
            {
                isSupervisor = sup.isConsSupervisor;
            }

            return isSupervisor;
        }
    }
}
