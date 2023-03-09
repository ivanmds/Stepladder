using App.Settings.Actions.Types;
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
            }

            return result;
        }
    }
}
