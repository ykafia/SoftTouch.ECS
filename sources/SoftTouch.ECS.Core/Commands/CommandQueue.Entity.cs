using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public partial class WorldCommands : Queue<ComponentUpdate>
{
    public EntityCommands Spawn()
    {
        var id = new ArchetypeID();
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
        }
        else
        {
            world.Archetypes[id] = Archetype.CreateEmpty(world);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count - 1];
    }
    public EntityCommands Spawn<T1>(in T1 component1)
            where T1 : struct, IEquatable<T1>
    {
        var id = new ArchetypeID(typeof(T1));
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
            Enqueue(new ComponentAdd<T1>(in component1, in entity));
        }
        else
        {
            world.Archetypes[id] = Archetype.Create(world, id, world.Entities.Count, in component1);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count - 1];
    }

    public EntityCommands Spawn<T1, T2>(
            in T1 component1,
            in T2 component2
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
    {
        var id = new ArchetypeID(typeof(T1), typeof(T2));
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
            Enqueue(new ComponentAdd<T1>(in component1, in entity));
            Enqueue(new ComponentAdd<T2>(in component2, in entity));
        }
        else
        {
            world.Archetypes[id] = Archetype.Create(world, id, world.Entities.Count, in component1, in component2);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count -1];
    }
    public EntityCommands Spawn<T1, T2, T3>(
            in T1 component1,
            in T2 component2,
            in T3 component3
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
    {
        var id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3));
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
            Enqueue(new ComponentAdd<T1>(in component1, in entity));
            Enqueue(new ComponentAdd<T2>(in component2, in entity));
            Enqueue(new ComponentAdd<T3>(in component3, in entity));
        }
        else
        {
            world.Archetypes[id] = Archetype.Create(world, id, world.Entities.Count, in component1, in component2, in component3);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count -1];
    }
    public EntityCommands Spawn<T1, T2, T3, T4>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
    {
        var id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
            Enqueue(new ComponentAdd<T1>(in component1, in entity));
            Enqueue(new ComponentAdd<T2>(in component2, in entity));
            Enqueue(new ComponentAdd<T3>(in component3, in entity));
            Enqueue(new ComponentAdd<T4>(in component4, in entity));
        }
        else
        {
            world.Archetypes[id] = Archetype.Create(world, id, world.Entities.Count, in component1, in component2, in component3, in component4);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count -1];
    }
    public EntityCommands Spawn<T1, T2, T3, T4, T5>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
    {
        var id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
            Enqueue(new ComponentAdd<T1>(in component1, in entity));
            Enqueue(new ComponentAdd<T2>(in component2, in entity));
            Enqueue(new ComponentAdd<T3>(in component3, in entity));
            Enqueue(new ComponentAdd<T4>(in component4, in entity));
            Enqueue(new ComponentAdd<T5>(in component5, in entity));
        }
        else
        {
            world.Archetypes[id] = Archetype.Create(world, id, world.Entities.Count, in component1, in component2, in component3, in component4, in component5);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count -1];
    }
    public EntityCommands Spawn<T1, T2, T3, T4, T5, T6>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
    {
        var id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
            Enqueue(new ComponentAdd<T1>(in component1, in entity));
            Enqueue(new ComponentAdd<T2>(in component2, in entity));
            Enqueue(new ComponentAdd<T3>(in component3, in entity));
            Enqueue(new ComponentAdd<T4>(in component4, in entity));
            Enqueue(new ComponentAdd<T5>(in component5, in entity));
            Enqueue(new ComponentAdd<T6>(in component6, in entity));
        }
        else
        {
            world.Archetypes[id] = Archetype.Create(world, id, world.Entities.Count, in component1, in component2, in component3, in component4, in component5, in component6);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count -1];
    }
    public EntityCommands Spawn<T1, T2, T3, T4, T5, T6, T7>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6,
            in T7 component7
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
        where T7 : struct, IEquatable<T7>
    {
        var id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        if (world.Archetypes.TryGetValue(id, out var arch))
        {
            var entity = new Entity(world.Entities.Count, arch);
            world.Entities.Add(entity);
            Enqueue(new ComponentAdd<T1>(in component1, in entity));
            Enqueue(new ComponentAdd<T2>(in component2, in entity));
            Enqueue(new ComponentAdd<T3>(in component3, in entity));
            Enqueue(new ComponentAdd<T4>(in component4, in entity));
            Enqueue(new ComponentAdd<T5>(in component5, in entity));
            Enqueue(new ComponentAdd<T6>(in component6, in entity));
            Enqueue(new ComponentAdd<T7>(in component7, in entity));
        }
        else
        {
            world.Archetypes[id] = Archetype.Create(world, id, world.Entities.Count, in component1, in component2, in component3, in component4, in component5, in component6, in component7);
            world.Entities.Add(new Entity(world.Entities.Count, world.Archetypes[id]));
        }
        return world.Entities[world.Entities.Count -1];
    }
    public EntityCommands Spawn<T1>()
        where T1 : struct, IEquatable<T1>
    {
        return Spawn<T1>(default);
    }
    public EntityCommands Spawn<T1, T2>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
    {
        return Spawn<T1, T2>(default, default);
    }
    public EntityCommands Spawn<T1, T2, T3>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
    {
        return Spawn<T1, T2, T3>(default, default, default);
    }
    public EntityCommands Spawn<T1, T2, T3, T4>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
    {
        return Spawn<T1, T2, T3, T4>(default, default, default, default);
    }
    public EntityCommands Spawn<T1, T2, T3, T4, T5>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
    {
        return Spawn<T1, T2, T3, T4, T5>(default, default, default, default, default);
    }

    public EntityCommands Spawn<T1, T2, T3, T4, T5, T6>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
    {
        return Spawn<T1, T2, T3, T4, T5, T6>(default, default, default, default, default, default);
    }
    public EntityCommands Spawn<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
        where T7 : struct, IEquatable<T7>
    {
        return Spawn<T1, T2, T3, T4, T5, T6, T7>(default, default, default, default, default, default, default);
    }


}