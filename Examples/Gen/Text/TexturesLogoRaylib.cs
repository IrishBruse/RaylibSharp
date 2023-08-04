using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TexturesLogoRaylib : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - texture loading and drawing");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
        Texture texture = LoadTexture("resources/raylib_logo.png");        // Texture loading

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawTexture(texture, screenWidth/2 - texture.Width/2, screenHeight/2 - texture.Height/2, White);

                DrawText("this IS a texture!", 360, 370, 10, Gray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);       // Texture unloading

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
