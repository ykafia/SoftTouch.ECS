using System.Numerics;
using ECSharp.ComponentData;


namespace ECSharp.Components
{
    public struct ModelComponent
    {
        public List<Vector3> Buffer = new();
        public int Size => Buffer.Count;

        public ModelComponent()
        {
        }        
    }
}