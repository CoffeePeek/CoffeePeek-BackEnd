using CoffeePeek.Contract.Requests.Auth;
using CoffeePeek.Contract.Response.Auth;
using CoffeePeek.Contract.Response.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator): Controller
{
    [HttpPost("login")]
    public Task<LoginResponse> Login([FromBody]LoginRequest request)
    {
        return mediator.Send(request);
    }

    [HttpGet("token")]
    public Task<GetTokenResponse> Token(GetTokenRequest request)
    {
        return mediator.Send(request);
    }
    
    [HttpGet("refresh")]
    public Task<GetRefreshTokenResponse> RefreshToken(GetRefreshTokenRequest request)
    {
        return mediator.Send(request);
    }
}