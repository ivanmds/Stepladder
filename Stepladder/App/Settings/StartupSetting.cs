using App.Settings.HttpClients;
using App.Settings.ValidateRules;
using App.Settings.Validations;

namespace App.Settings
{
    public class StartupSetting : IValidable
    {
        public List<HttpClientAuthentication> HttpClientAuthentication { get; set; }

        public ValidationResult Valid()
        {
            var validations = new IValidateSetting<StartupSetting>[]
            {
                new StartupSettingValidateRule(),
            };

            var finalResult = ValidationResult.Create();
            foreach (var validation in validations)
            {
                var result = validation.Validate(this);
                finalResult.Concate(result);
            }

            return finalResult;
        }
    }
}