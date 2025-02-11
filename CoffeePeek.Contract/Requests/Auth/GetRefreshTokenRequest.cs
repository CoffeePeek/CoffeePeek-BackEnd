using CoffeePeek.Contract.Response.Auth;
using MediatR;

namespace CoffeePeek.Contract.Requests.Auth;

public class GetRefreshTokenRequest : IRequest<GetRefreshTokenResponse>
{
    public string RefreshToken { get; }

    public GetRefreshTokenRequest(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}