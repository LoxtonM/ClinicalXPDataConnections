﻿using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface IWaitingListData
    {
        public List<WaitingList> GetWaitingList(string? clinician, string? clinic);
        public List<WaitingList> GetWaitingListByCGUNo(string searchTerm);
        public WaitingList GetWaitingListEntry(int intID, string clinicianID, string clinicID);
        public WaitingList GetWaitingListEntryByID(int id);        
    }
    public class WaitingListData : IWaitingListData
    {
        private readonly ClinicalContext _context;
        public WaitingListData(ClinicalContext context)
        {
            _context = context;
        }
       
        public List<WaitingList> GetWaitingList(string? clinician, string? clinic)
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
            return wl.OrderBy(l => l.PriorityLevel).ThenBy(l => l.AddedDate).ToList();
        }

        public List<WaitingList> GetWaitingListByCGUNo(string searchTerm)
        {
            IQueryable<WaitingList> wl = _context.WaitingList.Where(w => w.CGU_No.Contains(searchTerm));                       
            
            return wl.OrderBy(l => l.AddedDate).ToList();
        }
        
        public WaitingList GetWaitingListEntry(int intID, string clinicianID, string clinicID)
        {
            WaitingList waitingList;

            if (clinicID != null) //because of course there are nulls. Why would there not be nulls?
            {
                waitingList = _context.WaitingList.FirstOrDefault(w => w.IntID == intID && w.ClinicID == clinicID && w.ClinicianID == clinicianID);
            }
            else
            {
                waitingList = _context.WaitingList.FirstOrDefault(w => w.IntID == intID && w.ClinicID == "" && w.ClinicianID == clinicianID);
            }

            return waitingList;
        }

        public WaitingList GetWaitingListEntryByID(int id)
        {
            WaitingList waitingList = _context.WaitingList.FirstOrDefault(w => w.ID == id);
            

            return waitingList;
        }        
    }
}
