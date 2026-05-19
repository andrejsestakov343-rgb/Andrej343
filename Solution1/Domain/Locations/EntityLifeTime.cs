public class EntityLifeTime
{
    public EntityLifeTime()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsArchived = false;
    }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsArchived { get; private set; }

    public void UpdateUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        IsArchived = true;
        UpdateUpdatedAt();

    }

    public void Restore()
    {
        IsArchived = false;
        UpdateUpdatedAt();
    }
}



