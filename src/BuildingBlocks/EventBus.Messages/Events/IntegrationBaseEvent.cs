namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id, DateTime createDate)
    {
        Id = id;
        CreationDate = createDate;
    }
    
    public Guid Id { get; }

    public DateTime CreationDate { get; }
}