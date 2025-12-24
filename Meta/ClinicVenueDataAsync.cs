using ClinicalXPDataConnections.Models;
using ClinicalXPDataConnections.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IClinicVenueDataAsync
    {
        public Task<ClinicVenue> GetVenueDetails(string ven);
        public Task<List<ClinicVenue>> GetVenueList();
    }
    public class ClinicVenueDataAsync : IClinicVenueDataAsync
    {
        private readonly ClinicalContext _context;
        public ClinicVenueDataAsync(ClinicalContext context)
        {
            _context = context;
        }        

        public async Task<ClinicVenue> GetVenueDetails(string ven)
        {
            ClinicVenue clin = await _context.ClinicalFacilities.FirstAsync(v => v.FACILITY == ven);
            
            return clin;
        }

        public async Task<List<ClinicVenue>> GetVenueList() 
        {
            IQueryable<ClinicVenue> venuelist = _context.ClinicalFacilities.Where(v => v.NON_ACTIVE == 0).OrderBy(v => v.NAME);
            
            return await venuelist.ToListAsync();
        }       
    }
}
