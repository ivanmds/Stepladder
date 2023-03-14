﻿using App.Settings.Actions;
using App.Settings.ContractMap;
using App.Settings.Entrypoints;
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
        public EntrypointSetting Entrypoints { get; set; }
        public List<ActionSetting> Actions { get; set; }
        public List<FlowActionsSetting> FlowActions { get; set; }
        public List<ContractMapSetting> ContractMaps { get; set; }


        public List<IValidable> GetValidables()
        {
            var validables = new List<IValidable>();
            if (Startup != null)
            {
                validables.Add(Startup);

                if (Startup.HttpClientAuthentication != null)
                    validables.AddRange(Startup.HttpClientAuthentication);

                validables.Add(Startup.ApiSecuret);
                validables.Add(Entrypoints);

                if (Entrypoints.Routes != null)
                    validables.AddRange(Entrypoints.Routes);

                if (Actions != null)
                    validables.AddRange(Actions);

                if (FlowActions != null)
                    validables.AddRange(FlowActions);

                if (ContractMaps != null)
                    validables.AddRange(ContractMaps);
            }

            return validables;
        }
    }
}
