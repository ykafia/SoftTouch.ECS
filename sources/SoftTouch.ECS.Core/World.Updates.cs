using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public partial class World
{
    internal void ApplyUpdates()
    {
        Resources.Get<WorldCommands>().ExecuteUpdates();
    }
}