using App.Validations;

namespace App.Settings.Entrypoints.Routes.Rules
{
    public class RouteSettingRule : IRule<RouteSetting>
    {
        private static string[] VALID_METHODS = new string[] { "GET", "POST", "PUT", "PATCH", "DELETE" };

        public ValidateResult Do(RouteSetting value)
        {
            var result = ValidateResult.Create();

            if (value.Method == Types.MethodType.NONE)
                result.AddError("Route.Method is required");


            if (string.IsNullOrEmpty(value.Route))
                result.AddError("Route.Route is required");

            return result;
        }
    }
}
