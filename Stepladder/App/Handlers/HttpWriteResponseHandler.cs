using App.Contexts;

namespace App.Handlers
{
    public class HttpWriteResponseHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HttpResponseMessage != null)
            {
                var response = context.HttpResponseMessage;
                context.HttpContext.Response.StatusCode = (int)response.StatusCode;

                var contentyType = response.Content.Headers.ContentType;
                context.HttpContext.Response.Headers.Add("Content-Type", contentyType.ToString());

                await context.HttpContext.Response.WriteAsync(context.ResponseBodyStringValue);
            }

            await NextAsync(context);
        }
    }
}
