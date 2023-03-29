using App.Settings.MapVariables.Types;
using App.Validations;

namespace App.Settings.MapVariables.Rules
{
    public class ValueFromSettingRule : IRule<ValueFromSetting>
    {
        public ValidateResult Do(ValueFromSetting value)
        {
            var result = ValidateResult.Create();

            if (string.IsNullOrEmpty(value.Name))
                result.AddError("ValueFrom.Name is required");

            if (value.Type == ValueFromType.None)
                result.AddError("ValueFrom.Type is required");

            if (value.Type == ValueFromType.AwsSecret)
            {
                var appSetting = ApplicationSetting.Current;
                if (appSetting.Startup.AwsSecretEnable == false)
                    result.AddError("ValueFrom.Type AwsSecret need that Startup.AwsSecretEnable is enabled");
            }

            return result;
        }
    }
}
