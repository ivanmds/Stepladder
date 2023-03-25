using App.Contexts;
using App.JsonHelpers;

namespace App.Handlers
{
    public class HttpResponseContractMapHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (context.HasCache == false && 
                context.HasNoErrorValidation && 
                context.ResponseContext.ResponseBodyStringValue != null &&
                ContractMap != null)
            {
                var json = context.ResponseContext.GetJsonResponseBody();

                var jsonMapParse = new JsonMapParse(json, ContractMap);
                json = jsonMapParse.MapParse();
                context.ResponseContext.ResponseBodyStringValue = json.ToString();
            }

            await NextAsync(context);
        }
    }
}
