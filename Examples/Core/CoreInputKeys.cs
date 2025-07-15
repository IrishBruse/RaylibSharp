/*******************************************************************************************
*
*   raylib [core] example - Keyboard input
*
*   Example originally created with raylib 1.0, last time updated with raylib 1.0
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2014-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreInputKeys : ExampleHelper
{
    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - keyboard input");

        Vector2 ballPosition = new((float)screenWidth/2, (float)screenHeight/2);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            if (IsKeyDown(Key.Right)) ballPosition.X += 2.0f;
            if (IsKeyDown(Key.Left)) ballPosition.X -= 2.0f;
            if (IsKeyDown(Key.Up)) ballPosition.Y -= 2.0f;
            if (IsKeyDown(Key.Down)) ballPosition.Y += 2.0f;
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                DrawText("move the ball with arrow keys", 10, 10, 20, DARKGRAY);

                DrawCircleV(ballPosition, 50, MAROON);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

