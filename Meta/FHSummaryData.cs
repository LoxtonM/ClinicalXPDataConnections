using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IFHSummaryData
    {
        public List<FHSummary> GetFHSummaryList(int id);
        
    }
    public class FHSummaryData : IFHSummaryData
    {
        private readonly ClinicalContext _clinContext;      

        public FHSummaryData(ClinicalContext context)
        {
            _clinContext = context;
        }


        public List<FHSummary> GetFHSummaryList(int id) //Get list of relatives of patient by MPI
        {
            Patient patient = _clinContext.Patients.FirstOrDefault(i => i.MPI == id);
            int wmfacsID = patient.WMFACSID;

            IQueryable<FHSummary> fhs = from r in _clinContext.FHSummary
                           where r.WMFACSID == wmfacsID
                           select r;           

            return fhs.ToList();
        }        
    }
}
