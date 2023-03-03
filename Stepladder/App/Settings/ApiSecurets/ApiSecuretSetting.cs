using App.Settings.ApiSecurets.Types;
using App.Settings.ApiSecurets.ValidateRules;
using App.Settings.Types;
using App.Validations;

namespace App.Settings.ApiSecurets
{
    public class ApiSecuretSetting : IValidable
    {
        public ApiSecuretType Type { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public ValueFromType ValueFrom { get; set; }


        public ValidateResult Valid()
        {
            var rules = new IRule<ApiSecuretSetting>[]
            {
                new ApiSecuretSettingTypeBasicValidateRule(),
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}
