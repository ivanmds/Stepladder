using System.Text.Json.Serialization;

namespace App.Helpers.Validations
{
    public class ResultValidation
    {
        private bool _success = true;
        public ResultValidation() { }

        public List<ResultFieldValidation> FieldValidation { get; private set; } = new List<ResultFieldValidation>();

        [JsonIgnore]
        public bool Success => _success;

        public void Append(ResultFieldValidation fieldValidation)
        {
            if (fieldValidation.Success)
                return;
            
            _success = false;
            FieldValidation.Add(fieldValidation);
        }

        public void Append(params ResultFieldValidation[] fieldValidations)
            => FieldValidation.AddRange(fieldValidations);

        public static ResultValidation Create()
           => new ResultValidation();
    }
}
