/*******************************************************************************************
*
*   raylib [core] example - Gamepad information
*
*   NOTE: This example requires a Gamepad connected to the system
*         Check raylib.h for buttons configuration
*
*   Example originally created with raylib 4.6, last time updated with raylib 4.6
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2013-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreInputGamepadInfo : ExampleHelper
{
    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static void Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint);  // Set MSAA 4X hint before windows creation

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - gamepad information");

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            // TODO: Update your variables here
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                for (int i = 0, y = 5; i < 4; i++)     // MAX_GAMEPADS = 4
                {
                    if (IsGamepadAvailable(i))
                    {
                        DrawText(TextFormat("Gamepad name: %s", GetGamepadName(i)), 10, y, 10, BLACK);
                        y += 11;
                        DrawText(TextFormat("\tAxis count:   %d", GetGamepadAxisCount(i)), 10, y, 10, BLACK);
                        y += 11;

                        for (int axis = 0; axis < GetGamepadAxisCount(i); axis++)
                        {
                            DrawText(TextFormat("\tAxis %d = %f", axis, GetGamepadAxisMovement(i, (GamepadAxis)axis)), 10, y, 10, BLACK);
                            y += 11;
                        }

                        for (int button = 0; button < 32; button++)
                        {
                            DrawText(TextFormat("\tButton %d = %d", button, IsGamepadButtonDown(i, (GamepadButton)button)), 10, y, 10, BLACK);
                            y += 11;
                        }
                    }
                }

                DrawFPS(GetScreenWidth() - 100, 100);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------
    }
}

