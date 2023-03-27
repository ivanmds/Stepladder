﻿using App.Contexts;
using App.Settings.Actions;
using App.Settings.ContractMap;
using App.Settings.ContractValidations;
using App.Settings.Strategies;

namespace App.Handlers.Http
{
    public abstract class Handler
    {
        protected Handler _next;
        public ActionSetting ActionSetting { get; set; }
        public ContractMapSetting ContractMap { get; set; }
        public ContractValidation ContractValidation { get; set; }
        public CacheSetting CacheSetting { get; set; }

        public void SetNext(Handler next)
            => _next = next;

        public abstract Task DoAsync(StepladderHttpContext context);

        protected async Task NextAsync(StepladderHttpContext context)
        {
            if (_next != null)
                await _next.DoAsync(context);
        }

        protected async Task LoadHttpResponseMessageAsync(StepladderHttpContext context, HttpResponseMessage response)
        {
            context.ResponseContext.ResponseBodyStringValue = await response.Content.ReadAsStringAsync();
            context.ResponseContext.ResponseStatusCode = (int)response.StatusCode;
            context.ResponseContext.IsSuccessStatusCode = response.IsSuccessStatusCode;
        }
    }
}