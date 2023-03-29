using App.Validations;

namespace App.Settings.MapVariables.Rules
{
    public class MapVariableSettingRule : IRule<MapVariableSetting>
    {
        public ValidateResult Do(MapVariableSetting value)
        {
            var result = ValidateResult.Create();

            if (string.IsNullOrEmpty(value.Name))
                result.AddError("MapVariable.Name is required");

            return result;
        }
    }
}
