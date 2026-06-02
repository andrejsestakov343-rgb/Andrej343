namespace Domain.Shared.ValueObjects;

public sealed record EntityLifeTime(DateTimeOffset CreatedAt, DateTimeOffset? UpdatedAt = null)
{
    public static EntityLifeTime CreateNew() => new(DateTimeOffset.UtcNow);

    public EntityLifeTime Update() => this with { UpdatedAt = DateTimeOffset.UtcNow };

     public bool IsArchived => UpdatedAt < DateTimeOffset.UtcNow.AddDays(-30);
}

