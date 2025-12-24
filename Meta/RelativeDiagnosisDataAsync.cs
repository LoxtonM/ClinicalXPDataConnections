using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRelativeDiagnosisDataAsync
    {
        public Task<List<RelativesDiagnosis>> GetRelativeDiagnosisList(int id);
        public Task<RelativesDiagnosis> GetRelativeDiagnosisDetails(int id);
        public Task<List<CancerReg>> GetCancerRegList();
        public Task<List<RequestStatus>> GetRequestStatusList();
        public Task<List<TumourSite>> GetTumourSiteList();
        public Task<List<TumourLat>> GetTumourLatList();
        public Task<List<TumourMorph>> GetTumourMorphList();
    }
    public class RelativeDiagnosisDataAsync : IRelativeDiagnosisDataAsync
    {
        private readonly ClinicalContext _clinContext;
        

        public RelativeDiagnosisDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<List<RelativesDiagnosis>> GetRelativeDiagnosisList(int id)
        {
            IQueryable<RelativesDiagnosis> reldiag = from r in _clinContext.RelativesDiagnoses
                                                     where r.RelsID == id
                                                     select r;

            return await reldiag.ToListAsync();
        }

        public async Task<RelativesDiagnosis> GetRelativeDiagnosisDetails(int id)
        {
            RelativesDiagnosis item = await _clinContext.RelativesDiagnoses.FirstAsync(rd => rd.TumourID == id);
            return item;
        }

        public async Task<List<CancerReg>> GetCancerRegList()
        {
            IQueryable<CancerReg> creg = from c in _clinContext.CancerReg
                                         where c.Creg_InUse == true
                                         select c;
            int dfs = creg.Count();
            return await creg.ToListAsync();
        }

        public async Task<List<RequestStatus>> GetRequestStatusList()
        {
            IQueryable<RequestStatus> status = from s in _clinContext.RequestStatus
                                               select s;
            int dfs = status.Count();
            return await status.ToListAsync();
        }

        public async Task<List<TumourSite>> GetTumourSiteList()
        {
            IQueryable<TumourSite> item = from i in _clinContext.TumourSite
                                          select i;

            return await item.ToListAsync();
        }

        public async Task<List<TumourLat>> GetTumourLatList()
        {
            IQueryable<TumourLat> item = from i in _clinContext.TumourLat
                                         select i;

            return await item.ToListAsync();
        }

        public async Task<List<TumourMorph>> GetTumourMorphList()
        {
            IQueryable<TumourMorph> item = from i in _clinContext.TumourMorph
                                           select i;

            return await item.ToListAsync();
        }
    }
}