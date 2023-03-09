using App.Contexts;
using App.Settings.Actions;

namespace App.Handlers
{
    public class HttpGetRequestHandler : Handler
    {
        private ActionSetting _actionSetting;

        public HttpGetRequestHandler(ActionSetting actionSetting) 
        {
            _actionSetting = actionSetting;
        }

        public override async Task DoAsync(StepladderHttpContext context)
        {
            var httpClientFactory = context.HttpContext.RequestServices.GetService<IHttpClientFactory>();
            using var httpClient = httpClientFactory.CreateClient(_actionSetting.Uri);

            context.HttpResponseMessage = await httpClient.GetAsync(_actionSetting.Uri);

            await NextAsync(context);
        }
    }
}
