using App.Settings;
using App.Settings.HttpClients;
using App.Settings.ValidateRules;

namespace Test.Settings.ValidateRules
{
    public class StartupSettingRuleTest
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
            
            var rule = new StartupSettingRule();

            // act
            var result = rule.Do(startupSetting);

            // assert
            var contains = result.Errors.Contains("Startup.HttpClientAuthentication.Id duplicate");
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

            var rule = new StartupSettingRule();

            // act
            var result = rule.Do(startupSetting);

            // assert
            var contains = result.Errors.Contains("Startup.HttpClientAuthentication.Id duplicate");
            Assert.False(contains);
        }

        [Fact]
        public void WhenStartupServiceNameIsEmpty_ShouldReturnError()
        {
            // arrange 
            var startupSetting = new StartupSetting();
            var rule = new StartupSettingRule();

            // act
            var result = rule.Do(startupSetting);

            // assert
            var contains = result.Errors.Contains("Startup.ServiceName is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenStartupServiceVersionIsEmpty_ShouldReturnError()
        {
            // arrange 
            var startupSetting = new StartupSetting();
            var rule = new StartupSettingRule();

            // act
            var result = rule.Do(startupSetting);

            // assert
            var contains = result.Errors.Contains("Startup.ServiceVersion is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenStartupPrefixIsEmptyAndAwsSecretEnableIsTrue_ShouldReturnError()
        {
            // arrange 
            var startupSetting = new StartupSetting { AwsSecretEnable = true };
            var rule = new StartupSettingRule();

            // act
            var result = rule.Do(startupSetting);

            // assert
            var contains = result.Errors.Contains("Startup.Prefix is required");
            Assert.True(contains);
        }
    }
}
