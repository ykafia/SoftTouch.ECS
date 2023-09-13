using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;

namespace SoftTouch.ECS.Storage;

public partial class Archetype
{

    public bool IsStrictSupersetOf(Archetype t) => ID.IsStrictSupersetOf(t.ID);
    public bool IsStrictSupersetOf(params Type[] types) => ID.IsStrictSupersetOf(new(types));
    public bool IsSupersetOf(Archetype t) => ID.IsSupersetOf(t.ID);
    public bool IsSupersetOf(Span<Type> types) => ID.IsSupersetOf(types);
    public bool IsStrictSubsetOf(Archetype t) => ID.IsStrictSubsetOf(t.ID);
    public bool IsStrictSubsetOf(Span<Type> types) => ID.IsStrictSubsetOf(types);

    public void TypeIntersect(Archetype t, out Type[] types) => ID.Intersect(t.ID, out types);
    public void TypeExcept(Archetype t, out Type[] types) => ID.Except(t.ID, out types);



    public bool IsStrictSupersetOf<T1>()
    {
        return ID.Count > 1 && ID.Types.Contains(typeof(T1));
    }
    public bool IsStrictSupersetOf<T1, T2>()
    {
        return
            ID.Count > 2
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2));
    }
    public bool IsStrictSupersetOf<T1, T2, T3>()
    {
        return
            ID.Count > 3
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3));
    }
    public bool IsStrictSupersetOf<T1, T2, T3, T4>()
    {
        return
            ID.Count > 4
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4));
    }
    public bool IsStrictSupersetOf<T1, T2, T3, T4, T5>()
    {
        return
            ID.Count > 5
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4))
            && ID.Types.Contains(typeof(T5));
    }
    public bool IsStrictSupersetOf<T1, T2, T3, T4, T5, T6>()
    {
        return
            ID.Count > 6
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4))
            && ID.Types.Contains(typeof(T5))
            && ID.Types.Contains(typeof(T6));
    }
    public bool IsSupersetOf<T1>()
    {
        return ID.Count >= 1 && ID.Types.Contains(typeof(T1));
    }
    public bool IsSupersetOf<T1, T2>()
    {
        return
            ID.Count >= 2
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2));
    }
    public bool IsSupersetOf<T1, T2, T3>()
    {
        return
            ID.Count >= 3
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3));
    }
    public bool IsSupersetOf<T1, T2, T3, T4>()
    {
        return
            ID.Count >= 4
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4));
    }
    public bool IsSupersetOf<T1, T2, T3, T4, T5>()
    {
        return
            ID.Count >= 5
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4))
            && ID.Types.Contains(typeof(T5));
    }
    public bool IsSupersetOf<T1, T2, T3, T4, T5, T6>()
    {
        return
            ID.Count >= 6
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4))
            && ID.Types.Contains(typeof(T5))
            && ID.Types.Contains(typeof(T6));
    }
    public bool IsSupersetOf<T1, T2, T3, T4, T5, T6,T7>()
    {
        return
            ID.Count >= 6
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4))
            && ID.Types.Contains(typeof(T5))
            && ID.Types.Contains(typeof(T6))
            && ID.Types.Contains(typeof(T7));
    }
    public bool IsSupersetOf<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        return
            ID.Count >= 6
            && ID.Types.Contains(typeof(T1))
            && ID.Types.Contains(typeof(T2))
            && ID.Types.Contains(typeof(T3))
            && ID.Types.Contains(typeof(T4))
            && ID.Types.Contains(typeof(T5))
            && ID.Types.Contains(typeof(T6))
            && ID.Types.Contains(typeof(T7))
            && ID.Types.Contains(typeof(T8));
    }
}