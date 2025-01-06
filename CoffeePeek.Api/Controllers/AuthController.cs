using CoffeePeek.Contract.Requests.Auth;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Api.Controllers;

public class AuthController(IMediator mediator): Controller
{
    [HttpGet("login")]
    public Task<Response> Login(LoginRequest request)
    {
        return mediator.Send(request);
    }

    [HttpGet("token")]
    public Task<GetTokenResponse> Token(GetTokenRequest request)
    {
        return mediator.Send(request);
    }
    
    [HttpGet("refresh")]
    public Task<GetTokenResponse> RefreshToken(GetTokenRequest request)
    {
        return mediator.Send(request);
    }
}