using CoffeePeek.Contract.Requests.Auth;
using CoffeePeek.Contract.Response.Auth;
using MediatR;

namespace CoffeePeek.BusinessLogic.RequestHandlers.Auth;

public class GetRefreshTokenRequestHandler : IRequestHandler<GetRefreshTokenRequest, GetTokenResponse>
{
    public Task<GetTokenResponse> Handle(GetRefreshTokenRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}