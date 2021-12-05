using System;

namespace ECSharp
{
    public partial class ComponentBox
    {
        public virtual object Get() => throw new Exception("Component is empty");
        public virtual Type GetComponentType() => throw new Exception("Component is empty");
        public virtual ComponentArray ToArray() => throw new Exception("Component is empty");
        public virtual ComponentArray EmptyArray() => throw new Exception("Component is empty");

    }

    public class ComponentBox<T> : ComponentBox where T : struct
    {
        public T Component;

        public ComponentBox(T cmp)
        {
            this.Component = cmp;
        }

        public override object Get()
        {
            return Component;
        }
        public override Type GetComponentType() => typeof(T);

        public override ComponentArray ToArray() => new ComponentArray<T>(Component);
        public override ComponentArray EmptyArray() => new ComponentArray<T>();

    }
}