using CoffeePeek.Contract.Response.Login;
using MediatR;

namespace CoffeePeek.Contract.Requests.Auth;

public class LoginRequest : IRequest<LoginResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}