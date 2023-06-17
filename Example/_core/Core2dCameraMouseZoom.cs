using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class Example
{

    // Program main entry point
    public static int Core2dCameraMouseZoom()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - 2d camera mouse zoom");

        Camera2D camera = new();
        camera.Zoom = 1.0f;

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            // Translate based on mouse right click
            if (IsMouseButtonDown(MouseButton.Right))
            {
                Vector2 delta = GetMouseDelta();
                delta *= -1.0f / camera.Zoom;

                camera.Target += delta;
            }

            // Zoom based on mouse wheel
            float wheel = GetMouseWheelMove();
            if (wheel != 0)
            {
                // Get the world point that is under the mouse
                Vector2 mouseWorldPos = GetScreenToWorld2D(GetMousePosition(), camera);

                // Set the offset to where the mouse is
                camera.Offset = GetMousePosition();

                // Set the target to match, so that the camera maps the world space point
                // under the cursor to the screen space point under the cursor at any zoom
                camera.Target = mouseWorldPos;

                // Zoom increment
                const float zoomIncrement = 0.125f;

                camera.Zoom += wheel * zoomIncrement;
                if (camera.Zoom < zoomIncrement)
                {
                    camera.Zoom = zoomIncrement;
                }
            }

            // Draw
            BeginDrawing();
            ClearBackground(Black);

            BeginMode2D(camera);

            // Draw the 3d grid, rotated 90 degrees and centered around 0,0
            // just so we have something in the XY plane
            // rlPushMatrix();
            // rlTranslatef(0, 25 * 50, 0);
            // rlRotatef(90, 1, 0, 0);
            DrawGrid(100, 50);
            // rlPopMatrix();

            // Draw a reference circle
            DrawCircle(100, 100, 50, Yellow);

            EndMode2D();

            DrawText("Mouse right button drag to move, mouse wheel to zoom", 10, 10, 20, White);

            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context
        return 0;
    }
}
