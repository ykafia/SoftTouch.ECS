using System.Numerics;
using Raylib_cs;
using SoftTouch.ECS.Example.RaylibUtilities;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using static Raylib_cs.Raylib;

namespace SoftTouch.ECS.Example.Rlib;


public class SpawnEntities() : Processor<Commands>(null!)
{
    public override void Update()
    {

        SetWindowState(ConfigFlags.FLAG_WINDOW_UNDECORATED);
        var camera = new Camera3D()
        {
            Position = new(10),
            FovY = 45,
            Projection = CameraProjection.CAMERA_PERSPECTIVE,
            Target = Vector3.Zero,
            Up = Vector3.UnitY
        };
        Query.Spawn(camera);

        var cube = LoadModelFromMesh(GenMeshCube(1, 1, 1));

        var shader = LoadShader("GLSL/lighting.vert", "GLSL/lighting.frag");

        unsafe
        {
            shader.Locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(shader, "viewPos");
        }


        // Set shader value: ambient light level
        int ambientLoc = GetShaderLocation(shader, "ambient");
        SetShaderValue(shader, ambientLoc, [0.1f, 0.1f, 0.1f, 1.0f], ShaderUniformDataType.SHADER_UNIFORM_VEC4);

        unsafe { cube.Materials[0].Shader = shader; }

        Query.Spawn()
            .Insert(cube)
            .Insert(new Transform() { Translation = new(1, 0, 1) });
        Query.Spawn()
            .Insert(cube)
            .Insert(new Transform() { Translation = new(-1, 0, 1) });
        Query.Spawn()
            .Insert(cube)
            .Insert(new Transform() { Translation = new(-1, 0, -1) });
        Query.Spawn()
            .Insert(cube)
            .Insert(new Transform() { Translation = new(1, 0, -1) });



        Query.Spawn().Insert(Util.CreateLight(LightType.LIGHT_POINT, new(2, 1, 2), Vector3.Zero, Color.YELLOW, shader));
        Query.Spawn().Insert(Util.CreateLight(LightType.LIGHT_POINT, new(-2, 1, 2), Vector3.Zero, Color.RED, shader));
        Query.Spawn().Insert(Util.CreateLight(LightType.LIGHT_POINT, new(-2, 1, -2), Vector3.Zero, Color.GREEN, shader));
        Query.Spawn().Insert(Util.CreateLight(LightType.LIGHT_POINT, new(-2, 1, 2), Vector3.Zero, Color.BLUE, shader));

    }
}