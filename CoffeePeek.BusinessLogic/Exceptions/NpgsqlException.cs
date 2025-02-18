namespace CoffeePeek.BusinessLogic.Exceptions;

public class NpgsqlException : Exception
{
    public NpgsqlException(string message) : base(message)
    {
        
    }
}