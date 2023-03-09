using App.Settings.Entrypoints.Routes;
using System.Text.Json.Nodes;

namespace App.Contexts
{
    public class StepladderHttpContext
    {
        public RouteSetting RouteSetting { get; set; }
        public HttpContext HttpContext { get; set; }

        public JsonObject JsonResponseBody { get; set; }
        public string ResponseBodyStringValue { get; set; }
        public string ResponseContentType { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string> GetCurrentBodyToRequestStringAsync()
        {
            if (HttpResponseMessage == null)
                return await GetHttpContextRequestBodyStringAsync();
            else
                return ResponseBodyStringValue;
        }


        private async Task<string> GetHttpContextRequestBodyStringAsync()
        {
            string requestBodyString = "";
            using StreamReader stream = new StreamReader(HttpContext.Request.Body);
            requestBodyString = await stream.ReadToEndAsync();

            return requestBodyString;
        }
    }
}