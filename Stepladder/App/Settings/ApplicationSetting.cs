using App.Validations;

namespace App.Settings
{
    public class ApplicationSetting
    {
        public static ApplicationSetting Current { get; private set; }

        public ApplicationSetting()
        {
            if (Current == null)
                Current = this;
        }

        public StartupSetting Startup { get; set; }


        public List<IValidable> GetValidables()
        {
            var validables = new List<IValidable>();
            if (Startup != null)
            {
                validables.Add(Startup);
                validables.AddRange(Startup.HttpClientAuthentication);
                validables.Add(Startup.ApiSecuret);
            }


            return validables;
        }
    }
}
