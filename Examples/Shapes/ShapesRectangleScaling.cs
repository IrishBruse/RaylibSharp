using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShapesRectangleScaling : ExampleHelper
{
    const int MOUSE_SCALE_MARK_SIZE = 12;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - rectangle scaling mouse");

        Rectangle rec = new(100, 100, 200, 80);
        bool mouseScaleMode = false;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            Vector2 mousePosition = GetMousePosition();


            bool mouseScaleReady;
            if (CheckCollisionPoint(mousePosition, new(rec.X + rec.Width - MOUSE_SCALE_MARK_SIZE, rec.Y + rec.Height - MOUSE_SCALE_MARK_SIZE, MOUSE_SCALE_MARK_SIZE, MOUSE_SCALE_MARK_SIZE)))
            {
                mouseScaleReady = true;
                if (IsMouseButtonPressed(MouseButton.Left))
                {
                    mouseScaleMode = true;
                }
            }
            else
            {
                mouseScaleReady = false;
            }

            if (mouseScaleMode)
            {
                mouseScaleReady = true;

                rec.Width = mousePosition.X - rec.X;
                rec.Height = mousePosition.Y - rec.Y;

                // Check minimum rec size
                if (rec.Width < MOUSE_SCALE_MARK_SIZE)
                {
                    rec.Width = MOUSE_SCALE_MARK_SIZE;
                }

                if (rec.Height < MOUSE_SCALE_MARK_SIZE)
                {
                    rec.Height = MOUSE_SCALE_MARK_SIZE;
                }

                // Check maximum rec size
                if (rec.Width > (GetScreenWidth() - rec.X))
                {
                    rec.Width = GetScreenWidth() - rec.X;
                }

                if (rec.Height > (GetScreenHeight() - rec.Y))
                {
                    rec.Height = GetScreenHeight() - rec.Y;
                }

                if (IsMouseButtonReleased(MouseButton.Left))
                {
                    mouseScaleMode = false;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawText("Scale rectangle dragging from bottom-right corner!", 10, 10, 20, Gray);

                DrawRectangle(rec, Fade(Green, 0.5f));

                if (mouseScaleReady)
                {
                    DrawRectangleLines(rec, 1, Red);
                    DrawTriangle(new(rec.X + rec.Width - MOUSE_SCALE_MARK_SIZE, rec.Y + rec.Height),
                                 new(rec.X + rec.Width, rec.Y + rec.Height),
                                 new(rec.X + rec.Width, rec.Y + rec.Height - MOUSE_SCALE_MARK_SIZE), Red);
                }

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
