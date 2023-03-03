using App.Validations;

namespace App.Settings.Entrypoints.Routes
{
    public class RouteSetting : IValidable
    {
        public string Route { get; set; }
        public string Method { get; set; }


        public ValidateResult Valid()
        {
            throw new NotImplementedException();
        }
    }
}
