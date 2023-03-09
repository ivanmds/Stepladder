using App.Contexts;

namespace App.Handlers
{
    public static class ChainHandlerBuilder
    {
        public static Handler ChainBuilder(StepladderHttpContext context)
        {
            var handler = new HttpFirstHandler();



            return handler; ;
        }
    }
}
