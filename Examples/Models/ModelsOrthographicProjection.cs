using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsOrthographicProjection : ExampleHelper
{
    const float FOVY_PERSPECTIVE = 45.0f;
    const float WIDTH_ORTHOGRAPHIC = 10.0f;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - geometric shapes");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(10.0f, 10.0f, 10.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.Space))
            {
                if (camera.Projection == CameraProjection.Perspective)
                {
                    camera.Fovy = WIDTH_ORTHOGRAPHIC;
                    camera.Projection = CameraProjection.Orthographic;
                }
                else
                {
                    camera.Fovy = FOVY_PERSPECTIVE;
                    camera.Projection = CameraProjection.Perspective;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    DrawCube(new(-4.0f, 0.0f, 2.0f), 2.0f, 5.0f, 2.0f, Red);
                    DrawCubeWires(new(-4.0f, 0.0f, 2.0f), 2.0f, 5.0f, 2.0f, Gold);
                    DrawCubeWires(new(-4.0f, 0.0f, -2.0f), 3.0f, 6.0f, 2.0f, Maroon);

                    DrawSphere(new(-1.0f, 0.0f, -2.0f), 1.0f, Green);
                    DrawSphereWires(new(1.0f, 0.0f, 2.0f), 2.0f, 16, 16, Lime);

                    DrawCylinder(new(4.0f, 0.0f, -2.0f), 1.0f, 2.0f, 3.0f, 4, SkyBlue);
                    DrawCylinderWires(new(4.0f, 0.0f, -2.0f), 1.0f, 2.0f, 3.0f, 4, DarkBlue);
                    DrawCylinderWires(new(4.5f, -1.0f, 2.0f), 1.0f, 1.0f, 2.0f, 6, Brown);

                    DrawCylinder(new(1.0f, 0.0f, -4.0f), 0.0f, 1.5f, 3.0f, 8, Gold);
                    DrawCylinderWires(new(1.0f, 0.0f, -4.0f), 0.0f, 1.5f, 3.0f, 8, Pink);

                    DrawGrid(10, 1.0f); // Draw a grid

                }
                EndMode3D();

                DrawText("Press Spacebar to switch camera type", 10, GetScreenHeight() - 30, 20, DarkGray);

                if (camera.Projection == CameraProjection.Orthographic)
                {
                    DrawText("ORTHOGRAPHIC", 10, 40, 20, Black);
                }
                else if (camera.Projection == CameraProjection.Perspective)
                {
                    DrawText("PERSPECTIVE", 10, 40, 20, Black);
                }

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
