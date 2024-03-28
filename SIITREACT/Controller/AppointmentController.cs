using Microsoft.AspNetCore.Mvc;
using SIITREACT.Model;
using SIITREACT.Service;
namespace SIITREACT.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                var createdAppointment = await _appointmentService.CreateAppointment(appointment);
                return Ok("Appointment created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating appointment: {ex.Message}");
            }
        }
    }
}