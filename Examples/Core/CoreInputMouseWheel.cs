using static RaylibSharp.Raylib;

public class CoreInputMouseWheel : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - Core - input mouse wheel");

        int boxPositionY = (screenHeight / 2) - 40;
        int scrollSpeed = 4;            // Scrolling speed in pixels

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            boxPositionY -= (int)GetMouseWheelMove().Y * scrollSpeed;

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawRectangle((screenWidth / 2) - 40, boxPositionY, 80, 80, Maroon);

                DrawText("Use mouse wheel to move the cube up and down!", 10, 10, 20, Gray);
                DrawText("Box position Y: " + boxPositionY, 10, 40, 20, LightGray);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }
}
