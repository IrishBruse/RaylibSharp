using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class CoreScissorTest
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - scissor test");

        RectangleF scissorArea = new(0, 0, 300, 300);
        bool scissorMode = true;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.S))
            {
                scissorMode = !scissorMode;
            }

            // Centre the scissor area around the mouse position
            scissorArea.X = GetMouseX() - (scissorArea.Width / 2);
            scissorArea.Y = GetMouseY() - (scissorArea.Height / 2);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                if (scissorMode)
                {
                    BeginScissorMode((int)scissorArea.X, (int)scissorArea.Y, (int)scissorArea.Width, (int)scissorArea.Height);
                }

                // Draw full screen rectangle and some text
                // NOTE: Only part defined by scissor area will be rendered
                DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Red);
                DrawText("Move the mouse around to reveal this text!", 190, 200, 20, LightGray);

                if (scissorMode)
                {
                    EndScissorMode();
                }

                DrawRectangleLinesEx(scissorArea, 1, Black);
                DrawText("Press S to toggle scissor test", 10, 10, 20, Black);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
