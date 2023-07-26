using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsRlglSolarSystem : ExampleHelper 
{

    // Module Functions Declaration
    static void DrawSphereBasic(Color color);      // Draw sphere without any matrix transformation

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

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - rlgl module usage with push/pop matrix transformations");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(16.0f,16.0f, 16.0f); // Camera position
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);      // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        float rotationSpeed = 0.2f;         // General system rotation speed

        float earthRotation = 0.0f;         // Rotation of earth around itself (days) in degrees
        float earthOrbitRotation = 0.0f;    // Rotation of earth around the Sun (years) in degrees
        float moonRotation = 0.0f;          // Rotation of moon around itself
        float moonOrbitRotation = 0.0f;     // Rotation of moon around earth in degrees

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            earthRotation += (5.0f*rotationSpeed);
            earthOrbitRotation += (365/360.0f*(5.0f*rotationSpeed)*rotationSpeed);
            moonRotation += (2.0f*rotationSpeed);
            moonOrbitRotation += (8.0f*rotationSpeed);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                    rlPushMatrix();
                        rlScalef(sunRadius, sunRadius, sunRadius);          // Scale Sun
                        DrawSphereBasic(Gold);                              // Draw the Sun
                    rlPopMatrix();

                    rlPushMatrix();
                        rlRotatef(earthOrbitRotation, 0.0f, 1.0f, 0.0f);    // Rotation for Earth orbit around Sun
                        rlTranslatef(earthOrbitRadius, 0.0f, 0.0f);         // Translation for Earth orbit

                        rlPushMatrix();
                            rlRotatef(earthRotation, 0.25, 1.0, 0.0);       // Rotation for Earth itself
                            rlScalef(earthRadius, earthRadius, earthRadius);// Scale Earth

                            DrawSphereBasic(Blue);                          // Draw the Earth
                        rlPopMatrix();

                        rlRotatef(moonOrbitRotation, 0.0f, 1.0f, 0.0f);     // Rotation for Moon orbit around Earth
                        rlTranslatef(moonOrbitRadius, 0.0f, 0.0f);          // Translation for Moon orbit
                        rlRotatef(moonRotation, 0.0f, 1.0f, 0.0f);          // Rotation for Moon itself
                        rlScalef(moonRadius, moonRadius, moonRadius);       // Scale Moon

                        DrawSphereBasic(LightGray);                         // Draw the Moon
                    rlPopMatrix();

                    // Some reference elements (not affected by previous matrix transformations)
                    DrawCircle3D((Vector3)new(0.0f,0.0f, 0.0f), earthOrbitRadius, new( 1, 0, 0 ), 90.0f, Fade(Red, 0.5f));
                    DrawGrid(20, 1.0f);

                }EndMode3D();

                DrawText("EARTH ORBITING AROUND THE SUN!", 400, 10, 20, Maroon);
                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    // Module Functions Definitions (local)

    // Draw sphere without any matrix transformation
    // NOTE: Sphere is drawn in world position ( 0, 0, 0 ) with radius 1.0f
    static void DrawSphereBasic(Color color)
    {
        int rings = 16;
        int slices = 16;

        // Make sure there is enough space in the internal render batch
        // buffer to store all required vertex, batch is reseted if required
        rlCheckRenderBatchLimit((rings + 2)*slices*6);

        rlBegin(RL_TRIANGLES);
            rlColor4ub(color.r, color.g, color.b, color.a);

            for (int i = 0; i < (rings + 2); i++)
            {
                for (int j = 0; j < slices; j++)
                {
                    rlVertex3f(MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*i))*MathF.Sin(DEG2RAD*(j*360/slices)),
                               MathF.Sin(DEG2RAD*(270+(180/(rings + 1))*i)),
                               MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*i))*MathF.Cos(DEG2RAD*(j*360/slices)));
                    rlVertex3f(MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i+1)))*MathF.Sin(DEG2RAD*((j+1)*360/slices)),
                               MathF.Sin(DEG2RAD*(270+(180/(rings + 1))*(i+1))),
                               MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i+1)))*MathF.Cos(DEG2RAD*((j+1)*360/slices)));
                    rlVertex3f(MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i+1)))*MathF.Sin(DEG2RAD*(j*360/slices)),
                               MathF.Sin(DEG2RAD*(270+(180/(rings + 1))*(i+1))),
                               MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i+1)))*MathF.Cos(DEG2RAD*(j*360/slices)));

                    rlVertex3f(MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*i))*MathF.Sin(DEG2RAD*(j*360/slices)),
                               MathF.Sin(DEG2RAD*(270+(180/(rings + 1))*i)),
                               MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*i))*MathF.Cos(DEG2RAD*(j*360/slices)));
                    rlVertex3f(MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i)))*MathF.Sin(DEG2RAD*((j+1)*360/slices)),
                               MathF.Sin(DEG2RAD*(270+(180/(rings + 1))*(i))),
                               MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i)))*MathF.Cos(DEG2RAD*((j+1)*360/slices)));
                    rlVertex3f(MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i+1)))*MathF.Sin(DEG2RAD*((j+1)*360/slices)),
                               MathF.Sin(DEG2RAD*(270+(180/(rings + 1))*(i+1))),
                               MathF.Cos(DEG2RAD*(270+(180/(rings + 1))*(i+1)))*MathF.Cos(DEG2RAD*((j+1)*360/slices)));
                }
            }
        rlEnd();
    }
}
