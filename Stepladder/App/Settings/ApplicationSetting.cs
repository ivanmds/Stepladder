using App.Settings.Validations;

namespace App.Settings
{
    public class ApplicationSetting
    {
        public static ApplicationSetting CurrentValue { get; private set; }

        public ApplicationSetting()
        {
            CurrentValue = this;
        }

        public StartupSetting Startup { get; set; }


        public List<IValidable> GetValidables()
        {
            var validables = new List<IValidable>();
            if(Startup != null)
            {
                validables.Add(Startup);
                validables.AddRange(Startup.HttpClientAuthentication);
            }


            return validables;
        }
    }
}
