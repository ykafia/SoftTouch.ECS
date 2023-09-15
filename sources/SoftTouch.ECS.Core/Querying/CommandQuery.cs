using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public interface ICommandQuery : IWorldQuery
{
}


public partial struct Commands : IWorldQuery
{
    public World World { get; set; }

    public WorldCommands Content => World.Resources.Get<WorldCommands>();

    public EntityCommands Spawn() => Content.Spawn();
    public void Spawn<T>() where T : struct, IEquatable<T> => Content.Spawn<T>();

    public void Spawn<T1, T2>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        => Content.Spawn<T1, T2>();

    public void Spawn<T1, T2, T3>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        => Content.Spawn<T1, T2, T3>();

    public void Spawn<T1, T2, T3, T4>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        => Content.Spawn<T1, T2, T3, T4>();

    public void Spawn<T1, T2, T3, T4, T5>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        => Content.Spawn<T1, T2, T3, T4, T5>();

    public void Spawn<T1, T2, T3, T4, T5, T6>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
        => Content.Spawn<T1, T2, T3, T4, T5, T6>();

    public void Spawn<T1,T2,T3,T4,T5,T6,T7>() 
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
        where T7 : struct, IEquatable<T7>
        => Content.Spawn<T1, T2, T3, T4, T5, T6, T7>();
}