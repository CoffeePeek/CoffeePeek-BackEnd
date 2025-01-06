namespace CoffeePeek.Contract.Response;

public class UpdateEntityResponse<T>
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public T Data { get; set; }
    
    public T? OldEntity { get; set; }

    public UpdateEntityResponse() { }

    public UpdateEntityResponse(bool success, string message, T data, T? oldEntity = default)
    {
        Success = success;
        Message = message;
        Data = data;
        OldEntity = oldEntity;
    }

    public static UpdateEntityResponse<T> SuccessResponse(T data, string message = "Entity updated successfully", T? oldEntity = default)
    {
        return new UpdateEntityResponse<T>(true, message, data, oldEntity);
    }

    public static UpdateEntityResponse<T> ErrorResponse(string message, T data = default)
    {
        return new UpdateEntityResponse<T>(false, message, data);
    }
}