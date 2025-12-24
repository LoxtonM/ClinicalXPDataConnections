using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IWaitingListDataAsync
    {
        public Task<List<WaitingList>> GetWaitingList(string? clinician, string? clinic);
        public Task<List<WaitingList>> GetWaitingListByCGUNo(string searchTerm);
        public Task<WaitingList> GetWaitingListEntry(int intID, string clinicianID, string clinicID);
        public Task<WaitingList> GetWaitingListEntryByID(int id);        
    }
    public class WaitingListDataAsync : IWaitingListDataAsync
    {
        private readonly ClinicalContext _context;
        public WaitingListDataAsync(ClinicalContext context)
        {
            _context = context;
        }
       
        public async Task<List<WaitingList>> GetWaitingList(string? clinician, string? clinic)
        {
            IQueryable<WaitingList> wl = _context.WaitingList;

            if (clinician != null)
            {
                wl = wl.Where(l => l.ClinicianID == clinician);
            }
            if (clinic != null)
            {
                wl = wl.Where(l => l.ClinicID == clinic);
            }
            return await wl.OrderBy(l => l.PriorityLevel).ThenBy(l => l.AddedDate).ToListAsync();
        }

        public async Task<List<WaitingList>> GetWaitingListByCGUNo(string searchTerm)
        {
            IQueryable<WaitingList> wl = _context.WaitingList.Where(w => w.CGU_No.Contains(searchTerm));                       
            
            return await wl.OrderBy(l => l.AddedDate).ToListAsync();
        }
        
        public async Task<WaitingList> GetWaitingListEntry(int intID, string clinicianID, string clinicID)
        {
            WaitingList waitingList;

            if (clinicID != null) //because of course there are nulls. Why would there not be nulls?
            {
                waitingList = await _context.WaitingList.FirstAsync(w => w.IntID == intID && w.ClinicID == clinicID && w.ClinicianID == clinicianID);
            }
            else
            {
                waitingList = await _context.WaitingList.FirstAsync(w => w.IntID == intID && w.ClinicID == "" && w.ClinicianID == clinicianID);
            }

            return waitingList;
        }

        public async Task<WaitingList> GetWaitingListEntryByID(int id)
        {
            WaitingList waitingList = await _context.WaitingList.FirstAsync(w => w.ID == id);
            
            return waitingList;
        }        
    }
}
