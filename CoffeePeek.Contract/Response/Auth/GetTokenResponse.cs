namespace CoffeePeek.Contract.Response.Auth;

public class GetTokenResponse : Response
{
    public string AccessToken { get;  }
    public string RefreshToken { get; }
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public GetTokenResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}