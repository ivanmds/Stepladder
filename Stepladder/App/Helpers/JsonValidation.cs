using App.Helpers.Validations;
using App.Settings.ContractValidations;
using App.Settings.ContractValidations.Types;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace App.Helpers
{
    public class JsonValidation
    {
        private readonly JsonObject _jsonObject;

        public JsonValidation(JsonObject jsonObject)
        {
            _jsonObject = jsonObject;
        }

        public ResultFieldValidation Validate(IGrouping<string, FieldValidation> fieldValidations)
        {
            var fieldValidateSplited = fieldValidations.Key.Split('.');

            JsonNode mapFromJsonNode = null;
            JsonNode mapToJsonNode = null;
            JsonObject jsonObjectCurrent = _jsonObject;
            var result = ResultFieldValidation.Create(fieldValidations.Key);
            var validations = fieldValidations.ToArray();

            foreach (var field in fieldValidateSplited)
            {
                if (jsonObjectCurrent.TryGetPropertyValue(field, out mapFromJsonNode))
                {
                    if (mapFromJsonNode.GetType() == typeof(JsonObject))
                        jsonObjectCurrent = mapFromJsonNode as JsonObject;
                    else
                    {
                        var jsonValue = mapFromJsonNode.AsValue();

                        if (jsonValue.TryGetValue<string>(out var value))
                        {
                            foreach (var validation in validations)
                            {
                                if (validation.Type == FieldValidationType.Required)
                                {
                                    if (string.IsNullOrEmpty(value))
                                        result.AppendError("Field is required");
                                }
                                else if (validation.Type == FieldValidationType.LessThan)
                                {
                                    if (value?.Count() >= validation.Size)
                                        result.AppendError($"Field should be less than {validation.Size}");
                                }
                                else if (validation.Type == FieldValidationType.BiggerThan)
                                {
                                    if (value?.Count() <= validation.Size)
                                        result.AppendError($"Field should be bigger than {validation.Size}");
                                }
                            }
                        }
                    }
                }
                else
                    break;
            }

            return result;
        }
    }
}
