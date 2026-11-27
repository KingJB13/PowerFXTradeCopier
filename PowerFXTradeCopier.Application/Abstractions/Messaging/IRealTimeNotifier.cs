using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFXTradeCopier.Application.Abstractions.Messaging;

public interface IRealTimeNotifier
{
    Task SendToAllAsync<T>(
        string eventName,
        T data,
        CancellationToken cancellationToken = default
    );
    Task SendToUserAsync<T>(
        string userId,
        string eventName,
        T data,
        CancellationToken cancellationToken = default
    );
}
