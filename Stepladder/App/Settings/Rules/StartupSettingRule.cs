using App.Validations;

namespace App.Settings.ValidateRules
{
    public class StartupSettingRule : IRule<StartupSetting>
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

            if(value.EnableTelemetry)
            {
                if(string.IsNullOrEmpty(value.ServiceName))
                    result.AddError("StartupSetting.ServiceName is required");

                if (string.IsNullOrEmpty(value.ServiceVersion))
                    result.AddError("StartupSetting.ServiceVersion is required");

                if (string.IsNullOrEmpty(value.OtelEndpoint))
                    result.AddError("StartupSetting.OtelEndpoint is required");
            }

            return result;
        }
    }
}
