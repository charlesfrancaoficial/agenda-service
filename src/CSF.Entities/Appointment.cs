using CSF.Domain.Data;

namespace CSF.Entities
{
    public class Appointment: BaseModel<Guid>
    {
        public string UserId { get; set; }
        public string ProfessionalUserId { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }

        public override bool Validate()
        {
            return true;
        }
    }
}