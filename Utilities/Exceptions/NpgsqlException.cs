namespace Utilities.Exceptions;

public class NpgsqlException : Exception
{
    public NpgsqlException(string message) : base(message)
    {
        
    }
}