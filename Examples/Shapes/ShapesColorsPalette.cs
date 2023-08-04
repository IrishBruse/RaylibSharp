using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShapesColorsPalette : ExampleHelper 
{

private const int MAX_COLORS_COUNT = 21;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - colors palette");

        Color [] colors = new Color [MAX_COLORS_COUNT]{
            DarkGray, Maroon, Orange, DarkGreen, DarkBlue, DarkPurple, DarkBrown,
            Gray, Red, Gold, Lime, Blue, Violet, Brown, LightGray, Pink, Yellow,
            Green, SkyBlue, Purple, Beige };

        string [] colorNames = new string [MAX_COLORS_COUNT]{
            "DarkGray", "Maroon", "Orange", "DarkGreen", "DarkBlue", "DarkPurple",
            "DarkBrown", "Gray", "Red", "Gold", "Lime", "Blue", "Violet", "Brown",
            "LightGray", "Pink", "Yellow", "Green", "SkyBlue", "Purple", "Beige" };

        RectangleF [] colorsRecs = new RectangleF [MAX_COLORS_COUNT];     // Rectangles array

        // Fills colorsRecs data (for every rectangle)
        for (int i = 0; i < MAX_COLORS_COUNT; i++)
        {
            colorsRecs[i].X = 20.0f + 100.0f *(i%7) + 10.0f *(i%7);
            colorsRecs[i].Y = 80.0f + 100.0f *(i/7) + 10.0f *(i/7);
            colorsRecs[i].Width = 100.0f;
            colorsRecs[i].Height = 100.0f;
        }

        bool [] colorState = new bool [MAX_COLORS_COUNT];           // Color state: 0-DEFAULT, 1-MOUSE_HOVER

        Vector2 mousePoint = new( 0.0f, 0.0f );

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            mousePoint = GetMousePosition();

            for (int i = 0; i < MAX_COLORS_COUNT; i++)
            {
                if (CheckCollisionPoint(mousePoint, colorsRecs[i])) colorState[i] = true;
                else colorState[i] = false;
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("raylib colors palette", 28, 42, 20, Black);
                DrawText("press SPACE to see all colors", GetScreenWidth() - 180, GetScreenHeight() - 40, 10, Gray);

                for (int i = 0; i < MAX_COLORS_COUNT; i++)    // Draw all rectangles
                {
                    DrawRectangle(colorsRecs[i], Fade(colors[i], colorState[i]? 0.6f : 1.0f));

                    if (IsKeyDown(Key.Space) || colorState[i])
                    {
                        DrawRectangle((int)colorsRecs[i].X, (int)(colorsRecs[i].Y + colorsRecs[i].Height - 26), (int)colorsRecs[i].Width, 20, Black);
                        DrawRectangleLines(colorsRecs[i], 6, Fade(Black, 0.3f));
                        DrawText(colorNames[i], (int)(colorsRecs[i].X + colorsRecs[i].Width - MeasureText(colorNames[i], 10) - 12),
                            (int)(colorsRecs[i].Y + colorsRecs[i].Height - 20), 10, colors[i]);
                    }
                }

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
