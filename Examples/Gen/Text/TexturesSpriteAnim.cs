using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesSpriteAnim : ExampleHelper 
{

private const int MAX_FRAME_SPEED = 15;
private const int MIN_FRAME_SPEED = 1;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - texture - sprite anim");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
        Texture scarfy = LoadTexture("resources/scarfy.png");        // Texture loading

        Vector2 position = new( 350.0f, 280.0f );
        RectangleF frameRec = new( 0.0f, 0.0f, (float)scarfy.Width/6, (float)scarfy.Height );
        int currentFrame = 0;

        int framesCounter = 0;
        int framesSpeed = 8;            // Number of spritesheet frames shown by second

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            framesCounter++;

            if (framesCounter >= (60/framesSpeed))
            {
                framesCounter = 0;
                currentFrame++;

                if (currentFrame > 5) currentFrame = 0;

                frameRec.X = (float)currentFrame*(float)scarfy.Width/6;
            }

            // Control frames speed
            if (IsKeyPressed(Key.Right)) framesSpeed++;
            else if (IsKeyPressed(Key.Left)) framesSpeed--;

            if (framesSpeed > MAX_FRAME_SPEED) framesSpeed = MAX_FRAME_SPEED;
            else if (framesSpeed < MIN_FRAME_SPEED) framesSpeed = MIN_FRAME_SPEED;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawTexture(scarfy, 15, 40, White);
                DrawRectangleLines(15, 40, scarfy.Width, scarfy.Height, Lime);
                DrawRectangleLines(15 + (int)frameRec.X, 40 + (int)frameRec.Y, (int)frameRec.Width, (int)frameRec.Height, Red);

                DrawText("FRAME SPEED: ", 165, 210, 10, DarkGray);
                DrawText(TextFormat("%02i FPS", framesSpeed), 575, 210, 10, DarkGray);
                DrawText("PRESS RIGHT/LEFT KEYS to CHANGE SPEED!", 290, 240, 10, DarkGray);

                for (int i = 0; i < MAX_FRAME_SPEED; i++)
                {
                    if (i < framesSpeed) DrawRectangle(250 + 21*i, 205, 20, 20, Red);
                    DrawRectangleLines(250 + 21*i, 205, 20, 20, Maroon);
                }

                DrawTexture(scarfy, frameRec, position, White);  // Draw part of the texture

                DrawText("(c) Scarfy sprite by Eiden Marsal", screenWidth - 200, screenHeight - 20, 10, Gray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(scarfy);       // Texture unloading

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
