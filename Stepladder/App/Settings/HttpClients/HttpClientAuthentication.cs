﻿using App.Settings.HttpClients.Types;
using App.Settings.HttpClients.ValidateRules;
using App.Settings.Types;
using App.Settings.Validations;

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


        public ValidationResult Valid()
        {
            var validations = new IValidateSetting<HttpClientAuthentication>[]
            {
                new HttpClientAuthenticationBaseValidateRule(),
                new HttpClientAuthenticationClientCredentialValidateRule()
            };

            var finalResult = ValidationResult.Create();
            foreach (var validation in validations) 
            {
                var result = validation.Validate(this);
                finalResult.Concate(result);
            }

            return finalResult;
        }
    }
}