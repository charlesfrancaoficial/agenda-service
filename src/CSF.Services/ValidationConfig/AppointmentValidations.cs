using CSF.Entities;
using CSF.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.Services.ValidationConfig
{
    public class AppointmentValidator : AbstractValidator<AddAppointmentDto>
    {
        public AppointmentValidator()
        {
            RuleFor(user => user.Date).NotEmpty().WithMessage("Date is required.");
            RuleFor(user => user.Notes).NotEmpty().WithMessage("Notes are required.");
            RuleFor(user => user.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(user => user.ProfessionalUserId).NotEmpty().WithMessage("ProfessionalUserId is required.");
        }
    }
}
