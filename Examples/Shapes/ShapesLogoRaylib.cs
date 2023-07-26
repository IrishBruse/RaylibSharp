using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShapesLogoRaylib : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - raylib logo using shapes");

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawRectangle(screenWidth/2 - 128, screenHeight/2 - 128, 256, 256, Black);
                DrawRectangle(screenWidth/2 - 112, screenHeight/2 - 112, 224, 224, RayWhite);
                DrawText("raylib", screenWidth/2 - 44, screenHeight/2 + 48, 50, Black);

                DrawText("this is NOT a texture!", 350, 370, 10, Gray);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
