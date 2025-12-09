using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IStaffUserData
    {
        public StaffMember GetStaffMemberDetails(string suser);
        public List<StaffMember> GetClinicalStaffList();
        public List<StaffMember> GetStaffMemberList();
        public List<StaffMember> GetStaffMemberListAll();
        public List<StaffMember> GetConsultantsList();
        public List<StaffMember> GetSpRList();
        public List<StaffMember> GetGCList();
        public List<StaffMember> GetAdminList();
        public List<string> GetSecTeamsList();
        public string GetStaffCode(string userName);
        public string GetStaffName(string userName);
        public string GetStaffNameFromStaffCode(string staffCode);
        public StaffMember GetStaffMemberDetailsByStaffCode(string staffCode);
        public List<StaffMember> GetStaffMemberListByRole(string jobRole);
    }
    public class StaffUserData : IStaffUserData
    {
        private readonly ClinicalContext _clinContext;       

        public StaffUserData(ClinicalContext context)
        {
            _clinContext = context;
        }
                
        public StaffMember GetStaffMemberDetails(string suser) //Get details of a staff member by login name
        {
            StaffMember item = _clinContext.StaffMembers.FirstOrDefault(i => i.EMPLOYEE_NUMBER == suser);
            return item;
        }

        public List<StaffMember> GetClinicalStaffList() //Get list of all clinical staff members currently in post
        {
            IQueryable<StaffMember> clinicians = from s in _clinContext.StaffMembers
                             where s.InPost == true && (s.CLINIC_SCHEDULER_GROUPS == "GC" || s.CLINIC_SCHEDULER_GROUPS == "Consultant" || s.CLINIC_SCHEDULER_GROUPS == "SpR")
                             orderby s.NAME
                             select s;

            return clinicians.ToList();
        }

        public List<StaffMember> GetStaffMemberList() //Get list of all staff members currently in post 
        {
            IQueryable<StaffMember> sm = from s in _clinContext.StaffMembers
                     where s.InPost.Equals(true)
                     orderby s.NAME
                     select s;

            return sm.ToList();
        }

        public List<StaffMember> GetStaffMemberListAll() //Get list of all staff members currently in post 
        {
            IQueryable<StaffMember> sm = from s in _clinContext.StaffMembers                                         
                                         orderby s.NAME
                                         select s;

            return sm.ToList();
        }

        public List<StaffMember> GetConsultantsList() //Get list of all consultants
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                             where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS == "Consultant"
                             orderby rf.NAME
                             select rf;

            return clinicians.ToList();
        }

        public List<StaffMember> GetSpRList() //Get list of all sprs
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                                                 where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS.ToLower() == "spr"
                                                 orderby rf.NAME
                                                 select rf;

            return clinicians.ToList();
        }

        public List<StaffMember> GetGCList() //Get list of all GCs
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                             where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS == "GC"
                             orderby rf.NAME
                             select rf;

            return clinicians.ToList();
        }

        public List<StaffMember> GetAdminList() //Get list of all Admin staff
        {
            IQueryable<StaffMember> clinicians = from rf in _clinContext.StaffMembers
                                                 where rf.InPost == true && rf.CLINIC_SCHEDULER_GROUPS == "Admin"
                                                 orderby rf.NAME
                                                 select rf;

            return clinicians.ToList();
        }

        public List<string> GetSecTeamsList() //Get list of all secretarial teams
        {
            IQueryable<string> secteams = from rf in _clinContext.StaffMembers
                           where rf.BILL_ID != null && rf.BILL_ID.Contains("Team")
                           select rf.BILL_ID;

            return secteams.Distinct().ToList();
        }

        public string GetStaffCode(string userName)
        {
            string staffCode = _clinContext.StaffMembers.FirstOrDefault(s => s.EMPLOYEE_NUMBER == userName).STAFF_CODE;
            return staffCode;
        }

        public string GetStaffName(string userName)
        {
            string staffName = _clinContext.StaffMembers.FirstOrDefault(s => s.EMPLOYEE_NUMBER == userName).NAME;
            return staffName;
        }

        public string GetStaffNameFromStaffCode(string staffCode)
        {
            string staffName = _clinContext.StaffMembers.FirstOrDefault(s => s.STAFF_CODE == staffCode).NAME;
            return staffName;
        }

        public StaffMember GetStaffMemberDetailsByStaffCode(string staffCode)
        {
            var staffUser = _clinContext.StaffMembers.FirstOrDefault(s => s.STAFF_CODE == staffCode);

            return staffUser;
        }

        public List<StaffMember> GetStaffMemberListByRole(string jobRole)
        {
            var staffList = _clinContext.StaffMembers.Where(s => s.CLINIC_SCHEDULER_GROUPS == jobRole && s.InPost == true).OrderBy(s => s.NAME).ToList();

            return staffList;
        }



    }
}
