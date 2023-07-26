using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersHotReloading : ExampleHelper 
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - hot reloading");

        string fragShaderFileName = "resources/shaders/glsl%i/reload.fs";
        time_t fragShaderFileModTime = GetFileModTime(TextFormat(fragShaderFileName, GLSL_VERSION));

        // Load raymarching shader
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(0, TextFormat(fragShaderFileName, GLSL_VERSION));

        // Get shader locations for required uniforms
        int resolutionLoc = GetShaderLocation(shader, "resolution");
        int mouseLoc = GetShaderLocation(shader, "mouse");
        int timeLoc = GetShaderLocation(shader, "time");

        float [] resolution = new float [2]new( (float)screenWidth, (float)screenHeight );
        SetShaderValue(shader, resolutionLoc, resolution, SHADER_UNIFORM_VEC2);

        float totalTime = 0.0f;
        bool shaderAutoReloading = false;

        SetTargetFPS(60);                       // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())            // Detect window close button or ESC key
        {
            // Update
            totalTime += GetFrameTime();
            Vector2 mouse = GetMousePosition();
            float [] mousePos = new float [2]new( mouse.X, mouse.Y );

            // Set shader required uniform values
            SetShaderValue(shader, timeLoc, &totalTime, SHADER_UNIFORM_FLOAT);
            SetShaderValue(shader, mouseLoc, mousePos, SHADER_UNIFORM_VEC2);

            // Hot shader reloading
            if (shaderAutoReloading || (IsMouseButtonPressed(MouseButton.Left)))
            {
                long currentFragShaderModTime = GetFileModTime(TextFormat(fragShaderFileName, GLSL_VERSION));

                // Check if shader file has been modified
                if (currentFragShaderModTime != fragShaderFileModTime)
                {
                    // Try reloading updated shader
                    Shader updatedShader = LoadShader(0, TextFormat(fragShaderFileName, GLSL_VERSION));

                    if (updatedShader.id != rlGetShaderIdDefault())      // It was correctly loaded
                    {
                        UnloadShader(shader);
                        shader = updatedShader;

                        // Get shader locations for required uniforms
                        resolutionLoc = GetShaderLocation(shader, "resolution");
                        mouseLoc = GetShaderLocation(shader, "mouse");
                        timeLoc = GetShaderLocation(shader, "time");

                        // Reset required uniforms
                        SetShaderValue(shader, resolutionLoc, resolution, SHADER_UNIFORM_VEC2);
                    }

                    fragShaderFileModTime = currentFragShaderModTime;
                }
            }

            if (IsKeyPressed(Key.A)) shaderAutoReloading = !shaderAutoReloading;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                // We only draw a white full-screen rectangle, frame is generated in shader
                BeginShaderMode(shader);
                    DrawRectangle(0, 0, screenWidth, screenHeight, White);
                EndShaderMode();

                DrawText(TextFormat("PRESS [A] to TOGGLE SHADER AUTOLOADING: %s",
                         shaderAutoReloading? "AUTO" : "MANUAL"), 10, 10, 10, shaderAutoReloading? Red : Black);
                if (!shaderAutoReloading) DrawText("MOUSE CLICK to SHADER RE-LOADING", 10, 30, 10, Black);

                DrawText(TextFormat("Shader last modification: %s", asctime(localtime(&fragShaderFileModTime))), 10, 430, 10, Black);

            }EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);           // Unload shader

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
