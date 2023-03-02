using App.Settings.Validations;

namespace App.Settings
{
    public class ApplicationSetting
    {
        public static ApplicationSetting Value { get; private set; }

        public ApplicationSetting()
        {
            Value = this;
        }

        public StartupSetting Startup { get; set; }


        public List<IValidable> GetValidables()
        {
            var validables = new List<IValidable>();
            if(Startup != null)
            {
                validables.AddRange(Startup.HttpClientAuthentication);
            }


            return validables;
        }
    }
}
