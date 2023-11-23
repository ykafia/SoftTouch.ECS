using System.Numerics;
using Raylib_cs;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;


public class RenderCubes() : Processor<Query<Write<Model,Transform>>, Query<Write<Light>>, Query<Write<Camera3D>>>(null!)
{
    public override void Update()
    {
        Camera3D camera = new();
        foreach(var e in Query3)
        {
            camera = e.Get<Camera3D>();
            break;
        }
        Raylib.BeginMode3D(camera);
        foreach(var e in Query2)
        {
            ref var light = ref e.GetRef<Light>();
            if (light.Enabled) 
                Raylib.DrawSphereEx(light.Position, 0.2f, 8, 8, light.Color);
            else 
                Raylib.DrawSphereWires(light.Position, 0.2f, 8, 8, Raylib.ColorAlpha(light.Color, 0.3f));
        }
        foreach(var e in Query1)
        {
            ref var cube = ref e.GetRef<Model>();
            ref var pos = ref e.GetRef<Transform>();
            Raylib.DrawModel(cube, new(), 1, Color.WHITE);
        }
        Raylib.EndMode3D();

    }
} 