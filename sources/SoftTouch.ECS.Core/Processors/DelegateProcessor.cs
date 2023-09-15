using SoftTouch.ECS.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;


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


public class DelegateProcessor<Q1> : Processor<Q1>
    where Q1 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1> func;

    public DelegateProcessor(UpdaterFunc<Q1> func) : base(null!)
    {
        this.func = func;
    }

    public override void Update()
    {
        func.Invoke(Query);
    }
}

public class DelegateProcessor<Q1, Q2> : Processor<Q1, Q2>
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1, Q2> func;

    public DelegateProcessor(UpdaterFunc<Q1, Q2> func) : base(null!)
    {
        this.func = func;
    }

    public override void Update()
    {
        func(Query1, Query2);
    }
}


public class DelegateProcessor<Q1, Q2, Q3> : Processor<Q1, Q2, Q3>
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1, Q2, Q3> func;

    public DelegateProcessor(UpdaterFunc<Q1, Q2, Q3> func) : base(null!)
    {
        this.func = func;
    }

    public override void Update()
    {
        func(Query1, Query2, Query3);
    }
}


public class DelegateProcessor<Q1, Q2, Q3, Q4> : Processor<Q1, Q2, Q3, Q4>
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
    where Q4 : struct, IWorldQuery
{
    readonly UpdaterFunc<Q1, Q2, Q3, Q4> func;

    public DelegateProcessor(UpdaterFunc<Q1, Q2, Q3, Q4> func) : base(null!)
    {
        this.func = func;
    }

    public override void Update()
    {
        func(Query1, Query2, Query3, Query4);
    }
}
