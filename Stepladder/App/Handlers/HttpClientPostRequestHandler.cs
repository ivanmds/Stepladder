using App.Contexts;
using App.Helpers;
using App.Settings.Actions;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace App.Handlers
{
    public class HttpClientPostRequestHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasNoError && ActionSetting != null)
            {
                var httpClientFactory = context.HttpContext.RequestServices.GetService<IHttpClientFactory>();
                using var httpClient = httpClientFactory.CreateClient(ActionSetting.Uri);
                HttpClientHelper.MapHeaderValue(context, httpClient, ActionSetting);
                var jsonBody = await context.GetCurrentBodyToRequestStringAsync();
                JsonObject jsonObject = null;
                if(string.IsNullOrEmpty(jsonBody) == false)
                    jsonObject = JsonSerializer.Deserialize<JsonObject>(jsonBody);

                context.ResponseContext.HttpResponseMessage = await httpClient.PostAsJsonAsync(ActionSetting.Uri, jsonObject);
            }

            await NextAsync(context);
        }
    }
}
