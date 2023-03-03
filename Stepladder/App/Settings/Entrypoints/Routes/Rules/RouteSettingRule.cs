using App.Validations;

namespace App.Settings.Entrypoints.Routes.Rules
{
    public class RouteSettingRule : IRule<RouteSetting>
    {
        private static string[] VALID_METHODS = new string[] { "GET", "POST", "PUT", "PATCH", "DELETE" };

        public ValidateResult Do(RouteSetting value)
        {
            var result = ValidateResult.Create();

            if (string.IsNullOrEmpty(value.Method))
                result.AddError("Route.Method is required");
            else
            {
                var methodSetting = value.Method.ToUpper();
                if (!VALID_METHODS.Contains(methodSetting))
                    result.AddError("Route.Method is invalid");
            }


            if (string.IsNullOrEmpty(value.Route))
                result.AddError("Route.Route is required");
            else
            {
                Uri uri;
                Uri.TryCreate(value.Route, UriKind.Relative, out uri);
                if (uri is null)
                    result.AddError("Route.Route is invalid");
            }


            return result;
        }
    }
}
