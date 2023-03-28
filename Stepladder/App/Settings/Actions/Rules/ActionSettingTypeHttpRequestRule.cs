﻿using App.Settings.Actions.Types;
using App.Settings.Entrypoints.Routes.Types;
using App.Validations;

namespace App.Settings.Actions.Rules
{
    public class ActionSettingTypeHttpRequestRule : IRule<ActionSetting>
    {
        public ValidateResult Do(ActionSetting value)
        {
            var result = ValidateResult.Create();

            if (value.Type == ActionType.HttpRequest)
            {
                if (string.IsNullOrEmpty(value.Uri))
                    result.AddError("ActionSetting.Uri is required");
                else
                {
                    Uri uri;
                    Uri.TryCreate(value.Uri, UriKind.Absolute, out uri);
                    if (uri is null)
                        result.AddError("ActionSetting.Uri should a valid uri");
                }

                if (value.Method == MethodType.NONE)
                    result.AddError("ActionSetting.Method is required");

                if(value.ReponseContractMapId != null)
                {
                    var appSetting = ApplicationSetting.Current;
                    var hasContractMap = appSetting?.ContractMaps?.Any(c => c.Id == value.ReponseContractMapId) ?? false;
                    if(hasContractMap == false)
                        result.AddError($"ActionSetting.ReponseContractMapId {value.ReponseContractMapId} should configured before use");
                }


                if (value.RequestContractValidationId != null)
                {
                    var appSetting = ApplicationSetting.Current;
                    var hasContractMap = appSetting?.ContractValidations?.Any(c => c.Id == value.RequestContractValidationId) ?? false;
                    if (hasContractMap == false)
                        result.AddError($"ActionSetting.RequestContractValidationId {value.RequestContractValidationId} should configured before use");

                    if (value.Method == MethodType.GET ||
                        value.Method == MethodType.DELETE)
                        result.AddError("ActionSetting.RequestContractValidationId only used in post, put and patch methods");
                }

                if(value.StrategyCacheId != null)
                {
                    var appSetting = ApplicationSetting.Current;
                    var hasStrategyCacheId = appSetting?.Strategies.Caches.Any(c => c.Id == value.StrategyCacheId) ?? false;
                    if (hasStrategyCacheId == false)
                        result.AddError($"ActionSetting.StrategyCacheId {value.StrategyCacheId} should configured before use");

                    if(value.Method != MethodType.GET)
                        result.AddError("ActionSetting.StrategyCacheId only used in get methods");
                }

                if (value.StrategyHttpIdempotencyId != null)
                {
                    var appSetting = ApplicationSetting.Current;
                    var hasStrategyCacheId = appSetting?.Strategies.HttpIdempotencies.Any(c => c.Id == value.StrategyHttpIdempotencyId) ?? false;
                    if (hasStrategyCacheId == false)
                        result.AddError($"ActionSetting.StrategyHttpIdempotencyId {value.StrategyHttpIdempotencyId} should configured before use");

                    if (value.Method == MethodType.GET)
                       result.AddError("ActionSetting.StrategyHttpIdempotencyId only used in post, put, patch and delete methods");
                }
            }

            return result;
        }
    }
}
