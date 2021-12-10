using System.Numerics;
using ECSharp.ComponentData;


namespace ECSharp.Components
{
    public class ModelComponent : Component
    {
        public List<Vector3> Buffer = new();
        public int Size => Buffer.Count;
    }
}