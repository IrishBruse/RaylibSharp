using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersTextureWaves : ExampleHelper 
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - texture waves");

        // Load texture texture to apply shaders
        Texture texture = LoadTexture("resources/space.png");

        // Load shader and setup location points and values
        Shader shader = LoadShader(0, TextFormat("resources/shaders/glsl%i/wave.fs", GLSL_VERSION));

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

        float [] screenSize = new float [2]new( (float)GetScreenWidth(), (float)GetScreenHeight() );
        SetShaderValue(shader, GetShaderLocation(shader, "size"), &screenSize, SHADER_UNIFORM_VEC2);
        SetShaderValue(shader, freqXLoc, &freqX, SHADER_UNIFORM_FLOAT);
        SetShaderValue(shader, freqYLoc, &freqY, SHADER_UNIFORM_FLOAT);
        SetShaderValue(shader, ampXLoc, &ampX, SHADER_UNIFORM_FLOAT);
        SetShaderValue(shader, ampYLoc, &ampY, SHADER_UNIFORM_FLOAT);
        SetShaderValue(shader, speedXLoc, &speedX, SHADER_UNIFORM_FLOAT);
        SetShaderValue(shader, speedYLoc, &speedY, SHADER_UNIFORM_FLOAT);

        float seconds = 0.0f;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        // -------------------------------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            seconds += GetFrameTime();

            SetShaderValue(shader, secondsLoc, &seconds, SHADER_UNIFORM_FLOAT);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginShaderMode(shader);

                    DrawTexture(texture, 0, 0, White);
                    DrawTexture(texture, texture.Width, 0, White);

                EndShaderMode();

            }EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);         // Unload shader
        UnloadTexture(texture);       // Unload texture

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
