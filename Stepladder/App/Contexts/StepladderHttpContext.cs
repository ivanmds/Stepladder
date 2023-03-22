﻿using App.Settings.Entrypoints.Routes;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace App.Contexts
{
    public class StepladderHttpContext
    {
        public StepladderHttpContext()
        {
            ResponseContext = new ResponseContext();
        }

        public ResponseContext ResponseContext { get; set; }
        public RouteSetting RouteSetting { get; set; }
        public HttpContext HttpContext { get; set; }

        private bool _hasNoError = true;
        public bool HasNoError => _hasNoError;
       
        public void SetHttpValidationWithError()
            => _hasNoError = false;

        public async Task<string> GetCurrentBodyToRequestStringAsync()
        {
            if (ResponseContext.HttpResponseMessage == null)
                return await GetHttpContextRequestBodyStringAsync();
            else
                return ResponseContext.ResponseBodyStringValue;
        }

        private string _httpContextRequestBodyJsonText = "";
        private async Task<string> GetHttpContextRequestBodyStringAsync()
        {
            if (string.IsNullOrEmpty(_httpContextRequestBodyJsonText))
            {
                using StreamReader stream = new StreamReader(HttpContext.Request.Body);
                _httpContextRequestBodyJsonText = await stream.ReadToEndAsync();
            }

            return _httpContextRequestBodyJsonText;
        }

        private JsonObject _httpContextRequestBodyJson = null;
        public async Task<JsonObject> GetHttpContextRequestBodyAsync()
        {
            if(_httpContextRequestBodyJson == null)
            {
                var jsonString = await GetHttpContextRequestBodyStringAsync();
                _httpContextRequestBodyJson = JsonSerializer.Deserialize<JsonObject>(jsonString);
            }

            return _httpContextRequestBodyJson;
        }
    }
}