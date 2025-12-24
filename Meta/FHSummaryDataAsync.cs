using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IFHSummaryDataAsync
    {
        public Task<List<FHSummary>> GetFHSummaryList(int id);
        
    }
    public class FHSummaryDataAsync : IFHSummaryDataAsync
    {
        private readonly ClinicalContext _clinContext;      

        public FHSummaryDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }


        public async Task<List<FHSummary>> GetFHSummaryList(int id) //Get list of relatives of patient by MPI
        {
            Patient patient = await _clinContext.Patients.FirstAsync(i => i.MPI == id);
            int wmfacsID = patient.WMFACSID;

            IQueryable<FHSummary> fhs = from r in _clinContext.FHSummary
                           where r.WMFACSID == wmfacsID
                           select r;           

            return await fhs.ToListAsync();
        }        
    }
}
