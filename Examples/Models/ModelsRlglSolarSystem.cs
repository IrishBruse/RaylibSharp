using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ModelsRlglSolarSystem : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        const float sunRadius = 4.0f;
        const float earthRadius = 0.6f;
        const float earthOrbitRadius = 8.0f;
        const float moonRadius = 0.16f;
        const float moonOrbitRadius = 1.5f;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - RLGL.Gl module usage with push/pop matrix transformations");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(16.0f, 16.0f, 16.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        float rotationSpeed = 0.2f; // General system rotation speed

        float earthRotation = 0.0f; // Rotation of earth around itself (days) in degrees
        float earthOrbitRotation = 0.0f; // Rotation of earth around the Sun (years) in degrees
        float moonRotation = 0.0f; // Rotation of moon around itself
        float moonOrbitRotation = 0.0f; // Rotation of moon around earth in degrees

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            earthRotation += 5.0f * rotationSpeed;
            earthOrbitRotation += 365 / 360.0f * (5.0f * rotationSpeed) * rotationSpeed;
            moonRotation += 2.0f * rotationSpeed;
            moonOrbitRotation += 8.0f * rotationSpeed;

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {
                    RLGL.PushMatrix();
                    RLGL.Scalef(sunRadius, sunRadius, sunRadius); // Scale Sun
                    DrawSphereBasic(Gold); // Draw the Sun
                    RLGL.PopMatrix();

                    RLGL.PushMatrix();
                    RLGL.Rotatef(earthOrbitRotation, 0.0f, 1.0f, 0.0f); // Rotation for Earth orbit around Sun
                    RLGL.Translatef(earthOrbitRadius, 0.0f, 0.0f); // Translation for Earth orbit

                    RLGL.PushMatrix();
                    RLGL.Rotatef(earthRotation, 0.25f, 1.0f, 0.0f); // Rotation for Earth itself
                    RLGL.Scalef(earthRadius, earthRadius, earthRadius);// Scale Earth

                    DrawSphereBasic(Blue); // Draw the Earth
                    RLGL.PopMatrix();

                    RLGL.Rotatef(moonOrbitRotation, 0.0f, 1.0f, 0.0f); // Rotation for Moon orbit around Earth
                    RLGL.Translatef(moonOrbitRadius, 0.0f, 0.0f); // Translation for Moon orbit
                    RLGL.Rotatef(moonRotation, 0.0f, 1.0f, 0.0f); // Rotation for Moon itself
                    RLGL.Scalef(moonRadius, moonRadius, moonRadius); // Scale Moon

                    DrawSphereBasic(LightGray); // Draw the Moon
                    RLGL.PopMatrix();

                    // Some reference elements (not affected by previous matrix transformations)
                    DrawCircle3D(new(0.0f, 0.0f, 0.0f), earthOrbitRadius, new(1, 0, 0), 90.0f, Fade(Red, 0.5f));
                    DrawGrid(20, 1.0f);
                }
                EndMode3D();

                DrawText("EARTH ORBITING AROUND THE SUN!", 400, 10, 20, Maroon);
                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

    // Module Functions Definitions (local)

    // Draw sphere without any matrix transformation
    // NOTE: Sphere is drawn in woRLGL.d position ( 0, 0, 0 ) with radius 1.0f
    static void DrawSphereBasic(Color color)
    {
        int rings = 16;
        int slices = 16;

        // Make sure there is enough space in the internal render batch
        // buffer to store all required vertex, batch is reseted if required
        RLGL.CheckRenderBatchLimit((rings + 2) * slices * 6);

        RLGL.Begin(RLGL.RlTriangles);
        RLGL.Color4ub(color.R, color.G, color.B, color.A);

        for (int i = 0; i < (rings + 2); i++)
        {
            for (int j = 0; j < slices; j++)
            {
                RLGL.Vertex3f(MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * i))) * MathF.Sin(DEG2RAD * (j * 360 / slices)),
                           MathF.Sin(DEG2RAD * (270 + (180 / (rings + 1) * i))),
                           MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * i))) * MathF.Cos(DEG2RAD * (j * 360 / slices)));
                RLGL.Vertex3f(MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))) * MathF.Sin(DEG2RAD * ((j + 1) * 360 / slices)),
                           MathF.Sin(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))),
                           MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))) * MathF.Cos(DEG2RAD * ((j + 1) * 360 / slices)));
                RLGL.Vertex3f(MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))) * MathF.Sin(DEG2RAD * (j * 360 / slices)),
                           MathF.Sin(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))),
                           MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))) * MathF.Cos(DEG2RAD * (j * 360 / slices)));

                RLGL.Vertex3f(MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * i))) * MathF.Sin(DEG2RAD * (j * 360 / slices)),
                           MathF.Sin(DEG2RAD * (270 + (180 / (rings + 1) * i))),
                           MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * i))) * MathF.Cos(DEG2RAD * (j * 360 / slices)));
                RLGL.Vertex3f(MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * i))) * MathF.Sin(DEG2RAD * ((j + 1) * 360 / slices)),
                           MathF.Sin(DEG2RAD * (270 + (180 / (rings + 1) * i))),
                           MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * i))) * MathF.Cos(DEG2RAD * ((j + 1) * 360 / slices)));
                RLGL.Vertex3f(MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))) * MathF.Sin(DEG2RAD * ((j + 1) * 360 / slices)),
                           MathF.Sin(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))),
                           MathF.Cos(DEG2RAD * (270 + (180 / (rings + 1) * (i + 1)))) * MathF.Cos(DEG2RAD * ((j + 1) * 360 / slices)));
            }
        }
        RLGL.End();
    }
}
