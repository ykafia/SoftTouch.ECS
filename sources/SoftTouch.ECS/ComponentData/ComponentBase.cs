using System;
using SoftTouch.ECS.Arrays;

namespace SoftTouch.ECS.ComponentData
{
    public abstract class ComponentBase
    {
        public abstract object Get();
        public abstract Type GetComponentType();
        public abstract ComponentList ToArray();
        public abstract ComponentList EmptyArray();
    }
}