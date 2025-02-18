using CoffeePeek.BusinessLogic.Services;
using CoffeePeek.BusinessLogic.Services.Auth;
using CoffeePeek.Contract.Requests.Auth;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.Login;
using CoffeePeek.Data;
using CoffeePeek.Data.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoffeePeek.BusinessLogic.RequestHandlers.Login;

public class LoginRequestHandler(
    IHashingService hashingService,
    IRepository<RefreshToken> refreshTokenRepository,
    IAuthService authService,
    UserManager<User> userManager)
    : IRequestHandler<LoginRequest, Response<LoginResponse>>
{
    public async Task<Response<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Response.ErrorResponse<Response<LoginResponse>>("Account does not exist.");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            return Response.ErrorResponse<Response<LoginResponse>>("Password is incorrect.");
        }

        var accessToken = await authService.GenerateToken(user);
        var refreshToken = GenerateRefreshToken(user.Id);

        await SaveRefreshTokenAsync(user.Id, refreshToken);

        var result = new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return Response.SuccessResponse<Response<LoginResponse>>(result);
    }

    private string GenerateRefreshToken(int userId)
    {
        var refreshToken = authService.GenerateRefreshToken(userId);
        return refreshToken;
    }

    private async Task SaveRefreshTokenAsync(int userId, string refreshToken)
    {
        var refreshTokenHash = hashingService.HashString(refreshToken);

        var refreshTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = refreshTokenHash,
            ExpiryDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow,
        };

        refreshTokenRepository.Add(refreshTokenEntity);
        await refreshTokenRepository.SaveChangesAsync(CancellationToken.None);
    }
}
