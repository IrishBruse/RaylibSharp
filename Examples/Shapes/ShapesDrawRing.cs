using System;
using System.Numerics;

using static RaylibSharp.Raylib;

public partial class ShapesDrawRing : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - draw ring");

        Vector2 center = new((GetScreenWidth() - 300) / 2.0f, GetScreenHeight() / 2.0f);

        float innerRadius = 80.0f;
        float outerRadius = 190.0f;

        float startAngle = 0.0f;
        float endAngle = 360.0f;
        int segments = 0;

        bool drawRing = true;
        bool drawRingLines = false;
        bool drawCircleLines = false;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // NOTE: All variables update happens inside GUI control functions

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawLine(500, 0, 500, GetScreenHeight(), Fade(LightGray, 0.6f));
                DrawRectangle(500, 0, GetScreenWidth() - 500, GetScreenHeight(), Fade(LightGray, 0.3f));

                if (drawRing)
                {
                    DrawRing(center, innerRadius, outerRadius, startAngle, endAngle, segments, Fade(Maroon, 0.3f));
                }

                if (drawRingLines)
                {
                    DrawRingLines(center, innerRadius, outerRadius, startAngle, endAngle, segments, Fade(Black, 0.4f));
                }

                if (drawCircleLines)
                {
                    DrawCircleSectorLines(center, outerRadius, startAngle, endAngle, segments, Fade(Black, 0.4f));
                }

                // Draw GUI controls
                startAngle = GuiSliderBar(new(600, 40, 120, 20), "StartAngle", null, startAngle, -450, 450);
                endAngle = GuiSliderBar(new(600, 70, 120, 20), "EndAngle", null, endAngle, -450, 450);

                innerRadius = GuiSliderBar(new(600, 140, 120, 20), "InnerRadius", null, innerRadius, 0, 100);
                outerRadius = GuiSliderBar(new(600, 170, 120, 20), "OuterRadius", null, outerRadius, 0, 200);

                segments = (int)GuiSliderBar(new(600, 240, 120, 20), "Segments", null, segments, 0, 100);

                drawRing = GuiCheckBox(new(600, 320, 20, 20), "Draw Ring", drawRing);
                drawRingLines = GuiCheckBox(new(600, 350, 20, 20), "Draw RingLines", drawRingLines);
                drawCircleLines = GuiCheckBox(new(600, 380, 20, 20), "Draw CircleLines", drawCircleLines);

                int minSegments = (int)MathF.Ceiling((endAngle - startAngle) / 90);
                DrawText("MODE: " + ((segments >= minSegments) ? "MANUAL" : "AUTO"), 600, 270, 10, (segments >= minSegments) ? Maroon : DarkGray);

                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
