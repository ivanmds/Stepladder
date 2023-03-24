using App.Validations;

namespace App.Settings.Strategies
{
    public class StrategiesSetting : IValidable
    {
        public CacheSetting Cache { get; set; }

        public ValidateResult Valid()
        {
            var result = ValidateResult.Create();

            result.Concate(Cache.Valid());

            return result;
        }
    }
}
