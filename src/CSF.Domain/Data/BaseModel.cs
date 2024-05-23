using System.ComponentModel.DataAnnotations.Schema;

namespace CSF.Domain.Data
{
    public abstract class BaseModel<TId> : IBaseModel
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [NotMapped]
        private List<BusinessRule> brokedRules { get; set; }

        public BaseModel()
        {
            this.brokedRules = new List<BusinessRule>();
        }

        public bool IsValid()
        {
            this.Validate();
            return this.brokedRules.Count == 0;
        }

        public List<BusinessRule> GetBrokedRules()
        {
            return this.brokedRules;
        }

        public void AddBrokedRules(BusinessRule brokedRule)
        {
            this.brokedRules.Add(brokedRule);
        }

        public abstract bool Validate();

    }
}