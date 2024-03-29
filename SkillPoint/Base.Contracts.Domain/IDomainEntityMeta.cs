﻿namespace Base.Contracts.Domain;

public interface IDomainEntityMeta
{
    public string? CratedBy { get; set; }
    public DateTime CratedAt { get; set; }

    public string? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}
