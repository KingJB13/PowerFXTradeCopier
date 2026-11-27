namespace PowerFXTradeCopier.Domain.Common;

public abstract class AuditableEntity<TId> : Entity<TId>
{
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected AuditableEntity(TId id) : base(id)
    {
        CreatedAt = DateTime.UtcNow;
    }
    protected void UpdateAudit()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
