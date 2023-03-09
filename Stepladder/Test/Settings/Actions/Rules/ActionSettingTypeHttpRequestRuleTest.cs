using App.Settings.Actions;
using App.Settings.Actions.Rules;
using App.Settings.Actions.Types;

namespace Test.Settings.Actions.Rules
{
    public class ActionSettingTypeHttpRequestRuleTest
    {
        [Fact]
        public void WhenActionSettingTypeHttpRequestHasEmptyOrNullUri_ShouldReturnError()
        {
            // arrange
            var setting = new ActionSetting { Type = ActionType.HttpRequest, Uri = "" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.Uri is required");
            Assert.True(constains);
        }

        [Fact]
        public void WhenActionSettingTypeHttpRequestHasInvalidUri_ShouldReturnError()
        {
            // arrange
            var setting = new ActionSetting { Type = ActionType.HttpRequest, Uri = "test@com" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.Uri should a valid uri");
            Assert.True(constains);
        }

        [Fact]
        public void WhenActionSettingTypeHttpRequestHasValidUri_ShouldReturnSuccess()
        {
            // arrange
            var setting = new ActionSetting { Type = ActionType.HttpRequest, Uri = "http://google.com" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.Uri should a valid uri");
            Assert.False(constains);
        }
    }
}
