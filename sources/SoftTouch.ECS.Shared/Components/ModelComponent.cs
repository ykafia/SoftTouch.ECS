using System.Numerics;
using SoftTouch.ECS.ComponentData;


namespace SoftTouch.ECS.Shared.Components
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