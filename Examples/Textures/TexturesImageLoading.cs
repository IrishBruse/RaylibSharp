using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesImageLoading : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - image loading");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

        Image image = LoadImage("resources/raylib_logo.png");     // Loaded in CPU memory (RAM)
        Texture texture = LoadTextureFromImage(image);          // Image converted to texture, GPU memory (VRAM)
        UnloadImage(image);   // Once image has been converted to texture and uploaded to VRAM, it can be unloaded from RAM

        SetTargetFPS(60);     // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawTexture(texture, (screenWidth / 2) - (texture.Width / 2), (screenHeight / 2) - (texture.Height / 2), White);

                DrawText("this IS a texture loaded from an image!", 300, 370, 10, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);       // Texture unloading

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
