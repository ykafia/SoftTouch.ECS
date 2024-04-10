using SoftTouch.ECS.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;

public delegate void UpdaterFunc();
public delegate void UpdaterFunc<Q1>(Q1 query1)
    where Q1 : struct, IWorldQuery;
public delegate void UpdaterFunc<Q1, Q2>(Q1 query1, Q2 query2)
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery;
public delegate void UpdaterFunc<Q1, Q2, Q3>(Q1 query1, Q2 query2, Q3 query3)
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery;
public delegate void UpdaterFunc<Q1, Q2, Q3, Q4>(Q1 query1, Q2 query2, Q3 query3, Q4 query4)
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
    where Q4 : struct, IWorldQuery;


public class DelegateProcessor<Q1>(UpdaterFunc<Q1> func) : Processor<Q1>(null!)
    where Q1 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1> func = func;

    public override void Update()
    {
        func.Invoke(Query);
    }

    public static implicit operator DelegateProcessor<Q1>(UpdaterFunc<Q1> updaterFunc) => new(updaterFunc);
}

public class DelegateProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func) : Processor<Q1, Q2>(null!)
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1, Q2> func = func;

    public override void Update()
    {
        func(Query1, Query2);
    }

    public static implicit operator DelegateProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> updaterFunc) => new(updaterFunc);

}


public class DelegateProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func) : Processor<Q1, Q2, Q3>(null!)
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1, Q2, Q3> func = func;

    public override void Update()
    {
        func(Query1, Query2, Query3);
    }
    public static implicit operator DelegateProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> updaterFunc) => new(updaterFunc);
}


public class DelegateProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func) : Processor<Q1, Q2, Q3, Q4>(null!)
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
    where Q4 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1, Q2, Q3, Q4> func = func;

    public override void Update()
    {
        func(Query1, Query2, Query3, Query4);
    }

    public static implicit operator DelegateProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> updaterFunc) => new(updaterFunc);

}
