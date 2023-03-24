using System.Text.Json.Serialization;

namespace App.Helpers.Validations
{
    public class ResultValidation
    {
        private bool _success = true;
        public ResultValidation() { }

        public List<ResultPropertyValidation> PropertyValidation { get; private set; } = new List<ResultPropertyValidation>();

        [JsonIgnore]
        public bool Success => _success;

        public void Append(ResultPropertyValidation propertyValidation)
        {
            if (propertyValidation.Success)
                return;

            _success = false;
            PropertyValidation.Add(propertyValidation);
        }

        public void Concate(ResultValidation resultValidation)
            => Append(resultValidation.PropertyValidation.ToArray());

        public void Append(params ResultPropertyValidation[] PropertyValidations)
        {
            foreach (var PropertyValidation in PropertyValidations)
                Append(PropertyValidation);
        }

        public static ResultValidation Create()
           => new ResultValidation();
    }
}
