using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAppointmentDataAsync
    {
        public Task<Appointment> GetAppointmentDetails(int refID);
        public Task<Appointment> GetAppointmentByClinicno(string? clinicno);
        public Task<List<Appointment>> GetAppointments(DateTime dFrom, DateTime dTo, string? clinician, string? clinic);
        public Task<List<Appointment>> GetAppointmentsForADay(DateTime clinicDate, string? clinician = null , string? clinic = null);
        public Task<List<Appointment>> GetAppointmentsForBWH(DateTime clinicDate);
        public Task<List<Appointment>> GetAppointmentsForWholeFamily(int refID);
        public Task<List<Appointment>> GetAppointmentsByClinicians(string staffCode, DateTime? startDate, DateTime? endDate);
        public Task<List<Appointment>> GetMDC(string staffCode, DateTime? startDate, DateTime? endDate);
        public Task<List<Appointment>> GetAppointmentsByClinic(string staffCode, string clinic, DateTime? startDate, DateTime? endDate);
        public Task<List<Appointment>> GetAppointmentsByMonth(string staffCode, int month, int year);
        public Task<List<Appointment>> GetAppointmentListByReferral(int refID);
        public Task<List<Appointment>> GetAppointmentListByPatient(int mpi);

    }
    public class AppointmentDataAsync : IAppointmentDataAsync
    {
        private readonly ClinicalContext _context;
        public AppointmentDataAsync(ClinicalContext context)
        {
            _context = context;
        }
       

        public async Task<Appointment> GetAppointmentDetails(int refID)
        {
            Appointment appt = await _context.Clinics.FirstOrDefaultAsync(a => a.RefID == refID);
            return appt;
        }

        public async Task<Appointment> GetAppointmentByClinicno(string? clinicno)
        {
            Appointment appointment = await _context.Clinics.FirstOrDefaultAsync(w => w.REFERRAL_CLINICNO == clinicno);
            return appointment;
        }

        public async Task<List<Appointment>> GetAppointments(DateTime dFrom, DateTime dTo, string? clinician, string? clinic)
        {            
            IQueryable<Appointment> appts = _context.Clinics.Where(a => a.BOOKED_DATE >= dFrom & 
                    a.BOOKED_DATE <= dTo & a.Attendance != "Declined" & a.Attendance != "Cancelled by professional" 
                    & a.Attendance != "Cancelled by patient" && a.MPI != 67066);

            if (clinician != null)
            {
                appts = appts.Where(l => l.STAFF_CODE_1 == clinician);
            }
            if (clinic != null)
            {
                appts = appts.Where(l => l.FACILITY == clinic);
            }            

            appts = appts.OrderByDescending(a => a.RefID); //to do the latest first, so that the first one appears on top
            
            return await appts.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsForADay(DateTime clinicDate, string? clinician = null, string? clinic = null)
        {
            IQueryable<Appointment> appts = _context.Clinics.Where(a => a.BOOKED_DATE == clinicDate 
            & a.Attendance != "Declined" & a.Attendance != "Cancelled by professional"
                    & a.Attendance != "Cancelled by patient");

            if (clinician != null)
            {
                appts = appts.Where(l => l.STAFF_CODE_1 == clinician);
            }
            if (clinic != null)
            {
                appts = appts.Where(l => l.FACILITY == clinic);
            }
            
            return await appts.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsForBWH(DateTime clinicDate)
        {
            IQueryable<Appointment> appts = _context.Clinics.Where(a => a.BOOKED_DATE == clinicDate
            & a.Attendance != "Declined" & a.Attendance != "Cancelled by professional"
                    & a.Attendance != "Cancelled by patient");

            appts = appts.Where(l => l.FACILITY.Contains("BWH"));
                                 
            return await appts.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsForWholeFamily(int refID)
        {
            Appointment appt = _context.Clinics.FirstOrDefault(a => a.RefID == refID);

            IQueryable<Appointment> appts = _context.Clinics.Where(a => a.BOOKED_DATE == appt.BOOKED_DATE & a.BOOKED_TIME == appt.BOOKED_TIME &
            a.STAFF_CODE_1 == appt.STAFF_CODE_1 & a.FACILITY == appt.FACILITY & a.Attendance == "NOT RECORDED").OrderBy(a => a.RefID);
            
            return await appts.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByClinicians(string staffCode, DateTime? startDate, DateTime? endDate)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode)
                                                    & !a.AppType.Contains("MD")
                                                    & !a.AppType.Contains("Admin"));

            apt = apt.Where(a => a.BOOKED_DATE > startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return await apt.ToListAsync();
        }

        public async Task<List<Appointment>> GetMDC(string staffCode, DateTime? startDate, DateTime? endDate)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode)
                                                    & a.AppType.Contains("MD"));

            apt = apt.Where(a => a.BOOKED_DATE > startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return await apt.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByClinic(string staffCode, string clinic, DateTime? startDate, DateTime? endDate)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode                                                    )
                                                    & a.Clinic == clinic);

            apt = apt.Where(a => a.BOOKED_DATE > startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return await apt.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByMonth(string staffCode, int month, int year)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode));

            DateTime startDate = DateTime.Parse(year + "-" + month + "-" + 1);
            DateTime endDate = DateTime.Parse(year + "-" + (month + 1) + "-" + 1);

            apt = apt.Where(a => a.BOOKED_DATE >= startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return await apt.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentListByReferral(int refID)
        {
            var apt = _context.Clinics.Where(a => a.ReferralRefID == refID);

            return await apt.ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentListByPatient(int mpi)
        {
            var apt = _context.Clinics.Where(a => a.MPI == mpi);

            return await apt.ToListAsync();
        }
    }
}
