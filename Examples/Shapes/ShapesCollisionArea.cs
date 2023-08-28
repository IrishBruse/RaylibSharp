using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShapesCollisionArea : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - collision area");

        // Box A: Moving box
        RectangleF boxA = new(10, (GetScreenHeight() / 2.0f) - 50, 200, 100);
        int boxASpeedX = 4;

        // Box B: Mouse moved box
        RectangleF boxB = new((GetScreenWidth() / 2.0f) - 30, (GetScreenHeight() / 2.0f) - 30, 60, 60);

        RectangleF boxCollision = new(); // Collision rectangle

        int screenUpperLimit = 40;      // Top menu limits

        bool pause = false;             // Movement pause

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // Move box if not paused
            if (!pause)
            {
                boxA.X += boxASpeedX;
            }

            // Bounce box on x screen limits
            if (((boxA.X + boxA.Width) >= GetScreenWidth()) || (boxA.X <= 0))
            {
                boxASpeedX *= -1;
            }

            // Update player-controlled-box (box02)
            boxB.X = GetMouseX() - (boxB.Width / 2);
            boxB.Y = GetMouseY() - (boxB.Height / 2);

            // Make sure Box B does not go out of move area limits
            if ((boxB.X + boxB.Width) >= GetScreenWidth())
            {
                boxB.X = GetScreenWidth() - boxB.Width;
            }
            else if (boxB.X <= 0)
            {
                boxB.X = 0;
            }

            if ((boxB.Y + boxB.Height) >= GetScreenHeight())
            {
                boxB.Y = GetScreenHeight() - boxB.Height;
            }
            else if (boxB.Y <= screenUpperLimit)
            {
                boxB.Y = screenUpperLimit;
            }

            // Check boxes collision
            bool collision = CheckCollisionRecs(boxA, boxB);

            // Get collision rectangle (only on collision)
            if (collision)
            {
                boxCollision = GetCollision(boxA, boxB);
            }

            // Pause Box A movement
            if (IsKeyPressed(Key.Space))
            {
                pause = !pause;
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawRectangle(0, 0, screenWidth, screenUpperLimit, collision ? Red : Black);

                DrawRectangle(boxA, Gold);
                DrawRectangle(boxB, Blue);

                if (collision)
                {
                    // Draw collision area
                    DrawRectangle(boxCollision, Lime);

                    // Draw collision message
                    DrawText("COLLISION!", (GetScreenWidth() / 2) - (MeasureText("COLLISION!", 20) / 2), (screenUpperLimit / 2) - 10, 20, Black);

                    // Draw collision area
                    DrawText("Collision Area: " + ((int)boxCollision.Width * (int)boxCollision.Height), (GetScreenWidth() / 2) - 100, screenUpperLimit + 10, 20, Black);
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
