using System;
using System.Numerics;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersHotReloading : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - hot reloading");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        string fragShaderFileName = $"resources/shaders/glsl{glslVersion}/reload.fs";
        long fragShaderFileModTime = GetFileModTime(fragShaderFileName);

        // Load raymarching shader
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(null, fragShaderFileName);

        // Get shader locations for required uniforms
        int resolutionLoc = GetShaderLocation(shader, "resolution");
        int mouseLoc = GetShaderLocation(shader, "mouse");
        int timeLoc = GetShaderLocation(shader, "time");

        Vector2 resolution = new(screenWidth, screenHeight);
        SetShaderValue(shader, resolutionLoc, ref resolution, ShaderUniformDataType.ShaderUniformVec2);

        float totalTime = 0.0f;
        bool shaderAutoReloading = false;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())            // Detect window close button or ESC key
        {
            // Update
            totalTime += GetFrameTime();
            Vector2 mouse = GetMousePosition();
            Vector2 mousePos = new(mouse.X, mouse.Y);

            // Set shader required uniform values
            SetShaderValue(shader, timeLoc, ref totalTime, ShaderUniformDataType.ShaderUniformFloat);
            SetShaderValue(shader, mouseLoc, ref mousePos, ShaderUniformDataType.ShaderUniformVec2);

            // Hot shader reloading
            if (shaderAutoReloading || IsMouseButtonPressed(MouseButton.Left))
            {
                long currentFragShaderModTime = GetFileModTime(fragShaderFileName);

                // Check if shader file has been modified
                if (currentFragShaderModTime != fragShaderFileModTime)
                {
                    // Try reloading updated shader
                    Shader updatedShader = LoadShader(null, fragShaderFileName);

                    if (updatedShader.Id != RLGL.GetShaderIdDefault())      // It was correctly loaded
                    {
                        UnloadShader(shader);
                        shader = updatedShader;

                        // Get shader locations for required uniforms
                        resolutionLoc = GetShaderLocation(shader, "resolution");
                        mouseLoc = GetShaderLocation(shader, "mouse");
                        timeLoc = GetShaderLocation(shader, "time");

                        // Reset required uniforms
                        SetShaderValue(shader, resolutionLoc, ref resolution, ShaderUniformDataType.ShaderUniformVec2);
                    }

                    fragShaderFileModTime = currentFragShaderModTime;
                }
            }

            if (IsKeyPressed(Key.A))
            {
                shaderAutoReloading = !shaderAutoReloading;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // We only draw a white full-screen rectangle, frame is generated in shader
                BeginShaderMode(shader);
                {
                    DrawRectangle(0, 0, screenWidth, screenHeight, White);
                }
                EndShaderMode();

                DrawText("PRESS [A] to TOGGLE SHADER AUTOLOADING: " + (shaderAutoReloading ? "AUTO" : "MANUAL"), 10, 10, 10, shaderAutoReloading ? Red : Black);
                if (!shaderAutoReloading)
                {
                    DrawText("MOUSE CLICK to SHADER RE-LOADING", 10, 30, 10, Black);
                }

                DrawText("Shader last modification: " + fragShaderFileModTime, 10, 430, 10, Black);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader); // Unload shader

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
