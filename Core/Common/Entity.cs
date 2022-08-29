namespace Vulns.Core;

public abstract class Entity
{
    public abstract string Id { get; set; }
    public abstract DateTime CreatedAt { get; set; }
    public abstract DateTime? ModifiedAt { get; set; }
}