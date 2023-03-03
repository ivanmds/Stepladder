using App.Settings;
using App.Settings.HttpClients;
using App.Settings.ValidateRules;

namespace Test.Settings.ValidateRules
{
    public class StartupSettingValidateRuleTest
    {
        [Fact]
        public void WhenStartupSettingHttpClientAuthenticationIdHasDuplicate_ShouldReturnError()
        {
            // arrange
            var startupSetting = new StartupSetting
            {
                HttpClientAuthentication = new List<HttpClientAuthentication>
                {
                    new HttpClientAuthentication { Id = "test_duplicate_id" },
                    new HttpClientAuthentication { Id = "test_duplicate_id" }
                }
            };
            
            var rule = new StartupSettingValidateRule();

            // act
            var result = rule.Do(startupSetting);

            // assert
            var contains = result.Errors.Contains("StartupSetting.HttpClientAuthentication.Id duplicate");
            Assert.True(contains);
        }

        [Fact]
        public void WhenStartupSettingHttpClientAuthenticationIdNotDuplicate_ShouldReturnSuccess()
        {
            // arrange
            var startupSetting = new StartupSetting
            {
                HttpClientAuthentication = new List<HttpClientAuthentication>
                {
                    new HttpClientAuthentication { Id = "test_duplicate_id1" },
                    new HttpClientAuthentication { Id = "test_duplicate_id2" }
                }
            };

            var rule = new StartupSettingValidateRule();

            // act
            var result = rule.Do(startupSetting);

            // assert
            var contains = result.Errors.Contains("StartupSetting.HttpClientAuthentication.Id duplicate");
            Assert.False(contains);
        }
    }
}
