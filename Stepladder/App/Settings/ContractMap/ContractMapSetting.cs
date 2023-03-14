using App.Settings.ContractMap.Rules;
using App.Validations;

namespace App.Settings.ContractMap
{
    public class ContractMapSetting : IValidable
    {
        public string Id { get; set; }
        public List<string> MapFromTo { get; set; }
        public List<string> Remove { get; set; }
        public ContractMapArray MapArray { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<ContractMapSetting>[]
            {
                new ContractMapSettingRule(),
            };

            var result =  RuleExecute.Execute(this, rules);
            result.Concate(MapArray.Valid());

            return result;
        }
    }
}
