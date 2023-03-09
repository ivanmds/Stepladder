using App.Validations;

namespace App.Settings.Entrypoints.Routes.Rules
{
    public class RouteSettingRule : IRule<RouteSetting>
    {
        public ValidateResult Do(RouteSetting value)
        {
            var result = ValidateResult.Create();

            if (value.Method == Types.MethodType.NONE)
                result.AddError("Route.Method is required");


            if (string.IsNullOrEmpty(value.Route))
                result.AddError("Route.Route is required");

            if (string.IsNullOrEmpty(value.FlowActionId))
                result.AddError("Route.FlowActionId is required");
            else
            {
                var appSetting = ApplicationSetting.Current;
                var hasFlowActionId =  appSetting.FlowActions?.FirstOrDefault(f => f.Id == value.FlowActionId) != null;

                if(hasFlowActionId is false)
                    result.AddError($"Route.FlowActionId {value.FlowActionId} should be configured");
            }


            return result;
        }
    }
}
