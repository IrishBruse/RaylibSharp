using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShapesBouncingBall : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - bouncing ball");

        Vector2 ballPosition = new(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
        Vector2 ballSpeed = new(5.0f, 4.0f);
        int ballRadius = 20;

        bool pause = false; ;
        int framesCounter = 0;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.Space))
            {
                pause = !pause;
            }

            if (!pause)
            {
                ballPosition.X += ballSpeed.X;
                ballPosition.Y += ballSpeed.Y;

                // Check walls collision for bouncing
                if ((ballPosition.X >= (GetScreenWidth() - ballRadius)) || (ballPosition.X <= ballRadius))
                {
                    ballSpeed.X *= -1.0f;
                }

                if ((ballPosition.Y >= (GetScreenHeight() - ballRadius)) || (ballPosition.Y <= ballRadius))
                {
                    ballSpeed.Y *= -1.0f;
                }
            }
            else
            {
                framesCounter++;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawCircle(ballPosition, ballRadius, Maroon);
                DrawText("PRESS SPACE to PAUSE BALL MOVEMENT", 10, GetScreenHeight() - 25, 20, LightGray);

                // On pause, we draw a blinking message
                if (pause && (framesCounter / 30 % 2 == 0))
                {
                    DrawText("PAUSED", 350, 200, 30, Gray);
                }

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
