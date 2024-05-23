namespace CSF.ViewModel
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ProfessionalUserId { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}