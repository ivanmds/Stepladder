﻿using App.Contexts;
using App.Helpers;
using App.Settings.Actions;

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
                var body = await context.GetCurrentBodyToRequestStringAsync();
                context.ResponseContext.HttpResponseMessage = await httpClient.PostAsJsonAsync(ActionSetting.Uri, body);
            }

            await NextAsync(context);
        }
    }
}
