using App.Contexts;
using App.Settings.Actions;

namespace App.Handlers
{
    public class HttpClientGetRequestHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (ActionSetting != null)
            {
                var httpClientFactory = context.HttpContext.RequestServices.GetService<IHttpClientFactory>();
                using var httpClient = httpClientFactory.CreateClient(ActionSetting.Uri);
                context.HttpResponseMessage = await httpClient.GetAsync(ActionSetting.Uri);
            }

            await NextAsync(context);
        }
    }
}
