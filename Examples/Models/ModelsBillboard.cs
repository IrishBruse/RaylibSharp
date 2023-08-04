using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsBillboard : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - drawing billboards");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(5.0f, 4.0f, 5.0f);    // Camera3D position
        camera.Target = new(0.0f, 2.0f, 0.0f);      // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        Texture bill = LoadTexture("resources/billboard.png");    // Our billboard texture
        Vector3 billPositionStatic = new(0.0f, 2.0f, 0.0f);          // Position of static billboard
        Vector3 billPositionRotating = new(1.0f, 2.0f, 1.0f);        // Position of rotating billboard

        // Entire billboard texture, source is used to take a segment from a larger texture.
        RectangleF source = new(0.0f, 0.0f, bill.Width, bill.Height);

        // NOTE: Billboard locked on axis-Y
        Vector3 billUp = new(0.0f, 1.0f, 0.0f);

        // Rotate around origin
        // Here we choose to rotate around the image center
        // NOTE: (-1, 1) is the range where origin.X, origin.Y is inside the texture
        Vector2 rotateOrigin = new(0, 0);

        // Distance is needed for the correct billboard draw order
        // Larger distance (further away from the camera) should be drawn prior to smaller distance.
        float distanceStatic;
        float distanceRotating;
        float rotation = 0.0f;

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            rotation += 0.4f;
            distanceStatic = Vector3.Distance(camera.Position, billPositionStatic);
            distanceRotating = Vector3.Distance(camera.Position, billPositionRotating);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    DrawGrid(10, 1.0f);        // Draw a grid

                    // Draw order matters!
                    if (distanceStatic > distanceRotating)
                    {
                        DrawBillboard(camera, bill, billPositionStatic, 2.0f, White);
                        DrawBillboard(camera, bill, source, billPositionRotating, billUp, new(1.0f, 1.0f), rotateOrigin, rotation, White);
                    }
                    else
                    {
                        DrawBillboard(camera, bill, source, billPositionRotating, billUp, new(1.0f, 1.0f), rotateOrigin, rotation, White);
                        DrawBillboard(camera, bill, billPositionStatic, 2.0f, White);
                    }

                }
                EndMode3D();

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(bill);        // Unload texture

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
