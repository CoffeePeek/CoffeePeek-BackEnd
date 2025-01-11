namespace CoffeePeek.BusinessLogic.Abstractions;

public interface IValidationStrategy<in TEntity>
{
    ValidationResult Validate(TEntity entity);
}