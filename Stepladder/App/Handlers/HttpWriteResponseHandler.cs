using App.Contexts;

namespace App.Handlers
{
    public class HttpWriteResponseHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            context.HttpContext.Response.Headers.Add("Content-Type", context.ResponseContext.ResponseContentType);

            if (context.HasNoErrorValidation && context.ResponseContext.ResponseBodyStringValue != null)
            {
                context.HttpContext.Response.StatusCode = context.ResponseContext.ResponseStatusCode;
                await context.HttpContext.Response.WriteAsync(context.ResponseContext.ResponseBodyStringValue);
            }
            else if (context.HasNoErrorValidation == false)
            {
                context.HttpContext.Response.StatusCode = 400;
                await context.HttpContext.Response.WriteAsync(context.ResponseContext.ResponseBodyStringValue);
            }

            await NextAsync(context);
        }
    }
}
