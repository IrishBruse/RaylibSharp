using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersTextureDrawing : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - texture drawing");

        Image imBlank = GenImageColor(1024, 1024, Blank);
        Texture texture = LoadTextureFromImage(imBlank); // Load blank texture to fill on shader
        UnloadImage(imBlank);

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // NOTE: Using GLSL 330 shader version, on OpenGL ES 2.0 use GLSL 100 shader version
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/cubes_panning.fs");

        float time = 0.0f;
        int timeLoc = GetShaderLocation(shader, "uTime");
        SetShaderValue(shader, timeLoc, ref time, ShaderUniformDataType.ShaderUniformFloat);

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            time = (float)GetTime();
            SetShaderValue(shader, timeLoc, ref time, ShaderUniformDataType.ShaderUniformFloat);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginShaderMode(shader);
                {    // Enable our custom shader for next shapes/textures drawings
                    DrawTexture(texture, 0, 0, White); // Drawing Blank texture, all magic happens on shader
                }
                EndShaderMode(); // Disable our custom shader, return to default shader

                DrawText("BACKGROUND is PAINTED and ANIMATED on SHADER!", 10, 10, 20, Maroon);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
