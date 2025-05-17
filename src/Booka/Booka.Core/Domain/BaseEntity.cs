﻿namespace Booka.Core.Domain;

public class BaseEntity<T>
{
    public T Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
