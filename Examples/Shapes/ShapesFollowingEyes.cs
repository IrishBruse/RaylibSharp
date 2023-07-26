using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShapesFollowingEyes : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - following eyes");

        Vector2 scleraLeftPosition = new( GetScreenWidth()/2.0f - 100.0f, GetScreenHeight()/2.0f );
        Vector2 scleraRightPosition = new( GetScreenWidth()/2.0f + 100.0f, GetScreenHeight()/2.0f );
        float scleraRadius = 80;

        Vector2 irisLeftPosition = new( GetScreenWidth()/2.0f - 100.0f, GetScreenHeight()/2.0f );
        Vector2 irisRightPosition = new( GetScreenWidth()/2.0f + 100.0f, GetScreenHeight()/2.0f );
        float irisRadius = 24;

        float angle = 0.0f;
        float dx = 0.0f, dy = 0.0f, dxx = 0.0f, dyy = 0.0f;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            irisLeftPosition = GetMousePosition();
            irisRightPosition = GetMousePosition();

            // Check not inside the left eye sclera
            if (!CheckCollisionPointCircle(irisLeftPosition, scleraLeftPosition, scleraRadius - 20))
            {
                dx = irisLeftPosition.X - scleraLeftPosition.X;
                dy = irisLeftPosition.Y - scleraLeftPosition.Y;

                angle = MathF.Atan2(dy, dx);

                dxx = (scleraRadius - irisRadius)*MathF.Cos(angle);
                dyy = (scleraRadius - irisRadius)*MathF.Sin(angle);

                irisLeftPosition.X = scleraLeftPosition.X + dxx;
                irisLeftPosition.Y = scleraLeftPosition.Y + dyy;
            }

            // Check not inside the right eye sclera
            if (!CheckCollisionPointCircle(irisRightPosition, scleraRightPosition, scleraRadius - 20))
            {
                dx = irisRightPosition.X - scleraRightPosition.X;
                dy = irisRightPosition.Y - scleraRightPosition.Y;

                angle = MathF.Atan2(dy, dx);

                dxx = (scleraRadius - irisRadius)*MathF.Cos(angle);
                dyy = (scleraRadius - irisRadius)*MathF.Sin(angle);

                irisRightPosition.X = scleraRightPosition.X + dxx;
                irisRightPosition.Y = scleraRightPosition.Y + dyy;
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawCircle(scleraLeftPosition, scleraRadius, LightGray);
                DrawCircle(irisLeftPosition, irisRadius, Brown);
                DrawCircle(irisLeftPosition, 10, Black);

                DrawCircle(scleraRightPosition, scleraRadius, LightGray);
                DrawCircle(irisRightPosition, irisRadius, DarkGreen);
                DrawCircle(irisRightPosition, 10, Black);

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
