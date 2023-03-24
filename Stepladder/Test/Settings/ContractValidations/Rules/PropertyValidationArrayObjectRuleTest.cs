using App.Settings.ContractValidations;
using App.Settings.ContractValidations.Rules;

namespace Test.Settings.ContractValidations.Rules
{
    public class PropertyValidationArrayObjectRuleTest
    {
        [Fact]
        public void WhenPropertyValidationArrayObjectHasEmptyOrNullArrayPropertyName_ShouldReturnError()
        {
            // arrange
            var setting = new PropertyValidationArrayObject();
            var rule = new PropertyValidationArrayObjectRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("PropertyValidationArrayObject.ArrayPropertyName is required");
            Assert.True(contains);  
        }

        [Fact]
        public void WhenPropertyValidationArrayNotAcceptEmptyValueInArrayPropertyName_ShouldReturnError()
        {
            // arrange
            var setting = new PropertyValidationArrayObject { ArrayPropertyName = "customer .addresses" };
            var rule = new PropertyValidationArrayObjectRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("PropertyValidationArrayObject.ArrayPropertyName not accept empty value");
            Assert.True(contains);
        }


        [Fact]
        public void WhenPropertyValidationArrayHasNullProperties_ShouldReturnError()
        {
            // arrange
            var setting = new PropertyValidationArrayObject { Properties = null };
            var rule = new PropertyValidationArrayObjectRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("PropertyValidationArrayObject.Properties is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenPropertyValidationArrayHasEmptyProperties_ShouldReturnError()
        {
            // arrange
            var setting = new PropertyValidationArrayObject { Properties = new List<PropertyValidation>() };
            var rule = new PropertyValidationArrayObjectRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("PropertyValidationArrayObject.Properties is required");
            Assert.True(contains);
        }
    }
}
