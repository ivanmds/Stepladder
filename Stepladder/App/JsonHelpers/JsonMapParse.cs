using App.Settings.ContractMap;
using System.Text.Json.Nodes;

namespace App.JsonHelpers
{
    public class JsonMapParse
    {
        private JsonObject _jsonObject;
        private ContractMapSetting _contractMapSetting;

        public JsonMapParse(JsonObject jsonObject, ContractMapSetting contractMapSetting)
        {
            _jsonObject = jsonObject;
            _contractMapSetting = contractMapSetting;
        }

        public JsonObject MapParse()
        {
            JsonParseFieldsRun();
            JsonRemoveFieldsRun();
            return _jsonObject;
        }

        // see how can do better performance
        private void JsonParseFieldsRun()
        {
            foreach (var mapFromTo in _contractMapSetting.MapFromTo)
            {
                var fields = mapFromTo.Split(":");
                var (keyFrom, keyTo) = (fields[0], fields[1]);

                var keyFromSplited = keyFrom.Split(".");
                var keyToSplited = keyTo.Split(".");
                JsonParseFieldsRun(keyFromSplited, keyToSplited);
            }
        }

        private void JsonParseFieldsRun(string[] mapFromSplited, string[] mapToSplited)
        {
            JsonNode mapFromJsonNode = null;
            JsonNode mapToJsonNode = null;
            JsonObject jsonObjectCurrent = _jsonObject;

            foreach (var field in mapFromSplited)
            {
                if (jsonObjectCurrent.TryGetPropertyValue(field, out mapFromJsonNode))
                {
                    if (mapFromJsonNode.GetType() == typeof(JsonObject))
                        jsonObjectCurrent = mapFromJsonNode as JsonObject;
                    else
                        jsonObjectCurrent.Remove(field);
                }
            }

            jsonObjectCurrent = _jsonObject;
            if (mapFromJsonNode != null)
            {
                var totalSplited = mapToSplited.Length;
                var index = 0;
                foreach (var field in mapToSplited)
                {
                    index++;

                    if (index == totalSplited)
                        jsonObjectCurrent.Add(field, mapFromJsonNode);
                    else
                    {
                        if (jsonObjectCurrent.TryGetPropertyValue(field, out mapToJsonNode))
                        {
                            if (mapToJsonNode.GetType() == typeof(JsonObject))
                                jsonObjectCurrent = mapToJsonNode as JsonObject;
                        }
                        else
                        {
                            var nextJsonObjectCurrent = new JsonObject();
                            jsonObjectCurrent.TryAdd(field, nextJsonObjectCurrent);
                            jsonObjectCurrent = nextJsonObjectCurrent;
                        }
                    }
                }
            }
        }

        private void JsonRemoveFieldsRun()
        {
            if (_contractMapSetting.Remove == null)
                return;

            foreach (var removeField in _contractMapSetting.Remove)
            {
                var removeFieldSplited = removeField.Split(".");
                JsonNode jsonNode = null;
                JsonObject jsonObjectCurrent = _jsonObject;

                var totalSplited = removeFieldSplited.Length;
                var index = 0;

                foreach (var field in removeFieldSplited)
                {
                    index++;
                    if (index == totalSplited)
                        jsonObjectCurrent.Remove(field);
                    else
                    {
                        if (jsonObjectCurrent.TryGetPropertyValue(field, out jsonNode))
                            if (jsonNode.GetType() == typeof(JsonObject))
                                jsonObjectCurrent = jsonNode as JsonObject;
                    }
                }
            }
        }
    }
}
