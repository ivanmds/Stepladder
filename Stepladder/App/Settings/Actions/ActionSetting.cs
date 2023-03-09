using App.Settings.Actions.Rules;
using App.Settings.Actions.Types;
using App.Settings.Entrypoints.Routes.Types;
using App.Validations;

namespace App.Settings.Actions
{
    public class ActionSetting : IValidable
    {
        public string Id { get; set; }
        public ActionType Type { get; set; }
        public MethodType Method { get; set; }
        public string Uri { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<ActionSetting>[]
            {
                new ActionSettingTypeHttpRequestRule()
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}
