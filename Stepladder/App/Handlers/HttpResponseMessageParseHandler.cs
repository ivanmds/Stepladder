using App.Contexts;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace App.Handlers
{
    public class HttpResponseMessageParseHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasNoError && context.ResponseContext.HttpResponseMessage != null)
            {
                var response = context.ResponseContext.HttpResponseMessage;
                context.ResponseContext.ResponseBodyStringValue = await response.Content.ReadAsStringAsync();
                var contentyType = response.Content.Headers.ContentType;
                context.ResponseContext.ResponseContentType = contentyType.MediaType?.ToLower();

                if (context.ResponseContext.ResponseContentType == "application/json")
                {
                    if (response.IsSuccessStatusCode)
                    {
                        if (!string.IsNullOrEmpty(context.ResponseContext.ResponseBodyStringValue))
                            context.ResponseContext.JsonResponseBody = JsonSerializer.Deserialize<JsonObject>(context.ResponseContext.ResponseBodyStringValue);
                    }
                }
            }

            await NextAsync(context);
        }
    }
}
