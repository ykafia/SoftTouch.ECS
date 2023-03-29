using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;


public delegate void UpdaterFunc<Q1>(Q1 query1)
    where Q1 : Query, new();
public delegate void UpdaterFunc<Q1, Q2>(Q1 query1, Q2 query2)
    where Q1 : Query, new()
    where Q2 : Query, new();
public delegate void UpdaterFunc<Q1, Q2, Q3>(Q1 query1, Q2 query2, Q3 query3)
    where Q1 : Query, new()
    where Q2 : Query, new()
    where Q3 : Query, new();
public delegate void UpdaterFunc<Q1, Q2, Q3, Q4>(Q1 query1, Q2 query2, Q3 query3, Q4 query4)
    where Q1 : Query, new()
    where Q2 : Query, new()
    where Q3 : Query, new()
    where Q4 : Query, new();


public class DelegateProcessor<Q1> : Processor<Q1>
    where Q1 : Query, new()
{
    readonly UpdaterFunc<Q1> func;

    public DelegateProcessor(UpdaterFunc<Q1> func) : base()
    {
        this.func = func;
    }

    public override void Update()
    {
        func.Invoke(Entities1);
    }
}

public class DelegateProcessor<Q1, Q2> : Processor<Q1, Q2>
    where Q1 : Query, new()
    where Q2 : Query, new()
{
    readonly UpdaterFunc<Q1, Q2> func;

    public DelegateProcessor(UpdaterFunc<Q1, Q2> func) : base()
    {
        this.func = func;
    }

    public override void Update()
    {
        func(Entities1, Entities2);
    }
}


public class DelegateProcessor<Q1, Q2, Q3> : Processor<Q1, Q2, Q3>
    where Q1 : Query, new()
    where Q2 : Query, new()
    where Q3 : Query, new()
{
    readonly UpdaterFunc<Q1, Q2, Q3> func;

    public DelegateProcessor(UpdaterFunc<Q1, Q2, Q3> func) : base()
    {
        this.func = func;
    }

    public override void Update()
    {
        func(Entities1, Entities2, Entities3);
    }
}


public class DelegateProcessor<Q1, Q2, Q3, Q4> : Processor<Q1, Q2, Q3, Q4>
    where Q1 : Query, new()
    where Q2 : Query, new()
    where Q3 : Query, new()
    where Q4 : Query, new()
{
    readonly UpdaterFunc<Q1, Q2, Q3, Q4> func;

    public DelegateProcessor(UpdaterFunc<Q1, Q2, Q3, Q4> func) : base()
    {
        this.func = func;
    }

    public override void Update()
    {
        func(Entities1,Entities2,Entities3,Entities4);
    }
}
