using System.Drawing;

using static RaylibSharp.Raylib;

public partial class ShapesDrawRectangleRounded : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - draw rectangle rounded");

        float roundness = 0.2f;
        int width = 200;
        int height = 100;
        int segments = 0;
        int lineThick = 1;

        bool drawRect = false;
        bool drawRoundedRect = true;
        bool drawRoundedLines = false;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            RectangleF rec = new(((float)GetScreenWidth() - width - 250) / 2, (GetScreenHeight() - height) / 2.0f, width, height);

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawLine(560, 0, 560, GetScreenHeight(), Fade(LightGray, 0.6f));
                DrawRectangle(560, 0, GetScreenWidth() - 500, GetScreenHeight(), Fade(LightGray, 0.3f));

                if (drawRect)
                {
                    DrawRectangle(rec, Fade(Gold, 0.6f));
                }

                if (drawRoundedRect)
                {
                    DrawRectangleRounded(rec, roundness, segments, Fade(Maroon, 0.2f));
                }

                if (drawRoundedLines)
                {
                    DrawRectangleRoundedLines(rec, roundness, segments, lineThick, Fade(Maroon, 0.4f));
                }

                // Draw GUI controls
                width = (int)GuiSliderBar(new(640, 40, 105, 20), "Width", null, width, 0, (float)GetScreenWidth() - 300);
                height = (int)GuiSliderBar(new(640, 70, 105, 20), "Height", null, height, 0, (float)GetScreenHeight() - 50);
                roundness = GuiSliderBar(new(640, 140, 105, 20), "Roundness", null, roundness, 0.0f, 1.0f);
                lineThick = (int)GuiSliderBar(new(640, 170, 105, 20), "Thickness", null, lineThick, 0, 20);
                segments = (int)GuiSliderBar(new(640, 240, 105, 20), "Segments", null, segments, 0, 60);

                drawRoundedRect = GuiCheckBox(new(640, 320, 20, 20), "DrawRoundedRect", drawRoundedRect);
                drawRoundedLines = GuiCheckBox(new(640, 350, 20, 20), "DrawRoundedLines", drawRoundedLines);
                drawRect = GuiCheckBox(new(640, 380, 20, 20), "DrawRect", drawRect);

                DrawText("MODE: " + ((segments >= 4) ? "MANUAL" : "AUTO"), 640, 280, 10, (segments >= 4) ? Maroon : DarkGray);

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
