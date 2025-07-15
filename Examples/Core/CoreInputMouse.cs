/*******************************************************************************************
*
*   raylib [core] example - Mouse input
*
*   Example originally created with raylib 1.0, last time updated with raylib 4.0
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2014-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreInputMouse : ExampleHelper
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - mouse input");

        Vector2 ballPosition = new(-100.0f, -100.0f);
        Color ballColor = DARKBLUE;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //---------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            ballPosition = GetMousePosition();

            if (IsMouseButtonPressed(MouseButton.Left)) ballColor = MAROON;
            else if (IsMouseButtonPressed(MouseButton.Middle)) ballColor = LIME;
            else if (IsMouseButtonPressed(MouseButton.Right)) ballColor = DARKBLUE;
            else if (IsMouseButtonPressed(MouseButton.Side)) ballColor = PURPLE;
            else if (IsMouseButtonPressed(MouseButton.Extra)) ballColor = YELLOW;
            else if (IsMouseButtonPressed(MouseButton.Forward)) ballColor = ORANGE;
            else if (IsMouseButtonPressed(MouseButton.Back)) ballColor = BEIGE;
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                DrawCircleV(ballPosition, 40, ballColor);

                DrawText("move ball with mouse and click mouse button to change color", 10, 10, 20, DARKGRAY);

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

