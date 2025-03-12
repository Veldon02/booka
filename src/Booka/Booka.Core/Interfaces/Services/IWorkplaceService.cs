using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Services;

public interface IWorkplaceService
{
    Task<List<Workplace>> GetByWorkspace(int workspaceId);

    Task<List<Workplace>> GetWithBookingsByWorkspace(int workspaceId);

    Task<Workplace> Create(int workspaceId, Workplace workplace);

    Task Update(int workspaceId, int workplaceId, Workplace workplace);

    Task Delete(int workspaceId, int workplaceId);

    Task<Stream> GenerateCode(int workspaceId, int workplaceId);
}