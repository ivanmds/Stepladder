using App.Contexts;
using App.JsonHelpers;

namespace App.Handlers
{
    public class HttpResponseContractMapHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasNoError && 
                context.ResponseContext.JsonResponseBody != null &&
                ContractMap != null)
            {
                var json = context.ResponseContext.JsonResponseBody;

                var jsonMapParse = new JsonMapParse(json, ContractMap);
                json = jsonMapParse.MapParse();
                context.ResponseContext.ResponseBodyStringValue = json.ToString();
            }

            await NextAsync(context);
        }
    }
}
