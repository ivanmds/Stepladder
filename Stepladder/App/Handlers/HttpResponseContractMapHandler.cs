using App.Contexts;
using System.Text.Json.Nodes;

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

                foreach(var map  in ContractMap.MapFromTo) 
                {
                    var fields = map.Split(":");
                    var (from, to) = (fields[0], fields[1]);

                    JsonNode jsonNode;
                    if (json.TryGetPropertyValue(from, out jsonNode) == false)
                    {
                        throw new ArgumentException($"Not found field {from} from request body.");
                    }

                    json.Remove(from);
                    json.TryAdd(to, jsonNode.AsValue());
                }

                context.ResponseContext.ResponseBodyStringValue = json.ToString();
            }

            await NextAsync(context);
        }
    }
}
