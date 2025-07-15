/*******************************************************************************************
*
*   raylib [core] example - Scissor test
*
*   Example originally created with raylib 2.5, last time updated with raylib 3.0
*
*   Example contributed by Chris Dill (@MysteriousSpace) and reviewed by Ramon Santamaria (@raysan5)
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2019-2024 Chris Dill (@MysteriousSpace)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreScissorTest : ExampleHelper
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - scissor test");

        Rectangle scissorArea = new(0, 0, 300, 300);
        bool scissorMode = true;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            if (IsKeyPressed(Key.S)) scissorMode = !scissorMode;

            // Centre the scissor area around the mouse position
            scissorArea.X = GetMouseX() - scissorArea.Width/2;
            scissorArea.Y = GetMouseY() - scissorArea.Height/2;
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                if (scissorMode) BeginScissorMode((int)scissorArea.X, (int)scissorArea.Y, (int)scissorArea.Width, (int)scissorArea.Height);

                // Draw full screen rectangle and some text
                // NOTE: Only part defined by scissor area will be rendered
                DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), RED);
                DrawText("Move the mouse around to reveal this text!", 190, 200, 20, LIGHTGRAY);

                if (scissorMode) EndScissorMode();

                DrawRectangleLinesEx(scissorArea, 1, BLACK);
                DrawText("Press S to toggle scissor test", 10, 10, 20, BLACK);

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

