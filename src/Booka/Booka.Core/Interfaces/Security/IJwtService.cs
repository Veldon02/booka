namespace Booka.Core.Interfaces.Security;

public interface IJwtService
{
    string GenerateToken(Dictionary<string, string> claims, double? expiresIn = null);

    bool ValidateToken(string token, string secretKey);
}