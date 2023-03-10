using App.Settings;
using App.Settings.Actions;
using App.Settings.Actions.Rules;

namespace Test.Settings.Actions.Rules
{
    public class FlowActionsSettingRuleTest
    {
        [Fact]
        public void WhenFlowActionsSettingHasEmptyOrNullId_ShouldReturnError()
        {
            // arrange
            var setting = new FlowActionsSetting { Id = null };
            var rule = new FlowActionsSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("FlowActionsSetting.Id is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenFlowActionsSettingHasActionsIdNotConfigured_ShouldReturnError()
        {
            // arrange
            var appSetting = new ApplicationSetting { Actions = new List<ActionSetting>() };
            appSetting.Actions.Add(new ActionSetting { Id = "request_goggle" });

            var setting = new FlowActionsSetting { ActionsId = new List<string> { "request_goggle", "not_configured" } };
            var rule = new FlowActionsSettingRule();
            
            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("FlowActionsSetting.ActionsId not_configured should be configured");
            Assert.True(contains);
        }

        [Fact]
        public void WhenFlowActionsSettingHasActionsIdConfigured_ShouldReturnSuccess()
        {
            // arrange
            var appSetting = new ApplicationSetting { Actions = new List<ActionSetting>() };
            appSetting.Actions.Add(new ActionSetting { Id = "request_goggle" });

            var setting = new FlowActionsSetting { ActionsId = new List<string> { "request_goggle" } };
            var rule = new FlowActionsSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("FlowActionsSetting.ActionsId request_goggle should be configured");
            Assert.False(contains);
        }
    }
}
