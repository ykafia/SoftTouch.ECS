﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Querying;

public interface ICommandQuery : IWorldQuery;


public partial struct Commands : ICommandQuery
{
    public World World { get; set; }

    public Processor CallingProcessor { get; init; }
    public readonly WorldCommands Content => World.Resources.Get<WorldCommands>();

    public readonly EntityCommands Spawn() => Content.SpawnEmpty();

    public readonly EntityCommands Spawn<T1>(in T1 component1)
            where T1 : struct
        => Content.Spawn(component1);

    public readonly EntityCommands Spawn<T1, T2>(
            in T1 component1,
            in T2 component2
        )
        where T1 : struct
        where T2 : struct
        => Content.Spawn(component1, component2);
    public readonly EntityCommands Spawn<T1, T2, T3>(
            in T1 component1,
            in T2 component2,
            in T3 component3
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        => Content.Spawn(component1, component2, component3);

    public readonly EntityCommands Spawn<T1, T2, T3, T4>(
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        => Content.Spawn(component1, component2, component3, component4);

    public readonly EntityCommands Spawn<T1, T2, T3, T4, T5>(
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
        => Content.Spawn(component1, component2, component3, component4, component5);

    public readonly EntityCommands Spawn<T1, T2, T3, T4, T5, T6>(
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
        => Content.Spawn(component1, component2, component3, component4, component5, component6);

    public readonly EntityCommands Spawn<T1, T2, T3, T4, T5, T6, T7>(
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
        => Content.Spawn(component1, component2, component3, component4, component5, component6, component7);


    public readonly EntityCommands Spawn<T1, T2, T3, T4, T5, T6, T7, T8>(
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
        => Content.Spawn(component1, component2, component3, component4, component5, component6, component7, component8);
    public readonly void Spawn<T>() where T : struct, IEquatable<T> => Content.Spawn<T>();
    public readonly void Spawn<T1, T2>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        => Content.Spawn<T1, T2>();

    public readonly void Spawn<T1, T2, T3>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        => Content.Spawn<T1, T2, T3>();

    public readonly void Spawn<T1, T2, T3, T4>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        => Content.Spawn<T1, T2, T3, T4>();

    public readonly void Spawn<T1, T2, T3, T4, T5>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        => Content.Spawn<T1, T2, T3, T4, T5>();

    public readonly void Spawn<T1, T2, T3, T4, T5, T6>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
        => Content.Spawn<T1, T2, T3, T4, T5, T6>();

    public readonly void Spawn<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
        where T7 : struct, IEquatable<T7>
        => Content.Spawn<T1, T2, T3, T4, T5, T6, T7>();

    public readonly void Spawn<T>(T comp) where T : struct, IEquatable<T> => Content.Spawn(comp);
}