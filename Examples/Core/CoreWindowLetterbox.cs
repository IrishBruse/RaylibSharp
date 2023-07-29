using System;
using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreWindowLetterbox : ExampleHelper
{

    // #define MAX(a, b) ((a)>(b)? (a) : (b))
    // #define MIN(a, b) ((a)<(b)? (a) : (b))

    // Program main entry point
    public static int Example()
    {
        const int windowWidth = 800;
        const int windowHeight = 450;

        // Enable config flags for resizable window and vertical synchro
        SetConfigFlags(WindowFlag.Resizable | WindowFlag.VsyncHint);
        InitWindow(windowWidth, windowHeight, "RaylibSharp - core - window scale letterbox");
        SetWindowMinSize(320, 240);

        int gameScreenWidth = 640;
        int gameScreenHeight = 480;

        // Render texture initialization, used to hold the rendering result so we can easily resize it
        RenderTexture target = LoadRenderTexture(gameScreenWidth, gameScreenHeight);
        SetTextureFilter(target.Texture, TextureFilter.Bilinear);  // Texture scale filter to use

        Color[] colors = new Color[10];
        for (int i = 0; i < 10; i++)
        {
            colors[i] = Color.FromArgb(255, GetRandomValue(100, 250), GetRandomValue(50, 150), GetRandomValue(10, 100));
        }

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            // Compute required framebuffer scaling
            float scale = Math.Min((float)GetScreenWidth() / gameScreenWidth, (float)GetScreenHeight() / gameScreenHeight);

            if (IsKeyPressed(Key.Space))
            {
                // Recalculate random colors for the bars
                for (int i = 0; i < 10; i++)
                {
                    colors[i] = Color.FromArgb(255, GetRandomValue(100, 250), GetRandomValue(50, 150), GetRandomValue(10, 100));
                }
            }

            // Update virtual mouse (clamped mouse value behind game screen)
            Vector2 mouse = GetMousePosition();
            Vector2 virtualMouse = new();
            virtualMouse.X = (mouse.X - ((GetScreenWidth() - (gameScreenWidth * scale)) * 0.5f)) / scale;
            virtualMouse.Y = (mouse.Y - ((GetScreenHeight() - (gameScreenHeight * scale)) * 0.5f)) / scale;
            virtualMouse = Vector2.Clamp(virtualMouse, new(0, 0), new(gameScreenWidth, gameScreenHeight));

            // Apply the same transformation as the virtual mouse to the real mouse (i.e. to work with raygui)
            //SetMouseOffset(-(GetScreenWidth() - (gameScreenWidth*scale))*0.5f, -(GetScreenHeight() - (gameScreenHeight*scale))*0.5f);
            //SetMouseScale(1/scale, 1/scale);

            // Draw
            // Draw everything in the render texture, note this will not be rendered on screen, yet
            BeginTextureMode(target);
            ClearBackground(RayWhite);  // Clear render texture background color

            for (int i = 0; i < 10; i++)
            {
                DrawRectangle(0, gameScreenHeight / 10 * i, gameScreenWidth, gameScreenHeight / 10, colors[i]);
            }

            DrawText("If executed inside a window,\nyou can resize the window,\nand see the screen scaling!", 10, 25, 20, White);
            DrawText($"Default Mouse: [{(int)mouse.X} , {(int)mouse.Y}]", 350, 25, 20, Green);
            DrawText($"Virtual Mouse: [{(int)virtualMouse.X} , {(int)virtualMouse.Y}]", 350, 55, 20, Yellow);
            EndTextureMode();

            BeginDrawing();
            {
                ClearBackground(Black);     // Clear screen background

                RectangleF source = new(
                    0.0f,
                    0.0f,
                    target.Texture.Width,
                     -target.Texture.Height
                );

                RectangleF dest = new(
                    (GetScreenWidth() - (gameScreenWidth * scale)) * 0.5f,
                    (GetScreenHeight() - (gameScreenHeight * scale)) * 0.5f,
                    gameScreenWidth * scale,
                    gameScreenHeight * scale
                );

                // Draw render texture to screen, properly scaled
                DrawTexture(target.Texture, source, dest, new(0, 0), 0.0f, White);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadRenderTexture(target);        // Unload render texture

        CloseWindow();

        return 0;
    }
}
