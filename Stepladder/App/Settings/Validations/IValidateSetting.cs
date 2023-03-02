namespace App.Settings.Validations
{
    public interface IValidateSetting<T>
    {
        ValidationResult Validate(T value);
    }
}
