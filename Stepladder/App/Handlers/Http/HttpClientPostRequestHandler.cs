using App.Contexts;
using App.Helpers;
using App.Settings.Actions;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace App.Handlers.Http
{
    public class HttpClientPostRequestHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasCache == false && context.HasNoErrorProcessor && ActionSetting != null)
            {
                var httpClientFactory = context.HttpContext.RequestServices.GetService<IHttpClientFactory>();
                using var httpClient = httpClientFactory.CreateClient(ActionSetting.Uri);
                HttpClientHelper.MapHeaderValue(context, httpClient, ActionSetting);
                var jsonBody = await context.GetCurrentBodyToRequestStringAsync();
                JsonObject jsonObject = null;
                if (string.IsNullOrEmpty(jsonBody) == false)
                    jsonObject = JsonSerializer.Deserialize<JsonObject>(jsonBody);

                var httpResponseMessage = await httpClient.PostAsJsonAsync(ActionSetting.Uri, jsonObject);
                await LoadHttpResponseMessageAsync(context, httpResponseMessage);
            }

            await NextAsync(context);
        }
    }
}
