using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesBlendModes : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - blend modes");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
        Image bgImage = LoadImage("resources/cyberpunk_street_background.png");     // Loaded in CPU memory (RAM)
        Texture bgTexture = LoadTextureFromImage(bgImage);          // Image converted to texture, GPU memory (VRAM)

        Image fgImage = LoadImage("resources/cyberpunk_street_foreground.png");     // Loaded in CPU memory (RAM)
        Texture fgTexture = LoadTextureFromImage(fgImage);          // Image converted to texture, GPU memory (VRAM)

        // Once image has been converted to texture and uploaded to VRAM, it can be unloaded from RAM
        UnloadImage(bgImage);
        UnloadImage(fgImage);

        const BlendMode blendCountMax = BlendMode.SubtractColors;
        BlendMode blendMode = 0;

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.Space))
            {
                if (blendMode >= (blendCountMax - 1))
                {
                    blendMode = 0;
                }
                else
                {
                    blendMode++;
                }
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawTexture(bgTexture, (screenWidth / 2) - (bgTexture.Width / 2), (screenHeight / 2) - (bgTexture.Height / 2), White);

                // Apply the blend mode and then draw the foreground texture
                BeginBlendMode(blendMode);
                DrawTexture(fgTexture, (screenWidth / 2) - (fgTexture.Width / 2), (screenHeight / 2) - (fgTexture.Height / 2), White);
                EndBlendMode();

                // Draw the texts
                DrawText("Press SPACE to change blend modes.", 310, 350, 10, Gray);

                switch (blendMode)
                {
                    case BlendMode.Alpha: DrawText("Current: BLEND_ALPHA", (screenWidth / 2) - 60, 370, 10, Gray); break;
                    case BlendMode.Additive: DrawText("Current: BLEND_ADDITIVE", (screenWidth / 2) - 60, 370, 10, Gray); break;
                    case BlendMode.Multiplied: DrawText("Current: BLEND_MULTIPLIED", (screenWidth / 2) - 60, 370, 10, Gray); break;
                    case BlendMode.AddColors: DrawText("Current: BLEND_ADD_COLORS", (screenWidth / 2) - 60, 370, 10, Gray); break;
                    default: break;
                }

                DrawText("(c) Cyberpunk Street Environment by Luis Zuno (@ansimuz)", screenWidth - 330, screenHeight - 20, 10, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(fgTexture); // Unload foreground texture
        UnloadTexture(bgTexture); // Unload background texture

        CloseWindow();            // Close window and OpenGL context

        return 0;
    }
}
