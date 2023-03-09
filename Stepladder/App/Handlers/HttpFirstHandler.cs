using App.Contexts;

namespace App.Handlers
{
    public class HttpFirstHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            context.HttpContext.Response.StatusCode = 200;
            await context.HttpContext.Response.WriteAsJsonAsync("{ 'msg': 'hello' }");

            if(_next != null)
                await _next.DoAsync(context);
        }
    }
}
