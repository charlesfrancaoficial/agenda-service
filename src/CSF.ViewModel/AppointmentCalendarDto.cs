namespace CSF.ViewModel
{
    public class AppointmentCalendarDto
    {
        public List<AppointmentCalendarDayDto> Days { get; set; }
    }

    public class AppointmentCalendarDayDto
    {
        public DateTime Date { get; set; }
        public List<AppointmentDto> Appointments { get; set; }
        public string FormattedDate
        {
            get
            {
                return Date.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
    }
}