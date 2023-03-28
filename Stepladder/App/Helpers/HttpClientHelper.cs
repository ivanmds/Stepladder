using App.Contexts;
using App.Settings.Actions;
using App.Settings.Actions.Types;

namespace App.Helpers
{
    public static class HttpClientHelper
    {
        public static void MapHeaderValue(
            StepladderHttpContext context,
            HttpClient httpClient,
            ActionSetting actionSetting)
        {
            if (actionSetting.HeaderMaps.Count > 0)
            {
                foreach (var headerMap in actionSetting.HeaderMaps)
                {
                    var mapSplitted = headerMap.MapFromTo.Split(":");
                    var (keyFrom, keyTo) = (mapSplitted[0], mapSplitted[1]);

                    if (headerMap.FromType == FromType.HttpRequest)
                    {
                        if(context.HttpContext.Request.Headers.TryGetValue(keyFrom, out var headerValue))
                            httpClient.DefaultRequestHeaders.Add(keyTo, headerValue.ToString());
                    }
                }
            }
        }

        public static string GetHeaderValue(IHeaderDictionary headerDictionary, string headerKey)
        {
            if (headerDictionary.TryGetValue(headerKey, out var headerValue))
                return headerValue.ToString();

            return string.Empty;
        }
    }
}
