using App.Settings.Actions.Types;
using App.Validations;

namespace App.Settings.Actions
{
    public class ActionHeaderMap : IValidable
    {
        public string MapFromTo { get; set; }
        public FromType FromType { get; set; }

        public ValidateResult Valid()
        {
            throw new NotImplementedException();
        }
    }
}