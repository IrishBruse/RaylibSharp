using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsGeometricShapes : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - geometric shapes");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(0.0f, 10.0f, 10.0f);
        camera.Target = new(0.0f, 0.0f, 0.0f);
        camera.Up = new(0.0f, 1.0f, 0.0f);
        camera.Fovy = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

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

                    DrawCapsule(new(-3.0f, 1.5f, -4.0f), new(-4.0f, -1.0f, -4.0f), 1.2f, 8, 8, Violet);
                    DrawCapsuleWires(new(-3.0f, 1.5f, -4.0f), new(-4.0f, -1.0f, -4.0f), 1.2f, 8, 8, Purple);

                    DrawGrid(10, 1.0f); // Draw a grid

                }
                EndMode3D();

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
