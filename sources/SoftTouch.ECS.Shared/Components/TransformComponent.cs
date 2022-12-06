using System.Numerics;

namespace SoftTouch.ECS.Components
{
    public struct TransformComponent
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
    }
    public struct TransformTRSComponent
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
    }
}