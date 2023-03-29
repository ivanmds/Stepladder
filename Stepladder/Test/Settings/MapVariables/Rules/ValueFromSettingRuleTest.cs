using App.Settings;
using App.Settings.MapVariables;
using App.Settings.MapVariables.Rules;
using App.Settings.MapVariables.Types;

namespace Test.Settings.MapVariables.Rules
{
    public class ValueFromSettingRuleTest
    {
        [Fact]
        public void WhenValueFromHasEmptyName_ShouldReturnError()
        {
            // arrange
            var setting = new ValueFromSetting();
            var rule = new ValueFromSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("ValueFrom.Name is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenValueFromHasTypeNone_ShouldReturnError()
        {
            // arrange
            var setting = new ValueFromSetting();
            var rule = new ValueFromSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("ValueFrom.Type is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenValueFromTypeIsAwsSecretAndStartupAwsSecretEnableIsFalse_ShouldReturnError()
        {
            // arrange
            ApplicationSetting.Current.Startup = new StartupSetting { AwsSecretEnable = false };
            var setting = new ValueFromSetting { Type = ValueFromType.AwsSecret };
            var rule = new ValueFromSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("ValueFrom.Type AwsSecret need that Startup.AwsSecretEnable is enabled");
            Assert.True(contains);
        }
    }
}
