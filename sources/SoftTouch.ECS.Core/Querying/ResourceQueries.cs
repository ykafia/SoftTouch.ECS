using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public interface IResourceQuery : IWorldQuery
{
}


public struct Resource<T> : IWorldQuery
    where T : class
{
    public World World { get; set; }
    public T Content => World.Resources.Get<T>();

    public static implicit operator T(Resource<T> resource) => resource.Content;
}