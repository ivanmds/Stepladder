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
            }

            return result;
        }
    }
}
