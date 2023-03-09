using App.Delegates;
using App.Handlers.FlowActions;
using App.Settings;
using App.Settings.Entrypoints.Routes.Types;
using System.Text;

namespace App.Extensions
{
    public static class LoadEntrypointsExtensions
    {
        private static StringBuilder ENDPOINT_LOADED = new StringBuilder();

        public static void UseConfigRoutes(this WebApplication app)
        {
            var appConfig = ApplicationSetting.Current;

            if(appConfig.Entrypoints?.Routes?.Count() > 0)
            {
                foreach(var route in appConfig.Entrypoints.Routes)
                {

                    var info = $"STARTED ROUTE: {route.Route} METHOD: {route.Method}";
                    ENDPOINT_LOADED.AppendLine(info);

                    if (route.Method == MethodType.POST)
                    {
                        var httpPost = new HttpPostDelegate(route);
                        app.MapPost(route.Route, route.EnableAnonymous ? httpPost.Do_Anonymous : httpPost.Do_Authorize);
                        EntrypointHttpFlowActionsChainBuilder.Builder(route);
                    }
                }
            }

            app.MapGet("/", () => ENDPOINT_LOADED.ToString());
        }
    }
}
