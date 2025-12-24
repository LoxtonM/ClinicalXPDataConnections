using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IClinicDataAsync
    {
        public Task<List<Appointment>> GetClinicList(string username);
        public Task<List<Appointment>> GetClinicListByDate(DateTime dateFrom, DateTime dateTo);
        public Task<List<Appointment>> GetClinicByPatientsList(int mpi);
        public Task<List<Appointment>> GetAllOutstandingClinics();
        public Task<Appointment> GetClinicDetails(int refID);
        public Task<List<Outcome>> GetOutcomesList();
        public Task<List<Ethnicity>> GetEthnicitiesList();
    }
    public class ClinicDataAsync : IClinicDataAsync
    {
        private readonly ClinicalContext _clinContext;        
        private readonly StaffUserData _staffUser;

        public ClinicDataAsync(ClinicalContext context)
        {
            _clinContext = context;
            _staffUser = new StaffUserData(_clinContext);
        }
        
             

        public async Task<List<Appointment>> GetClinicList(string username) //Get list of your clinics
        {
            string staffCode = _staffUser.GetStaffMemberDetails(username).STAFF_CODE;

            IQueryable<Appointment> clinics = from c in _clinContext.Clinics
                          where c.AppType.Contains("App") && c.STAFF_CODE_1 == staffCode && c.Attendance != "Declined" && !c.Attendance.Contains("Canc")
                          select c;

            return await clinics.ToListAsync();
        }

        public async Task<List<Appointment>> GetClinicListByDate(DateTime dateFrom, DateTime dateTo) //Get list of clinics in date range
        {
            IQueryable<Appointment> clinics = from c in _clinContext.Clinics
                                              where c.AppType.Contains("App") && c.Attendance == "NOT RECORDED"
                                              && c.BOOKED_DATE >= dateFrom && c.BOOKED_DATE <= dateTo
                                              select c;

            return await clinics.ToListAsync();
        }


        public async Task<List<Appointment>> GetClinicByPatientsList(int mpi)
        {
            IQueryable<Appointment> appts = from c in _clinContext.Clinics
                        where c.MPI.Equals(mpi)
                        orderby c.BOOKED_DATE descending
                        select c;

            return await appts.ToListAsync();
        }

        public async Task<List<Appointment>> GetAllOutstandingClinics()
        {
            IQueryable<Appointment> appts = from c in _clinContext.Clinics
                                            where c.BOOKED_DATE != null && c.BOOKED_TIME != null && c.Attendance == "NOT RECORDED"
                                            orderby c.BOOKED_DATE descending, c.BOOKED_TIME 
                                            select c; 

            return await appts.ToListAsync();
        }

        public async Task<Appointment> GetClinicDetails(int refID) //Get details of an appointment for display only
        {
            Appointment appt = await _clinContext.Clinics.FirstOrDefaultAsync(a => a.RefID == refID);

            return appt;
        }

        public async Task<List<Outcome>> GetOutcomesList() //Get list of outcomes for clinic appointments
        {
            IQueryable<Outcome> outcomes = from o in _clinContext.Outcomes
                          where o.DEFAULT_CLINIC_STATUS.Equals("Active")
                          select o;

            return await outcomes.ToListAsync();
        }

        public async Task<List<Ethnicity>> GetEthnicitiesList() //Get list of ethnicities
        {
            IQueryable<Ethnicity> ethnicities = from e in _clinContext.Ethnicity
                         orderby e.Ethnic
                         select e;

            return await ethnicities.ToListAsync();
        }     
        
    }
}
