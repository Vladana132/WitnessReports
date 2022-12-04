using FluentValidation;
using WitnessReports.Common.DTO;

namespace WitnessReports.Common.Validators
{
    public  class WitnessReportValidator  : AbstractValidator<WitnessReportDto>
    {
        public WitnessReportValidator()
        {
            RuleFor(x => x.WitnessName).NotEmpty().NotNull().WithMessage("Witness is required!");

            RuleFor(x => x.FugitiveName).NotEmpty().NotNull().WithMessage("Fugitive is required!");

            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("Phone number is required!")
                .Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Phone number must be in E.164 international format!");
        }
    }
}
