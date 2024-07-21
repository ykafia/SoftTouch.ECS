using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public partial class WorldCommands
{
    public EntityWorld? GetOrSpawn(Entity entity)
    {
        throw new NotImplementedException();
        // #error implement this function
        // if entity exists return EntityWorld using the entity index

        // else if there is an entity with a different generation return null

        // else spawn new one
    }
    public WorldCommands Spawn()
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        Updates.Add(update);
        return this;
    }
    public EntityCommands SpawnEmpty()
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        Updates.Add(update);
        return new(update, this);
    }
    public WorldCommands Spawn<T1>(in T1 component1)
            where T1 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        Updates.Add(update);
        return this;
    }

    public WorldCommands Spawn<T1, T2>(
            in T1 component1,
            in T2 component2
        )
        where T1 : struct
        where T2 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        update.Add(in component2);
        Updates.Add(update);
        return this;
    }
    public WorldCommands Spawn<T1, T2, T3>(
            in T1 component1,
            in T2 component2,
            in T3 component3
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        update.Add(in component2);
        update.Add(in component3);
        Updates.Add(update);
        return this;
    }
    public WorldCommands Spawn<T1, T2, T3, T4>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        update.Add(in component2);
        update.Add(in component3);
        update.Add(in component4);
        Updates.Add(update);
        return this;
    }
    public WorldCommands Spawn<T1, T2, T3, T4, T5>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        update.Add(in component2);
        update.Add(in component3);
        update.Add(in component4);
        update.Add(in component5);
        Updates.Add(update);
        return this;
    }
    public WorldCommands Spawn<T1, T2, T3, T4, T5, T6>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        update.Add(in component2);
        update.Add(in component3);
        update.Add(in component4);
        update.Add(in component5);
        update.Add(in component6);
        Updates.Add(update);
        return this;
    }
    public WorldCommands Spawn<T1, T2, T3, T4, T5, T6, T7>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6,
            in T7 component7
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        update.Add(in component2);
        update.Add(in component3);
        update.Add(in component4);
        update.Add(in component5);
        update.Add(in component6);
        update.Add(in component7);
        Updates.Add(update);
        return this;
    }

    public WorldCommands Spawn<T1, T2, T3, T4, T5, T6, T7, T8>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6,
            in T7 component7,
            in T8 component8
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
    {
        var entity = world.Entities.GetOrCreate();
        var update = new SpawnEntity(entity);
        update.Add(in component1);
        update.Add(in component2);
        update.Add(in component3);
        update.Add(in component4);
        update.Add(in component5);
        update.Add(in component6);
        update.Add(in component7);
        update.Add(in component8);
        Updates.Add(update);
        return this;
    }
    public WorldCommands Spawn<T1>()
        where T1 : struct
    {
        return Spawn<T1>(default);
    }
    public WorldCommands Spawn<T1, T2>()
        where T1 : struct
        where T2 : struct
    {
        return Spawn<T1, T2>(default, default);
    }
    public WorldCommands Spawn<T1, T2, T3>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        return Spawn<T1, T2, T3>(default, default, default);
    }
    public WorldCommands Spawn<T1, T2, T3, T4>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        return Spawn<T1, T2, T3, T4>(default, default, default, default);
    }
    public WorldCommands Spawn<T1, T2, T3, T4, T5>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        return Spawn<T1, T2, T3, T4, T5>(default, default, default, default, default);
    }

    public WorldCommands Spawn<T1, T2, T3, T4, T5, T6>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        return Spawn<T1, T2, T3, T4, T5, T6>(default, default, default, default, default, default);
    }
    public WorldCommands Spawn<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
    {
        return Spawn<T1, T2, T3, T4, T5, T6, T7>(default, default, default, default, default, default, default);
    }
    public WorldCommands Spawn<T1, T2, T3, T4, T5, T6, T7, T8>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
    {
        return Spawn<T1, T2, T3, T4, T5, T6, T7,T8>(default, default, default, default, default, default, default, default);
    }


}