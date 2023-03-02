using App.Settings.Validations;

namespace App.Settings.HttpClients.Validations
{
    public class HttpClientAuthenticationBaseValidate : IValidateSetting<HttpClientAuthentication>
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
