using System.Text.Json.Nodes;

namespace App.Contexts
{
    public class ResponseContext
    {
        public JsonObject JsonResponseBody { get; set; }
        public string ResponseBodyStringValue { get; set; }
        public string ResponseContentType { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
