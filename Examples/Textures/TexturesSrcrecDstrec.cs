using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesSrcrecDstrec : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [textures] examples - texture source and destination rectangles");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)

        Texture scarfy = LoadTexture("resources/scarfy.png");        // Texture loading

        int frameWidth = scarfy.Width / 6;
        int frameHeight = scarfy.Height;

        // Source rectangle (part of the texture to use for drawing)
        RectangleF sourceRec = new(0.0f, 0.0f, frameWidth, frameHeight);

        // Destination rectangle (screen rectangle where drawing part of texture)
        RectangleF destRec = new(screenWidth / 2.0f, screenHeight / 2.0f, frameWidth * 2.0f, frameHeight * 2.0f);

        // Origin of the texture (rotation/scale point), it's relative to destination rectangle size
        Vector2 origin = new(frameWidth, frameHeight);

        int rotation = 0;

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            rotation++;

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // NOTE: Using DrawTexture() we can easily rotate and scale the part of the texture we draw
                // sourceRec defines the part of the texture we use for drawing
                // destRec defines the rectangle where our texture part will fit (scaling it to fit)
                // origin defines the point of the texture used as reference for rotation and scaling
                // rotation defines the texture rotation (using origin as rotation point)
                DrawTexture(scarfy, sourceRec, destRec, origin, rotation, White);

                DrawLine((int)destRec.X, 0, (int)destRec.X, screenHeight, Gray);
                DrawLine(0, (int)destRec.Y, screenWidth, (int)destRec.Y, Gray);

                DrawText("(c) Scarfy sprite by Eiden Marsal", screenWidth - 200, screenHeight - 20, 10, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(scarfy);        // Texture unloading

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
