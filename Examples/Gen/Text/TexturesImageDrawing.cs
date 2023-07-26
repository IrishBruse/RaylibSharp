using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesImageDrawing : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - image drawing");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

        Image cat = LoadImage("resources/cat.png");             // Load image in CPU memory (RAM)
        ImageCrop(&cat, new( 100, 10, 280, 380 ));      // Crop an image piece
        ImageFlipHorizontal(&cat);                              // Flip cropped image horizontally
        ImageResize(&cat, 150, 200);                            // Resize flipped-cropped image

        Image parrots = LoadImage("resources/parrots.png");     // Load image in CPU memory (RAM)

        // Draw one image over the other with a scaling of 1.5f
        ImageDraw(&parrots, cat, new( 0, 0, (float)cat.Width, (float)cat.Height ), new( 30, 40, cat.Width*1.5f, cat.Height*1.5f ), White);
        ImageCrop(&parrots, new( 0, 50, (float)parrots.Width, (float)parrots.Height - 100 )); // Crop resulting image

        // Draw on the image with a few image draw methods
        ImageDrawPixel(&parrots, 10, 10, RayWhite);
        ImageDrawCircleLines(&parrots, 10, 10, 5, RayWhite);
        ImageDrawRectangle(&parrots, 5, 20, 10, 10, RayWhite);

        UnloadImage(cat);       // Unload image from RAM

        // Load custom font for frawing on image
        Font font = LoadFont("resources/custom_jupiter_crash.png");

        // Draw over image using custom font
        ImageDrawText(&parrots, font, "PARROTS & CAT", new( 300, 230 ), (float)font.baseSize, -2, White);

        UnloadFont(font);       // Unload custom font (already drawn used on image)

        Texture texture = LoadTextureFromImage(parrots);      // Image converted to texture, uploaded to GPU memory (VRAM)
        UnloadImage(parrots);   // Once image has been converted to texture and uploaded to VRAM, it can be unloaded from RAM

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawTexture(texture, screenWidth/2 - texture.Width/2, screenHeight/2 - texture.Height/2 - 40, White);
                DrawRectangleLines(screenWidth/2 - texture.Width/2, screenHeight/2 - texture.Height/2 - 40, texture.Width, texture.Height, DarkGray);

                DrawText("We are drawing only one texture from various images composed!", 240, 350, 10, DarkGray);
                DrawText("Source images have been cropped, scaled, flipped and copied one over the other.", 190, 370, 10, DarkGray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);       // Texture unloading

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
