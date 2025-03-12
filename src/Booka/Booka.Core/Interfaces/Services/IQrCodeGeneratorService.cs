namespace Booka.Core.Interfaces.Services;

public interface IQrCodeGeneratorService
{
    Stream GenerateWorkplaceBook(int workplaceId);
}