using System;
using ECSharp.Arrays;

namespace ECSharp.ComponentData
{
    public abstract class ComponentBase
    {
        public abstract object Get();
        public abstract Type GetComponentType();
        public abstract ComponentArrayBase ToArray();
        public abstract ComponentArrayBase EmptyArray();
    }
}