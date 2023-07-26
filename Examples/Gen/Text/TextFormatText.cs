using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TextFormatText : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - text formatting");

        int score = 100020;
        int hiscore = 200450;
        int lives = 5;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText(TextFormat("Score: %08i", score), 200, 80, 20, Red);

                DrawText(TextFormat("HiScore: %08i", hiscore), 200, 120, 20, Green);

                DrawText(TextFormat("Lives: %02i", lives), 200, 160, 40, Blue);

                DrawText(TextFormat("Elapsed Time: %02.02f ms", GetFrameTime()*1000), 200, 220, 20, Black);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
