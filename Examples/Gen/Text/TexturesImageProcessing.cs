using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesImageProcessing : ExampleHelper 
{

private const int NUM_PROCESSES = 9;

    typedef enum {
        NONE = 0,
        COLOR_GraySCALE,
        COLOR_TINT,
        COLOR_INVERT,
        COLOR_CONTRAST,
        COLOR_BRIGHTNESS,
        GAUSSIAN_BLUR,
        FLIP_VERTICAL,
        FLIP_HORIZONTAL
    } ImageProcess;

    static string [] processText = new string []{
        "NO PROCESSING",
        "COLOR GraySCALE",
        "COLOR TINT",
        "COLOR INVERT",
        "COLOR CONTRAST",
        "COLOR BRIGHTNESS",
        "GAUSSIAN BLUR",
        "FLIP VERTICAL",
        "FLIP HORIZONTAL"
    };

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - image processing");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

        Image imOrigin = LoadImage("resources/parrots.png");   // Loaded in CPU memory (RAM)
        ImageFormat(&imOrigin, PIXELFORMAT_UNCOMPRESSED_R8G8B8A8);         // Format image to RGBA 32bit (required for texture update) <-- ISSUE
        Texture texture = LoadTextureFromImage(imOrigin);    // Image converted to texture, GPU memory (VRAM)

        Image imCopy = ImageCopy(imOrigin);

        int currentProcess = NONE;
        bool textureReload = false;

        RectangleF [] toggleRecs = new RectangleF [NUM_PROCESSES];
        int mouseHoverRec = -1;

        for (int i = 0; i < NUM_PROCESSES; i++) toggleRecs[i] = new( 40.0f, (float)(50 + 32*i), 150.0f, 30.0f );

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update

            // Mouse toggle group logic
            for (int i = 0; i < NUM_PROCESSES; i++)
            {
                if (CheckCollisionPoint(GetMousePosition(), toggleRecs[i]))
                {
                    mouseHoverRec = i;

                    if (IsMouseButtonReleased(MouseButton.Left))
                    {
                        currentProcess = i;
                        textureReload = true;
                    }
                    break;
                }
                else mouseHoverRec = -1;
            }

            // Keyboard toggle group logic
            if (IsKeyPressed(Key.Down))
            {
                currentProcess++;
                if (currentProcess > (NUM_PROCESSES - 1)) currentProcess = 0;
                textureReload = true;
            }
            else if (IsKeyPressed(Key.Up))
            {
                currentProcess--;
                if (currentProcess < 0) currentProcess = 7;
                textureReload = true;
            }

            // Reload texture when required
            if (textureReload)
            {
                UnloadImage(imCopy);                // Unload image-copy data
                imCopy = ImageCopy(imOrigin);     // Restore image-copy from image-origin

                // NOTE: Image processing is a costly CPU process to be done every frame,
                // If image processing is required in a frame-basis, it should be done
                // with a texture and by shaders
                switch (currentProcess)
                {
                    case COLOR_GraySCALE: ImageColorGrayscale(&imCopy); break;
                    case COLOR_TINT: ImageColorTint(&imCopy, Green); break;
                    case COLOR_INVERT: ImageColorInvert(&imCopy); break;
                    case COLOR_CONTRAST: ImageColorContrast(&imCopy, -40); break;
                    case COLOR_BRIGHTNESS: ImageColorBrightness(&imCopy, -80); break;
                    case GAUSSIAN_BLUR: ImageBlurGaussian(&imCopy, 10); break;
                    case FLIP_VERTICAL: ImageFlipVertical(&imCopy); break;
                    case FLIP_HORIZONTAL: ImageFlipHorizontal(&imCopy); break;
                    default: break;
                }

                Color *pixels = LoadImageColors(imCopy);    // Load pixel data from image (RGBA 32bit)
                UpdateTexture(texture, pixels);             // Update texture with new image data
                UnloadImageColors(pixels);                  // Unload pixels data from RAM

                textureReload = false;
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("IMAGE PROCESSING:", 40, 30, 10, DarkGray);

                // Draw rectangles
                for (int i = 0; i < NUM_PROCESSES; i++)
                {
                    DrawRectangle(toggleRecs[i], ((i == currentProcess) || (i == mouseHoverRec)) ? SkyBlue : LightGray);
                    DrawRectangleLines((int)toggleRecs[i].X, (int) toggleRecs[i].Y, (int) toggleRecs[i].Width, (int) toggleRecs[i].Height, ((i == currentProcess) || (i == mouseHoverRec)) ? Blue : Gray);
                    DrawText( processText[i], (int)( toggleRecs[i].X + toggleRecs[i].Width/2 - MeasureText(processText[i], 10)/2), (int) toggleRecs[i].Y + 11, 10, ((i == currentProcess) || (i == mouseHoverRec)) ? DarkBlue : DarkGray);
                }

                DrawTexture(texture, screenWidth - texture.Width - 60, screenHeight/2 - texture.Height/2, White);
                DrawRectangleLines(screenWidth - texture.Width - 60, screenHeight/2 - texture.Height/2, texture.Width, texture.Height, Black);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);       // Unload texture from VRAM
        UnloadImage(imOrigin);        // Unload image-origin from RAM
        UnloadImage(imCopy);          // Unload image-copy from RAM

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
