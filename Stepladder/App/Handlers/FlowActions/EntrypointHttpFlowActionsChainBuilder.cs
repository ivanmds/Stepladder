using App.Settings.Actions.Types;
using App.Settings.Actions;
using App.Settings.Entrypoints.Routes;
using App.Settings.Entrypoints.Routes.Types;
using App.Settings;

namespace App.Handlers.FlowActions
{
    public static class EntrypointHttpFlowActionsChainBuilder
    {
        public static void Builder(RouteSetting routeSetting)
        {
            var appSetting = ApplicationSetting.Current;
            FlowActionsChain.StartFlowActions(routeSetting.FlowActionId);

            var flowAction = appSetting.FlowActions.FirstOrDefault(f => f.Id == routeSetting.FlowActionId);

            var actionsSetting = new List<ActionSetting>();

            foreach (var actionId in flowAction.ActionsId)
            {
                var action = appSetting.Actions.FirstOrDefault(a => a.Id == actionId);
                actionsSetting.Add(action);
            }

            foreach (var action in actionsSetting)
            {
                if (action.Type == ActionType.HttpRequest)
                {
                    if (action.Method == MethodType.GET)
                        FlowActionsChain.PutFlowAction(routeSetting.FlowActionId, typeof(HttpClientGetRequestHandler), action);
                    else if (action.Method == MethodType.POST)
                        FlowActionsChain.PutFlowAction(routeSetting.FlowActionId, typeof(HttpClientPostRequestHandler), action);

                    FlowActionsChain.PutFlowAction(routeSetting.FlowActionId, typeof(HttpResponseMessageParseHandler));
                }
            }

            FlowActionsChain.PutFlowAction(routeSetting.FlowActionId, typeof(HttpWriteResponseHandler));
        }
    }
}
