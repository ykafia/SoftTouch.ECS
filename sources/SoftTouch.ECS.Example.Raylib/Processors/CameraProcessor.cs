using SoftTouch.ECS.Processors;
using Raylib_cs;
using SoftTouch.ECS.Querying;

using static Raylib_cs.Raylib;

namespace SoftTouch.ECS.Example.Rlib;




public class CameraProcessor() : Processor<Query<Write<Camera3D>>>(null!)
{
    public override void Update()
    {
        foreach(var entity in Query)
        {
            ref var camera = ref entity.GetRef<Camera3D>();
            UpdateCamera(ref camera, CameraMode.CAMERA_ORBITAL);
        }
    }
}