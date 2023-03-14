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
            JsonArrayParseFieldsRun();

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


        private void JsonArrayParseFieldsRun()
        {
            if (_contractMapSetting.MapArray?.Count > 0)
            {
                foreach (var mapArray in _contractMapSetting.MapArray)
                {
                    var fields = mapArray.ArrayMapFromTo.Split(":");
                    var (keyFrom, keyTo) = (fields[0], fields[1]);
                    string[] mapFromSplited = keyFrom.Split(".");
                    string[] mapToSplited = keyTo.Split(".");
                    var (mapFrom, mapTo) = JsonArrayParseFieldsRun(mapFromSplited, mapToSplited);
                    MapJsonArrayFromTo(mapArray, mapFrom, mapTo);
                }
            }
        }

        private (JsonArray mapFrom, JsonArray mapTo) JsonArrayParseFieldsRun(string[] mapFromSplited, string[] mapToSplited)
        {
            JsonObject jsonObjectCurrent = _jsonObject;
            JsonArray jsonArrayMapFrom = null;

            foreach (var field in mapFromSplited)
            {
                if (jsonObjectCurrent.TryGetPropertyValue(field, out var mapFromJsonNode))
                {
                    if (jsonObjectCurrent.TryGetPropertyValue(field, out var mapToJsonNode))
                    {
                        if (mapToJsonNode.GetType() == typeof(JsonObject))
                            jsonObjectCurrent = mapToJsonNode as JsonObject;
                        else if (mapFromJsonNode.GetType() == typeof(JsonArray))
                        {
                            jsonArrayMapFrom = mapFromJsonNode as JsonArray;
                            break;
                        }
                    }
                }
            }

            if (jsonArrayMapFrom == null)
                return (null, null);

            JsonArray jsonArrayMapTo = null;
            jsonObjectCurrent = _jsonObject;
            var totalSplited = mapToSplited.Length;
            var index = 0;

            foreach (var field in mapToSplited)
            {
                index++;
                if (jsonObjectCurrent.TryGetPropertyValue(field, out var mapToJsonNode))
                {
                    if (mapToJsonNode.GetType() == typeof(JsonObject))
                        jsonObjectCurrent = mapToJsonNode as JsonObject;
                    else if (mapToJsonNode.GetType() == typeof(JsonArray))
                        jsonArrayMapTo = mapToJsonNode as JsonArray;

                }
                else
                {
                    if (index == totalSplited)
                    {
                        jsonArrayMapTo = new JsonArray();
                        jsonObjectCurrent.Add(field, jsonArrayMapTo);
                    }
                    else
                    {
                        var jsonObject = new JsonObject();
                        jsonObjectCurrent.Add(field, jsonObject);
                        jsonObjectCurrent = jsonObject;
                    }
                }
            }


            return (jsonArrayMapFrom, jsonArrayMapTo);
        }

        private void MapJsonArrayFromTo(ContractMapArray contractMapArray, JsonArray mapFrom, JsonArray mapTo)
        {
            if (contractMapArray.MapFromTo?.Count > 0)
            {
                foreach (var jsonNode in mapFrom.ToArray())
                {
                    foreach (var mapFromTo in contractMapArray.MapFromTo)
                    {
                        var jsonObject = jsonNode as JsonObject;
                        if (jsonObject != null)
                        {
                            var fields = mapFromTo.Split(":");
                            var (keyFrom, keyTo) = (fields[0], fields[1]);

                            if (jsonObject.TryGetPropertyValue(keyFrom, out var mapToJsonNode))
                            {
                                jsonObject.Remove(keyFrom);
                                jsonObject.Add(keyTo, mapToJsonNode);
                            }
                        }
                    }

                    mapFrom.Remove(jsonNode);
                    mapTo.Add(jsonNode);
                }
            }
            else
            {
                foreach (var jsonNode in mapFrom.ToArray())
                {
                    mapFrom.Remove(jsonNode);
                    mapTo.Add(jsonNode);
                }
            }
        }
    }
}
