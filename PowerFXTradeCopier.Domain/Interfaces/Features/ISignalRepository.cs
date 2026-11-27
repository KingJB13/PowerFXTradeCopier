using PowerFXTradeCopier.Domain.Entities;
using PowerFXTradeCopier.Domain.Enums;

namespace PowerFXTradeCopier.Domain.Interfaces.Features;

public interface ISignalRepository
{
    Task<bool> CancelSignal(Guid Id);
    Task<bool> BreakevenHit(Guid Id);
    Task<bool> StopLossHit(Guid Id);
    Task<bool> DetectActiveSignal(Guid Id);
    Task<bool> TrailingStopLossHit(Guid Id, double ClosingPrice);
    Task<Signals> SignalTracking(Guid Id, Result result);
    Task<bool> FullTPHit(Guid Id, double ClosingPrice);


    Task<Signals?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Signals>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Signals signal, CancellationToken cancellationToken = default);
    void Update(Signals signal);
    void Remove(Signals signal);
}
