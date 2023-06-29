using System.Numerics;

using static RaylibSharp.Raylib;

public static partial class Example
{
    private static readonly int MAX_TOUCH_POINTS = 10;

    // Program main entry point
    public static int CoreInputMultitouch()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - input multitouch");

        Vector2[] touchPositions = new Vector2[MAX_TOUCH_POINTS];

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // Get the touch point count ( how many fingers are touching the screen )
            int tCount = GetTouchPointCount();
            // Clamp touch points available ( set the maximum touch points allowed )
            if (tCount > MAX_TOUCH_POINTS)
            {
                tCount = MAX_TOUCH_POINTS;
            }
            // Get touch points positions
            for (int i = 0; i < tCount; ++i)
            {
                touchPositions[i] = GetTouchPosition(i);
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                for (int i = 0; i < tCount; ++i)
                {
                    // Make sure point is not (0, 0) as this means there is no touch for it
                    if ((touchPositions[i].X > 0) && (touchPositions[i].Y > 0))
                    {
                        // Draw circle and touch index number
                        DrawCircleV(touchPositions[i], 34, Orange);
                        DrawText(TextFormat("%d", i), (int)touchPositions[i].X - 10, (int)touchPositions[i].Y - 70, 40, Black);
                    }
                }

                DrawText("touch the screen at multiple locations to get multiple balls", 10, 10, 20, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
