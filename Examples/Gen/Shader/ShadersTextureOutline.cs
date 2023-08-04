using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersTextureOutline : ExampleHelper 
{

    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - Apply an outline to a texture");

        Texture texture = LoadTexture("resources/fudesumi.png");

        Shader shdrOutline = LoadShader(0, TextFormat("resources/shaders/glsl%i/outline.fs", GLSL_VERSION));

        float outlineSize = 2.0f;
        float [] outlineColor = new float [4]new( 1.0f, 0.0f, 0.0f, 1.0f );     // Normalized Red color
        float [] textureSize = new float [2]new( (float)texture.Width, (float)texture.Height );

        // Get shader locations
        int outlineSizeLoc = GetShaderLocation(shdrOutline, "outlineSize");
        int outlineColorLoc = GetShaderLocation(shdrOutline, "outlineColor");
        int textureSizeLoc = GetShaderLocation(shdrOutline, "textureSize");

        // Set shader values (they can be changed later)
        SetShaderValue(shdrOutline, outlineSizeLoc, ref outlineSize, SHADER_UNIFORM_FLOAT);
        SetShaderValue(shdrOutline, outlineColorLoc, outlineColor, SHADER_UNIFORM_VEC4);
        SetShaderValue(shdrOutline, textureSizeLoc, textureSize, SHADER_UNIFORM_VEC2);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            outlineSize += GetMouseWheelMove();
            if (outlineSize < 1.0f) outlineSize = 1.0f;

            SetShaderValue(shdrOutline, outlineSizeLoc, ref outlineSize, SHADER_UNIFORM_FLOAT);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginShaderMode(shdrOutline);

                    DrawTexture(texture, GetScreenWidth()/2 - texture.Width/2, -30, White);

                EndShaderMode();

                DrawText("Shader-based\ntexture\noutline", 10, 10, 20, Gray);

                DrawText(TextFormat("Outline size: %i px", (int)outlineSize), 10, 120, 20, Maroon);

                DrawFPS(710, 10);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);
        UnloadShader(shdrOutline);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
