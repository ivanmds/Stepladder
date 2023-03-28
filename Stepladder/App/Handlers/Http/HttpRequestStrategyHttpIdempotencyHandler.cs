using App.Contexts;

namespace App.Handlers.Http
{
    public class HttpRequestStrategyHttpIdempotencyHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
           
            await NextAsync(context);
        }
    }
}
