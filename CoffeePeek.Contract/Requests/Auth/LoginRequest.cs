using MediatR;

namespace CoffeePeek.Contract.Requests.Auth;

public class LoginRequest : IRequest<Response.Response>
{
    public string Username { get; set; }
    public string Password { get; set; }
}