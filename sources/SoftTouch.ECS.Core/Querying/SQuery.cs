namespace SoftTouch.ECS.Processors;

public struct QueryDeclaration
{
    public MemoryOwner<Type> With {get; init;}
    public MemoryOwner<Type> Without {get; init;}
    public MemoryOwner<Type> WithAll {get; init;}
    public MemoryOwner<Type> Exact {get; init;}
}