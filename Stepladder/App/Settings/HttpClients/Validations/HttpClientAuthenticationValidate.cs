using App.Settings.Validations;

namespace App.Settings.HttpClients.Validations
{
    public class HttpClientAuthenticationClientCredentialValidate : IValidateSetting<HttpClientAuthentication>
    {
        public ValidationResult Validate(HttpClientAuthentication value)
        {
            var result = ValidationResult.Create();

            if(value.Type == Types.AuthenticationType.ClientCredential)
            {
                if (string.IsNullOrEmpty(value.ClientId))
                    result.AddError("HttpClientAuthentication.ClientId is required");

                if (string.IsNullOrEmpty(value.ClientSecret))
                    result.AddError("HttpClientAuthentication.ClientSecret is required");

                if(string.IsNullOrEmpty(value.TokenUri))
                    result.AddError("HttpClientAuthentication.TokenUri is required");
                else
                {
                    Uri uri;
                    Uri.TryCreate(value.TokenUri, UriKind.Absolute, out uri);
                    if(uri is null)
                        result.AddError("HttpClientAuthentication.TokenUri should a valid uri");
                }
            }

            return result;
        }
    }
}
