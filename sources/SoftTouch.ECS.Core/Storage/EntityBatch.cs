using SoftTouch.ECS.Arrays;

namespace SoftTouch.ECS.Storage;


public struct EntityComponentBatch<T>() : IDisposable
{
    ReusableList<int> EntityIds = [];
    ReusableList<ComponentArray> Components = [];

    public readonly void Dispose()
    {
        EntityIds.Dispose();
        Components.Dispose();
    }
}