using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CoffeePeek.BuildingBlocks.AuthOptions;
using CoffeePeek.Data.Models.Users;
using CoffeePeek.Infrastructure.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CoffeePeek.Infrastructure.Services.Auth;

public class AuthService(
    IOptions<AuthenticationOptions> authenticationOptions,
    UserManager<User> userManager) 
    : IAuthService
{
    public AuthenticationOptions AuthOptions { get; } = authenticationOptions.Value;

    public async Task<string> GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AuthOptions.JwtSecretKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var userRoles = await userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };
    
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims, "Bearer"),
            Expires = DateTime.UtcNow.AddMinutes(AuthOptions.ExpireIntervalMinutes),
            SigningCredentials = credentials,
            Audience = AuthOptions.ValidAudience,
            Issuer = AuthOptions.ValidIssuer,    
            NotBefore = DateTime.UtcNow           
        };


        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public string GenerateRefreshToken(int userId)
    {
        var refreshTokenData = new
        {
            UserId = userId,
            Expiry = DateTime.UtcNow.AddDays(AuthOptions.ExpireRefreshIntervalDays)
        };

        var jsonData = JsonConvert.SerializeObject(refreshTokenData);

        using var aesAlg = Aes.Create();

        var keyBytes = Encoding.UTF8.GetBytes(AuthOptions.JwtSecretKey);
        using var sha256 = SHA256.Create();
        var hashedKey = sha256.ComputeHash(keyBytes);

        aesAlg.Key = hashedKey;
        aesAlg.IV = new byte[16];

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        byte[] encrypted;

        using (var msEncrypt = new MemoryStream())
        {
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (var writer = new StreamWriter(csEncrypt))
                {
                    writer.Write(jsonData);
                }
            }

            encrypted = msEncrypt.ToArray();
        }

        return Convert.ToBase64String(encrypted);
    }

    public int? DecryptRefreshToken(string refreshToken)
    {
        try
        {
            var encryptedBytes = Convert.FromBase64String(refreshToken);

            using var aesAlg = Aes.Create();

            var keyBytes = Encoding.UTF8.GetBytes(AuthOptions.JwtSecretKey);
            using var sha256 = SHA256.Create();
            var hashedKey = sha256.ComputeHash(keyBytes);

            aesAlg.Key = hashedKey;
            aesAlg.IV = new byte[16];

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using var msDecrypt = new MemoryStream(encryptedBytes);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(csDecrypt);
            var decryptedJson = reader.ReadToEnd();
            var refreshTokenData = JsonConvert.DeserializeObject<dynamic>(decryptedJson);

            return refreshTokenData.UserId;
        }
        catch
        {
            return null;
        }
    }

    private ClaimsIdentity GenerateClaims(User user, IEnumerable<string> userRoles)
    {
        var claims = new ClaimsIdentity("Bearer");
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email!));
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Aud, AuthOptions.ValidAudience));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iss, AuthOptions.ValidIssuer));

        foreach (var role in userRoles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}