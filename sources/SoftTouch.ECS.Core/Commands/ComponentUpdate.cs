using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public abstract class ComponentUpdate
{
    public Entity EntityRecord {get;set;}
    public abstract void UpdateRecord();
}
