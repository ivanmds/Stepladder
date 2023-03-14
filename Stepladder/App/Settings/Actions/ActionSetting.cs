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
        public string ReponseContractMapId { get; set; }
        public List<ActionRouteMap> RouteMaps { get; set; } = new List<ActionRouteMap>();
        public List<ActionHeaderMap> HeaderMaps { get; set; } = new List<ActionHeaderMap>();

        public ValidateResult Valid()
        {
            var rules = new IRule<ActionSetting>[]
            {
                new ActionSettingTypeHttpRequestRule()
            };

            var results = RuleExecute.Execute(this, rules);
            foreach (var route in RouteMaps) 
                results.Concate(route.Valid());

            foreach (var header in HeaderMaps)
                results.Concate(header.Valid());

            return results;
        }
    }
}
