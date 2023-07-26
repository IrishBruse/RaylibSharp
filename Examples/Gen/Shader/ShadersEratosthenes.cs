using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersEratosthenes : ExampleHelper 
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - Sieve of Eratosthenes");

        RenderTexture target = LoadRenderTexture(screenWidth, screenHeight);

        // Load Eratosthenes shader
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(0, TextFormat("resources/shaders/glsl%i/eratosthenes.fs", GLSL_VERSION));

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            // Nothing to do here, everything is happening in the shader

            // Draw
            BeginTextureMode(target);       // Enable drawing to texture
                ClearBackground(Black);     // Clear the render texture

                // Draw a rectangle in shader mode to be used as shader canvas
                // NOTE: RectangleF uses font white character texture coordinates,
                // so shader can not be applied here directly because input vertexTexCoord
                // do not represent full screen coordinates (space where want to apply shader)
                DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Black);
            EndTextureMode();               // End drawing to texture (now we have a blank texture available for the shader)

            BeginDrawing();{
                ClearBackground(RayWhite);  // Clear screen background

                BeginShaderMode(shader);
                    // NOTE: Render texture must be y-flipped due to default OpenGL coordinates (left-bottom)
                    DrawTexture(target.texture, new( 0, 0, (float)target.texture.Width, (float)-target.texture.Height ), new( 0.0f, 0.0f ), White);
                EndShaderMode();
            }EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);               // Unload shader
        UnloadRenderTexture(target);        // Unload render texture

        CloseWindow();                      // Close window and OpenGL context

        return 0;
    }
}
