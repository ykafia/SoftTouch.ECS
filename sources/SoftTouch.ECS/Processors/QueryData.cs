using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;

internal readonly ref struct CmpIdx
{
    public readonly Archetype Archetype;
    public readonly int Index;
    public CmpIdx(Archetype A, int Idx)
    {
        Archetype = A;
        Index = Idx;
    }
}

public ref struct Components<T1>
    where T1 : struct
{
    public ref T1 Component1;

    public Components(ref T1 component1)
    {
        Component1 = ref component1;
    }
}
public ref struct Components<T1, T2>
    where T1 : struct
    where T2 : struct
{
    public ref T1 Component1;
    public ref T2 Component2;

    public Components(ref T1 component1, ref T2 component2)
    {
        Component1 = ref component1;
        Component2 = ref component2;
    }
}
public ref struct Components<T1, T2, T3>
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    public ref T1 Component1;
    public ref T2 Component2;
    public ref T3 Component3;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
    }
}
public ref struct Components<T1, T2, T3, T4>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    public ref T1 Component1;
    public ref T2 Component2;
    public ref T3 Component3;
    public ref T4 Component4;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
    }
}
public ref struct Components<T1, T2, T3, T4,T5>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
{
    public ref T1 Component1;
    public ref T2 Component2;
    public ref T3 Component3;
    public ref T4 Component4;
    public ref T5 Component5;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
    }
}

public ref struct Components<T1, T2, T3, T4, T5, T6>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
    where T6 : struct
{
    public ref T1 Component1;
    public ref T2 Component2;
    public ref T3 Component3;
    public ref T4 Component4;
    public ref T5 Component5;
    public ref T6 Component6;

    public Components(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, ref T6 component6)
    {
        Component1 = ref component1;
        Component2 = ref component2;
        Component3 = ref component3;
        Component4 = ref component4;
        Component5 = ref component5;
        Component6 = ref component6;
    }
}