namespace MechanicShop.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; set; }

    private readonly List<DomainEvent> _domainEvents = []; 



    protected Entity(Guid id)
    {
        this.Id = id == Guid.Empty ? Guid.NewGuid() : id;
    }   
    
    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }   
    

    protected Entity()
    {

    } 
}