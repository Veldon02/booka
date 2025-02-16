using Booka.Core.Domain;

namespace Booka.Core.Interfaces.Repositories;

public interface IWorkplaceRepository : IBaseRepository<Workplace, int>
{
    Task<List<Workplace>> GetByWorkspace(int workspaceId);

    Task<List<Workplace>> GetWithBookingsByWorkspace(int workspaceId);

    Task<Workplace?> GetByIdWithBookings(int id, bool asNoTracking = true);
}