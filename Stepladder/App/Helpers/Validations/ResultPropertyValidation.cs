using System.Text.Json.Serialization;

namespace App.Helpers.Validations
{
    public class ResultPropertyValidation
    {
        public string PropertyName { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();

        [JsonIgnore]
        public bool Success => Errors.Count == 0;

        public void AppendError(string error)
            => Errors.Add(error);

        public static ResultPropertyValidation Create(string PropertyName)
        {
            var result = new ResultPropertyValidation { PropertyName = PropertyName };
            return result;
        }
    }
}
