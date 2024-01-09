using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreInputMouse : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - mouse input");

        Color ballColor = DarkBlue;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            Vector2 ballPosition = GetMousePosition();

            if (IsMouseButtonPressed(MouseButton.Left))
            {
                ballColor = Maroon;
            }
            else if (IsMouseButtonPressed(MouseButton.Middle))
            {
                ballColor = Lime;
            }
            else if (IsMouseButtonPressed(MouseButton.Right))
            {
                ballColor = DarkBlue;
            }
            else if (IsMouseButtonPressed(MouseButton.Side))
            {
                ballColor = Purple;
            }
            else if (IsMouseButtonPressed(MouseButton.Extra))
            {
                ballColor = Yellow;
            }
            else if (IsMouseButtonPressed(MouseButton.Forward))
            {
                ballColor = Orange;
            }
            else if (IsMouseButtonPressed(MouseButton.Back))
            {
                ballColor = Beige;
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawCircle(ballPosition, 40, ballColor);

                DrawText("move ball with mouse and click mouse button to change color", 10, 10, 20, DarkGray);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }
}
