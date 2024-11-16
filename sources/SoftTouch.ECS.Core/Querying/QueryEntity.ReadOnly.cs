using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS.Querying;

public readonly ref struct ReadonlyQueryEntity<Q>(Archetype archetype, int archetypeIndex, Q query)
    where Q : struct, IEntityQuery
{
    readonly QueryEntity<Q> queryEntity = new();
    public readonly EntityMeta EntityIndex => query.World[archetype.EntityLookup[archetypeIndex]];
    public readonly Entity Entity => EntityIndex;

    public ref T Get<T>()
        where T : struct
        => ref queryEntity.Get<T>();

}
