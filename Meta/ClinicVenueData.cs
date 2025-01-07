using ClinicalXPDataConnections.Models;
using ClinicalXPDataConnections.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IClinicVenueData
    {
        public ClinicVenue GetVenueDetails(string ven);
        public List<ClinicVenue> GetVenueList();
    }
    public class ClinicVenueData : IClinicVenueData
    {
        private readonly ClinicalContext _context;
        public ClinicVenueData(ClinicalContext context)
        {
            _context = context;
        }        

        public ClinicVenue GetVenueDetails(string ven)
        {
            ClinicVenue clin = _context.ClinicalFacilities.FirstOrDefault(v => v.FACILITY == ven);
            return clin;
        }

        public List<ClinicVenue> GetVenueList() 
        {
            IQueryable<ClinicVenue> venuelist = _context.ClinicalFacilities.Where(v => v.NON_ACTIVE == 0).OrderBy(v => v.NAME);
            return venuelist.ToList();
        }       
    }
}
