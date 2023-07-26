using static RaylibSharp.Raylib;

public class CoreBasicWindow : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - basic window");

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose()) // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);
                DrawText("Congrats! You created your first window!", 190, 200, 20, LightGray);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }
}
