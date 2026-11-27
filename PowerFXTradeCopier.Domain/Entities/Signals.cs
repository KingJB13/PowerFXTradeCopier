using PowerFXTradeCopier.Domain.Common;
using PowerFXTradeCopier.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerFXTradeCopier.Domain.Entities;

#pragma warning disable CS8618
public class Signals : AuditableEntity<Guid>
{
    public int MessageID { get; private set; }
    public DateTime MessageTimeStamp { get; private set; }
    public Symbol Symbol { get; private set; }
    public DirectionType DirectionType { get; private set; }
    public double Entry { get; private set; }
    public double StopLoss { get; private set; }
    public double[] TargetProfit { get; private set; } = [];
    // Running Behavior
    public Result RunningResult { get; private set; }
    // Systematic Behavior
    public Result SignalResult { get; private set; }
    public CloseReason CloseReason { get; private set; }
    public double? ClosingPrice { get; private set; } = null;
    public bool IsClosed { get; private set; } = false;
    // Relationship
    [ForeignKey(nameof(Coach))]
    public Guid CoachID { get; private set; }
    public Coach Coach { get; private set; }

    private Signals(
        Guid id,
        int messageID,
        DateTime messageTimeStamp,
        Symbol symbol,
        DirectionType directionType,
        double entry,
        double stopLoss,
        double[] targetProfit,
        Guid coachID
    ) : base(id)
    {
        MessageID = messageID;
        MessageTimeStamp = messageTimeStamp;
        Symbol = symbol;
        DirectionType = directionType;
        Entry = entry;
        StopLoss = stopLoss;
        TargetProfit = [.. targetProfit];
        CoachID = coachID;
        CloseReason = CloseReason.None;
        RunningResult = Result.Pending;
        SignalResult = Result.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public static Signals Create(
        Guid id,
        int messageID,
        DateTime messageTimeStamp,
        Symbol symbol,
        DirectionType directionType,
        double entry,
        double stopLoss,
        double[] targetProfit,
        Guid coachID
    )
    {
        ArgumentNullException.ThrowIfNull(targetProfit);

        if (messageID <= 0)
            throw new ArgumentException("Invalid message ID.");

        if (coachID == Guid.Empty)
            throw new ArgumentException("Invalid coach ID.");

        return new Signals(
            id,
            messageID,
            messageTimeStamp,
            symbol,
            directionType,
            entry,
            stopLoss,
            targetProfit,
            coachID
        );
    }
    public void Activate()
    {
        EnsureNotClosed();

        if (SignalResult != Result.Pending)
            throw new InvalidOperationException(
                "Signal has already been processed.");

        RunningResult = Result.Active;
        SignalResult = Result.Active;
        UpdateAudit();
    }
    public void CloseAtBreakeven()
    {
        EnsureNotClosed();

        if (RunningResult != Result.BE)
            throw new InvalidOperationException();

        CloseReason = CloseReason.BE;
        ClosingPrice = Entry;
        UpdateAudit();
        IsClosed = true;
    }
    public void CloseAtTakeProfit(double closedAt)
    {
        EnsureNotClosed();
        if (SignalResult != Result.TP5)
            throw new InvalidOperationException(
                "Take profit is not active.");
        RunningResult = Result.TP5;
        CloseReason = CloseReason.FullTP;
        ClosingPrice = closedAt;
        UpdateAudit();
        IsClosed = true;
    }

    public void CloseAtTrailingStop(double closedAt)
    {
        EnsureNotClosed();

        if (RunningResult == Result.Pending ||
            RunningResult == Result.Active ||
            RunningResult == Result.BE ||
            RunningResult == Result.Cancelled ||
            RunningResult == Result.SL)
            {
                throw new InvalidOperationException(
                    "Trailing stop is not active.");
            }
        CloseReason = CloseReason.TSL;
        ClosingPrice = closedAt;
        UpdateAudit();
        IsClosed = true;
    }
    public void SignalTracking(Result target)
    {
        EnsureNotClosed();

        if (SignalResult == Result.Pending ||
            SignalResult == Result.Cancelled ||
            SignalResult == Result.SL)
        {
            throw new InvalidOperationException(
                "Signal cannot reach take profit in its current state.");
        }

        var nextTarget = SignalResult switch
        {
            Result.Active => Result.TP1,
            Result.TP1 => Result.TP2,
            Result.TP2 => Result.TP3,
            Result.TP3 => Result.TP4,
            Result.TP4 => Result.TP5,
            _ => throw new InvalidOperationException(
                "Invalid take profit progression.")
        };

        if (target != nextTarget)
            throw new InvalidOperationException(
                $"Expected {nextTarget}, received {target}.");

        SignalResult = target;

        RunningResult = target switch
        {
            Result.TP1 => Result.BE,
            Result.TP2 => Result.TP1,
            Result.TP3 => Result.TP2,
            Result.TP4 => Result.TP3,
            Result.TP5 => Result.TP4,
            _ => RunningResult
        };
        UpdateAudit();
    }

    public void HitStopLoss()
    {
        EnsureNotClosed();

        if (RunningResult != Result.Active)
            throw new InvalidOperationException(
                "Stop loss cannot be applied in the current state.");

        RunningResult = Result.SL;
        SignalResult = Result.SL;
        CloseReason = CloseReason.SL;
        ClosingPrice = StopLoss;
        UpdateAudit();
        IsClosed = true;
    }

    public void Cancel()
    {
        EnsureNotClosed();

        if (SignalResult != Result.Pending)
            throw new InvalidOperationException(
                "Only pending signals can be cancelled.");

        RunningResult = Result.Cancelled;
        SignalResult = Result.Cancelled;
        CloseReason = CloseReason.Cancelled;
        UpdateAudit();
        IsClosed = true;
    }

    private void EnsureNotClosed()
    {
        if (IsClosed)
            throw new InvalidOperationException(
                "Signal is already closed.");
    }
}
#pragma warning restore CS8618
