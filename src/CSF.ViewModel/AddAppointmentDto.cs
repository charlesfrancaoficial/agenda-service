namespace CSF.ViewModel
{
    public class AddAppointmentDto
    {
        public string UserId { get; set; }
        public string ProfessionalUserId { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
    }
}