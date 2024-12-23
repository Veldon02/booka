using System.Security.Cryptography;
using Booka.Core.Interfaces.Security;

namespace Booka.Infrastructure.Security;

public class Hasher : IHasher
{
    private const int SaltSize = 16; //bytes
    private const int HashSize = 32; //bytes
    private const int Iterations = 100_000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        var base64Hash = Convert.ToBase64String(hashBytes);

        return $"{Iterations}-{base64Hash}";
    }

    public bool Verify(string passwordHash, string password)
    {

        var splitHash = passwordHash.Split('-');
        var iterations = int.Parse(splitHash[0]);
        var base64Hash = splitHash[1];

        var hashBytes = Convert.FromBase64String(base64Hash);

        var salt = new byte[SaltSize];
        var originalHash = new byte[HashSize];

        Array.Copy(hashBytes, 0, salt, 0, SaltSize);
        Array.Copy(hashBytes, SaltSize, originalHash, 0, HashSize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, HashSize);

        return IsHashEqual(originalHash, hash);
    }

    private static bool IsHashEqual(byte[] originalHash, byte[] hash)
    {
        for (var i = 0; i < HashSize; i++)
        {
            if (originalHash[i] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}