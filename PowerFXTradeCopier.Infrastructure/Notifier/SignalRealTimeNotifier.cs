using Microsoft.AspNetCore.SignalR;
using PowerFXTradeCopier.Application.Abstractions.Messaging;
using PowerFXTradeCopier.Infrastructure.Notifier.Hubs;


namespace PowerFXTradeCopier.Infrastructure.Notifier;

public class SignalRealTimeNotifier(IHubContext<SignalHub> hubContext) : IRealTimeNotifier
{
    public Task SendToAllAsync<T>(
        string eventName,
        T data,
        CancellationToken cancellationToken = default)
    {
        return hubContext.Clients.All.SendAsync(
            eventName,
            data,
            cancellationToken
        );
    }

    public Task SendToUserAsync<T>(
        string userId,
        string eventName,
        T data,
        CancellationToken cancellationToken = default)
    {
        return hubContext.Clients.User(userId).SendAsync(
            eventName,
            data,
            cancellationToken
        );
    }

}
