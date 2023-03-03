using App.Settings.HttpClients.Types;
using App.Settings.HttpClients.ValidateRules;
using App.Settings.Types;
using App.Validations;

namespace App.Settings.HttpClients
{
    public class HttpClientAuthentication : IValidable
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string EndpointAuth { get; set; }

        public AuthenticationType Type { get; set; }
        public ValueFromType ValueFrom { get; set; }


        public ValidateResult Valid()
        {
            var rules = new IRule<HttpClientAuthentication>[]
            {
                new HttpClientAuthenticationBaseValidateRule(),
                new HttpClientAuthenticationClientCredentialValidateRule()
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}