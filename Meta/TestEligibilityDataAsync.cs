using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITestEligibilityDataAsync
    {
        public Task<List<Eligibility>> GetTestingEligibilityList(int mpi);
        public Task<Eligibility> GetTestingEligibilityDetails(int id);
    }
    public class TestEligibilityDataAsync : ITestEligibilityDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public TestEligibilityDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
                

        public async Task<List<Eligibility>> GetTestingEligibilityList(int mpi) //Get list of testing aligibility codes by MPI
        {
            IQueryable<Eligibility> eligibilities = from e in _clinContext.Eligibility
                              where e.MPI == mpi
                              select e;

            return await eligibilities.ToListAsync();
        }

        public async Task<Eligibility> GetTestingEligibilityDetails(int id)
        {
            Eligibility eligibility = await _clinContext.Eligibility.FirstAsync(e => e.ID == id);

            return eligibility;
        }
    }
}
