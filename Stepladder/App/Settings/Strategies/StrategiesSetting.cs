using App.Validations;

namespace App.Settings.Strategies
{
    public class StrategiesSetting : IValidable
    {
        public List<CacheSetting> Caches { get; set; }

        public ValidateResult Valid()
        {
            var result = ValidateResult.Create();

            if (Caches != null)
                foreach (var cache in Caches)
                    result.Concate(cache.Valid());

            return result;
        }
    }
}
