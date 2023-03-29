using App.Settings.MapVariables.Rules;
using App.Validations;

namespace App.Settings.MapVariables
{
    public class MapVariableSetting : IValidable
    {
        public string Name { get; set; }
        public ValueFromSetting ValueFrom { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<MapVariableSetting>[]
            {
                new MapVariableSettingRule()
            };

            var result = RuleExecute.Execute(this, rules);
            result.Concate(ValueFrom.Valid());

            return result;
        }
    }
}
