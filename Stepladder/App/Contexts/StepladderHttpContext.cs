using App.Settings.Entrypoints.Routes;

namespace App.Contexts
{
    public class StepladderHttpContext
    {
        public RouteSetting RouteSetting { get; set; }
        public HttpContext HttpContext { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
