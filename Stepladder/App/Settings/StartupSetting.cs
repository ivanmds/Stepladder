using App.Settings.ApiSecurets;
using App.Settings.HttpClients;
using App.Settings.ValidateRules;
using App.Validations;

namespace App.Settings
{
    public class StartupSetting : IValidable
    {
        public string ServiceName { get; set; }
        public string ServiceVersion { get; set; }
        public bool EnableTelemetry { get; set; }
        public string OtelEndpoint { get; set; }


        public List<HttpClientAuthentication> HttpClientAuthentication { get; set; }
        public ApiSecuretSetting ApiSecuret { get; set; }
        
        public ValidateResult Valid()
        {
            var rules = new IRule<StartupSetting>[]
            {
                new StartupSettingRule(),
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}