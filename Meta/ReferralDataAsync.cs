using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClinicalXPDataConnections.Meta
{
    public interface IReferralDataAsync
    {
        public Task<Referral> GetReferralDetails(int id);
        public Task<List<Referral>> GetReferralsList(int id);
        public Task<List<Referral>> GetTempRegList(int id);
        public Task<List<Referral>> GetReferralsByStaffMember(string staffCode, DateTime? startDate, DateTime? endDate);
        public Task<List<Referral>> GetActiveReferralsList();
        public Task<List<Referral>> GetActiveReferralsListForPatient(int id);
        public Task<List<Referral>> GetUnassignedReferrals();
    }
    public class ReferralDataAsync : IReferralDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public ReferralDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
                
        public async Task<Referral> GetReferralDetails(int id) //Get details of referral by RefID
        {
            Referral referral = await _clinContext.Referrals?.FirstOrDefaultAsync(i => i.refid == id);
            return referral;
        } 
                
        public async Task<List<Referral>> GetReferralsList(int id) //Get list of referrals for patient by MPI
        {
            IQueryable<Referral> referrals = from r in _clinContext.Referrals
                           where r.MPI == id & r.RefType.Contains("Referral")
                           orderby r.RefDate
                           select r;            

            return await referrals.ToListAsync();
        }

        public async Task<List<Referral>> GetTempRegList(int id) //Get list of temp reges for patient by MPI
        {
            IQueryable<Referral> treg = from r in _clinContext.Referrals
                                             where r.MPI == id & r.RefType.Contains("Temp")
                                             orderby r.RefDate
                                             select r;

            return await treg.ToListAsync();
        }

        public async Task<List<Referral>> GetReferralsByStaffMember(string staffCode, DateTime? startDate, DateTime? endDate)
        {
            var refs = _clinContext.Referrals.Where(r => r.PATIENT_TYPE_CODE == staffCode ||
                                                    r.GC_CODE == staffCode);

            refs = refs.Where(a => a.RefDate > startDate);
            refs = refs.Where(a => a.RefDate < endDate);

            return await refs.ToListAsync();
        }

        public async Task<List<Referral>> GetActiveReferralsList()
        {
            var patientReferralsList = _clinContext.Referrals.Where(r => r.RefType.Contains("Refer")
                                                                        && r.COMPLETE != "Complete"
                                                                        && r.logicaldelete == false
                                                                        && r.AdminContactCode != null).OrderBy(r => r.WeeksFromReferral);

            return await patientReferralsList.ToListAsync();
        }

        public async Task<List<Referral>> GetActiveReferralsListForPatient(int id)
        {
            var patientReferralsList = _clinContext.Referrals.Where(r => r.RefType.Contains("Refer")
                                                                        && r.COMPLETE != "Complete"
                                                                        && r.logicaldelete == false
                                                                        && r.MPI == id).OrderBy(r => r.WeeksFromReferral);

            return await patientReferralsList.ToListAsync();
        }


        public async Task<List<Referral>> GetUnassignedReferrals()
        {
            IQueryable<Referral> referrals = _clinContext.Referrals.Where(r => r.RefType.Contains("Refer") && r.COMPLETE == "Missing Data");

            return await referrals.ToListAsync();
        }
    }
}
