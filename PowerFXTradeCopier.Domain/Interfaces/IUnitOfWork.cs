using PowerFXTradeCopier.Domain.Interfaces.Features;

namespace PowerFXTradeCopier.Domain.Interfaces;

public interface IUnitOfWork
{
    ISignalRepository Signals { get; }
    ISignalProviderRepository SignalProvider { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
    );
}
