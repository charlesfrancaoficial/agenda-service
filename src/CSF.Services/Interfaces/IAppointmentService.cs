using CSF.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Services.Interfaces
{
    public interface IAppointmentService
    {
        GetAppointmentsResponse GetAppointments(GetAppointmentsRequest request);
        AddAppointmentsResponse AddAppointment(AddAppointmentsRequest request);
        GetAppointmentsCalendarResponse GetAppointmentsCalendar(GetAppointmentsCalendarRequest request);
    }
}
