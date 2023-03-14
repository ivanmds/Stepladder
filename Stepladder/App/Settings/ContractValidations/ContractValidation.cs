using App.Settings.ContractValidations.Rules;
using App.Validations;

namespace App.Settings.ContractValidations
{
    public class ContractValidation : IValidable
    {
        public string Id { get; set; }
        public List<FieldValidation> Fields { get; set; } = new List<FieldValidation>();

        public ValidateResult Valid()
        {
            var rules = new IRule<ContractValidation>[]
            {
                new ContractValidationRule(),
            };
            
            var results = RuleExecute.Execute(this, rules);
            foreach (var field in Fields)
                results.Concate(field.Valid());
            
            return results;
        }
    }
}
