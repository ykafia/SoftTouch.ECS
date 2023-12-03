using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public partial class World
{
    internal void AddArchetypeUpdate(ComponentUpdates component)
    {
        throw new NotImplementedException();
        //Resources.Get<WorldCommands>().Add(component);
    }
    internal void ApplyUpdates() => Resources.Get<WorldCommands>().ExecuteUpdates();


}