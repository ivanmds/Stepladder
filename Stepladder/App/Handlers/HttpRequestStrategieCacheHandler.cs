using App.Contexts;
using StackExchange.Redis;
using System.Text.Json;

namespace App.Handlers
{
    public class HttpRequestStrategieCacheHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            var database = context.HttpContext.RequestServices.GetService<IDatabase>();
            var cacheKey = context.HttpContext.Request.Path.ToString();
            var cacheValueString = await database.StringGetAsync(cacheKey, CommandFlags.PreferReplica);

            if (string.IsNullOrEmpty(cacheValueString) == false)
            {
                context.HasCache = true;
                var cacheValue = JsonSerializer.Deserialize<ResponseContext>(cacheValueString);
                context.ResponseContext.ResponseBodyStringValue = cacheValue.ResponseBodyStringValue;
                context.ResponseContext.ResponseStatusCode = cacheValue.ResponseStatusCode;
                context.ResponseContext.IsSuccessStatusCode = cacheValue.IsSuccessStatusCode;
            }

            await NextAsync(context);

            if (context.ResponseContext.IsSuccessStatusCode && context.HasCache == false)
            {
                var cacheValue = new ResponseContext
                {
                    ResponseBodyStringValue = context.ResponseContext.ResponseBodyStringValue,
                    ResponseStatusCode = context.ResponseContext.ResponseStatusCode,
                    IsSuccessStatusCode = context.ResponseContext.IsSuccessStatusCode
                };

                cacheValueString = JsonSerializer.Serialize(cacheValue);
                await database.StringSetAsync(cacheKey, cacheValueString, TimeSpan.FromSeconds(CacheSetting.Ttl), flags: CommandFlags.FireAndForget);
            }
        }
    }
}
