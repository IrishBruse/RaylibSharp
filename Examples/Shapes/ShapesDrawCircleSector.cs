using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShapesDrawCircleSector : ExampleHelper 
{



    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - draw circle sector");

        Vector2 center = new((GetScreenWidth() - 300)/2.0f, GetScreenHeight()/2.0f );

        float outerRadius = 180.0f;
        float startAngle = 0.0f;
        float endAngle = 180.0f;
        int segments = 0;
        int minSegments = 4;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // NOTE: All variables update happens inside GUI control functions

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawLine(500, 0, 500, GetScreenHeight(), Fade(LightGray, 0.6f));
                DrawRectangle(500, 0, GetScreenWidth() - 500, GetScreenHeight(), Fade(LightGray, 0.3f));

                DrawCircleSector(center, outerRadius, startAngle, endAngle, segments, Fade(Maroon, 0.3f));
                DrawCircleSectorLines(center, outerRadius, startAngle, endAngle, segments, Fade(Maroon, 0.6f));

                // Draw GUI controls
                startAngle = GuiSliderBar(new( 600, 40, 120, 20), "StartAngle", null, startAngle, 0, 720);
                endAngle = GuiSliderBar(new( 600, 70, 120, 20), "EndAngle", null, endAngle, 0, 720);

                outerRadius = GuiSliderBar(new( 600, 140, 120, 20), "Radius", null, outerRadius, 0, 200);
                segments = (int)GuiSliderBar(new( 600, 170, 120, 20), "Segments", null, (float)segments, 0, 100);

                minSegments = (int)MathF.Ceiling((endAngle - startAngle) / 90);
                DrawText(TextFormat("MODE: %s", (segments >= minSegments)? "MANUAL" : "AUTO"), 600, 200, 10, (segments >= minSegments)? Maroon : DarkGray);

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
