using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IReferralData
    {
        public Referral GetReferralDetails(int id);
        public List<Referral> GetReferralsList(int id);
        public List<Referral> GetReferralsByStaffMember(string staffCode, DateTime? startDate, DateTime? endDate);
        public List<Referral> GetActiveReferralsList();
        public List<Referral> GetActiveReferralsListForPatient(int id);
    }
    public class ReferralData : IReferralData
    {
        private readonly ClinicalContext _clinContext;

        public ReferralData(ClinicalContext context)
        {
            _clinContext = context;
        }
                
        public Referral GetReferralDetails(int id) //Get details of referral by RefID
        {
            Referral referral = _clinContext.Referrals?.FirstOrDefault(i => i.refid == id);
            return referral;
        } 
                
        public List<Referral> GetReferralsList(int id) //Get list of active referrals for patient by MPI
        {
            IQueryable<Referral> referrals = from r in _clinContext.Referrals
                           where r.MPI == id & r.RefType.Contains("Referral") & r.COMPLETE != "Complete"
                           orderby r.RefDate
                           select r;            

            return referrals.ToList();
        }

        public List<Referral> GetReferralsByStaffMember(string staffCode, DateTime? startDate, DateTime? endDate)
        {
            var refs = _clinContext.Referrals.Where(r => r.PATIENT_TYPE_CODE == staffCode ||
                                                    r.GC_CODE == staffCode);

            refs = refs.Where(a => a.RefDate > startDate);
            refs = refs.Where(a => a.RefDate < endDate);

            return refs.ToList();
        }

        public List<Referral> GetActiveReferralsList()
        {
            var patientReferralsList = _clinContext.Referrals.Where(r => r.RefType.Contains("Refer")
                                                                        && r.COMPLETE != "Complete"
                                                                        && r.logicaldelete == false
                                                                        && r.AdminContactCode != null).OrderBy(r => r.WeeksFromReferral).ToList();

            return patientReferralsList;
        }

        public List<Referral> GetActiveReferralsListForPatient(int id)
        {
            var patientReferralsList = _clinContext.Referrals.Where(r => r.RefType.Contains("Refer")
                                                                        && r.COMPLETE != "Complete"
                                                                        && r.logicaldelete == false
                                                                        && r.MPI == id).OrderBy(r => r.WeeksFromReferral).ToList();

            return patientReferralsList;
        }
    }
}
