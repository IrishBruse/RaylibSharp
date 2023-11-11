using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersShapesTextures : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - shapes and texture shaders");

        Texture fudesumi = LoadTexture("resources/fudesumi.png");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load shader to be used on some parts drawing
        // NOTE 1: Using GLSL 330 shader version, on OpenGL ES 2.0 use GLSL 100 shader version
        // NOTE 2: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/grayscale.fs");

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // Start drawing with default shader

                DrawText("USING DEFAULT SHADER", 20, 40, 10, Red);

                DrawCircle(80, 120, 35, DarkBlue);
                DrawCircleGradient(80, 220, 60, Green, SkyBlue);
                DrawCircleLines(80, 340, 80, DarkBlue);

                // Activate our custom shader to be applied on next shapes/textures drawings
                BeginShaderMode(shader);
                {

                    DrawText("USING CUSTOM SHADER", 190, 40, 10, Red);

                    DrawRectangle(250 - 60, 90, 120, 60, Red);
                    DrawRectangleGradientH(250 - 90, 170, 180, 130, Maroon, Gold);
                    DrawRectangleLines(250 - 40, 320, 80, 60, Orange);

                    // Activate our default shader for next drawings
                }
                EndShaderMode();

                DrawText("USING DEFAULT SHADER", 370, 40, 10, Red);

                DrawTriangle(new(430, 80),
                             new(430 - 60, 150),
                             new(430 + 60, 150), Violet);

                DrawTriangleLines(new(430, 160),
                                  new(430 - 20, 230),
                                  new(430 + 20, 230), DarkBlue);

                DrawPoly(new(430, 320), 6, 80, 0, Brown);

                // Activate our custom shader to be applied on next shapes/textures drawings
                BeginShaderMode(shader);
                {

                    DrawTexture(fudesumi, 500, -30, White); // Using custom shader

                    // Activate our default shader for next drawings
                }
                EndShaderMode();

                DrawText("(c) Fudesumi sprite by Eiden Marsal", 380, screenHeight - 20, 10, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader); // Unload shader
        UnloadTexture(fudesumi); // Unload texture

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
