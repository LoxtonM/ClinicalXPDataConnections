using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAppointmentData
    {
        public Appointment GetAppointmentDetails(int refID);
        public List<Appointment> GetAppointments(DateTime dFrom, DateTime dTo, string? clinician, string? clinic);
        public List<Appointment> GetAppointmentsForADay(DateTime clinicDate, string? clinician = null , string? clinic = null);
        public List<Appointment> GetAppointmentsForBWH(DateTime clinicDate);
        public List<Appointment> GetAppointmentsForWholeFamily(int refID);
        public List<Appointment> GetAppointmentsByClinicians(string staffCode, DateTime? startDate, DateTime? endDate);
        public List<Appointment> GetMDC(string staffCode, DateTime? startDate, DateTime? endDate);
        public List<Appointment> GetAppointmentsByClinic(string staffCode, string clinic, DateTime? startDate, DateTime? endDate);
        public List<Appointment> GetAppointmentsByMonth(string staffCode, int month, int year);
        public List<Appointment> GetAppointmentListByReferral(int refID);
        public List<Appointment> GetAppointmentListByPatient(int mpi);

    }
    public class AppointmentData : IAppointmentData
    {
        private readonly ClinicalContext _context;
        public AppointmentData(ClinicalContext context)
        {
            _context = context;
        }
       

        public Appointment GetAppointmentDetails(int refID)
        {
            Appointment appt = _context.Clinics.FirstOrDefault(a => a.RefID == refID);
            return appt;
        }

        public List<Appointment> GetAppointments(DateTime dFrom, DateTime dTo, string? clinician, string? clinic)
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
            
            return appts.ToList();
        }

        public List<Appointment> GetAppointmentsForADay(DateTime clinicDate, string? clinician = null, string? clinic = null)
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
            
            return appts.ToList();
        }

        public List<Appointment> GetAppointmentsForBWH(DateTime clinicDate)
        {
            IQueryable<Appointment> appts = _context.Clinics.Where(a => a.BOOKED_DATE == clinicDate
            & a.Attendance != "Declined" & a.Attendance != "Cancelled by professional"
                    & a.Attendance != "Cancelled by patient");

            appts = appts.Where(l => l.FACILITY.Contains("BWH"));
                                 
            return appts.ToList();
        }

        public List<Appointment> GetAppointmentsForWholeFamily(int refID)
        {
            Appointment appt = _context.Clinics.FirstOrDefault(a => a.RefID == refID);

            IQueryable<Appointment> appts = _context.Clinics.Where(a => a.BOOKED_DATE == appt.BOOKED_DATE & a.BOOKED_TIME == appt.BOOKED_TIME &
            a.STAFF_CODE_1 == appt.STAFF_CODE_1 & a.FACILITY == appt.FACILITY & a.Attendance == "NOT RECORDED").OrderBy(a => a.RefID);
            
            return appts.ToList();
        }

        public List<Appointment> GetAppointmentsByClinicians(string staffCode, DateTime? startDate, DateTime? endDate)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode)
                                                    & !a.AppType.Contains("MD")
                                                    & !a.AppType.Contains("Admin"));

            apt = apt.Where(a => a.BOOKED_DATE > startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return apt.ToList();
        }

        public List<Appointment> GetMDC(string staffCode, DateTime? startDate, DateTime? endDate)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode)
                                                    & a.AppType.Contains("MD"));

            apt = apt.Where(a => a.BOOKED_DATE > startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return apt.ToList();
        }

        public List<Appointment> GetAppointmentsByClinic(string staffCode, string clinic, DateTime? startDate, DateTime? endDate)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode                                                    )
                                                    & a.Clinic == clinic);

            apt = apt.Where(a => a.BOOKED_DATE > startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return apt.ToList();
        }

        public List<Appointment> GetAppointmentsByMonth(string staffCode, int month, int year)
        {
            var apt = _context.Clinics.Where(a => (a.STAFF_CODE_1 == staffCode ||
                                                    a.STAFF_CODE_2 == staffCode ||
                                                    a.STAFF_CODE_3 == staffCode));

            DateTime startDate = DateTime.Parse(year + "-" + month + "-" + 1);
            DateTime endDate = DateTime.Parse(year + "-" + (month + 1) + "-" + 1);

            apt = apt.Where(a => a.BOOKED_DATE >= startDate);
            apt = apt.Where(a => a.BOOKED_DATE < endDate);

            return apt.ToList();
        }

        public List<Appointment> GetAppointmentListByReferral(int refID)
        {
            var apt = _context.Clinics.Where(a => a.ReferralRefID == refID);

            return apt.ToList();
        }

        public List<Appointment> GetAppointmentListByPatient(int mpi)
        {
            var apt = _context.Clinics.Where(a => a.MPI == mpi);

            return apt.ToList();
        }
    }
}
