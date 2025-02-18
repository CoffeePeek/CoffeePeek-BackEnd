using CoffeePeek.Contract.Response.Auth;
using MediatR;

namespace CoffeePeek.Contract.Requests.Auth;

public class GetRefreshTokenRequest(string refreshToken) : IRequest<Response.Response<GetRefreshTokenResponse>>
{
    public string RefreshToken { get; } = refreshToken;
}