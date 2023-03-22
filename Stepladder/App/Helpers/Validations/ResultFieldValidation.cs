using System.Text.Json.Serialization;

namespace App.Helpers.Validations
{
    public class ResultFieldValidation
    {
        public string FieldName { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        [JsonIgnore]
        public bool Success => Errors.Count == 0;

        public void AppendError(string error)
            => Errors.Add(error);

        public static ResultFieldValidation Create(string fieldName)
        {
            var result = new ResultFieldValidation { FieldName = fieldName };
            return result;
        }
    }
}
