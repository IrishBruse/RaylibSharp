using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class Example
{
    // Program main entry point
    public static int CoreWindowShouldClose()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - window should close");

        SetExitKey(Key.Null);       // Disable KEY_ESCAPE to close window, X-button still works

        bool exitWindowRequested = false;   // Flag to request window to exit
        bool exitWindow = false;    // Flag to set window to exit

        SetTargetFPS(60);           // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!exitWindow)
        {
            // Update
            // Detect if X-button or KEY_ESCAPE have been pressed to close window
            if (WindowShouldClose() || IsKeyPressed(Key.Escape))
            {
                exitWindowRequested = true;
            }

            if (exitWindowRequested)
            {
                // A request for close window has been issued, we can save data before closing
                // or just show a message asking for confirmation

                if (IsKeyPressed(Key.Y))
                {
                    exitWindow = true;
                }
                else if (IsKeyPressed(Key.N))
                {
                    exitWindowRequested = false;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                if (exitWindowRequested)
                {
                    DrawRectangle(0, 100, screenWidth, 200, Black);
                    DrawText("Are you sure you want to exit program? [Y/N]", 40, 180, 30, White);
                }
                else
                {
                    DrawText("Try to close the window to get confirmation message!", 120, 200, 20, LightGray);
                }
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
