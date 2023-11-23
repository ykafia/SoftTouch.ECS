using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS;

public class WorldResources
{
    Dictionary<Type, object> resources;

    public WorldResources()
    {
        resources = new Dictionary<Type, object>();
    }

    internal void Set<T>(T obj) where T : class
    {
        resources[typeof(T)] = obj;
    }
    public T Get<T>() where T : class
    {
        return (T)resources[typeof(T)];
    }
}
