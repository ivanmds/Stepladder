using App.Contexts;
using App.Settings.Actions;

namespace App.Handlers
{
    public class HttpClientPostRequestHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (ActionSetting != null)
            {
                var httpClientFactory = context.HttpContext.RequestServices.GetService<IHttpClientFactory>();
                using var httpClient = httpClientFactory.CreateClient(ActionSetting.Uri);

                var body = await context.GetCurrentBodyToRequestStringAsync();
                context.HttpResponseMessage = await httpClient.PostAsJsonAsync(ActionSetting.Uri, body);
            }

            await NextAsync(context);
        }
    }
}
