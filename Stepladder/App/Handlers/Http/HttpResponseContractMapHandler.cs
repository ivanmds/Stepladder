using App.Contexts;
using App.JsonHelpers;
using Bankly.Sdk.Opentelemetry.Trace;
using System.Diagnostics;

namespace App.Handlers.Http
{
    public class HttpResponseContractMapHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasCache == false &&
                context.HasNoErrorProcessor &&
                context.ResponseContext.ResponseBodyStringValue != null &&
                ContractMap != null)
            {
                
                var trace = context.HttpContext.RequestServices.GetService<ITraceService>();
                Activity activity = null;
                if (trace != null)
                    activity = trace.StartActivity("HttpResponseContractMapHandler");

                var json = context.ResponseContext.GetJsonResponseBody();

                var jsonMapParse = new JsonMapParse(json, ContractMap);
                json = jsonMapParse.MapParse();
                context.ResponseContext.ResponseBodyStringValue = json.ToString();

                activity?.Dispose();
            }

            await NextAsync(context);
        }
    }
}
