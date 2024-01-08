using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesImageText : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - texture - image text drawing");

        Image parrots = LoadImage("resources/parrots.png"); // Load image in CPU memory (RAM)

        // TTF Font loading with custom generation parameters
        Font font = LoadFont("resources/KAISG.ttf", 64, 0, 0);

        // Draw over image using custom font
        ImageDrawText(ref parrots, font, "[Parrots font drawing]", new(20.0f, 20.0f), font.BaseSize, 0.0f, Red);

        Texture texture = LoadTextureFromImage(parrots);  // Image converted to texture, uploaded to GPU memory (VRAM)
        UnloadImage(parrots);   // Once image has been converted to texture and uploaded to VRAM, it can be unloaded from RAM

        Vector2 position = new((screenWidth / 2) - (texture.Width / 2), (screenHeight / 2) - (texture.Height / 2) - 20);
        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {

            bool showFont;
            // Update
            if (IsKeyDown(Key.Space))
            {
                showFont = true;
            }
            else
            {
                showFont = false;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                if (!showFont)
                {
                    // Draw texture with text already drawn inside
                    DrawTexture(texture, position, White);

                    // Draw text directly using sprite font
                    DrawText(font, "[Parrots font drawing]", new Vector2(position.X + 20, position.Y + 20 + 280), font.BaseSize, 0.0f, White);
                }
                else
                {
                    DrawTexture(font.Texture, (screenWidth / 2) - (font.Texture.Width / 2), 50, Black);
                }

                DrawText("PRESS SPACE to SHOW FONT ATLAS USED", 290, 420, 10, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);     // Texture unloading

        UnloadFont(font);           // Unload custom font

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
