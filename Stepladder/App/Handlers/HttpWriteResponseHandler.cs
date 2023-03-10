using App.Contexts;

namespace App.Handlers
{
    public class HttpWriteResponseHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasNoError && context.ResponseContext.HttpResponseMessage != null)
            {
                var response = context.ResponseContext.HttpResponseMessage;
                context.HttpContext.Response.StatusCode = (int)response.StatusCode;

                var contentyType = response.Content.Headers.ContentType;
                context.HttpContext.Response.Headers.Add("Content-Type", contentyType.ToString());

                await context.HttpContext.Response.WriteAsync(context.ResponseContext.ResponseBodyStringValue);
            }

            if(context.HasNoError == false)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.HttpContext.Response.Headers.Add("Content-Type", "application/json");
                await context.HttpContext.Response.WriteAsync(context.ErrorString());
            }

            await NextAsync(context);
        }
    }
}
