using App.Settings.MapVariables.Rules;
using App.Settings.MapVariables.Types;
using App.Validations;

namespace App.Settings.MapVariables
{
    public class ValueFromSetting : IValidable
    {
        public string Name { get; set; }
        public ValueFromType Type { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<ValueFromSetting>[]
            {
                new ValueFromSettingRule()
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}
