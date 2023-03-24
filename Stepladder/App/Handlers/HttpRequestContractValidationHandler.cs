using App.Contexts;
using App.Helpers;
using App.Helpers.Validations;
using System.Text.Json;

namespace App.Handlers
{
    public class HttpRequestContractValidationHandler : Handler
    {
        public override async Task DoAsync(StepladderHttpContext context)
        {
            if (ContractValidation != null)
            {
                var json = await context.GetHttpContextRequestBodyAsync();
                var jsonValidation = new JsonValidation(json);

                var resultValidation = ResultValidation.Create();

                var resultPropertyValidation = jsonValidation.Validate(ContractValidation.Properties);
                var resultArrayObjectsValidation = jsonValidation.Validate(ContractValidation.ValidationArrayObjects);

                resultValidation.Concate(resultPropertyValidation);
                resultValidation.Concate(resultArrayObjectsValidation);

                if (resultValidation.Success == false)
                {
                    context.SetHttpValidationWithError();
                    context.ResponseContext.ResponseBodyStringValue = JsonSerializer.Serialize(resultValidation);
                }
            }

            await NextAsync(context);
        }
    }
}
