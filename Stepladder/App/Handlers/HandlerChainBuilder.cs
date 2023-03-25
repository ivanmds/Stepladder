using App.Contexts;
using App.Handlers.FlowActions;

namespace App.Handlers
{
    public static class HandlerChainBuilder
    {
        public static Handler ChainBuilder(StepladderHttpContext context)
        {
            var routeSetting = context.RouteSetting;

            var flowActions = FlowActionsChain.GetFlowActionsChain(routeSetting.GetFlowActionId);

            Handler firstHandler = new HttpFirstHandler();
            var current = firstHandler;

            foreach (var flowAction in flowActions)
            {
                var handler = context.HttpContext.RequestServices.GetService(flowAction.HandleType) as Handler;
                
                if(flowAction.ActionSetting != null)
                    handler.ActionSetting = flowAction.ActionSetting;

                if (flowAction.ContractValidation != null)
                    handler.ContractValidation = flowAction.ContractValidation;

                if (flowAction.ContractMap != null)
                    handler.ContractMap = flowAction.ContractMap;

                if (flowAction.CacheSetting != null)
                    handler.CacheSetting = flowAction.CacheSetting;

                current.SetNext(handler);
                current = handler;
            }

            return firstHandler;
        }
    }
}
