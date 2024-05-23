using CSF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Services.Messages
{
    public class GetAppointmentsResponse
    {
        public List<AppointmentDto> Appointments { get; set; }
    }
}
