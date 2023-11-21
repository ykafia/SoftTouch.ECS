// See https://aka.ms/new-console-template for more information
using System.Numerics;
using Raylib_cs;
Console.WriteLine("Hello, World!");


Raylib.InitWindow(1920, 1080, "Hello World");
Camera3D camera = new(){
    Position = new(10,10,10),
    Target = new(0),
    Up = Vector3.UnitY,
    FovY = 45,
    Projection = CameraProjection.CAMERA_PERSPECTIVE
};
Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_UNDECORATED);

while (!Raylib.WindowShouldClose())
{
    if(Raylib.IsKeyPressed(KeyboardKey.KEY_Z))
        camera.Target = new(0);
    Raylib.UpdateCamera(ref camera, CameraMode.CAMERA_ORBITAL);
    
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.BLACK);

    Raylib.DrawText("Hello, world!", 12, 12, 20, Color.WHITE);
    Raylib.BeginMode3D(camera);
    Raylib.DrawCube(Vector3.Zero, 3,3,3,Color.BLUE);
    Raylib.EndMode3D();
    Raylib.BeginMode3D(camera);
    Raylib.DrawSphere(Vector3.UnitY, 1, Color.RED);
    Raylib.EndMode3D();
    Raylib.EndDrawing();
}

Raylib.CloseWindow();