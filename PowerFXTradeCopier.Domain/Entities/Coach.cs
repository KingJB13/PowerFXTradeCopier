using PowerFXTradeCopier.Domain.Common;

namespace PowerFXTradeCopier.Domain.Entities;

#pragma warning disable CS8618
public class Coach : Entity<Guid>
{
    public string TopicName { get; private set; }
    public int TopicID { get; private set; }
    public int SenderID { get; private set; }
    public bool IsEnabled { get; private set; }

    //Relationship
    private readonly List<Signals> _signals = [];
    public IReadOnlyCollection<Signals> Signal => _signals.AsReadOnly();

    private Coach(
        Guid id, 
        string topicName, 
        int topicID, 
        int senderID, 
        bool isEnabled = true
    ) : base(id)
    {
        TopicName = topicName;
        TopicID = topicID;
        SenderID = senderID;
        IsEnabled = isEnabled;
    }

    public static Coach Create(
        Guid id, 
        string topicName, 
        int topicID, 
        int senderID, 
        bool isEnabled
    )
    {
        if (string.IsNullOrWhiteSpace(topicName))
            throw new ArgumentException("Topic name is required.");

        if (topicID <= 0)
            throw new ArgumentException("Invalid topic ID.");

        if (senderID <= 0)
            throw new ArgumentException("Invalid sender ID.");

        return new Coach(
            id, 
            topicName, 
            topicID, 
            senderID, 
            isEnabled
        );
    }

    public void Update(
        string topicName, 
        int topicID, 
        int senderID
    )
    {
        if (string.IsNullOrWhiteSpace(topicName))
            throw new ArgumentException("Coach name is required.");

        if (topicID <= 0)
            throw new ArgumentException("Invalid topic ID.");

        if (senderID <= 0)
            throw new ArgumentException("Invalid sender ID.");

        TopicName = topicName;
        TopicID = topicID;
        SenderID = senderID;
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }
}
#pragma warning restore CS8618