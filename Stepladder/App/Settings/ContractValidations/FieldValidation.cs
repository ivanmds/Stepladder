using App.Settings.ContractValidations.Rules;
using App.Settings.ContractValidations.Types;
using App.Validations;

namespace App.Settings.ContractValidations
{
    public class FieldValidation : IValidable
    {
        public string Field { get; set; }
        public FieldValidationType Type { get; set; }
        public int Size { get; set; }

        public ValidateResult Valid()
        {
            var rules = new IRule<FieldValidation>[]
            {
                new FieldValidationRule(),
            };

            return RuleExecute.Execute(this, rules);
        }
    }
}
