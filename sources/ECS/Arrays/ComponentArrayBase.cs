using ECSharp;
using ECSharp.ComponentData;

namespace ECSharp.Arrays
{
    public abstract class ComponentArrayBase
    {
        public abstract string StringRepresentation();
        public abstract void AddComponents(List<ComponentBase> components);
        public abstract void Add(ComponentBase c);
        public abstract ComponentBase RemoveAt(int i);
        public abstract ComponentArrayBase New(ComponentBase c);
        public abstract ComponentArrayBase New(Type t);
        public static ComponentArrayBase NewArrayStruct<T>() where T : struct => new ComponentArrayStruct<T>();
        public abstract void TransferTo(ComponentArrayBase other, int index);
        public abstract Type GetElementType();
        
        public virtual int GetLength() => 0;
    }
}