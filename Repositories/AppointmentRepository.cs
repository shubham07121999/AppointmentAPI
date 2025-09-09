using AppointmentAPI.Data;
using AppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repositories
{
    public class AppointmentRepository: Repository<Appointment>, IAppointmentRepository
    {

        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasOverlappingAppointment(Appointment appointment)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.DoctorName == appointment.DoctorName &&
                ((appointment.StartTime >= a.StartTime && appointment.StartTime < a.EndTime) ||
                 (appointment.EndTime > a.StartTime && appointment.EndTime <= a.EndTime)));
        }
    }
}
