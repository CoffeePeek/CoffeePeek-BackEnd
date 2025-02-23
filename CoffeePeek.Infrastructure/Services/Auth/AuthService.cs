using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CoffeePeek.Data.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using AuthenticationOptions = CoffeePeek.BuildingBlocks.AuthOptions.AuthenticationOptions;

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

        var claims = new Dictionary<string, object>
        {
            { ClaimTypes.Name, user.Email! },
            { ClaimTypes.NameIdentifier, user.Id.ToString() },
            { JwtRegisteredClaimNames.Aud, "test" },
            { JwtRegisteredClaimNames.Iss, "test" }
        };

        var userRoles = await userManager.GetRolesAsync(user);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user, userRoles),
            Expires = DateTime.UtcNow.AddMinutes(AuthOptions.ExpireIntervalMinutes),
            SigningCredentials = credentials,
            Claims = claims,
            Audience = "test",
            Issuer = "test"
        };

        var token = handler.CreateToken(tokenDescriptor);
        var result = handler.WriteToken(token);

        return result;
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

    private static ClaimsIdentity GenerateClaims(User user, IEnumerable<string> userRoles)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email!));
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Aud, "test"));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iss, "test"));

        foreach (var role in userRoles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}