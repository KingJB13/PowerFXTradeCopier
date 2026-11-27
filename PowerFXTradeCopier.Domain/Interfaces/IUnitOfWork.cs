using PowerFXTradeCopier.Domain.Interfaces.Features;

namespace PowerFXTradeCopier.Domain.Interfaces;

public interface IUnitOfWork
{
    ISignalRepository Signals { get; }
    ICoachRepository Coaches { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
    );
}
