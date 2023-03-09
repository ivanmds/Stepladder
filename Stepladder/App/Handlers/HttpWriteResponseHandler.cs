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
                var responseBodyString = await response.Content.ReadAsStringAsync();
                context.HttpContext.Response.StatusCode = (int)response.StatusCode;

                var contentyType = response.Content.Headers.ContentType;
                context.HttpContext.Response.Headers.Add("Content-Type", contentyType.ToString());

                await context.HttpContext.Response.WriteAsync(responseBodyString);
            }

            await NextAsync(context);
        }
    }
}
