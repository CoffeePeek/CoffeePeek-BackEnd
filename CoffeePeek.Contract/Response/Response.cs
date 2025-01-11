using System.Net;

namespace CoffeePeek.Contract.Response;

public class Response
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public object Data { get; set; }

    public Response() { }

    public Response(bool success, string message, object data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static Response SuccessResponse(object data, string message = "Operation successful")
    {
        return new Response(true, message, data);
    }

    public static Response ErrorResponse(string message, object data = default)
    {
        return new Response(false, message, data);
    }
}

public class Response<T> 
{
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public T Data { get; set; }

    public Response()
    {
        
    }
    public Response(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static Response<T> SuccessResponse(T data, string message = "Operation successful")
    {
        return new Response<T>(true, message, data);
    }

    public static Response<T> ErrorResponse(string message, T data = default)
    {
        return new Response<T>(false, message, data);
    }
}