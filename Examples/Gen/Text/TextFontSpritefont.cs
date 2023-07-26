using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TextFontSpritefont : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - sprite font loading");

        const char msg1[50] = "THIS IS A custom SPRITE FONT...";
        const char msg2[50] = "...and this is ANOTHER CUSTOM font...";
        const char msg3[50] = "...and a THIRD one! GREAT! :D";

        // NOTE: Textures/Fonts MUST be loaded after Window initialization (OpenGL context is required)
        Font font1 = LoadFont("resources/custom_mecha.png");          // Font loading
        Font font2 = LoadFont("resources/custom_alagard.png");        // Font loading
        Font font3 = LoadFont("resources/custom_jupiter_crash.png");  // Font loading

        Vector2 fontPosition1 = { screenWidth/2.0f - MeasureText(font1, msg1, (float)font1.baseSize, -3).X/2,
                                  screenHeight/2.0f - font1.baseSize/2.0f - 80.0f };

        Vector2 fontPosition2 = { screenWidth/2.0f - MeasureText(font2, msg2, (float)font2.baseSize, -2.0f).X/2.0f,
                                  screenHeight/2.0f - font2.baseSize/2.0f - 10.0f };

        Vector2 fontPosition3 = { screenWidth/2.0f - MeasureText(font3, msg3, (float)font3.baseSize, 2.0f).X/2.0f,
                                  screenHeight/2.0f - font3.baseSize/2.0f + 50.0f };

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update variables here...

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText(font1, msg1, fontPosition1, (float)font1.baseSize, -3, White);
                DrawText(font2, msg2, fontPosition2, (float)font2.baseSize, -2, White);
                DrawText(font3, msg3, fontPosition3, (float)font3.baseSize, 2, White);

            }EndDrawing();
        }

        // De-Initialization
        UnloadFont(font1);      // Font unloading
        UnloadFont(font2);      // Font unloading
        UnloadFont(font3);      // Font unloading

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
