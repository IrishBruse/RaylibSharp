/*******************************************************************************************
*
*   raylib [core] example - Window should close
*
*   Example originally created with raylib 4.2, last time updated with raylib 4.2
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2013-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreWindowShouldClose : ExampleHelper
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - window should close");

        SetExitKey(Key.Null);       // Disable Key.Escape to close window, X-button still works

        bool exitWindowRequested = false;   // Flag to request window to exit
        bool exitWindow = false;    // Flag to set window to exit

        SetTargetFPS(60);           // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!exitWindow)
        {
            // Update
            //----------------------------------------------------------------------------------
            // Detect if X-button or Key.Escape have been pressed to close window
            if (WindowShouldClose() || IsKeyPressed(Key.Escape)) exitWindowRequested = true;

            if (exitWindowRequested)
            {
                // A request for close window has been issued, we can save data before closing
                // or just show a message asking for confirmation

                if (IsKeyPressed(Key.Y)) exitWindow = true;
                else if (IsKeyPressed(Key.N)) exitWindowRequested = false;
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                if (exitWindowRequested)
                {
                    DrawRectangle(0, 100, screenWidth, 200, BLACK);
                    DrawText("Are you sure you want to exit program? [Y/N]", 40, 180, 30, WHITE);
                }
                else DrawText("Try to close the window to get confirmation message!", 120, 200, 20, LIGHTGRAY);

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

