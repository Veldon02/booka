﻿namespace Booka.Core.Domain;

public class Booking : BaseEntity<int>
{
    public int UserId { get; set; }

    public User User { get; set; }

    public int WorkplaceId { get; set; }

    public Workplace Workplace { get; set; }

    public DateTime BookDate { get; set; }

    public DateTime? CheckInDate { get; set; }
}
