using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesBunnymark : ExampleHelper
{

    const int MAX_BUNNIES = 50000;

    // This is the maximum amount of elements (quads) per batch
    // NOTE: This value is defined in [rlgl] module and can be changed there
    const int MAX_BATCH_ELEMENTS = 8192;

    struct Bunny
    {
        public Vector2 position;
        public Vector2 speed;
        public Color color;
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - bunnymark");

        // Load bunny texture
        Texture texBunny = LoadTexture("resources/wabbit_alpha.png");

        Bunny[] bunnies = []; // Bunnies array

        int bunniesCount = 0; // Bunnies counter

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose()) // Detect window close button or ESC key
        {
            // Update
            if (IsMouseButtonDown(MouseButton.Left))
            {
                // Create more bunnies
                for (int i = 0; i < 100; i++)
                {
                    if (bunniesCount < MAX_BUNNIES)
                    {
                        bunnies[bunniesCount].position = GetMousePosition();
                        bunnies[bunniesCount].speed.X = GetRandomValue(-250, 250) / 60.0f;
                        bunnies[bunniesCount].speed.Y = GetRandomValue(-250, 250) / 60.0f;
                        bunnies[bunniesCount].color = Color.FromArgb(
                            255,
                            GetRandomValue(50, 240),
                            GetRandomValue(80, 240),
                            GetRandomValue(100, 240)
                        );
                        bunniesCount++;
                    }
                }
            }

            // Update bunnies
            for (int i = 0; i < bunniesCount; i++)
            {
                bunnies[i].position.X += bunnies[i].speed.X;
                bunnies[i].position.Y += bunnies[i].speed.Y;

                if (((bunnies[i].position.X + (texBunny.Width / 2)) > GetScreenWidth()) ||
                    ((bunnies[i].position.X + (texBunny.Width / 2)) < 0))
                {
                    bunnies[i].speed.X *= -1;
                }

                if (((bunnies[i].position.Y + (texBunny.Height / 2)) > GetScreenHeight()) ||
                    ((bunnies[i].position.Y + (texBunny.Height / 2) - 40) < 0))
                {
                    bunnies[i].speed.Y *= -1;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                for (int i = 0; i < bunniesCount; i++)
                {
                    // NOTE: When internal batch buffer limit is reached (MAX_BATCH_ELEMENTS),
                    // a draw call is launched and buffer starts being filled again;
                    // before issuing a draw call, updated vertex data from internal CPU buffer is send to GPU...
                    // Process of sending data is costly and it could happen that GPU data has not been completely
                    // processed for drawing while new data is tried to be sent (updating current in-use buffers)
                    // it could generates a stall and consequently a frame drop, limiting the number of drawn bunnies
                    DrawTexture(texBunny, (int)bunnies[i].position.X, (int)bunnies[i].position.Y, bunnies[i].color);
                }

                DrawRectangle(0, 0, screenWidth, 40, Black);
                DrawText("bunnies: " + bunniesCount, 120, 10, 20, Green);
                DrawText("batched draw calls: " + (1 + (bunniesCount / MAX_BATCH_ELEMENTS)), 320, 10, 20, Maroon);

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texBunny); // Unload bunny texture

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
