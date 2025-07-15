/*******************************************************************************************
*
*   raylib [core] example - window flags
*
*   Example originally created with raylib 3.5, last time updated with raylib 3.5
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2020-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreWindowFlags : ExampleHelper
{
    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //---------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        // Possible window flags
        /*
        WindowFlag.VsyncHint
        WindowFlag.FullscreenMode    . not working properly . wrong scaling!
        WindowFlag.Resizable
        WindowFlag.Undecorated
        WindowFlag.Transparent
        WindowFlag.Hidden
        WindowFlag.Minimized   . Not supported on window creation
        WindowFlag.Maximized   . Not supported on window creation
        WindowFlag.Unfocused
        WindowFlag.Topmost
        WindowFlag.Highdpi     . errors after minimize-resize, fb size is recalculated
        WindowFlag.AlwaysRun
        WindowFlag.Msaa4xHint
        */

        // Set configuration flags for window creation
        //SetConfigFlags(WindowFlag.VsyncHint | WindowFlag.Msaa4xHint | WindowFlag.Highdpi);
        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - window flags");

        Vector2 ballPosition = new(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
        Vector2 ballSpeed = new(5.0f, 4.0f);
        float ballRadius = 20;

        int framesCounter = 0;

        //SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //----------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //-----------------------------------------------------
            if (IsKeyPressed(Key.F)) ToggleFullscreen();  // modifies window size when scaling!

            if (IsKeyPressed(Key.R))
            {
                if (IsWindowState(WindowFlag.Resizable)) ClearWindowState(WindowFlag.Resizable);
                else SetWindowState(WindowFlag.Resizable);
            }

            if (IsKeyPressed(Key.D))
            {
                if (IsWindowState(WindowFlag.Undecorated)) ClearWindowState(WindowFlag.Undecorated);
                else SetWindowState(WindowFlag.Undecorated);
            }

            if (IsKeyPressed(Key.H))
            {
                if (!IsWindowState(WindowFlag.Hidden)) SetWindowState(WindowFlag.Hidden);

                framesCounter = 0;
            }

            if (IsWindowState(WindowFlag.Hidden))
            {
                framesCounter++;
                if (framesCounter >= 240) ClearWindowState(WindowFlag.Hidden); // Show window after 3 seconds
            }

            if (IsKeyPressed(Key.N))
            {
                if (!IsWindowState(WindowFlag.Minimized)) MinimizeWindow();

                framesCounter = 0;
            }

            if (IsWindowState(WindowFlag.Minimized))
            {
                framesCounter++;
                if (framesCounter >= 240) RestoreWindow(); // Restore window after 3 seconds
            }

            if (IsKeyPressed(Key.M))
            {
                // NOTE: Requires WindowFlag.Resizable enabled!
                if (IsWindowState(WindowFlag.Maximized)) RestoreWindow();
                else MaximizeWindow();
            }

            if (IsKeyPressed(Key.U))
            {
                if (IsWindowState(WindowFlag.Unfocused)) ClearWindowState(WindowFlag.Unfocused);
                else SetWindowState(WindowFlag.Unfocused);
            }

            if (IsKeyPressed(Key.T))
            {
                if (IsWindowState(WindowFlag.Topmost)) ClearWindowState(WindowFlag.Topmost);
                else SetWindowState(WindowFlag.Topmost);
            }

            if (IsKeyPressed(Key.A))
            {
                if (IsWindowState(WindowFlag.AlwaysRun)) ClearWindowState(WindowFlag.AlwaysRun);
                else SetWindowState(WindowFlag.AlwaysRun);
            }

            if (IsKeyPressed(Key.V))
            {
                if (IsWindowState(WindowFlag.VsyncHint)) ClearWindowState(WindowFlag.VsyncHint);
                else SetWindowState(WindowFlag.VsyncHint);
            }

            // Bouncing ball logic
            ballPosition.X += ballSpeed.X;
            ballPosition.Y += ballSpeed.Y;
            if ((ballPosition.X >= (GetScreenWidth() - ballRadius)) || (ballPosition.X <= ballRadius)) ballSpeed.X *= -1.0f;
            if ((ballPosition.Y >= (GetScreenHeight() - ballRadius)) || (ballPosition.Y <= ballRadius)) ballSpeed.Y *= -1.0f;
            //-----------------------------------------------------

            // Draw
            //-----------------------------------------------------
            BeginDrawing();

            if (IsWindowState(WindowFlag.Transparent)) ClearBackground(BLANK);
            else ClearBackground(RAYWHITE);

            DrawCircleV(ballPosition, ballRadius, MAROON);
            DrawRectangleLinesEx((Rectangle) new(0, 0, (float)GetScreenWidth(), (float)GetScreenHeight()), 4, RAYWHITE);

            DrawCircleV(GetMousePosition(), 10, DARKBLUE);

            DrawFPS(10, 10);

            DrawText(TextFormat("Screen Size: [%i, %i]", GetScreenWidth(), GetScreenHeight()), 10, 40, 10, GREEN);

            // Draw window state info
            DrawText("Following flags can be set after window creation:", 10, 60, 10, GRAY);
            if (IsWindowState(WindowFlag.FullscreenMode)) DrawText("[F] WindowFlag.FullscreenMode: on", 10, 80, 10, LIME);
            else DrawText("[F] WindowFlag.FullscreenMode: off", 10, 80, 10, MAROON);
            if (IsWindowState(WindowFlag.Resizable)) DrawText("[R] WindowFlag.Resizable: on", 10, 100, 10, LIME);
            else DrawText("[R] WindowFlag.Resizable: off", 10, 100, 10, MAROON);
            if (IsWindowState(WindowFlag.Undecorated)) DrawText("[D] WindowFlag.Undecorated: on", 10, 120, 10, LIME);
            else DrawText("[D] WindowFlag.Undecorated: off", 10, 120, 10, MAROON);
            if (IsWindowState(WindowFlag.Hidden)) DrawText("[H] WindowFlag.Hidden: on", 10, 140, 10, LIME);
            else DrawText("[H] WindowFlag.Hidden: off", 10, 140, 10, MAROON);
            if (IsWindowState(WindowFlag.Minimized)) DrawText("[N] WindowFlag.Minimized: on", 10, 160, 10, LIME);
            else DrawText("[N] WindowFlag.Minimized: off", 10, 160, 10, MAROON);
            if (IsWindowState(WindowFlag.Maximized)) DrawText("[M] WindowFlag.Maximized: on", 10, 180, 10, LIME);
            else DrawText("[M] WindowFlag.Maximized: off", 10, 180, 10, MAROON);
            if (IsWindowState(WindowFlag.Unfocused)) DrawText("[G] WindowFlag.Unfocused: on", 10, 200, 10, LIME);
            else DrawText("[U] WindowFlag.Unfocused: off", 10, 200, 10, MAROON);
            if (IsWindowState(WindowFlag.Topmost)) DrawText("[T] WindowFlag.Topmost: on", 10, 220, 10, LIME);
            else DrawText("[T] WindowFlag.Topmost: off", 10, 220, 10, MAROON);
            if (IsWindowState(WindowFlag.AlwaysRun)) DrawText("[A] WindowFlag.AlwaysRun: on", 10, 240, 10, LIME);
            else DrawText("[A] WindowFlag.AlwaysRun: off", 10, 240, 10, MAROON);
            if (IsWindowState(WindowFlag.VsyncHint)) DrawText("[V] WindowFlag.VsyncHint: on", 10, 260, 10, LIME);
            else DrawText("[V] WindowFlag.VsyncHint: off", 10, 260, 10, MAROON);

            DrawText("Following flags can only be set before window creation:", 10, 300, 10, GRAY);
            if (IsWindowState(WindowFlag.Highdpi)) DrawText("WindowFlag.Highdpi: on", 10, 320, 10, LIME);
            else DrawText("WindowFlag.Highdpi: off", 10, 320, 10, MAROON);
            if (IsWindowState(WindowFlag.Transparent)) DrawText("WindowFlag.Transparent: on", 10, 340, 10, LIME);
            else DrawText("WindowFlag.Transparent: off", 10, 340, 10, MAROON);
            if (IsWindowState(WindowFlag.Msaa4xHint)) DrawText("WindowFlag.Msaa4xHint: on", 10, 360, 10, LIME);
            else DrawText("WindowFlag.Msaa4xHint: off", 10, 360, 10, MAROON);

            EndDrawing();
            //-----------------------------------------------------
        }

        // De-Initialization
        //---------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //----------------------------------------------------------

        return 0;
    }
}

