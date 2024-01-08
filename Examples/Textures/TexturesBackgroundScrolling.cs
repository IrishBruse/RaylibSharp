using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesBackgroundScrolling : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - background scrolling");

        // NOTE: Be careful, background width must be equal or bigger than screen width
        // if not, texture should be draw more than two times for scrolling effect
        Texture background = LoadTexture("resources/cyberpunk_street_background.png");
        Texture midground = LoadTexture("resources/cyberpunk_street_midground.png");
        Texture foreground = LoadTexture("resources/cyberpunk_street_foreground.png");

        float scrollingBack = 0.0f;
        float scrollingMid = 0.0f;
        float scrollingFore = 0.0f;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            scrollingBack -= 0.1f;
            scrollingMid -= 0.5f;
            scrollingFore -= 1.0f;

            // NOTE: Texture is scaled twice its size, so it sould be considered on scrolling
            if (scrollingBack <= -background.Width * 2)
            {
                scrollingBack = 0;
            }

            if (scrollingMid <= -midground.Width * 2)
            {
                scrollingMid = 0;
            }

            if (scrollingFore <= -foreground.Width * 2)
            {
                scrollingFore = 0;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(GetColor(0x052c46ff));

                // Draw background image twice
                // NOTE: Texture is scaled twice its size
                DrawTexture(background, new(scrollingBack, 20), 0.0f, 2.0f, White);
                DrawTexture(background, new((background.Width * 2) + scrollingBack, 20), 0.0f, 2.0f, White);

                // Draw midground image twice
                DrawTexture(midground, new(scrollingMid, 20), 0.0f, 2.0f, White);
                DrawTexture(midground, new((midground.Width * 2) + scrollingMid, 20), 0.0f, 2.0f, White);

                // Draw foreground image twice
                DrawTexture(foreground, new(scrollingFore, 70), 0.0f, 2.0f, White);
                DrawTexture(foreground, new((foreground.Width * 2) + scrollingFore, 70), 0.0f, 2.0f, White);

                DrawText("BACKGROUND SCROLLING ref  PARALLAX", 10, 10, 20, Red);
                DrawText("(c) Cyberpunk Street Environment by Luis Zuno (@ansimuz)", screenWidth - 330, screenHeight - 20, 10, RayWhite);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(background);  // Unload background texture
        UnloadTexture(midground);   // Unload midground texture
        UnloadTexture(foreground);  // Unload foreground texture

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
