using CSF.Services.Interfaces;
using CSF.Services.Messages;
using CSF.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AgendaService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(
            ILogger<AppointmentsController> logger,
            IAppointmentService appointmentService
        )
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        [HttpGet(Name = "GetAppointments")]
        public List<AppointmentDto> Get()
        {
            return _appointmentService.GetAppointments(new GetAppointmentsRequest()).Appointments;
        }

        [HttpPost(Name = "AddAppointment")]
        public IActionResult PostAsync(AddAppointmentDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validation error on AddAppointment: " + Newtonsoft.Json.JsonConvert.SerializeObject(ModelState.Values));
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var response = _appointmentService.AddAppointment(new AddAppointmentsRequest { model = model });
            return new JsonResult(response.Appointment);
        }

        [HttpGet("calendar", Name = "GetAppointmentsCalendar")]
        public IActionResult GetCalendar([FromQuery] GetAppointmentsCalendarRequest request)
        {
            return new JsonResult(_appointmentService.GetAppointmentsCalendar(request).AppointmentsCalendar);
        }
    }
}