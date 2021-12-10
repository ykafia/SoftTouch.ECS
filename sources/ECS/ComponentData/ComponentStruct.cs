using ECSharp.Arrays;

namespace ECSharp.ComponentData
{
    public class ComponentStruct<T> : ComponentBase 
        where T : struct
    {
        public T Value;

        public ComponentStruct(T cmp)
        {
            this.Value = cmp;
        }

        public override object Get()
        {
            return Value;
        }
        public override Type GetComponentType() => typeof(T);

        public override ComponentArrayBase ToArray() => new ComponentArrayStruct<T>(Value);
        public override ComponentArrayBase EmptyArray() => new ComponentArrayStruct<T>();

    }
}