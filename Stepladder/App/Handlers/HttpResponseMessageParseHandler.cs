using App.Contexts;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace App.Handlers
{
    public class HttpResponseMessageParseHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HttpResponseMessage != null)
            {
                var response = context.HttpResponseMessage;
                context.ResponseBodyStringValue = await response.Content.ReadAsStringAsync();
                var contentyType = response.Content.Headers.ContentType;
                context.ResponseContentType = contentyType.MediaType?.ToLower();

                if (context.ResponseContentType == "application/json")
                {
                    if (response.IsSuccessStatusCode)
                    {
                        if (!string.IsNullOrEmpty(context.ResponseBodyStringValue))
                            context.JsonResponseBody = JsonSerializer.Deserialize<JsonObject>(context.ResponseBodyStringValue);
                    }
                }
            }

            await NextAsync(context);
        }
    }
}
