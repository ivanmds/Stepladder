using App.Settings.Actions.Types;
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
                }
            }

            return result;
        }
    }
}
