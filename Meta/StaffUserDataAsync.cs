using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IStaffUserDataAsync
    {
        public Task<StaffMember> GetStaffMemberDetails(string suser);
        public Task<List<StaffMember>> GetClinicalStaffList();
        public Task<List<StaffMember>> GetStaffMemberList();
        public Task<List<StaffMember>> GetStaffMemberListAll();
        public Task<List<StaffMember>> GetConsultantsList();
        public Task<List<StaffMember>> GetGCList();
        public Task<List<StaffMember>> GetSpRList();
        public Task<List<StaffMember>> GetAdminList();
        public Task<List<string>> GetSecTeamsList();
        public Task<string> GetStaffCode(string userName);
        public Task<string> GetStaffName(string userName);
        public Task<string> GetStaffNameFromStaffCode(string staffCode);
        public Task<StaffMember> GetStaffMemberDetailsByStaffCode(string staffCode);
        public Task<List<StaffMember>> GetStaffMemberListByRole(string jobRole);
    }
    public class StaffUserDataAsync : IStaffUserDataAsync
    {
        private readonly ClinicalContext _clinContext;       

        public StaffUserDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
                
        public async Task<StaffMember> GetStaffMemberDetails(string suser) //Get details of a staff member by login name
        {
            StaffMember item = _clinContext.StaffMembers.FirstOrDefault(i => i.EMPLOYEE_NUMBER == suser);
            return item;
        }

        public async Task<List<StaffMember>> GetClinicalStaffList() //Get list of all clinical staff members currently in post
        {
            IQueryable<StaffMember> clinicians = from s in _clinContext.StaffMembers
                             where s.InPost == true && (s.CLINIC_SCHEDULER_GROUPS == "GC" || s.CLINIC_SCHEDULER_GROUPS == "Consultant" || s.CLINIC_SCHEDULER_GROUPS == "SpR")
                             orderby s.NAME
                             select s;

            return await clinicians.ToListAsync();
        }

        public async Task<List<StaffMember>> GetStaffMemberList() //Get list of all staff members currently in post 
        {
            IQueryable<StaffMember> sm = from s in _clinContext.StaffMembers
                     where s.InPost.Equals(true)
                     orderby s.NAME
                     select s;

            return await sm.ToListAsync();
        }

        public async Task<List<StaffMember>> GetStaffMemberListAll() //Get list of all staff members currently in post 
        {
            IQueryable<StaffMember> sm = from s in _clinContext.StaffMembers                                         
                                         orderby s.NAME
                                         select s;

            return await sm.ToListAsync();
        }

        public async Task<List<StaffMember>> GetConsultantsList() //Get list of all consultants
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                             where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS == "Consultant"
                             orderby rf.NAME
                             select rf;

            return await clinicians.ToListAsync();
        }

        public async Task<List<StaffMember>> GetGCList() //Get list of all GCs
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                             where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS == "GC"
                             orderby rf.NAME
                             select rf;

            return await clinicians.ToListAsync();
        }

        public async Task<List<StaffMember>> GetSpRList() //Get list of all GCs
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                                                 where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS.ToUpper() == "SPR"
                                                 orderby rf.NAME
                                                 select rf;

            return await clinicians.ToListAsync();
        }

        public async Task<List<StaffMember>> GetAdminList() //Get list of all Admin staff
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                                                 where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS == "Admin"
                                                 orderby rf.NAME
                                                 select rf;

            return await clinicians.ToListAsync();
        }

        public async Task<List<string>> GetSecTeamsList() //Get list of all secretarial teams
        {
            IQueryable<string> secteams = from rf in _clinContext.StaffMembers
                           where rf.BILL_ID != null && rf.BILL_ID.Contains("Team")
                           select rf.BILL_ID;

            return await secteams.Distinct().ToListAsync();
        }

        public async Task<string> GetStaffCode(string userName)
        {
            StaffMember staffCode = await _clinContext.StaffMembers.FirstOrDefaultAsync(s => s.EMPLOYEE_NUMBER == userName);
            
            return staffCode.STAFF_CODE;
        }

        public async Task<string> GetStaffName(string userName)
        {
            StaffMember staffName = await _clinContext.StaffMembers.FirstOrDefaultAsync(s => s.EMPLOYEE_NUMBER == userName);
            
            return staffName.NAME;
        }

        public async Task<string> GetStaffNameFromStaffCode(string staffCode)
        {
            StaffMember staffName = await _clinContext.StaffMembers.FirstOrDefaultAsync(s => s.STAFF_CODE == staffCode);
            
            return staffName.NAME;
        }

        public async Task<StaffMember> GetStaffMemberDetailsByStaffCode(string staffCode)
        {
            StaffMember staffUser = await _clinContext.StaffMembers.FirstOrDefaultAsync(s => s.STAFF_CODE == staffCode);

            return staffUser;
        }

        public async Task<List<StaffMember>> GetStaffMemberListByRole(string jobRole)
        {
            IQueryable<StaffMember> staffList = _clinContext.StaffMembers.Where(s => s.CLINIC_SCHEDULER_GROUPS == jobRole && s.InPost == true).OrderBy(s => s.NAME);

            return await staffList.ToListAsync();
        }
    }
}
