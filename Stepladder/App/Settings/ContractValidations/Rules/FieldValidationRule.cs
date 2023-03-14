
using App.Settings.ContractValidations.Types;
using App.Validations;

namespace App.Settings.ContractValidations.Rules
{

    public class FieldValidationRule : IRule<FieldValidation>
    {
        public ValidateResult Do(FieldValidation value)
        {
            var result = ValidateResult.Create();

            if (string.IsNullOrEmpty(value.Field))
                result.AddError("FieldValidation.Field is required");
            else
            { 
                if(value.Field.Contains(' '))
                    result.AddError("FieldValidation.Field not accept space");
            }

            if (value.Type == FieldValidationType.None)
                result.AddError("FieldValidation.Type is required");

            if (value.Type == FieldValidationType.BiggerThan ||
                value.Type == FieldValidationType.LessThan)
            {
                if(value.Size <= 0)
                    result.AddError("FieldValidation.Size should be informed");
            }

            return result;
        }
    }
}
