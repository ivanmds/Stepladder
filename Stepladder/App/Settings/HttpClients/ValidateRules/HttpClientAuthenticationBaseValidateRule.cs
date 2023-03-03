using App.Settings.Validations;

namespace App.Settings.HttpClients.ValidateRules
{
    public class HttpClientAuthenticationBaseValidateRule : IValidateSetting<HttpClientAuthentication>
    {
        public ValidationResult Validate(HttpClientAuthentication value)
        {
            var result = ValidationResult.Create();

            if (string.IsNullOrEmpty(value.Id))
                result.AddError("HttpClientAuthentication.Id is required");


            return result;
        }
    }
}
