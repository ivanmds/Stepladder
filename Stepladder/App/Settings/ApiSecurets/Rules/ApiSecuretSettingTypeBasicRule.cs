using App.Validations;

namespace App.Settings.ApiSecurets.ValidateRules
{
    public class ApiSecuretSettingTypeBasicRule : IRule<ApiSecuretSetting>
    {
        public ValidateResult Do(ApiSecuretSetting value)
        {
            var result = ValidateResult.Create();

            if (string.IsNullOrEmpty(value.User))
                result.AddError("ApiSecuret.User is required");

            if (string.IsNullOrEmpty(value.Password))
                result.AddError("ApiSecuret.Password is required");

            return result;
        }
    }
}
