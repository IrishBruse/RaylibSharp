using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesGifPlayer : ExampleHelper 
{

private const int MAX_FRAME_DELAY = 20;
private const int MIN_FRAME_DELAY = 1;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - gif playing");

        int animFrames = 0;

        // Load all GIF animation frames into a single Image
        // NOTE: GIF data is always loaded as RGBA (32bit) by default
        // NOTE: Frames are just appended one after another in image.data memory
        Image imScarfyAnim = LoadImageAnim("resources/scarfy_run.gif", &animFrames);

        // Load texture from image
        // NOTE: We will update this texture when required with next frame data
        // WARNING: It's not recommended to use this technique for sprites animation,
        // use spritesheets instead, like illustrated in textures_sprite_anim example
        Texture texScarfyAnim = LoadTextureFromImage(imScarfyAnim);

        uint nextFrameDataOffset = 0;  // Current byte offset to next frame in image.data

        int currentAnimFrame = 0;       // Current animation frame to load and draw
        int frameDelay = 8;             // Frame delay to switch between animation frames
        int frameCounter = 0;           // General frames counter

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            frameCounter++;
            if (frameCounter >= frameDelay)
            {
                // Move to next frame
                // NOTE: If final frame is reached we return to first frame
                currentAnimFrame++;
                if (currentAnimFrame >= animFrames) currentAnimFrame = 0;

                // Get memory offset position for next frame data in image.data
                nextFrameDataOffset = imScarfyAnim.Width*imScarfyAnim.Height*4*currentAnimFrame;

                // Update GPU texture data with next frame image data
                // WARNING: Data size (frame size) and pixel format must match already created texture
                UpdateTexture(texScarfyAnim, ((unsigned char *)imScarfyAnim.data) + nextFrameDataOffset);

                frameCounter = 0;
            }

            // Control frames delay
            if (IsKeyPressed(Key.Right)) frameDelay++;
            else if (IsKeyPressed(Key.Left)) frameDelay--;

            if (frameDelay > MAX_FRAME_DELAY) frameDelay = MAX_FRAME_DELAY;
            else if (frameDelay < MIN_FRAME_DELAY) frameDelay = MIN_FRAME_DELAY;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText(TextFormat("TOTAL GIF FRAMES:  %02i", animFrames), 50, 30, 20, LightGray);
                DrawText(TextFormat("CURRENT FRAME: %02i", currentAnimFrame), 50, 60, 20, Gray);
                DrawText(TextFormat("CURRENT FRAME IMAGE.DATA OFFSET: %02i", nextFrameDataOffset), 50, 90, 20, Gray);

                DrawText("FRAMES DELAY: ", 100, 305, 10, DarkGray);
                DrawText(TextFormat("%02i frames", frameDelay), 620, 305, 10, DarkGray);
                DrawText("PRESS RIGHT/LEFT KEYS to CHANGE SPEED!", 290, 350, 10, DarkGray);

                for (int i = 0; i < MAX_FRAME_DELAY; i++)
                {
                    if (i < frameDelay) DrawRectangle(190 + 21*i, 300, 20, 20, Red);
                    DrawRectangleLines(190 + 21*i, 300, 20, 20, Maroon);
                }

                DrawTexture(texScarfyAnim, GetScreenWidth()/2 - texScarfyAnim.Width/2, 140, White);

                DrawText("(c) Scarfy sprite by Eiden Marsal", screenWidth - 200, screenHeight - 20, 10, Gray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texScarfyAnim);   // Unload texture
        UnloadImage(imScarfyAnim);      // Unload image (contains all frames)

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
