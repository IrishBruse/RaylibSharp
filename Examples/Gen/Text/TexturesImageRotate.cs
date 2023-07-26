using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesImageRotate : ExampleHelper 
{

private const int NUM_TEXTURES = 3;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - texture rotation");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
        Image image45 = LoadImage("resources/raylib_logo.png");
        Image image90 = LoadImage("resources/raylib_logo.png");
        Image imageNeg90 = LoadImage("resources/raylib_logo.png");

        ImageRotate(&image45, 45);
        ImageRotate(&image90, 90);
        ImageRotate(&imageNeg90, -90);

        Texture textures[NUM_TEXTURES] = new();

        textures[0] = LoadTextureFromImage(image45);
        textures[1] = LoadTextureFromImage(image90);
        textures[2] = LoadTextureFromImage(imageNeg90);

        int currentTexture = 0;

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsMouseButtonPressed(MouseButton.Left) || IsKeyPressed(Key.Right))
            {
                currentTexture = (currentTexture + 1)%NUM_TEXTURES; // Cycle between the textures
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawTexture(textures[currentTexture], screenWidth/2 - textures[currentTexture].Width/2, screenHeight/2 - textures[currentTexture].Height/2, White);

            }EndDrawing();
        }

        // De-Initialization
        for (int i = 0; i < NUM_TEXTURES; i++) UnloadTexture(textures[i]);

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
