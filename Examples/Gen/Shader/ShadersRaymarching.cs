using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersRaymarching : ExampleHelper 
{

    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB . Not supported at this moment
private const int GLSL_VERSION = 100;
    #endif

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(FLAG_WINDOW_RESIZABLE);
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - raymarching shapes");

        Camera3D camera = new();
        camera.Position = (Vector3)new(2.5f,2.5f, 3.0f);    // Camera3D position
        camera.Target = (Vector3)new(0.0f,0.0f, 0.7f);      // Camera3D looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 65.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        // Load raymarching shader
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(0, TextFormat("resources/shaders/glsl%i/raymarching.fs", GLSL_VERSION));

        // Get shader locations for required uniforms
        int viewEyeLoc = GetShaderLocation(shader, "viewEye");
        int viewCenterLoc = GetShaderLocation(shader, "viewCenter");
        int runTimeLoc = GetShaderLocation(shader, "runTime");
        int resolutionLoc = GetShaderLocation(shader, "resolution");

        float [] resolution = new float [2]new( (float)screenWidth, (float)screenHeight );
        SetShaderValue(shader, resolutionLoc, resolution, SHADER_UNIFORM_VEC2);

        float runTime = 0.0f;

        DisableCursor();                    // Limit cursor to relative movement inside the window
        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            float [] cameraPos = new float [3]new( camera.Position.X, camera.Position.Y, camera.Position.Z );
            float [] cameraTarget = new float [3]new( camera.Target.X, camera.Target.Y, camera.Target.Z );

            float deltaTime = GetFrameTime();
            runTime += deltaTime;

            // Set shader required uniform values
            SetShaderValue(shader, viewEyeLoc, cameraPos, SHADER_UNIFORM_VEC3);
            SetShaderValue(shader, viewCenterLoc, cameraTarget, SHADER_UNIFORM_VEC3);
            SetShaderValue(shader, runTimeLoc, ref runTime, SHADER_UNIFORM_FLOAT);

            // Check if screen is resized
            if (IsWindowResized())
            {
                float [] resolution = new float [2]new( (float)GetScreenWidth(), (float)GetScreenHeight() );
                SetShaderValue(shader, resolutionLoc, resolution, SHADER_UNIFORM_VEC2);
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                // We only draw a white full-screen rectangle,
                // frame is generated in shader using raymarching
                BeginShaderMode(shader);
                    DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), White);
                EndShaderMode();

                DrawText("(c) Raymarching shader by Iñigo Quilez. MIT License.", GetScreenWidth() - 280, GetScreenHeight() - 20, 10, Black);

            }EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);           // Unload shader

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
