using static RaylibSharp.Raylib;

public partial class ShapesBasicShapes : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - basic shapes drawing");

        float rotation = 0.0f;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            rotation += 0.2f;

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawText("some basic shapes available on raylib", 20, 20, 20, DarkGray);

                // Circle shapes and lines
                DrawCircle(screenWidth / 5, 120, 35, DarkBlue);
                DrawCircleGradient(screenWidth / 5, 220, 60, Green, SkyBlue);
                DrawCircleLines(screenWidth / 5, 340, 80, DarkBlue);

                // Rectangle shapes and lines
                DrawRectangle((screenWidth / 4 * 2) - 60, 100, 120, 60, Red);
                DrawRectangleGradientH((screenWidth / 4 * 2) - 90, 170, 180, 130, Maroon, Gold);
                DrawRectangleLines((screenWidth / 4 * 2) - 40, 320, 80, 60, Orange); // NOTE: Uses QUADS internally, not lines

                // Triangle shapes and lines
                DrawTriangle(new(screenWidth / 4.0f * 3.0f, 80.0f), new((screenWidth / 4.0f * 3.0f) - 60.0f, 150.0f), new((screenWidth / 4.0f * 3.0f) + 60.0f, 150.0f), Violet);
                DrawTriangleLines(new(screenWidth / 4.0f * 3.0f, 160.0f), new((screenWidth / 4.0f * 3.0f) - 20.0f, 230.0f), new((screenWidth / 4.0f * 3.0f) + 20.0f, 230.0f), DarkBlue);

                // Polygon shapes and lines
                DrawPoly(new(screenWidth / 4.0f * 3, 330), 6, 80, rotation, Brown);
                DrawPolyLines(new(screenWidth / 4.0f * 3, 330), 6, 90, rotation, Brown);
                DrawPolyLines(new(screenWidth / 4.0f * 3, 330), 6, 85, rotation, 6, Beige);

                // NOTE: We draw all LINES based shapes together to optimize internal drawing,
                // this way, all LINES are rendered in a single draw pass
                DrawLine(18, 42, screenWidth - 18, 42, Black);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
