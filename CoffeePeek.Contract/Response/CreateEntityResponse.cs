namespace CoffeePeek.Contract.Response;

public class CreateEntityResponse<T>
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public T Data { get; set; }
    
    public int? EntityId { get; set; }

    public CreateEntityResponse() { }

    public CreateEntityResponse(bool success, string message, T data, int? entityId = null)
    {
        Success = success;
        Message = message;
        Data = data;
        EntityId = entityId;
    }

    public static CreateEntityResponse<T> SuccessResponse(T data, string message = "Entity created successfully", int? entityId = null)
    {
        return new CreateEntityResponse<T>(true, message, data, entityId);
    }

    public static CreateEntityResponse<T> ErrorResponse(string message, T data = default)
    {
        return new CreateEntityResponse<T>(false, message, data);
    }
}