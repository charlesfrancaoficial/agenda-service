using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Services.Messages
{
    public class GetAppointmentsCalendarRequest
    {
        public string UserId { get; set; }
        public string ProfessionalUserId { get; set; }
        public int CalendarMonth { get; set; }
    }
}
