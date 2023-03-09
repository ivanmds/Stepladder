using App.Contexts;
using App.Settings;
using App.Settings.Actions;
using App.Settings.Actions.Types;
using App.Settings.Entrypoints.Routes.Types;

namespace App.Handlers
{
    public static class ChainHandlerBuilder
    {
        public static Handler ChainBuilder(StepladderHttpContext context)
        {
            var handler = new HttpFirstHandler();

            var appSetting = ApplicationSetting.Current;
            var routeSetting = context.RouteSetting;
            
            var flowAction = appSetting.FlowActions.FirstOrDefault(f => f.Id == routeSetting.FlowActionId);

            var actionsSetting = new List<ActionSetting>();

            foreach (var actionId in flowAction.ActionsId)
            {
                var action = appSetting.Actions.FirstOrDefault(a => a.Id == actionId);
                actionsSetting.Add(action);
            }

            foreach(var action in actionsSetting)
            {
                if(action.Type == ActionType.HttpRequest)
                {
                    if(action.Method == MethodType.GET)
                    {
                        var httpGetHandler =  new HttpGetRequestHandler(action);
                        handler.SetNext(httpGetHandler);

                        
                        httpGetHandler.SetNext(handler);

                        var responseHandler = new HttpWriteResponseHandler();
                        httpGetHandler.SetNext(responseHandler);
                    }
                }
            }



            return handler; ;
        }
    }
}
