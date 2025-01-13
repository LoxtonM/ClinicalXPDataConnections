using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface IEthnicityData
    {        
        public List<Ethnicity> GetEthnicitiesList();
    }
    public class EthnicityData : IEthnicityData
    {
        private readonly ClinicalContext _clinContext;

        public EthnicityData(ClinicalContext context)
        {
            _clinContext = context;
        }
        public List<Ethnicity> GetEthnicitiesList()
        {
            IQueryable<Ethnicity> ethnicities = from e in _clinContext.Ethnicity
                                                orderby e.Ethnic
                                                select e;

            return ethnicities.ToList();
        }
    }
}
