using ECSharp;
using ECSharp.ComponentData;

namespace ECSharp.Arrays
{
    public partial class ComponentArrayBase
    {
        public virtual string StringRepresentation() => "";
        public virtual void AddComponents(List<object> components){}
        public virtual void Add(Component c){}
        public virtual void Add(object c){}
        public virtual ComponentBase RemoveAt(int i) => null;
        public virtual ComponentArrayBase New(Component c) => null;
        public virtual ComponentArrayBase New(object c) => null;
        public static ComponentArrayBase NewArrayStruct<T>() where T : struct => new ComponentArrayStruct<T>();
        public static ComponentArrayBase NewArray<T>() where T : Component => new ComponentArray<T>();
        
        public virtual Type GetElementType() => GetType();
        public virtual void Merge(ComponentArrayBase other){}
        public virtual ComponentArrayBase EmptyFrom(ComponentArrayBase array) => new();
        public virtual int GetLength() => 0;
        public virtual List<object> GetArray() => new();
    }
}