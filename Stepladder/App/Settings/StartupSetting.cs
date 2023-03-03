using App.Settings.ApiSecurets;
using App.Settings.HttpClients;
using App.Settings.ValidateRules;
using App.Validations;

namespace App.Settings
{
    public class StartupSetting : IValidable
    {
        public List<HttpClientAuthentication> HttpClientAuthentication { get; set; }
        public ApiSecuretSetting ApiSecuret { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<StartupSetting>[]
            {
                new StartupSettingValidateRule(),
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}