using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesSpriteButton : ExampleHelper
{

    const int NUM_FRAMES = 3;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - sprite button");

        InitAudioDevice();      // Initialize audio device

        Sound fxButton = LoadSound("resources/buttonfx.wav");   // Load button sound
        Texture button = LoadTexture("resources/button.png"); // Load button texture

        // Define frame rectangle for drawing
        float frameHeight = (float)button.Height / NUM_FRAMES;
        RectangleF sourceRec = new(0, 0, button.Width, frameHeight);

        // Define button bounds on screen
        RectangleF btnBounds = new((screenWidth / 2.0f) - (button.Width / 2.0f), (screenHeight / 2.0f) - (button.Height / NUM_FRAMES / 2.0f), button.Width, frameHeight);

        int btnState = 0;               // Button state: 0-NORMAL, 1-MOUSE_HOVER, 2-PRESSED
        bool btnAction = false;         // Button action should be activated

        Vector2 mousePoint = new(0.0f, 0.0f);

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            mousePoint = GetMousePosition();
            btnAction = false;

            // Check button state
            if (CheckCollisionPoint(mousePoint, btnBounds))
            {
                if (IsMouseButtonDown(MouseButton.Left))
                {
                    btnState = 2;
                }
                else
                {
                    btnState = 1;
                }

                if (IsMouseButtonReleased(MouseButton.Left))
                {
                    btnAction = true;
                }
            }
            else
            {
                btnState = 0;
            }

            if (btnAction)
            {
                PlaySound(fxButton);

                // TODO: Any desired action
            }

            // Calculate button frame rectangle to draw depending on button state
            sourceRec.Y = btnState * frameHeight;

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawTexture(button, sourceRec, new(btnBounds.X, btnBounds.Y), White); // Draw button frame

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(button);  // Unload button texture
        UnloadSound(fxButton);  // Unload sound

        CloseAudioDevice();     // Close audio device

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
