using CSF.AssyncMessaging.Domain;
using CSF.Domain;
using CSF.Entities;
using CSF.Services.Interfaces;
using CSF.Services.Messages;
using CSF.ViewModel;
using FluentValidation;
using Mapster;
using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CSF.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ILogger<IAppointmentRepository> _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IValidator<AddAppointmentDto> _appointmentValidator;
        private readonly IMessageService _messageService;
        private readonly IPublishEndpoint _publishEndpoint;
        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            ILogger<IAppointmentRepository> logger,
            IValidator<AddAppointmentDto> appointmentValidator,
            IMessageService messageService
        )
        {
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _appointmentValidator = appointmentValidator;
            _messageService = messageService;
        }

        public GetAppointmentsResponse GetAppointments(GetAppointmentsRequest request)
        {
            var response = new GetAppointmentsResponse();
            var appointmens = _appointmentRepository.GetAll();
            response.Appointments = appointmens.Adapt<List<AppointmentDto>>();
            return response;
        }

        public GetAppointmentsCalendarResponse GetAppointmentsCalendar(GetAppointmentsCalendarRequest request)
        {
            var response = new GetAppointmentsCalendarResponse();
            var appointmens = _appointmentRepository.GetAll(x => x.Date.Month == request.CalendarMonth);
            var monthDays = DateTime.DaysInMonth(DateTime.Now.Year, request.CalendarMonth);
            var calendarDays = new List<AppointmentCalendarDayDto>();

            for (int i = 1; i <= monthDays; i++)
            {
                var calendarDay = new AppointmentCalendarDayDto();
                calendarDay.Date = new DateTime(DateTime.Now.Year, request.CalendarMonth, i);

                var appointmentsForTheDay = appointmens.Where(x => x.Date.Day == i).ToList();
                calendarDay.Appointments = appointmentsForTheDay.Adapt<List<AppointmentDto>>();
                calendarDays.Add(calendarDay);
            }

            response.AppointmentsCalendar = new AppointmentCalendarDto { Days = calendarDays };
            return response;
        }

        public AddAppointmentsResponse AddAppointment(AddAppointmentsRequest request)
        {
            var validateResult = _appointmentValidator.Validate(request.model);
            if (!validateResult.IsValid)
            {
                string errors = "AddAppointment validation errors: " + JsonConvert.SerializeObject(validateResult.Errors);
                _logger.LogWarning("AddAppointment validation errors: " + errors);
                throw new Exception("AddAppointment validation errors: " + errors);
            }

            var response = new AddAppointmentsResponse();
            var newAppointmen = request.model.Adapt<Appointment>();
            var newAppointmentcreated = _appointmentRepository.Insert(newAppointmen);
            response.Appointment = newAppointmentcreated.Adapt<AppointmentDto>();
            var queuemessage = JsonConvert.SerializeObject(response.Appointment);
            try
            {
                _messageService.Enqueue(queuemessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending appointment to queue: " + queuemessage);
            }
            return response;
        }
    }
}
