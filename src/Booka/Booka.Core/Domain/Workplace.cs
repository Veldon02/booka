using Booka.Core.Domain.enums.Workspace;

namespace Booka.Core.Domain;

public class Workplace : BaseEntity<int>
{
    public int WorkspaceId {get; set; }

    public Workspace Workspace { get; set; }

    public int Number {  get; set; }

    public WorkplaceType Type { get; set; }

    public IList<Booking> Bookings { get; set; }

    // For now only full day booking is supported, once advanced booking
    // will be introduced this should be reworked to include time check 
    // within day
    public bool IsAvailable(DateTime bookDate)
    {
        var isBookingExist = Bookings.Any(x => x.BookDate.Date == bookDate.Date);

        return !isBookingExist;
    }
}
