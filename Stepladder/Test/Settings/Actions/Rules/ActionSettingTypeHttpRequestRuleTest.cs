using App.Settings;
using App.Settings.Actions;
using App.Settings.Actions.Rules;
using App.Settings.Actions.Types;
using App.Settings.ContractMap;
using App.Settings.ContractValidations;
using App.Settings.Entrypoints.Routes.Types;
using App.Settings.Strategies;

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


        [Fact]
        public void WhenActionSettingTypeHttpRequestHasEmptyMethod_ShouldReturnError()
        {
            // arrange
            var setting = new ActionSetting { Method = MethodType.NONE };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.Method is required");
            Assert.True(constains);
        }

        [Fact]
        public void WhenActionSettingTypeHttpRequestHasResponseContractMapIdNotConfigured_ShouldReturnError()
        {
            // arrange
            var setting = new ActionSetting { ReponseContractMapId = "not_configured" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.ReponseContractMapId not_configured should configured before use");
            Assert.True(constains);
        }

        [Fact]
        public void WhenActionSettingTypeHttpRequestHasResponseContractMapIdIsConfigured_ShouldReturnSuccess()
        {
            // arrange
            var appSetting = new ApplicationSetting { ContractMaps = new List<ContractMapSetting> { new ContractMapSetting { Id = "contract_configured" } } };
            var setting = new ActionSetting { ReponseContractMapId = "contract_configured" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.ReponseContractMapId contract_configured should configured before use");
            Assert.False(constains);
        }

        [Fact]
        public void WhenActionSettingTypeHttpRequestHasRequestContractValidationIdNotConfigured_ShouldReturnError()
        {
            // arrange
            var setting = new ActionSetting { RequestContractValidationId = "not_configured" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.RequestContractValidationId not_configured should configured before use");
            Assert.True(constains);
        }

        [Fact]
        public void WhenActionSettingTypeHttpRequestHasRequestHasRequestContractValidationIdIsConfigured_ShouldReturnSuccess()
        {
            // arrange
            var appSetting = new ApplicationSetting { ContractValidations = new List<ContractValidation> { new ContractValidation { Id = "contract_configured" } } };
            var setting = new ActionSetting { RequestContractValidationId = "contract_configured" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.RequestContractValidationId contract_configured should configured before use");
            Assert.False(constains);
        }

        [Theory]
        [InlineData(MethodType.GET)]
        [InlineData(MethodType.DELETE)]
        public void WhenActionSettingTypeHttpRequestHasRequestContractValidationIdCofiguredInvalidMethod_ShouldReturnError(MethodType methodType)
        {
            // arrange
            var setting = new ActionSetting { RequestContractValidationId = "configured", Method = methodType };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.RequestContractValidationId only used in post, put and patch methods");
            Assert.True(constains);
        }


        [Fact]
        public void WhenActionSettingTypeHttpRequestHasRequestStrategyCacheIdNotConfigured_ShouldReturnError()
        {
            // arrange
            var setting = new ActionSetting { StrategyCacheId = "not_configured" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.StrategyCacheId not_configured should configured before use");
            Assert.True(constains);
        }

        [Fact]
        public void WhenActionSettingTypeHttpRequestHasRequestHasRequestStrategyCacheIdIsConfigured_ShouldReturnSuccess()
        {
            // arrange
            var appSetting = new ApplicationSetting { Strategies = new StrategiesSetting { Caches = new List<CacheSetting> { new CacheSetting { Id = "cache_configured" } } } };
            var setting = new ActionSetting { StrategyCacheId = "cache_configured" };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.StrategyCacheId cache_configured should configured before use");
            Assert.False(constains);
        }

        [Theory]
        [InlineData(MethodType.POST)]
        [InlineData(MethodType.PATCH)]
        [InlineData(MethodType.PUT)]
        [InlineData(MethodType.DELETE)]
        public void WhenActionSettingTypeHttpRequestGetAndHasStrategyCacheIdConfigured_ShouldReturnError(MethodType methodType)
        {
            // arrange
            var setting = new ActionSetting { StrategyCacheId = "configured", Method = methodType };
            var rule = new ActionSettingTypeHttpRequestRule();

            // act
            var result = rule.Do(setting);

            // assert
            var constains = result.Errors.Contains("ActionSetting.StrategyCacheId only used in get methods");
            Assert.True(constains);
        }
    }
}
