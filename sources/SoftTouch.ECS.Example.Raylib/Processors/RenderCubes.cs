using System.Numerics;
using Raylib_cs;
using SoftTouch.ECS.Example.RaylibUtilities;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;


public class RenderCubes() : Processor<Query<Model>, Query<Light>, Query<Camera3D>>(null!)
{
    public override void Update()
    {
        foreach (var cameraEntity in Query3)
        {
            var camera = cameraEntity.Get<Camera3D>();
            Raylib.BeginMode3D(camera);
            foreach (var e in Query2)
            {
                ref var light = ref e.Get<Light>();
                if (light.Enabled)
                    Raylib.DrawSphereEx(light.Position, 0.2f, 8, 8, light.Color);
                else
                    Raylib.DrawSphereWires(light.Position, 0.2f, 8, 8, Raylib.ColorAlpha(light.Color, 0.3f));
            }
            foreach (var e in Query1)
            {
                ref var cube = ref e.Get<Model>();
                unsafe
                {
                    foreach (var el in Query2)
                        Util.UpdateLightValues(cube.Materials[0].Shader, el.Get<Light>());
                }
                // Raylib.DrawModel(cube, new(), 1, Color.WHITE);
                Raylib.DrawCube(cube.Transform.Translation, 1, 1, 1, Color.BLUE);
            }
            Raylib.EndMode3D();
        }
    }
}