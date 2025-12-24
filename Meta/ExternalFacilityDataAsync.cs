using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IExternalFacilityDataAsync
    {
        public Task<ExternalFacility> GetFacilityDetails(string sref);
        public Task<List<ExternalFacility>> GetFacilityList();
        public Task<List<ExternalFacility>> GetFacilityListAll();
        public Task<List<ExternalFacility>> GetGPPracticeList();
    }
    public class ExternalFacilityDataAsync : IExternalFacilityDataAsync
    {
        private readonly ClinicalContext _clinContext;
      
        public ExternalFacilityDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        

        public async Task<ExternalFacility> GetFacilityDetails(string sref) //Get details of external/referring facility
        {
            ExternalFacility item = await _clinContext.ExternalFacility.FirstAsync(f => f.MasterFacilityCode == sref);
            
            return item;
        }        

        public async Task<List<ExternalFacility>> GetFacilityList() //Get list of all active external/referring facilities
        {
            IQueryable<ExternalFacility> facilities = from rf in _clinContext.ExternalFacility
                             where rf.NONACTIVE == 0
                             orderby rf.NAME
                             select rf;

            return await facilities.ToListAsync();
        }

        public async Task<List<ExternalFacility>> GetFacilityListAll() //Get list of all external/referring facilities
        {
            IQueryable<ExternalFacility> facilities = from rf in _clinContext.ExternalFacility                                                      
                                                      orderby rf.NAME
                                                      select rf;

            return await facilities.ToListAsync();
        }

        public async Task<List<ExternalFacility>> GetGPPracticeList() //Get list of all external/referring facilities
        {
            IQueryable<ExternalFacility> facilities = from rf in _clinContext.ExternalFacility
                                                      where rf.NONACTIVE == 0
                                                      && rf.IS_GP_SURGERY == -1
                                                      orderby rf.NAME
                                                      select rf;

            return await facilities.Distinct().ToListAsync();
        }
    }
}
