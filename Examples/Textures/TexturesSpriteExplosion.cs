using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesSpriteExplosion : ExampleHelper
{

    const int NUM_FRAMES_PER_LINE = 5;
    const int NUM_LINES = 5;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - sprite explosion");

        InitAudioDevice();

        // Load explosion sound
        Sound fxBoom = LoadSound("resources/boom.wav");

        // Load explosion texture
        Texture explosion = LoadTexture("resources/explosion.png");

        // Init variables for animation
        float frameWidth = explosion.Width / NUM_FRAMES_PER_LINE;   // Sprite one frame rectangle width
        float frameHeight = explosion.Height / NUM_LINES;           // Sprite one frame rectangle height
        int currentFrame = 0;
        int currentLine = 0;

        RectangleF frameRec = new(0, 0, frameWidth, frameHeight);
        Vector2 position = new(0.0f, 0.0f);

        bool active = false;
        int framesCounter = 0;

        SetTargetFPS(120);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update

            // Check for mouse button pressed and activate explosion (if not active)
            if (IsMouseButtonPressed(MouseButton.Left) && !active)
            {
                position = GetMousePosition();
                active = true;

                position.X -= frameWidth / 2.0f;
                position.Y -= frameHeight / 2.0f;

                PlaySound(fxBoom);
            }

            // Compute explosion animation frames
            if (active)
            {
                framesCounter++;

                if (framesCounter > 2)
                {
                    currentFrame++;

                    if (currentFrame >= NUM_FRAMES_PER_LINE)
                    {
                        currentFrame = 0;
                        currentLine++;

                        if (currentLine >= NUM_LINES)
                        {
                            currentLine = 0;
                            active = false;
                        }
                    }

                    framesCounter = 0;
                }
            }

            frameRec.X = frameWidth * currentFrame;
            frameRec.Y = frameHeight * currentLine;

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // Draw explosion required frame rectangle
                if (active)
                {
                    DrawTexture(explosion, frameRec, position, White);
                }
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(explosion);   // Unload texture
        UnloadSound(fxBoom);        // Unload sound

        CloseAudioDevice();

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
