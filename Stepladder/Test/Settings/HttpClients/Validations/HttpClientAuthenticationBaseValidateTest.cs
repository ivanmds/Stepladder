using App.Settings.HttpClients;
using App.Settings.HttpClients.Validations;

namespace Test.Settings.HttpClients.Validations
{
    public class HttpClientAuthenticationBaseValidateTest
    {
        [Fact]
        public void WhenIdIsNullOrEmpty_ShouldReturnError()
        {
            // arrange
            var httpClientAuthentication = new HttpClientAuthentication();
            var validate = new HttpClientAuthenticationBaseValidate();

            // act
            var result = validate.Validate(httpClientAuthentication);

            // assert
            var contains = result.Errors.Contains("HttpClientAuthentication.Id is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenHttpClientAuthenticationHasError_ShouldReturnErrorTrue()
        {
            // arrange
            var httpClientAuthentication = new HttpClientAuthentication();
            var validate = new HttpClientAuthenticationBaseValidate();

            // act
            var result = validate.Validate(httpClientAuthentication);

            // assert
            Assert.True(result.HasError);
        }

        [Fact]
        public void WhenHttpClientAuthenticationHasError_ShouldReturnIsSuccessFalse()
        {
            // arrange
            var httpClientAuthentication = new HttpClientAuthentication();
            var validate = new HttpClientAuthenticationBaseValidate();

            // act
            var result = validate.Validate(httpClientAuthentication);

            // assert
            Assert.False(result.IsSuccess);
        }
    }
}
