using App.Settings.Strategies.Types;
using App.Validations;

namespace App.Settings.Strategies.Rules
{
    public class CacheSettingRule : IRule<CacheSetting>
    {
        public ValidateResult Do(CacheSetting value)
        {
            var result = ValidateResult.Create();

            if (string.IsNullOrEmpty(value.Id))
                result.AddError("Cache.Id is required");

            if(value.Type == CacheType.None)
                result.AddError("Cache.Type is required");

            var appSetting = ApplicationSetting.Current;

            if(value.Type ==  CacheType.Redis)
            {
                if (appSetting?.Connections?.Redis == null)
                    result.AddError("Configure connection redis before use cache redis");
            }

            return result;
        }
    }
}
