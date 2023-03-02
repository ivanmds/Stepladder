﻿using App.Settings.Validations;
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
            var applicationSettings = deserializer.Deserialize<Settings.ApplicationSetting>(File.ReadAllText(pathConfigFile));

            ApplicationSettingValidate(applicationSettings);
            Console.WriteLine($"AddConfigFile File {pathConfigFile} loaded and validated");
        }

        private static void ApplicationSettingValidate(Settings.ApplicationSetting setting)
        {
            var validables = setting.GetValidables();
            var result = ValidationResult.Create();
            foreach (var validable in validables)
                result.Concate(validable.Valid());

            if(result.HasError)
            {
                throw new Exception(result.ToString());
            }
        }
    }
}
