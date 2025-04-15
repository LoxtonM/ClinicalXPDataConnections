using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITestEligibilityData
    {
        public List<Eligibility> GetTestingEligibilityList(int? mpi);
    }
    public class TestEligibilityData : ITestEligibilityData
    {
        private readonly ClinicalContext _clinContext;

        public TestEligibilityData(ClinicalContext context)
        {
            _clinContext = context;
        }
                

        public List<Eligibility> GetTestingEligibilityList(int? mpi) //Get list of testing aligibility codes by IcpID
        {
            IQueryable<Eligibility> eligibilities = from e in _clinContext.Eligibility
                              where e.MPI == mpi
                              select e;

            return eligibilities.ToList();
        }
        
    }
}
