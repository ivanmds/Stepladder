using App.Contexts;
using App.Handlers;
using App.Settings;
using App.Validations;
using YamlDotNet.Serialization.NamingConventions;

namespace App.Extensions
{
    public static class LoadApplicationSettingsExtension
    {
        public static void AddLoadApplicationSettings(this WebApplicationBuilder builder)
        {
            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                       .WithNamingConvention(CamelCaseNamingConvention.Instance)
                       .Build();


            var pathConfigFile = "configApp.yaml";
            var applicationSettings = deserializer.Deserialize<ApplicationSetting>(File.ReadAllText(pathConfigFile));

            ApplicationSettingValidate(applicationSettings);
            Console.WriteLine($"AddConfigFile File {pathConfigFile} loaded and validated");

            builder.Services.AddScoped<StepladderHttpContext>();

            builder.Services.AddScoped<HttpClientGetRequestHandler>();
            builder.Services.AddScoped<HttpClientPostRequestHandler>();
            builder.Services.AddScoped<HttpFirstHandler>();
            builder.Services.AddScoped<HttpResponseMessageParseHandler>();
            builder.Services.AddScoped<HttpWriteResponseHandler>();
            builder.Services.AddScoped<HttpWriteResponseMockHandler>();
            builder.Services.AddScoped<HttpResponseContractMapHandler>();
            builder.Services.AddHttpClient();
        }

        private static void ApplicationSettingValidate(ApplicationSetting setting)
        {
            var validables = setting.GetValidables();
            var result = ValidateResult.Create();
            foreach (var validable in validables)
                result.Concate(validable.Valid());

            if(result.HasError)
            {
                throw new Exception(result.ToString());
            }
        }
    }
}
