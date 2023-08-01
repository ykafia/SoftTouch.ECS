using System.Numerics;

namespace SoftTouch.ECS.Shared.Components
{
    public record struct TransformComponent
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
    }
    public record struct TransformTRSComponent
    {
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
    }
}