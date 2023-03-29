using App.Settings.ApiSecurets;
using App.Settings.HttpClients;
using App.Settings.MapVariables;
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
        public string Prefix { get; set; }
        public bool AwsSecretEnable { get; set; }


        public List<HttpClientAuthentication> HttpClientAuthentication { get; set; }
        public List<MapVariableSetting>  MapVariables { get; set; }
        public ApiSecuretSetting ApiSecuret { get; set; }
        
        public ValidateResult Valid()
        {
            var rules = new IRule<StartupSetting>[]
            {
                new StartupSettingRule(),
            };

            var result = RuleExecute.Execute(this, rules);

            if(HttpClientAuthentication != null)
                foreach (var httpClientAuth in HttpClientAuthentication)
                    result.Concate(httpClientAuth.Valid());

            if(ApiSecuret != null)
                result.Concate(ApiSecuret.Valid());

            if(MapVariables != null)
                foreach (var mapVariable in MapVariables)
                    result.Concate(mapVariable.Valid());

            return result;
        }
    }
}