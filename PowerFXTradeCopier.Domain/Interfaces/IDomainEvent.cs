namespace PowerFXTradeCopier.Domain.Interfaces;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}
