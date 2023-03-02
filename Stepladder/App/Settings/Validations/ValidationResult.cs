using System.Text;

namespace App.Settings.Validations
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; private set; }

        public void AddError(string error)
            => Errors.Add(error);

        public void AddErrors(IEnumerable<string> errors)
            => Errors.AddRange(errors);

        public bool HasError => Errors.Count > 0;
        public bool IsSuccess => !HasError;
        public void Concate(ValidationResult validation)
            => Errors.AddRange(validation.Errors);

        public override string ToString()
        {
            if (IsSuccess)
                return "Success";

            var msgErrors = new StringBuilder();

            foreach (var error in Errors)
                msgErrors.AppendLine(error.ToString());

            return msgErrors.ToString();
        }


        public static ValidationResult Create() => new ValidationResult();
        public static ValidationResult Concate(params ValidationResult[] validations)
        {
            var result = Create();
            foreach (var validation in validations)
                result.AddErrors(validation.Errors);

            return result;
        }
    }
}
