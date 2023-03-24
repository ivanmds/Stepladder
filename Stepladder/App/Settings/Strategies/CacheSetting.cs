using App.Settings.Strategies.Rules;
using App.Settings.Strategies.Types;
using App.Validations;

namespace App.Settings.Strategies
{
    public class CacheSetting : IValidable
    {
        public string Id { get; set; }
        public CacheType Type { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<CacheSetting>[]
             {
                new CacheSettingRule()
             };

            return RuleExecute.Execute(this, rules);
        }
    }
}
