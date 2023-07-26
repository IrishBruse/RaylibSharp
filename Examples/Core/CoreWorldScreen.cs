using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

using Camera = RaylibSharp.Camera3D;

public class CoreWorldScreen : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - core world screen");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = new(10.0f, 10.0f, 10.0f); // Camera position
        camera.Target = new(0.0f, 0.0f, 0.0f);      // Camera looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.ThirdPerson);

            // Calculate cube screen space position (with a little offset to be in top)
            Vector2 cubeScreenPosition = GetWorldToScreen(new(cubePosition.X, cubePosition.Y + 2.5f, cubePosition.Z), camera);

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

                DrawText("Enemy: 100 / 100", (int)cubeScreenPosition.X - (MeasureText("Enemy: 100/100", 20) / 2), (int)cubeScreenPosition.Y, 20, Black);

                DrawText($"Cube position in screen space coordinates: [{(int)cubeScreenPosition.X}, {(int)cubeScreenPosition.Y}]", 10, 10, 20, Lime);
                DrawText("Text 2d should be always on top of the cube", 10, 40, 20, Gray);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }
}
