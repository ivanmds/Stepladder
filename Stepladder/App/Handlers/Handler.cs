using App.Contexts;

namespace App.Handlers
{
    public abstract class Handler
    {
        protected Handler _next;
        public void SetNext(Handler next)
            => _next = next;

        public abstract Task DoAsync(StepladderHttpContext context);
    }
}
