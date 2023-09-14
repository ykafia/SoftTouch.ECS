using CommunityToolkit.HighPerformance.Buffers;


namespace SoftTouch.ECS.Querying;

public struct QueryDeclaration
{
    MemoryOwner<Type> with;
    MemoryOwner<Type> without;
    MemoryOwner<Type> withAll;
    MemoryOwner<Type> withOnly;
}

