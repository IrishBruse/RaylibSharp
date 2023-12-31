using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesToImage : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - texture to image");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

        Image image = LoadImage("resources/raylib_logo.png");  // Load image data into CPU memory (RAM)
        Texture texture = LoadTextureFromImage(image);       // Image converted to texture, GPU memory (RAM . VRAM)
        UnloadImage(image);                                    // Unload image data from CPU memory (RAM)

        image = LoadImageFromTexture(texture);                 // Load image from GPU texture (VRAM . RAM)
        UnloadTexture(texture);                                // Unload texture from GPU memory (VRAM)

        texture = LoadTextureFromImage(image);                 // Recreate texture from retrieved image data (RAM . VRAM)
        UnloadImage(image);                                    // Unload retrieved image data from CPU memory (RAM)

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
