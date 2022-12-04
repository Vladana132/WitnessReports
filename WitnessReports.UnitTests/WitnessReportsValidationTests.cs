using WitnessReports.Common.Validators;
using Xunit;
using FluentValidation.TestHelper;

namespace WitnessReports.UnitTests
{
    public class WitnessReportsValidationTests
    {
        private readonly WitnessReportValidator _validator = new WitnessReportValidator();

        [Theory]
        [InlineData("Witness No1", "ROBERT WILLIAM FISHER", "+38165222333")]
        public void GivenCorrectValues_ShouldNotHaveValidationError(string witness, string fugitive, string phoneNumber)
        {
            _validator.ShouldNotHaveValidationErrorFor(model => model.WitnessName, witness);
            _validator.ShouldNotHaveValidationErrorFor(model => model.FugitiveName, fugitive);
            _validator.ShouldNotHaveValidationErrorFor(model => model.PhoneNumber, phoneNumber);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(null, null, null)]
        public void GivenEmptyOrNullValues_ShouldHaveValidationError(string witness, string fugitive, string phoneNumber)
        {
            _validator.ShouldHaveValidationErrorFor(model => model.WitnessName, witness);
            _validator.ShouldHaveValidationErrorFor(model => model.FugitiveName, fugitive);
            _validator.ShouldHaveValidationErrorFor(model => model.PhoneNumber, phoneNumber);
        }

        [Theory]
        [InlineData("9999rs")]
        [InlineData("065/222-333")]
        public void GivenNotValidPhoneNumber_ShouldHaveValidationError(string phoneNumber)
        {
            _validator.ShouldHaveValidationErrorFor(model => model.PhoneNumber, phoneNumber);
        }
    }
}
