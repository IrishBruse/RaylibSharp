using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersTextureWaves : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - texture waves");

        // Load texture texture to apply shaders
        Texture texture = LoadTexture("resources/space.png");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load shader and setup location points and values
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/wave.fs");

        int secondsLoc = GetShaderLocation(shader, "secondes");
        int freqXLoc = GetShaderLocation(shader, "freqX");
        int freqYLoc = GetShaderLocation(shader, "freqY");
        int ampXLoc = GetShaderLocation(shader, "ampX");
        int ampYLoc = GetShaderLocation(shader, "ampY");
        int speedXLoc = GetShaderLocation(shader, "speedX");
        int speedYLoc = GetShaderLocation(shader, "speedY");

        // Shader uniform values that can be updated at any time
        float freqX = 25.0f;
        float freqY = 25.0f;
        float ampX = 5.0f;
        float ampY = 5.0f;
        float speedX = 8.0f;
        float speedY = 8.0f;

        Vector2 screenSize = new(GetScreenWidth(), GetScreenHeight());
        SetShaderValue(shader, GetShaderLocation(shader, "size"), ref screenSize, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(shader, freqXLoc, ref freqX, ShaderUniformDataType.ShaderUniformFloat);
        SetShaderValue(shader, freqYLoc, ref freqY, ShaderUniformDataType.ShaderUniformFloat);
        SetShaderValue(shader, ampXLoc, ref ampX, ShaderUniformDataType.ShaderUniformFloat);
        SetShaderValue(shader, ampYLoc, ref ampY, ShaderUniformDataType.ShaderUniformFloat);
        SetShaderValue(shader, speedXLoc, ref speedX, ShaderUniformDataType.ShaderUniformFloat);
        SetShaderValue(shader, speedYLoc, ref speedY, ShaderUniformDataType.ShaderUniformFloat);

        float seconds = 0.0f;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        // -------------------------------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            seconds += GetFrameTime();

            SetShaderValue(shader, secondsLoc, ref seconds, ShaderUniformDataType.ShaderUniformFloat);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginShaderMode(shader);
                {

                    DrawTexture(texture, 0, 0, White);
                    DrawTexture(texture, texture.Width, 0, White);

                }
                EndShaderMode();

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);         // Unload shader
        UnloadTexture(texture);       // Unload texture

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
