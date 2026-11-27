using PowerFXTradeCopier.Domain.Entities;

namespace PowerFXTradeCopier.Domain.Interfaces.Features;

public interface ICoachRepository
{
    Task<bool> DisableCoach(Guid Id);
    Task<bool> EnableCoach(Guid Id);
    Task<Coach> UpdateCoach(CancellationToken cancellationToken = default);
    Task<IEnumerable<Coach>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Coach?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Coach coach, CancellationToken cancellationToken = default);
    void Update(Coach coach);
    void Remove(Coach coach);
}
