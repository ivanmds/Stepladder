using App.Settings.Actions.Types;
using App.Settings.Actions;
using App.Settings.Entrypoints.Routes;
using App.Settings.Entrypoints.Routes.Types;
using App.Settings;

namespace App.Handlers.Http.FlowActions
{
    public static class EntrypointHttpFlowActionsChainBuilder
    {
        public static void Builder(RouteSetting routeSetting)
        {
            var appSetting = ApplicationSetting.Current;

            var flowActionId = routeSetting.GetFlowActionId;

            FlowActionsChain.StartFlowActions(flowActionId);

            var flowAction = appSetting.FlowActions?.FirstOrDefault(f => f.Id == flowActionId);

            if (flowAction != null)
            {
                var actionsSetting = new List<ActionSetting>();

                foreach (var actionId in flowAction.ActionsId)
                {
                    var action = appSetting.Actions.FirstOrDefault(a => a.Id == actionId);
                    actionsSetting.Add(action);
                }

                foreach (var action in actionsSetting)
                {
                    if (string.IsNullOrEmpty(action.RequestContractValidationId) == false)
                    {
                        var contractValidation = appSetting.ContractValidations.FirstOrDefault(a => a.Id == action.RequestContractValidationId);
                        FlowActionsChain.PutFlowAction(flowActionId, typeof(HttpRequestContractValidationHandler), contractValidation: contractValidation);
                    }

                    if (string.IsNullOrEmpty(action.StrategieCacheId) == false)
                    {
                        var cacheSetting = appSetting.Strategies.Caches.FirstOrDefault(a => a.Id == action.StrategieCacheId);
                        FlowActionsChain.PutFlowAction(flowActionId, typeof(HttpRequestStrategieCacheHandler), actionSetting: action, cacheSetting: cacheSetting);
                    }

                    if (action.Type == ActionType.HttpRequest)
                    {
                        if (action.Method == MethodType.GET)
                            FlowActionsChain.PutFlowAction(flowActionId, typeof(HttpClientGetRequestHandler), actionSetting: action);
                        else if (action.Method == MethodType.POST)
                            FlowActionsChain.PutFlowAction(flowActionId, typeof(HttpClientPostRequestHandler), actionSetting: action);


                        if (action.ReponseContractMapId != null)
                        {
                            var contractMap = appSetting.ContractMaps.FirstOrDefault(a => a.Id == action.ReponseContractMapId);
                            FlowActionsChain.PutFlowAction(flowActionId, typeof(HttpResponseContractMapHandler), contractMap: contractMap);
                        }
                    }
                }

                if (routeSetting.ResponseMock == null)
                    FlowActionsChain.PutFlowAction(flowActionId, typeof(HttpWriteResponseHandler));
            }

            if (routeSetting.ResponseMock != null)
                FlowActionsChain.PutFlowAction(flowActionId, typeof(HttpWriteResponseMockHandler));
        }
    }
}
