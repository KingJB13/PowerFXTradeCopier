using PowerFXTradeCopier.Domain.Entities;

namespace PowerFXTradeCopier.Domain.Interfaces.Features;

public interface ISignalProviderRepository
{
    Task<bool> DisableSignalProvider(Guid Id);
    Task<bool> EnableSignalProvider(Guid Id);
    Task<SignalProvider> UpdateSignalProvider(CancellationToken cancellationToken = default);
    Task<IEnumerable<SignalProvider>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SignalProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(SignalProvider signalProvider, CancellationToken cancellationToken = default);
    void Update(SignalProvider signalProvider);
    void Remove(SignalProvider signalProvider);
}
