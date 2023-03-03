using App.Validations;

namespace App.Settings.ValidateRules
{
    public class StartupSettingValidateRule : IRule<StartupSetting>
    {
        public ValidateResult Do(StartupSetting value)
        {
            var result = ValidateResult.Create();

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
