using System.Numerics;


namespace SoftTouch.ECS.Shared.Components
{
    public record struct ModelComponent
    {
        public List<Vector3> Buffer = new();
        public int Size => Buffer.Count;

        public ModelComponent()
        {
        }        
    }
}