using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TexturesRawData : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - texture from raw data");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

        // Load RAW image data (512x512, 32bit RGBA, no file header)
        Image fudesumiRaw = LoadImageRaw("resources/fudesumi.Raw", 384, 512, PIXELFORMAT_UNCOMPRESSED_R8G8B8A8, 0);
        Texture fudesumi = LoadTextureFromImage(fudesumiRaw);  // Upload CPU (RAM) image to GPU (VRAM)
        UnloadImage(fudesumiRaw);                                // Unload CPU (RAM) image data

        // Generate a checked texture by code
        int width = 960;
        int height = 480;

        // Dynamic memory allocation to store pixels data (Color type)
        Color[] pixels = (Color[] )malloc(width*height*sizeof(Color));

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (((x/32+y/32)/1)%2 == 0 == 0) pixels[y*width + x] = Orange;
                else pixels[y*width + x] = Gold;
            }
        }

        // Load pixels data into an image structure and create texture
        Image checkedIm = {
            .data = pixels,             // We can assign pixels directly to data
            .Width = width,
            .Height = height,
            .format = PIXELFORMAT_UNCOMPRESSED_R8G8B8A8,
            .mipmaps = 1
        };

        Texture checked = LoadTextureFromImage(checkedIm);
        UnloadImage(checkedIm);         // Unload CPU (RAM) image data (pixels)

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawTexture(checked, screenWidth/2 - checked.Width/2, screenHeight/2 - checked.Height/2, Fade(White, 0.5f));
                DrawTexture(fudesumi, 430, -30, White);

                DrawText("CHECKED TEXTURE ", 84, 85, 30, Brown);
                DrawText("GENERATED by CODE", 72, 148, 30, Brown);
                DrawText("and RAW IMAGE LOADING", 46, 210, 30, Brown);

                DrawText("(c) Fudesumi sprite by Eiden Marsal", 310, screenHeight - 20, 10, Brown);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(fudesumi);    // Texture unloading
        UnloadTexture(checked);     // Texture unloading

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
