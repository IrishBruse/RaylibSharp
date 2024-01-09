using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreSmoothPixelperfect : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        const int virtualScreenWidth = 160;
        const int virtualScreenHeight = 90;

        const float virtualRatio = screenWidth / (float)virtualScreenWidth;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - smooth pixel-perfect camera");

        Camera2D worldSpaceCamera = new(); // Game world camera
        worldSpaceCamera.Zoom = 1.0f;

        Camera2D screenSpaceCamera = new(); // Smoothing camera
        screenSpaceCamera.Zoom = 1.0f;

        RenderTexture target = LoadRenderTexture(virtualScreenWidth, virtualScreenHeight); // This is where we'll draw all our objects.

        Rectangle rec01 = new(70.0f, 35.0f, 20.0f, 20.0f);
        Rectangle rec02 = new(90.0f, 55.0f, 30.0f, 10.0f);
        Rectangle rec03 = new(80.0f, 65.0f, 15.0f, 25.0f);

        // The target's height is flipped (in the source Rectangle), due to OpenGL reasons
        Rectangle sourceRec = new(0.0f, 0.0f, target.Texture.Width, -(float)target.Texture.Height);
        Rectangle destRec = new(-virtualRatio, -virtualRatio, screenWidth + (virtualRatio * 2), screenHeight + (virtualRatio * 2));

        Vector2 origin = new(0.0f, 0.0f);

        float rotation = 0.0f;
        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            rotation += 60.0f * GetFrameTime(); // Rotate the rectangles, 60 degrees per second

            // Make the camera move to demonstrate the effect
            float cameraX = (float)(Math.Sin(GetTime()) * 50.0f) - 10.0f;
            float cameraY = (float)Math.Cos(GetTime()) * 30.0f;

            // Set the camera's target to the values computed above
            screenSpaceCamera.Target = new(cameraX, cameraY);

            // Round worldSpace coordinates, keep decimals into screenSpace coordinates
            worldSpaceCamera.Target.X = (int)screenSpaceCamera.Target.X;
            screenSpaceCamera.Target.X -= worldSpaceCamera.Target.X;
            screenSpaceCamera.Target.X *= virtualRatio;

            worldSpaceCamera.Target.Y = (int)screenSpaceCamera.Target.Y;
            screenSpaceCamera.Target.Y -= worldSpaceCamera.Target.Y;
            screenSpaceCamera.Target.Y *= virtualRatio;

            // Draw
            BeginTextureMode(target);
            {
                ClearBackground(RayWhite);

                BeginMode2D(worldSpaceCamera);
                {
                    DrawRectangle(rec01, origin, rotation, Black);
                    DrawRectangle(rec02, origin, -rotation, Red);
                    DrawRectangle(rec03, origin, rotation + 45.0f, Blue);
                }
                EndMode2D();
            }
            EndTextureMode();

            BeginDrawing();
            {
                ClearBackground(Red);

                BeginMode2D(screenSpaceCamera);
                {
                    DrawTexture(target.Texture, sourceRec, destRec, origin, 0.0f, White);
                }
                EndMode2D();

                DrawText($"Screen resolution: {screenWidth}x{screenHeight}", 10, 10, 20, DarkBlue);
                DrawText($"World resolution: {virtualScreenWidth}x{virtualScreenHeight}", 10, 40, 20, DarkGreen);
                DrawFPS(GetScreenWidth() - 95, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadRenderTexture(target); // Unload render texture

        CloseWindow();

        return 0;
    }
}
