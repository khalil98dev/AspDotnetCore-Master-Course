namespace MechanicShop.Domain.Common;

public abstract class AuditableEntity : Entity
{

    protected AuditableEntity(Guid id) : base(id)
    {

    }

    protected AuditableEntity()
    {

    }

    public int Id { get; set; }


    public DateTimeOffset CreatedAtUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAtUtc { get; set; }
    public string? UpdatedBy { get; set; }
    

}
