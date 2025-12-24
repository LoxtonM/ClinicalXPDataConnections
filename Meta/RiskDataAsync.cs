using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRiskDataAsync
    {
        public Task<List<Risk>> GetRiskList(int icpID);
        public Task<List<Risk>> GetRiskListForPatient(int mpi);
        public Task<Risk> GetRiskDetails(int riskID);
        public Task<List<Risk>> GetRiskListByRefID(int refID);        
    }
    public class RiskDataAsync : IRiskDataAsync
    {
        private readonly ClinicalContext _clinContext;
        
        public RiskDataAsync(ClinicalContext context)
        {
            _clinContext = context;            
        }

        public async Task<List<Risk>> GetRiskList(int icpID) //Get list of all risk items for an ICP (by IcpID)
        {
            ICPCancer icp = _clinContext.ICPCancer.FirstOrDefault(c => c.ICP_Cancer_ID == icpID);

            IQueryable<Risk> risks = from r in _clinContext.Risk
                       where r.MPI == icp.MPI
                       select r;
           
            return await risks.ToListAsync();
        }

        public async Task<List<Risk>> GetRiskListForPatient(int mpi) //Get list of all risk items for a patient (by MPI)
        {            

            IQueryable<Risk> risks = from r in _clinContext.Risk
                                     where r.MPI == mpi
                                     select r;

            return await risks.ToListAsync();
        }

        public async Task<Risk> GetRiskDetails(int riskID) //Get details of risk item by RiskID
        {
            Risk risk = await _clinContext.Risk.FirstAsync(c => c.RiskID == riskID);
            return risk;
        }

        public async Task<List<Risk>> GetRiskListByRefID(int refID) //Get details of risk item by RiskID
        {
            IQueryable<Risk> risk = _clinContext.Risk.Where(c => c.RefID == refID && c.IncludeLetter != 0);
            
            return await risk.ToListAsync();
        }
    }
}
