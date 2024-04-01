using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.ECS.Arrays;

[DebuggerDisplay("ComponentsArray<{ComponentType.Name.ToString()}>[{Count}]")]
public abstract class ComponentArray : IDisposable
{
    public abstract Type ComponentType { get; }
    public int Count { get; protected set; }
    public abstract bool TryAdd(ComponentBox item);
    public abstract bool TryAdd<TOther>(TOther item) where TOther : struct, IEquatable<TOther>;
    public abstract bool TryRemove<TOther>(TOther item) where TOther : struct, IEquatable<TOther>;
    public abstract bool TryRemoveAt<TOther>(int index, out TOther item) where TOther : struct, IEquatable<TOther>;
    public abstract bool RemoveAt(int index);
    public abstract void Clear();
    public abstract ComponentArray Create();

    public abstract void MoveTo(int index, ComponentArray componentArray);
    public abstract void CopyTo(ComponentArray componentArray);
    public abstract ComponentArray Clone();

    public abstract ComponentBox GetComponent(int idx);

    public abstract void Dispose();
}


