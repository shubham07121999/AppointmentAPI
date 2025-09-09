using AppointmentAPI.Models;
using AppointmentAPI.Data;
namespace AppointmentAPI.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Appointments.Any())
            {
                context.Appointments.AddRange(
                    new Appointment { PatientName = "Yogesh", DoctorName = "Dr. Shubham", StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(2) },
                    new Appointment { PatientName = "Paras", DoctorName = "Dr. Amee", StartTime = DateTime.Now.AddHours(3), EndTime = DateTime.Now.AddHours(4) }
                );
                context.SaveChanges();
            }
        }
    }
}
