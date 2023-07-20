using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class Core3dCameraMode : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - Core - 3d camera mode");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(0.0f, 10.0f, 10.0f);          // Camera position
        camera.Target = new(0.0f, 0.0f, 0.0f);              // Camera looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);                  // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;   // Camera mode type

        Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {
                    DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, Red);
                    DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, Maroon);

                    DrawGrid(10, 1.0f);
                }
                EndMode3D();

                DrawText("Welcome to the third dimension!", 10, 40, 20, DarkGray);

                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }
}
