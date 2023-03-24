using App.Helpers.Validations;
using App.Settings.ContractValidations;
using App.Settings.ContractValidations.Types;
using System.Text.Json.Nodes;

namespace App.Helpers
{
    public class JsonValidation
    {
        private readonly JsonObject _jsonObject;
        private JsonObject _jsonCurrentValidation;
        private string _prefixPropertyName = "";

        public JsonValidation(JsonObject jsonObject)
        {
            _jsonObject = jsonObject;
        }

        public ResultValidation Validate(List<PropertyValidation> PropertyValidations)
        {
            _jsonCurrentValidation = _jsonObject;
            return JsonObjectValidate(PropertyValidations);
        }

        public ResultValidation Validate(List<PropertyValidationArrayObject> validationArrayObjects)
        {
            var resultValidation = ResultValidation.Create();
            JsonObject jsonObjectCurrent = _jsonCurrentValidation;
            JsonNode mapFromJsonNode = null;
            JsonNode mapToJsonNode = null;

            foreach (var validationObject in validationArrayObjects)
            {
                var arrayPropertyNameSplited = validationObject.ArrayPropertyName.Split('.');
                foreach (var Property in arrayPropertyNameSplited)
                {
                    if (jsonObjectCurrent.TryGetPropertyValue(Property, out mapFromJsonNode))
                    {
                        if (mapFromJsonNode.GetType() == typeof(JsonArray))
                        {
                            var jsonArray = mapFromJsonNode as JsonArray;
                            int index = 0;
                            foreach (var jsonObject in jsonArray)
                            {
                                if (jsonObject.GetType() == typeof(JsonObject))
                                {
                                    _jsonCurrentValidation = jsonObject as JsonObject;
                                    _prefixPropertyName = $"{validationObject.ArrayPropertyName}[{index}].";
                                    var result = JsonObjectValidate(validationObject.Properties);
                                    resultValidation.Concate(result);
                                }

                                index++;
                            }
                        }
                    }
                    else
                        break;
                }
            }

            return resultValidation;
        }

        private ResultValidation JsonObjectValidate(List<PropertyValidation> PropertyValidations)
        {
            var resultValidation = ResultValidation.Create();
            var groupByProperty = PropertyValidations.GroupBy(p => p.PropertyName);

            foreach (var keyValue in groupByProperty)
            {
                var PropertyValidation = PropertyValidate(keyValue);
                resultValidation.Append(PropertyValidation);
            }

            return resultValidation;
        }

        private ResultPropertyValidation PropertyValidate(IGrouping<string, PropertyValidation> groupPropertyValidations)
        {
            var PropertyValidateSplitted = groupPropertyValidations.Key.Split('.');

            JsonNode mapFromJsonNode = null;
            JsonNode mapToJsonNode = null;
            JsonObject jsonObjectCurrent = _jsonCurrentValidation;
            var result = ResultPropertyValidation.Create($"{_prefixPropertyName}{groupPropertyValidations.Key}");
            var PropertyValidations = groupPropertyValidations.ToArray();

            foreach (var Property in PropertyValidateSplitted)
            {
                if (jsonObjectCurrent.TryGetPropertyValue(Property, out mapFromJsonNode))
                {
                    if (mapFromJsonNode.GetType() == typeof(JsonObject))
                        jsonObjectCurrent = mapFromJsonNode as JsonObject;
                }
                else
                    break;
            }

            var jsonValue = mapFromJsonNode?.AsValue();
            foreach (var PropertyValidation in PropertyValidations)
            {
                if (PropertyValidation.ValueType == PropertyValueType.String)
                    StringPropertyValidation(result, jsonValue, PropertyValidation);
                else if (PropertyValidation.ValueType == PropertyValueType.Int)
                    IntPropertyValidation(result, jsonValue, PropertyValidation);
                else if (PropertyValidation.ValueType == PropertyValueType.Float)
                    FloatPropertyValidation(result, jsonValue, PropertyValidation);
            }

            return result;
        }

        private void StringPropertyValidation(ResultPropertyValidation result, JsonValue jsonValue, PropertyValidation PropertyValidation)
        {
            if (jsonValue?.TryGetValue<string>(out var value) == true)
            {
                if (PropertyValidation.ValidationType == PropertyValidationType.Required)
                {
                    if (string.IsNullOrEmpty(value))
                        result.AppendError("Property is required");
                }
                else if (PropertyValidation.ValidationType == PropertyValidationType.LessThan)
                {
                    if (value?.Count() >= PropertyValidation.Length)
                        result.AppendError($"Property should be less than {PropertyValidation.Length}");
                }
                else if (PropertyValidation.ValidationType == PropertyValidationType.BiggerThan)
                {
                    if (value?.Count() <= PropertyValidation.Length)
                        result.AppendError($"Property should be bigger than {PropertyValidation.Length}");
                }
            }
            else if (jsonValue != null)
                result.AppendError("Property value should be a string");
            else
            {
                if (PropertyValidation.ValidationType == PropertyValidationType.Required)
                {
                    result.AppendError("Property is required");
                }
            }
        }

        private void IntPropertyValidation(ResultPropertyValidation result, JsonValue jsonValue, PropertyValidation PropertyValidation)
        {
            if (jsonValue?.TryGetValue<int>(out var value) == true)
            {
                if (PropertyValidation.ValidationType == PropertyValidationType.LessThan)
                {
                    if (value >= PropertyValidation.Value)
                        result.AppendError($"Property should be less than {PropertyValidation.Value}");
                }
                else if (PropertyValidation.ValidationType == PropertyValidationType.BiggerThan)
                {
                    if (value <= PropertyValidation.Value)
                        result.AppendError($"Property should be bigger than {PropertyValidation.Value}");
                }
            }
            else if (jsonValue != null)
                result.AppendError("Property value should be a string");
            else
            {
                if (PropertyValidation.ValidationType == PropertyValidationType.Required)
                {
                    result.AppendError("Property is required");
                }
            }
        }

        private void FloatPropertyValidation(ResultPropertyValidation result, JsonValue jsonValue, PropertyValidation PropertyValidation)
        {
            if (jsonValue?.TryGetValue<float>(out var value) == true)
            {
                if (PropertyValidation.ValidationType == PropertyValidationType.LessThan)
                {
                    if (value >= PropertyValidation.Value)
                        result.AppendError($"Property should be less than {PropertyValidation.Value}");
                }
                else if (PropertyValidation.ValidationType == PropertyValidationType.BiggerThan)
                {
                    if (value <= PropertyValidation.Value)
                        result.AppendError($"Property should be bigger than {PropertyValidation.Value}");
                }
            }
            else if (jsonValue != null)
                result.AppendError("Property value should be a string");
            else
            {
                if (PropertyValidation.ValidationType == PropertyValidationType.Required)
                {
                    result.AppendError("Property is required");
                }
            }
        }
    }
}
