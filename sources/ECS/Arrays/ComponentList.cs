namespace ECSharp.Arrays;

// Copyright (c) Stride contributors (https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

public interface ComponentList : IList
{
    public Type ComponentType {get;}
    public ComponentList New();
    public void TransferTo(ComponentList l, int i);
    public ComponentList Clone();
}

public class ComponentList<T> : List<T>, ComponentList where T : struct
{
    public Span<T> AsSpan() => CollectionsMarshal.AsSpan(this);
    public List<T> AsList() => this;
    public Type ComponentType => typeof(T);

    public T Get(int i)
    {
        return this[i];
    }

    public ComponentList() : base(32){}

    public ComponentList(int capacity) : base(capacity){}

    public ComponentList New()
    {
        return new ComponentList<T>(100);
    }

    public void TransferTo(ComponentList l, int i)
    {
        ((ComponentList<T>)l).Add(this[i]);
        RemoveAt(i);
    }

    public ComponentList Clone()
    {
        return (ComponentList)new List<T>(this);
    }
}