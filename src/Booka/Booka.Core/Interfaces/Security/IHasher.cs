namespace Booka.Core.Interfaces.Security;

public interface IHasher
{
    string Hash(string password);

    bool Verify(string passwordHash, string password);
}