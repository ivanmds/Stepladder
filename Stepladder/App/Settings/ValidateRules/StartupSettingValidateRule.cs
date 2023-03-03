using App.Settings.Validations;

namespace App.Settings.ValidateRules
{
    public class StartupSettingValidateRule : IValidateSetting<StartupSetting>
    {
        public ValidationResult Validate(StartupSetting value)
        {
            var result = ValidationResult.Create();

            var httpClientAuthenticationDuplicateIds = value.HttpClientAuthentication?.GroupBy(p => p.Id)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            if (httpClientAuthenticationDuplicateIds?.Count > 0)
                result.AddError("StartupSetting.HttpClientAuthentication.Id duplicate");

            return result;
        }
    }
}
