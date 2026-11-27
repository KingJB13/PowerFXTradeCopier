using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace PowerFXTradeCopier.Infrastructure.Notifier.Hubs;

public abstract class BaseHub<THub> : Hub
    where THub : Hub
{
    private readonly ILogger<THub> _logger;

    protected BaseHub(ILogger<THub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation(
            "Client connected to {HubName}: {ConnectionId}",
            typeof(THub).Name,
            Context.ConnectionId
        );

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (exception is not null)
        {
            _logger.LogWarning(
                exception,
                "Client disconnected unexpectedly from {HubName}: {ConnectionId}",
                typeof(THub).Name,
                Context.ConnectionId
            );
        }
        else
        {
            _logger.LogInformation(
                "Client disconnected from {HubName}: {ConnectionId}",
                typeof(THub).Name,
                Context.ConnectionId
            );
        }

        await base.OnDisconnectedAsync(exception);
    }
}