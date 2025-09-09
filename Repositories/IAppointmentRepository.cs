using AppointmentAPI.Models;

namespace AppointmentAPI.Repositories
{
    public interface IAppointmentRepository: IRepository<Appointment>
    {
        Task<bool> HasOverlappingAppointment(Appointment appointment);
    }
}
