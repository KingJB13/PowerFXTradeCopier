using Microsoft.Extensions.Logging;

namespace PowerFXTradeCopier.Infrastructure.Notifier.Hubs;

public class SignalHub : BaseHub<SignalHub>
{
    public SignalHub(ILogger<SignalHub> logger)
        : base(logger)
    {
    }
}
