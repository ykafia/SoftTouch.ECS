using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Storage;


public class EntityComponent() : IResettable
{
    public virtual bool TryReset()
    {
        return false;
    }
}

public class EntityComponent<T>(T value) : EntityComponent
    where T : struct
{
    public T Value { get; set; } = value;
    public override bool TryReset() => true;
}

public class EntitySpawner : IResettable, IDisposable
{
    public World? World { get; set; }
    readonly List<EntityComponent> Components = [];

    public bool TryReset()
    {
        World = null;
        Components.Clear();
        return true;
    }

    public EntitySpawner With<T>()
        where T : struct
    {
        foreach (var c in Components)
            if (c is EntityComponent<T>) return this;
        Components.Add(new EntityComponent<T>(default));
        return this;
    }

    public void Dispose() => SpawnerPool.Return(this);
}

public class SpawnerPool
{
    static readonly ObjectPool<EntitySpawner> pool = ObjectPool.Create<EntitySpawner>();

    public static EntitySpawner GetFor(World world)
    {
        var s = pool.Get();
        s.World = world;
        return s;
    }

    public static void Return(EntitySpawner spawner)
    {
        pool.Return(spawner);
    }
}