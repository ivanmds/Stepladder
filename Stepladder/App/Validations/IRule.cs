namespace App.Validations
{
    public interface IRule<T>
    {
        ValidateResult Do(T value);
    }
}