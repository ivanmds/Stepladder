using App.Contexts;
using App.Settings.Actions;

namespace App.Handlers
{
    public class HttpClientGetRequestHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasNoError && ActionSetting != null)
            {
                var httpClientFactory = context.HttpContext.RequestServices.GetService<IHttpClientFactory>();
                using var httpClient = httpClientFactory.CreateClient(ActionSetting.Uri);
                var uri = BuildFinalHttpClientUri(context);
                context.ResponseContext.HttpResponseMessage = await httpClient.GetAsync(uri);
            }

            await NextAsync(context);
        }

        private string BuildFinalHttpClientUri(StepladderHttpContext context)
        {
            var httpClientUri = ActionSetting.Uri;
            if (ActionSetting.RouteMaps?.Count > 0)
            {
                foreach(var routeMap in ActionSetting.RouteMaps)
                {
                    var value = GetRouteValueFromHttpRequest(context, routeMap.RouteKey);
                    httpClientUri = httpClientUri.Replace(routeMap.RouteKey, value);
                }
            }

            return httpClientUri;
        }

        private string GetRouteValueFromHttpRequest(StepladderHttpContext context, string key)
        {
            key = key.Replace("{", "").Replace("}", "");

            context.HttpContext.Request.RouteValues.TryGetValue(key, out var value);
            return value.ToString();
        }
    }
}
