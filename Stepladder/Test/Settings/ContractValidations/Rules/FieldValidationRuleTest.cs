using App.Settings.ContractValidations;
using App.Settings.ContractValidations.Rules;
using App.Settings.ContractValidations.Types;

namespace Test.Settings.ContractValidations.Rules
{
    public class FieldValidationRuleTest
    {
        [Fact]
        public void WhenFieldValidationHasNullOrEmptyField_ShouldReturnError()
        {
            // arrange
            var setting = new FieldValidation();
            var rule = new FieldValidationRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contaings = result.Errors.Contains("FieldValidation.Field is required");
            Assert.True(contaings);
        }

        [Fact]
        public void WhenFieldValidationHasSpaceField_ShouldReturnError()
        {
            // arrange
            var setting = new FieldValidation() { Field = "customer. name" };
            var rule = new FieldValidationRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contaings = result.Errors.Contains("FieldValidation.Field not accept space");
            Assert.True(contaings);
        }


        [Fact]
        public void WhenFieldValidationNotInformedType_ShouldReturnError()
        {
            // arrange
            var setting = new FieldValidation() { Type = FieldValidationType.None };
            var rule = new FieldValidationRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contaings = result.Errors.Contains("FieldValidation.Type is required");
            Assert.True(contaings);
        }

        [Fact]
        public void WhenFieldValidationNotInformedSizeAndTypeIsBiggerThan_ShouldReturnError()
        {
            // arrange
            var setting = new FieldValidation() { Type = FieldValidationType.BiggerThan, Size = 0 };
            var rule = new FieldValidationRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contaings = result.Errors.Contains("FieldValidation.Size should be informed");
            Assert.True(contaings);
        }

        [Fact]
        public void WhenFieldValidationNotInformedSizeAndTypeIsLessThan_ShouldReturnError()
        {
            // arrange
            var setting = new FieldValidation() { Type = FieldValidationType.LessThan, Size = 0 };
            var rule = new FieldValidationRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contaings = result.Errors.Contains("FieldValidation.Size should be informed");
            Assert.True(contaings);
        }
    }
}
