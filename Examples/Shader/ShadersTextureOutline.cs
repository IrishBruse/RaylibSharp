using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersTextureOutline : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - Apply an outline to a texture");

        Texture texture = LoadTexture("resources/fudesumi.png");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        Shader shdrOutline = LoadShader(null, $"resources/shaders/glsl{glslVersion}/outline.fs");

        float outlineSize = 2.0f;
        Vector4 outlineColor = new(1.0f, 0.0f, 0.0f, 1.0f);     // Normalized Red color
        Vector2 textureSize = new(texture.Width, texture.Height);

        // Get shader locations
        int outlineSizeLoc = GetShaderLocation(shdrOutline, "outlineSize");
        int outlineColorLoc = GetShaderLocation(shdrOutline, "outlineColor");
        int textureSizeLoc = GetShaderLocation(shdrOutline, "textureSize");

        // Set shader values (they can be changed later)
        SetShaderValue(shdrOutline, outlineSizeLoc, ref outlineSize, ShaderUniformDataType.ShaderUniformFloat);
        SetShaderValue(shdrOutline, outlineColorLoc, ref outlineColor, ShaderUniformDataType.ShaderUniformVec4);
        SetShaderValue(shdrOutline, textureSizeLoc, ref textureSize, ShaderUniformDataType.ShaderUniformVec2);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            outlineSize += GetMouseWheelMove().Y;
            if (outlineSize < 1.0f)
            {
                outlineSize = 1.0f;
            }

            SetShaderValue(shdrOutline, outlineSizeLoc, ref outlineSize, ShaderUniformDataType.ShaderUniformFloat);

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                BeginShaderMode(shdrOutline);
                {
                    DrawTexture(texture, (GetScreenWidth() / 2) - (texture.Width / 2), -30, White);
                }
                EndShaderMode();

                DrawText("Shader-based\ntexture\noutline", 10, 10, 20, Gray);

                DrawText($"Outline size: {outlineSize} px", 10, 120, 20, Maroon);

                DrawFPS(710, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);
        UnloadShader(shdrOutline);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
