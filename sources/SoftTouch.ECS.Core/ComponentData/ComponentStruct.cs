using SoftTouch.ECS.Arrays;

namespace SoftTouch.ECS.ComponentData
{
    public class ComponentStruct<T> : ComponentBase 
        where T : struct, IEquatable<T>
    {
        public T Value;

        public ComponentStruct(T cmp)
        {
            Value = cmp;
        }

        public override object Get()
        {
            return Value;
        }
        public override Type GetComponentType() => typeof(T);

        public override ComponentArray ToArray() => new ComponentArray<T>().With(Value);
        public override ComponentArray EmptyArray() => new ComponentArray<T>();

    }
}