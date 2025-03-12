using Booka.Core.Domain;
using Booka.Core.DTOs.Common;

namespace Booka.Core.Interfaces.Repositories;

public interface IBookingRepository : IBaseRepository<Booking, int>
{
    /// <summary>
    ///     Retrieves user bookings; Includes workplace
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<Booking>> GetByUser(int userId);

    /// <summary>
    ///     Retrieves user booking based on workplace, without check in date 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<Booking>> GetByUserAndWorkplace(int userId, int workplaceId);
}