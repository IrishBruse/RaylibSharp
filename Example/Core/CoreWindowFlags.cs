using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class CoreWindowFlags
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        // Possible window flags
        /*
        FLAG_VSYNC_HINT
        FLAG_FULLSCREEN_MODE    . not working properly . wrong scaling!
        FLAG_WINDOW_RESIZABLE
        FLAG_WINDOW_UNDECORATED
        FLAG_WINDOW_TRANSPARENT
        FLAG_WINDOW_HIDDEN
        FLAG_WINDOW_MINIMIZED   . Not supported on window creation
        FLAG_WINDOW_MAXIMIZED   . Not supported on window creation
        FLAG_WINDOW_UNFOCUSED
        FLAG_WINDOW_TOPMOST
        FLAG_WINDOW_HIGHDPI     . errors after minimize-resize, fb size is recalculated
        FLAG_WINDOW_ALWAYS_RUN
        FLAG_MSAA_4X_HINT
        */

        // Set configuration flags for window creation
        //SetConfigFlags(WindowFlag.Vsync_int | FLAG_MSAA_4X_HINT | FLAG_WINDOW_HIGHDPI);
        InitWindow(screenWidth, screenHeight, "raylib [core] example - window flags");

        Vector2 ballPosition = new(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
        Vector2 ballSpeed = new(5.0f, 4.0f);
        float ballRadius = 20;

        int framesCounter = 0;

        //SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.F))
            {
                ToggleFullscreen();  // modifies window size when scaling!
            }

            if (IsKeyPressed(Key.R))
            {
                if (IsWindowState(WindowFlag.WindowResizable))
                {
                    ClearWindowState(WindowFlag.WindowResizable);
                }
                else
                {
                    SetWindowState(WindowFlag.WindowResizable);
                }
            }

            if (IsKeyPressed(Key.D))
            {
                if (IsWindowState(WindowFlag.WindowUndecorated))
                {
                    ClearWindowState(WindowFlag.WindowUndecorated);
                }
                else
                {
                    SetWindowState(WindowFlag.WindowUndecorated);
                }
            }

            if (IsKeyPressed(Key.H))
            {
                if (!IsWindowState(WindowFlag.WindowHidden))
                {
                    SetWindowState(WindowFlag.WindowHidden);
                }

                framesCounter = 0;
            }

            if (IsWindowState(WindowFlag.WindowHidden))
            {
                framesCounter++;
                if (framesCounter >= 240)
                {
                    ClearWindowState(WindowFlag.WindowHidden); // Show window after 3 seconds
                }
            }

            if (IsKeyPressed(Key.N))
            {
                if (!IsWindowState(WindowFlag.WindowMinimized))
                {
                    MinimizeWindow();
                }

                framesCounter = 0;
            }

            if (IsWindowState(WindowFlag.WindowMinimized))
            {
                framesCounter++;
                if (framesCounter >= 240)
                {
                    RestoreWindow(); // Restore window after 3 seconds
                }
            }

            if (IsKeyPressed(Key.M))
            {
                // NOTE: Requires FLAG_WINDOW_RESIZABLE enabled!
                if (IsWindowState(WindowFlag.WindowMaximized))
                {
                    RestoreWindow();
                }
                else
                {
                    MaximizeWindow();
                }
            }

            if (IsKeyPressed(Key.U))
            {
                if (IsWindowState(WindowFlag.WindowUnfocused))
                {
                    ClearWindowState(WindowFlag.WindowUnfocused);
                }
                else
                {
                    SetWindowState(WindowFlag.WindowUnfocused);
                }
            }

            if (IsKeyPressed(Key.T))
            {
                if (IsWindowState(WindowFlag.WindowTopmost))
                {
                    ClearWindowState(WindowFlag.WindowTopmost);
                }
                else
                {
                    SetWindowState(WindowFlag.WindowTopmost);
                }
            }

            if (IsKeyPressed(Key.A))
            {
                if (IsWindowState(WindowFlag.WindowAlwaysRun))
                {
                    ClearWindowState(WindowFlag.WindowAlwaysRun);
                }
                else
                {
                    SetWindowState(WindowFlag.WindowAlwaysRun);
                }
            }

            if (IsKeyPressed(Key.V))
            {
                if (IsWindowState(WindowFlag.VsyncHint))
                {
                    ClearWindowState(WindowFlag.VsyncHint);
                }
                else
                {
                    SetWindowState(WindowFlag.VsyncHint);
                }
            }

            // Bouncing ball logic
            ballPosition.X += ballSpeed.X;
            ballPosition.Y += ballSpeed.Y;
            if ((ballPosition.X >= (GetScreenWidth() - ballRadius)) || (ballPosition.X <= ballRadius))
            {
                ballSpeed.X *= -1.0f;
            }

            if ((ballPosition.Y >= (GetScreenHeight() - ballRadius)) || (ballPosition.Y <= ballRadius))
            {
                ballSpeed.Y *= -1.0f;
            }

            // Draw
            BeginDrawing();
            {

                if (IsWindowState(WindowFlag.WindowTransparent))
                {
                    ClearBackground(Blank);
                }
                else
                {
                    ClearBackground(RayWhite);
                }

                DrawCircleV(ballPosition, ballRadius, Maroon);
                DrawRectangleLinesEx(new(0, 0, GetScreenWidth(), GetScreenHeight()), 4, RayWhite);

                DrawCircleV(GetMousePosition(), 10, DarkBlue);

                DrawFPS(10, 10);

                DrawText(TextFormat("Screen Size: [%i, %i]", GetScreenWidth(), GetScreenHeight()), 10, 40, 10, Green);

                // Draw window state info
                DrawText("Following flags can be set after window creation:", 10, 60, 10, Gray);
                if (IsWindowState(WindowFlag.FullscreenMode))
                {
                    DrawText("[F] FLAG_FULLSCREEN_MODE: on", 10, 80, 10, Lime);
                }
                else
                {
                    DrawText("[F] FLAG_FULLSCREEN_MODE: off", 10, 80, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowResizable))
                {
                    DrawText("[R] FLAG_WINDOW_RESIZABLE: on", 10, 100, 10, Lime);
                }
                else
                {
                    DrawText("[R] FLAG_WINDOW_RESIZABLE: off", 10, 100, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowUndecorated))
                {
                    DrawText("[D] FLAG_WINDOW_UNDECORATED: on", 10, 120, 10, Lime);
                }
                else
                {
                    DrawText("[D] FLAG_WINDOW_UNDECORATED: off", 10, 120, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowHidden))
                {
                    DrawText("[H] FLAG_WINDOW_HIDDEN: on", 10, 140, 10, Lime);
                }
                else
                {
                    DrawText("[H] FLAG_WINDOW_HIDDEN: off", 10, 140, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowMinimized))
                {
                    DrawText("[N] FLAG_WINDOW_MINIMIZED: on", 10, 160, 10, Lime);
                }
                else
                {
                    DrawText("[N] FLAG_WINDOW_MINIMIZED: off", 10, 160, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowMaximized))
                {
                    DrawText("[M] FLAG_WINDOW_MAXIMIZED: on", 10, 180, 10, Lime);
                }
                else
                {
                    DrawText("[M] FLAG_WINDOW_MAXIMIZED: off", 10, 180, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowUnfocused))
                {
                    DrawText("[G] FLAG_WINDOW_UNFOCUSED: on", 10, 200, 10, Lime);
                }
                else
                {
                    DrawText("[U] FLAG_WINDOW_UNFOCUSED: off", 10, 200, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowTopmost))
                {
                    DrawText("[T] FLAG_WINDOW_TOPMOST: on", 10, 220, 10, Lime);
                }
                else
                {
                    DrawText("[T] FLAG_WINDOW_TOPMOST: off", 10, 220, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowAlwaysRun))
                {
                    DrawText("[A] FLAG_WINDOW_ALWAYS_RUN: on", 10, 240, 10, Lime);
                }
                else
                {
                    DrawText("[A] FLAG_WINDOW_ALWAYS_RUN: off", 10, 240, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.VsyncHint))
                {
                    DrawText("[V] FLAG_VSYNC_HINT: on", 10, 260, 10, Lime);
                }
                else
                {
                    DrawText("[V] FLAG_VSYNC_HINT: off", 10, 260, 10, Maroon);
                }

                DrawText("Following flags can only be set before window creation:", 10, 300, 10, Gray);
                if (IsWindowState(WindowFlag.WindowHighdpi))
                {
                    DrawText("FLAG_WINDOW_HIGHDPI: on", 10, 320, 10, Lime);
                }
                else
                {
                    DrawText("FLAG_WINDOW_HIGHDPI: off", 10, 320, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.WindowTransparent))
                {
                    DrawText("FLAG_WINDOW_TRANSPARENT: on", 10, 340, 10, Lime);
                }
                else
                {
                    DrawText("FLAG_WINDOW_TRANSPARENT: off", 10, 340, 10, Maroon);
                }

                if (IsWindowState(WindowFlag.Msaa4xHint))
                {
                    DrawText("FLAG_MSAA_4X_HINT: on", 10, 360, 10, Lime);
                }
                else
                {
                    DrawText("FLAG_MSAA_4X_HINT: off", 10, 360, 10, Maroon);
                }
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
