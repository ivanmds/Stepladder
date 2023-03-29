using App.Settings.ApiSecurets;
using App.Settings.ApiSecurets.Types;
using App.Settings.ApiSecurets.ValidateRules;

namespace Test.Settings.ApiSecurets.ValidateRules
{
    public class ApiSecuretSettingTypeBasicRuleTest
    {
        [Fact]
        public void WhenApiSecuretTypeBasicHasEmptyOrNullUser_ShouldReturnError()
        {
            // arrange
            var apiSecuret = new ApiSecuretSetting();
            var rule = new ApiSecuretSettingTypeBasicRule();

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
            var rule = new ApiSecuretSettingTypeBasicRule();

            // act
            var result = rule.Do(apiSecuret);

            // assert
            var constains = result.Errors.Contains("ApiSecuret.Password is required");
            Assert.True(constains);
        }

        [Fact]
        public void WhenApiSecuretTypeBasicHasTypeNone_ShouldReturnError()
        {
            // arrange
            var apiSecuret = new ApiSecuretSetting { Type = ApiSecuretType.None };
            var rule = new ApiSecuretSettingTypeBasicRule();

            // act
            var result = rule.Do(apiSecuret);

            // assert
            var constains = result.Errors.Contains("ApiSecuret.Type is required");
            Assert.True(constains);
        }
    }
}
