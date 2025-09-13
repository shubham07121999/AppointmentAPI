using AppointmentAPI.Data;
using AppointmentAPI.Models;
using AppointmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentsController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            var appointments = await _repository.GetAllAsync();
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {

            //Checking validation for patient name and doctor name must be present.
            if (string.IsNullOrWhiteSpace(appointment.PatientName) || string.IsNullOrWhiteSpace(appointment.DoctorName))
                return BadRequest("PatientName and DoctorName are required.");

            //Checking the overlapping condtion with same doctor.
            if (await _repository.HasOverlappingAppointment(appointment))
                return BadRequest("This doctor already has an appointment in the selected time slot.");


            var created = await _repository.AddAsync(appointment);
            return CreatedAtAction(nameof(GetAppointments), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id,[FromBody]Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest();

            await _repository.UpdateAsync(appointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentsByID(int id)
        {
            var appointments = await _repository.GetByIdAsync(id);
            return Ok(appointments);
        }
    }
}
