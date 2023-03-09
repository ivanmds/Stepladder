using App.Settings.Entrypoints.Routes.Rules;
using App.Settings.Entrypoints.Routes.Types;
using App.Validations;

namespace App.Settings.Entrypoints.Routes
{
    public class RouteSetting : IValidable
    {
        public string Route { get; set; }
        public MethodType Method { get; set; }
        public bool EnableAnonymous { get; set; }
        public string FlowActionId { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<RouteSetting>[]
            {
                new RouteSettingRule(),
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}
