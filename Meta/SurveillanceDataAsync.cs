using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ISurveillanceDataAsync
    {
        public Task<List<Surveillance>> GetSurveillanceList(int? mpi);
        public Task<List<Surveillance>> GetSurveillanceListByRiskID(int? riskID);
        public Task<Surveillance> GetSurvDetails(int? riskID);        
    }
    public class SurveillanceDataAsync : ISurveillanceDataAsync
    {
        private readonly ClinicalContext _clinContext;
        
        public SurveillanceDataAsync(ClinicalContext context)
        {
            _clinContext = context;        
        }
        
        public async Task<List<Surveillance>> GetSurveillanceList(int? mpi) //Get list of all surveillance recommendations for an ICP (by MPI)
        {
            IQueryable<Surveillance> surveillances = from r in _clinContext.Surveillance
                               where r.MPI == mpi
                               select r;

            return await surveillances.ToListAsync();
        }

        public async Task<List<Surveillance>> GetSurveillanceListByRiskID(int? riskID) //Get list of all surveillance recommendations for a  risk item (by RiskID)
        {
            IQueryable<Surveillance> surveillances = from r in _clinContext.Surveillance
                                where r.RiskID == riskID
                                select r;

            return await surveillances.ToListAsync();
        }       

        public async Task<Surveillance> GetSurvDetails(int? survID) //Get details of surveillance recommendation by RiskID
        {
            Surveillance surv = await _clinContext.Surveillance.FirstOrDefaultAsync(c => c.SurvRecID == survID);

            return surv;
        }       

    }
}
