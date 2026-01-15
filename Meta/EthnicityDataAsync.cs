using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IEthnicityDataAsync
    {        
        public Task<List<Ethnicity>> GetEthnicitiesList();
    }
    public class EthnicityDataAsync : IEthnicityDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public EthnicityDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
        public async Task<List<Ethnicity>> GetEthnicitiesList()
        {
            IQueryable<Ethnicity> ethnicities = from e in _clinContext.Ethnicity
                                                orderby e.Ethnic
                                                select e;

            return await ethnicities.ToListAsync();
        }
    }
}
