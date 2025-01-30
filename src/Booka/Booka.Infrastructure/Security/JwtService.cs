using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Security;
using Booka.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Booka.Infrastructure.Security;

public class JwtService : IJwtService
{
    private readonly JwtConfig _jwtConfig;

    public JwtService(IOptions<JwtConfig> jwtConfig)
    {
        _jwtConfig = jwtConfig.Value;
    }

    public string GenerateToken(Dictionary<string, string> claims, double? expiresIn = null)
    {
        var jwt = new JwtSecurityToken(
            null,
            null,
            PrepareClaims(claims),
            DateTime.UtcNow.AddMinutes(-5),
            DateTime.UtcNow.AddMinutes(10),
            SigningCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        if (string.IsNullOrEmpty(token))
        {
            throw new InternalServerException("Token can not be generated");
        }

        return token;
    }

    private static IEnumerable<Claim> PrepareClaims(Dictionary<string, string> claims)
    {
        claims[JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString();
        claims[JwtRegisteredClaimNames.Iat] = ToUnixEpochDate(DateTime.UtcNow).ToString();

        return claims.Select(x => new Claim(x.Key, x.Value)).ToList();
    }

    private static long ToUnixEpochDate(DateTime date)
    {
        return (long)Math.Round((date.ToUniversalTime() - DateTimeOffset.UnixEpoch).TotalSeconds);
    }

    private SigningCredentials SigningCredentials =>
        new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.SecretKey)), SecurityAlgorithms.HmacSha256);
}