using App.Contexts;
using App.Settings.Actions;
using App.Settings.ContractMap;

namespace App.Handlers
{
    public abstract class Handler
    {
        protected Handler _next;
        public ActionSetting ActionSetting { get; set; }
        public ContractMapSetting ContractMap { get; set; }

        public void SetNext(Handler next)
            => _next = next;

        public abstract Task DoAsync(StepladderHttpContext context);

        protected async Task NextAsync(StepladderHttpContext context)
        {
            if (_next != null)
                await _next.DoAsync(context);
        }
    }
}
