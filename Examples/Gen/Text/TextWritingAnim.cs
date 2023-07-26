using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TextWritingAnim : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - text writing anim");

        const char message[128] = "This sample illustrates a text writing\nanimation effect! Check it out! ;)";

        int framesCounter = 0;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyDown(Key.Space)) framesCounter += 8;
            else framesCounter++;

            if (IsKeyPressed(Key.Enter)) framesCounter = 0;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText(TextSubtext(message, 0, framesCounter/10), 210, 160, 20, Maroon);

                DrawText("PRESS [ENTER] to RESTART!", 240, 260, 20, LightGray);
                DrawText("PRESS [SPACE] to SPEED UP!", 239, 300, 20, LightGray);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
