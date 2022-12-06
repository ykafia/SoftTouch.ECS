using SoftTouch.ECS.Arrays;

namespace SoftTouch.ECS.ComponentData
{
    public class ComponentStruct<T> : ComponentBase 
        where T : struct
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

        public override ComponentList ToArray() => new ComponentList<T>(){Value};
        public override ComponentList EmptyArray() => new ComponentList<T>();

    }
}