using System;

namespace WonkECS
{
    public partial class ComponentBox
    {
        public virtual object? Get() => null;
        public virtual Type? GetComponentType() => null;
        public virtual ComponentArray? ToArray() => null;
        public virtual ComponentArray? EmptyArray() => null;

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