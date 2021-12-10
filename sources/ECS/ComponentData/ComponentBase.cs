using System;
using ECSharp.Arrays;

namespace ECSharp.ComponentData
{
    public partial class ComponentBase
    {
        public virtual object Get() => throw new Exception("Component is empty");
        public virtual Type GetComponentType() => throw new Exception("Component is empty");
        public virtual ComponentArrayBase ToArray() => throw new Exception("Component is empty");
        public virtual ComponentArrayBase EmptyArray() => throw new Exception("Component is empty");
    }
}