using System.Text.Json;
using System.Text.Json.Nodes;

namespace App.Contexts
{
    public class ResponseContext
    {
        private JsonObject _jsonResponseBody = null;

        public JsonObject GetJsonResponseBody()
        {
            if(_jsonResponseBody == null)
                _jsonResponseBody = JsonSerializer.Deserialize<JsonObject>(ResponseBodyStringValue);

            return _jsonResponseBody;
        }

        public string ResponseBodyStringValue { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseContentType { get; set; } = "application/json";
        public bool IsSuccessStatusCode { get; set; }
    }
}