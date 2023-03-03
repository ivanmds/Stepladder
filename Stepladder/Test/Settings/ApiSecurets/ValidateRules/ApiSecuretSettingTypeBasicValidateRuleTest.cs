using App.Settings.ApiSecurets;
using App.Settings.ApiSecurets.ValidateRules;

namespace Test.Settings.ApiSecurets.ValidateRules
{
    public class ApiSecuretSettingTypeBasicValidateRuleTest
    {
        [Fact]
        public void WhenApiSecuretTypeBasicHasEmptyOrNullUser_ShouldReturnError()
        {
            // arrange
            var apiSecuret = new ApiSecuretSetting();
            var rule = new ApiSecuretSettingTypeBasicValidateRule();

            // act
            var result = rule.Do(apiSecuret);

            // assert
            var constains = result.Errors.Contains("ApiSecuret.User is required");
            Assert.True(constains);
        }

        [Fact]
        public void WhenApiSecuretTypeBasicHasEmptyOrNullPassword_ShouldReturnError()
        {
            // arrange
            var apiSecuret = new ApiSecuretSetting();
            var rule = new ApiSecuretSettingTypeBasicValidateRule();

            // act
            var result = rule.Do(apiSecuret);

            // assert
            var constains = result.Errors.Contains("ApiSecuret.Password is required");
            Assert.True(constains);
        }
    }
}
