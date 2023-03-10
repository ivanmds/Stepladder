using App.Settings.Entrypoints.Routes;
using System.Text;

namespace App.Contexts
{
    public class StepladderHttpContext
    {
        public StepladderHttpContext()
        {
            ResponseContext = new ResponseContext();
        }

        public ResponseContext ResponseContext { get; set; }
        public RouteSetting RouteSetting { get; set; }
        public HttpContext HttpContext { get; set; }

        public bool HasNoError => Errors.Count() == 0;
        public List<string> Errors { get; set; } = new List<string>();
        public string ErrorString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in Errors)
                sb.AppendLine(error);

            return sb.ToString();
        }
            
            


        public async Task<string> GetCurrentBodyToRequestStringAsync()
        {
            if (ResponseContext.HttpResponseMessage == null)
                return await GetHttpContextRequestBodyStringAsync();
            else
                return ResponseContext.ResponseBodyStringValue;
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