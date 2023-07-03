using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class Core2dCamera
{
    private static readonly int MAX_BUILDINGS = 100;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - 2d camera");

        RectangleF player = new(400, 280, 40, 40);
        RectangleF[] buildings = new RectangleF[MAX_BUILDINGS];
        Color[] buildColors = new Color[MAX_BUILDINGS];

        int spacing = 0;

        for (int i = 0; i < MAX_BUILDINGS; i++)
        {
            buildings[i].Width = GetRandomValue(50, 200);
            buildings[i].Height = GetRandomValue(100, 800);
            buildings[i].Y = screenHeight - 130.0f - buildings[i].Height;
            buildings[i].X = -6000.0f + spacing;

            spacing += (int)buildings[i].Width;

            buildColors[i] = Color.FromArgb(255, GetRandomValue(200, 240), GetRandomValue(200, 240), GetRandomValue(200, 250));
        }

        Camera2D camera = new();
        camera.Target = new(player.X + 20.0f, player.Y + 20.0f);
        camera.Offset = new(screenWidth / 2.0f, screenHeight / 2.0f);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            // Player movement
            if (IsKeyDown(Key.Right))
            {
                player.X += 2;
            }
            else if (IsKeyDown(Key.Left))
            {
                player.X -= 2;
            }

            // Camera target follows player
            camera.Target = new(player.X + 20, player.Y + 20);

            // Camera rotation controls
            if (IsKeyDown(Key.A))
            {
                camera.Rotation--;
            }
            else if (IsKeyDown(Key.S))
            {
                camera.Rotation++;
            }

            // Limit camera rotation to 80 degrees (-40 to 40)
            if (camera.Rotation > 40)
            {
                camera.Rotation = 40;
            }
            else if (camera.Rotation < -40)
            {
                camera.Rotation = -40;
            }

            // Camera zoom controls
            camera.Zoom += (float)GetMouseWheelMove() * 0.05f;

            if (camera.Zoom > 3.0f)
            {
                camera.Zoom = 3.0f;
            }
            else if (camera.Zoom < 0.1f)
            {
                camera.Zoom = 0.1f;
            }

            // Camera reset (zoom and rotation)
            if (IsKeyPressed(Key.R))
            {
                camera.Zoom = 1.0f;
                camera.Rotation = 0.0f;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode2D(camera);
                {

                    DrawRectangle(-6000, 320, 13000, 8000, DarkGray);

                    for (int i = 0; i < MAX_BUILDINGS; i++)
                    {
                        DrawRectangleRec(buildings[i], buildColors[i]);
                    }

                    DrawRectangleRec(player, Red);

                    DrawLine((int)camera.Target.X, -screenHeight * 10, (int)camera.Target.X, screenHeight * 10, Green);
                    DrawLine(-screenWidth * 10, (int)camera.Target.Y, screenWidth * 10, (int)camera.Target.Y, Green);

                }
                EndMode2D();

                DrawText("SCREEN AREA", 640, 10, 20, Red);

                DrawRectangle(0, 0, screenWidth, 5, Red);
                DrawRectangle(0, 5, 5, screenHeight - 10, Red);
                DrawRectangle(screenWidth - 5, 5, 5, screenHeight - 10, Red);
                DrawRectangle(0, screenHeight - 5, screenWidth, 5, Red);

                DrawRectangle(10, 10, 250, 113, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(10, 10, 250, 113, Blue);

                DrawText("Free 2d camera controls:", 20, 20, 10, Black);
                DrawText("- Right/Left to move Offset", 40, 40, 10, DarkGray);
                DrawText("- Mouse Wheel to Zoom in-out", 40, 60, 10, DarkGray);
                DrawText("- A / S to Rotate", 40, 80, 10, DarkGray);
                DrawText("- R to reset Zoom and Rotation", 40, 100, 10, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
