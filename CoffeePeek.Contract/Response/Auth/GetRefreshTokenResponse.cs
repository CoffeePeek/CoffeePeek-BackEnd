namespace CoffeePeek.Contract.Response.Auth;

public class GetRefreshTokenResponse : GetTokenResponse
{
    // ReSharper disable once ConvertToPrimaryConstructor
    public GetRefreshTokenResponse(string accessToken, string refreshToken) : base(accessToken, refreshToken)
    {
    }
}