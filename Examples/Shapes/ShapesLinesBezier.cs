using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShapesLinesBezier : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint);
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - cubic-bezier lines");

        Vector2 start = new( 0, 0 );
        Vector2 end = new( (float)screenWidth, (float)screenHeight );

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsMouseButtonDown(MouseButton.Left)) start = GetMousePosition();
            else if (IsMouseButtonDown(MouseButton.Right)) end = GetMousePosition();

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("USE MOUSE LEFT-RIGHT CLICK to DEFINE LINE START and END POINTS", 15, 20, 20, Gray);

                DrawLineBezier(start, end, 2.0f, Red);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
