using System;
using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersMultiSample2d : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib - multiple sample2D");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        Image imRed = GenImageColor(800, 450, Color.FromArgb(255, 255, 0, 0));
        Texture texRed = LoadTextureFromImage(imRed);
        UnloadImage(imRed);

        Image imBlue = GenImageColor(800, 450, Color.FromArgb(255, 0, 0, 255));
        Texture texBlue = LoadTextureFromImage(imBlue);
        UnloadImage(imBlue);

        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/color_mix.fs");

        // Get an additional sampler2D location to be enabled on drawing
        int texBlueLoc = GetShaderLocation(shader, "texture1");

        // Get shader uniform for divider
        int dividerLoc = GetShaderLocation(shader, "divider");
        float dividerValue = 0.5f;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())                // Detect window close button or ESC key
        {
            // Update
            if (IsKeyDown(Key.Right))
            {
                dividerValue += 0.01f;
            }
            else if (IsKeyDown(Key.Left))
            {
                dividerValue -= 0.01f;
            }

            if (dividerValue < 0.0f)
            {
                dividerValue = 0.0f;
            }
            else if (dividerValue > 1.0f)
            {
                dividerValue = 1.0f;
            }

            SetShaderValue(shader, dividerLoc, ref dividerValue, ShaderUniformDataType.ShaderUniformFloat);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginShaderMode(shader);
                {

                    // WARNING: Additional samplers are enabled for all draw calls in the batch,
                    // EndShaderMode() forces batch drawing and consequently resets active textures
                    // to let other sampler2D to be activated on consequent drawings (if required)
                    SetShaderValueTexture(shader, texBlueLoc, texBlue);

                    // We are drawing texRed using default sampler2D texture0 but
                    // an additional texture units is enabled for texBlue (sampler2D texture1)
                    DrawTexture(texRed, 0, 0, White);

                }
                EndShaderMode();

                DrawText("Use Key.Left/Key.Right to move texture mixing in shader!", 80, GetScreenHeight() - 40, 20, RayWhite);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader); // Unload shader
        UnloadTexture(texRed); // Unload texture
        UnloadTexture(texBlue); // Unload texture

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
