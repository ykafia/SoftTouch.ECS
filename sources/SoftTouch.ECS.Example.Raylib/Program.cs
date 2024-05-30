// See https://aka.ms/new-console-template for more information
using System.Numerics;
using Raylib_cs;
using SoftTouch.ECS;
using SoftTouch.ECS.Example.Rlib;
using SoftTouch.ECS.Scheduling;
Console.WriteLine("Hello, World!");

var app =
    new App()
    .AddStartupProcessor<SpawnEntities>()
    .AddProcessors<Update, CameraUpdater, MoveCube, MoveLight>();

var renderApp =
    new RenderApp(app)
    .SetStages([new StartRender(), new MainRender(), new EndRender()])
    .AddProcessors<StartRender, BeginDraw>()
    .AddProcessors<MainRender, RenderCubes>()
    .AddProcessors<EndRender, EndDraw>();

app.SubApp = (SubApp)renderApp;
Console.WriteLine(app.Schedule);
Raylib.InitWindow(800, 600, "MyGame");


// while (!Raylib.WindowShouldClose())
// {
//     app.Update();
// }

// Raylib.InitWindow(800,600, "Hello World");
// Raylib.SetWindowPosition(0,0);
// Camera3D camera = new()
// {
//     Position = new(10, 10, 10),
//     Target = new(0),
//     Up = Vector3.UnitY,
//     FovY = 45,
//     Projection = CameraProjection.CAMERA_PERSPECTIVE
// };

// // Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_UNDECORATED);



// var plane = Raylib.LoadModelFromMesh(Raylib.GenMeshPlane(10,10,10,10));
// var cube = Raylib.LoadModelFromMesh(Raylib.GenMeshCube(1, 1, 1));

// var shader = Raylib.LoadShader("GLSL/lighting.vert", "GLSL/lighting.frag");

// unsafe
// {
//     shader.Locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW] = Raylib.GetShaderLocation(shader, "viewPos");
// }


// // Set shader value: ambient light level
// int ambientLoc = Raylib.GetShaderLocation(shader, "ambient");
// Raylib.SetShaderValue(shader, ambientLoc, [0.1f, 0.1f, 0.1f, 1.0f], ShaderUniformDataType.SHADER_UNIFORM_VEC4);
// Light[] lights =[
//     Util.CreateLight(LightType.LIGHT_POINT, new(-2, 1, -2), Vector3.Zero, Color.YELLOW, shader),
//     Util.CreateLight(LightType.LIGHT_POINT, new(2, 1, 2), Vector3.Zero, Color.RED, shader),
//     Util.CreateLight(LightType.LIGHT_POINT, new(-2, 1, 2), Vector3.Zero, Color.GREEN, shader),
//     Util.CreateLight(LightType.LIGHT_POINT, new(2, 1, -2), Vector3.Zero, Color.BLUE, shader),
// ];


// var matDefault = Raylib.LoadMaterialDefault();
// unsafe
// {
//     cube.Materials[0].Shader = shader;
//     plane.Materials[0].Shader = shader;
// }

// unsafe
// {
//     while (!Raylib.WindowShouldClose())
//     {
//         if (Raylib.IsKeyPressed(KeyboardKey.KEY_Z))
//             camera.Target = new(0);

//         if (Raylib.IsKeyPressed(KeyboardKey.KEY_Y)) { lights[0].Enabled = !lights[0].Enabled; }
//         if (Raylib.IsKeyPressed(KeyboardKey.KEY_R)) { lights[1].Enabled = !lights[1].Enabled; }
//         if (Raylib.IsKeyPressed(KeyboardKey.KEY_G)) { lights[2].Enabled = !lights[2].Enabled; }
//         if (Raylib.IsKeyPressed(KeyboardKey.KEY_B)) { lights[3].Enabled = !lights[3].Enabled; }
//         for (int i = 0; i < lights.Length; i++) Util.UpdateLightValues(shader, lights[i]);


//         Raylib.UpdateCamera(ref camera, CameraMode.CAMERA_ORBITAL);
//         Raylib.SetShaderValue(shader, shader.Locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW], camera.Position,ShaderUniformDataType.SHADER_UNIFORM_VEC3);
//         Raylib.BeginDrawing();
//         Raylib.ClearBackground(Color.BLACK);

//         Raylib.BeginMode3D(camera);
//         Raylib.DrawGrid(10,1);
//         Raylib.DrawModel(cube,Vector3.UnitY, 1, Color.WHITE);
//         Raylib.DrawModel(plane, Vector3.Zero, 1,Color.WHITE);
//         for (int i = 0; i < lights.Length; i++)
//         {
//             if (lights[i].Enabled) Raylib.DrawSphereEx(lights[i].Position, 0.2f, 8, 8, lights[i].Color);
//             else Raylib.DrawSphereWires(lights[i].Position, 0.2f, 8, 8, Raylib.ColorAlpha(lights[i].Color, 0.3f));
//         }


//         Raylib.EndMode3D();
//         Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);
//         Raylib.DrawFPS(10, 100);
//         Raylib.EndDrawing();
//     }
// }

// Raylib.CloseWindow();