using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShapesEasingsBallAnim : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - easings ball anim");

        // Ball variable value to be animated with easings
        int ballPositionX = -100;
        int ballRadius = 20;
        float ballAlpha = 0.0f;

        int state = 0;
        int framesCounter = 0;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (state == 0)             // Move ball position X with easing
            {
                framesCounter++;
                ballPositionX = (int)EaseElasticOut((float)framesCounter, -100, screenWidth/2.0f + 100, 120);

                if (framesCounter >= 120)
                {
                    framesCounter = 0;
                    state = 1;
                }
            }
            else if (state == 1)        // Increase ball radius with easing
            {
                framesCounter++;
                ballRadius = (int)EaseElasticIn((float)framesCounter, 20, 500, 200);

                if (framesCounter >= 200)
                {
                    framesCounter = 0;
                    state = 2;
                }
            }
            else if (state == 2)        // Change ball alpha with easing (background color blending)
            {
                framesCounter++;
                ballAlpha = EaseCubicOut((float)framesCounter, 0.0f, 1.0f, 200);

                if (framesCounter >= 200)
                {
                    framesCounter = 0;
                    state = 3;
                }
            }
            else if (state == 3)        // Reset state to play again
            {
                if (IsKeyPressed(Key.Enter))
                {
                    // Reset required variables to play again
                    ballPositionX = -100;
                    ballRadius = 20;
                    ballAlpha = 0.0f;
                    state = 0;
                }
            }

            if (IsKeyPressed(Key.R)) framesCounter = 0;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                if (state >= 2) DrawRectangle(0, 0, screenWidth, screenHeight, Green);
                DrawCircle(ballPositionX, 200, (float)ballRadius, Fade(Red, 1.0f - ballAlpha));

                if (state == 3) DrawText("PRESS [ENTER] TO PLAY AGAIN!", 240, 200, 20, Black);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
