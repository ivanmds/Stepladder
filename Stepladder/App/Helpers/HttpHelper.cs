using App.Contexts;
using App.Settings.Actions;
using App.Settings.Actions.Types;

namespace App.Helpers
{
    public static class HttpHelper
    {
        private static List<string> NOT_PROPAGATED_HEADER = new List<string>()
        {
            "Authorization",
            "Postman-Token",
            "Content-Length",
            "Host",
            "User-Agent",
            "Accept",
            "Accept-Encoding",
            "Connection"
        };

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

        public static void SetPropagatedHeadersFromHttpRequestToHttpClient(HttpRequest httpRequest, HttpClient httpClient)
        {
            foreach(var header in httpRequest.Headers)
            {
                if (NOT_PROPAGATED_HEADER.Contains(header.Key))
                    continue;

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value.ToString());
            }
        }

        public static void SetPropagatedHeadersFromHttpResponseMessageToHttpResponse(HttpResponseMessage responseMessage, HttpResponse response)
        {
            foreach (var header in responseMessage.Headers)
            {
                if (NOT_PROPAGATED_HEADER.Contains(header.Key))
                    continue;

                response.Headers.TryAdd(header.Key, header.Value.FirstOrDefault());
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
