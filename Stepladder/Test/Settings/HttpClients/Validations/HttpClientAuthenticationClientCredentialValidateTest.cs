using App.Settings.HttpClients;
using App.Settings.HttpClients.Validations;

namespace Test.Settings.HttpClients.Validations
{
    public class HttpClientAuthenticationClientCredentialValidateTest
    {
        [Fact]
        public void WhenClientIdIsNullOrEmpty_ShouldReturnError()
        {
            // arrange 
            var httpClientAuthentication = new HttpClientAuthentication();
            var validate = new HttpClientAuthenticationClientCredentialValidate();

            // act 

            var result = validate.Validate(httpClientAuthentication);

            // assert
            var contains = result.Errors.Contains("HttpClientAuthentication.ClientId is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenClientSecretIsNullOrEmpty_ShouldReturnError()
        {
            // arrange 
            var httpClientAuthentication = new HttpClientAuthentication();
            var validate = new HttpClientAuthenticationClientCredentialValidate();

            // act 
            var result = validate.Validate(httpClientAuthentication);

            // assert
            var contains = result.Errors.Contains("HttpClientAuthentication.ClientSecret is required");
            Assert.True(contains);
        }


        [Fact]
        public void WhenTokenUriIsNullOrEmpty_ShouldReturnError()
        {
            // arrange 
            var httpClientAuthentication = new HttpClientAuthentication();
            var validate = new HttpClientAuthenticationClientCredentialValidate();

            // act 
            var result = validate.Validate(httpClientAuthentication);

            // assert
            var contains = result.Errors.Contains("HttpClientAuthentication.TokenUri is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenTokenUriIsNotValid_ShouldReturnError()
        {
            // arrange 
            var httpClientAuthentication = new HttpClientAuthentication
            {
                TokenUri = "http://ts@tes@com"
            };
            var validate = new HttpClientAuthenticationClientCredentialValidate();

            // act 
            var result = validate.Validate(httpClientAuthentication);

            // assert
            var contains = result.Errors.Contains("HttpClientAuthentication.TokenUri should a valid uri");
            Assert.True(contains);
        }

        [Fact]
        public void WhenTokenUriIsValid_ShouldNotReturnError()
        {
            // arrange 
            var httpClientAuthentication = new HttpClientAuthentication
            {
                TokenUri = "http://pix.com/api/test"
            };
            var validate = new HttpClientAuthenticationClientCredentialValidate();

            // act 
            var result = validate.Validate(httpClientAuthentication);

            // assert
            var contains = result.Errors.Contains("HttpClientAuthentication.TokenUri should a valid uri");
            Assert.False(contains);
        }
    }
}
