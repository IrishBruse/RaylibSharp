using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreInputKeys : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - keyboard input");

        Vector2 ballPosition = new((float)screenWidth / 2, (float)screenHeight / 2);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyDown(Key.Right))
            {
                ballPosition.X += 2.0f;
            }

            if (IsKeyDown(Key.Left))
            {
                ballPosition.X -= 2.0f;
            }

            if (IsKeyDown(Key.Up))
            {
                ballPosition.Y -= 2.0f;
            }

            if (IsKeyDown(Key.Down))
            {
                ballPosition.Y += 2.0f;
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawText("move the ball with arrow keys", 10, 10, 20, DarkGray);

                DrawCircle(ballPosition, 50, Maroon);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }
}
