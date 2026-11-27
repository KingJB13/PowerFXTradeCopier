namespace PowerFXTradeCopier.Domain.Common;


public abstract class Entity<TId>(TId id)
{
    public TId Id { get; protected set; } = id;

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }
    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}

