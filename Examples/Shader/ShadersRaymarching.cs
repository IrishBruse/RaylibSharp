using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersRaymarching : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Resizable);
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - raymarching shapes");

        Camera3D camera = new();
        camera.Position = new(2.5f, 2.5f, 3.0f);    // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.7f);      // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 65.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load raymarching shader
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/raymarching.fs");

        // Get shader locations for required uniforms
        int viewEyeLoc = GetShaderLocation(shader, "viewEye");
        int viewCenterLoc = GetShaderLocation(shader, "viewCenter");
        int runTimeLoc = GetShaderLocation(shader, "runTime");
        int resolutionLoc = GetShaderLocation(shader, "resolution");

        Vector2 screenSize = new(screenWidth, screenHeight);
        SetShaderValue(shader, resolutionLoc, ref screenSize, ShaderUniformDataType.ShaderUniformVec2);

        float runTime = 0.0f;

        DisableCursor();                    // Limit cursor to relative movement inside the window
        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            Vector3 cameraPos = new(camera.Position.X, camera.Position.Y, camera.Position.Z);
            Vector3 cameraTarget = new(camera.Target.X, camera.Target.Y, camera.Target.Z);

            float deltaTime = GetFrameTime();
            runTime += deltaTime;

            // Set shader required uniform values
            SetShaderValue(shader, viewEyeLoc, ref cameraPos, ShaderUniformDataType.ShaderUniformVec3);
            SetShaderValue(shader, viewCenterLoc, ref cameraTarget, ShaderUniformDataType.ShaderUniformVec3);
            SetShaderValue(shader, runTimeLoc, ref runTime, ShaderUniformDataType.ShaderUniformFloat);

            // Check if screen is resized
            if (IsWindowResized())
            {
                Vector2 resolution = new(GetScreenWidth(), GetScreenHeight());
                SetShaderValue(shader, resolutionLoc, ref resolution, ShaderUniformDataType.ShaderUniformVec2);
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // We only draw a white full-screen rectangle,
                // frame is generated in shader using raymarching
                BeginShaderMode(shader);
                {
                    DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), White);
                }
                EndShaderMode();

                DrawText("(c) Raymarching shader by Iñigo Quilez. MIT License.", GetScreenWidth() - 280, GetScreenHeight() - 20, 10, Black);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);           // Unload shader

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
