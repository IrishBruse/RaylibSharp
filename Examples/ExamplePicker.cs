namespace Examples;

using System;
using System.Drawing;
using System.IO;
using System.Numerics;

using RaylibSharp;

public static class ExamplePicker
{
    public static bool IsExample { get; set; }

    public static void Run()
    {
        Example[] examples = ExamplesList.Examples;

        int exampleIndex = 0;

        const int screenWidth = 800;
        const int screenHeight = 450;

        const int previewWidth = 400;
        const int previewHeight = 225;

        Texture[] textures = new Texture[examples.Length];

        float scroll = 0;
        float scrollTarget = 0;
        const int imagesStartHeight = 100;
        string text = "raylib examples are organized by colors depending on the raylib module\nthey focus on. Press escape to close an example.\nClick on the module to filter examples:";

        Raylib.SetConfigFlags(WindowFlag.Msaa4xHint | WindowFlag.Resizable | WindowFlag.VsyncHint);
        Raylib.InitWindow(screenWidth, screenHeight, "RaylibSharp Example Picker");

        for (int i = 0; i < examples.Length; i++)
        {
            byte[] data = File.ReadAllBytes(examples[i].Path);
            textures[i] = Raylib.LoadTextureFromImage(Raylib.LoadImageFromMemory(".png", data, data.Length));
        }

        while (true)
        {
            bool exampleSelected = false;

            bool close = false;

            // Main game loop
            while (!close && !exampleSelected)        // Detect window close button or ESC key
            {
                int w = Raylib.GetScreenWidth();
                int pad = (w - screenWidth) / 2;

                Raylib.DrawText(text, pad, (int)scroll + 6, 20, Raylib.Black);

                if (Raylib.GetMouseWheelMove().Y != 0)
                {
                    scrollTarget += Raylib.GetMouseWheelMove().Y * 140;
                }

                if (scroll - scrollTarget >= 1 || scroll - scrollTarget <= -1)
                {
                    scroll = Lerp(scroll, scrollTarget, Raylib.GetFrameTime() * 14);
                }
                else
                {
                    scroll = scrollTarget;
                }

                if (scroll > 0)
                {
                    scrollTarget = scroll = 0;
                }

                if (scroll < (-(MathF.Ceiling(examples.Length / 2f) - 1) * previewHeight) + 24 - imagesStartHeight)
                {
                    scrollTarget = scroll = (-(MathF.Ceiling(examples.Length / 2f) - 1) * previewHeight) + 24 - imagesStartHeight;
                }

                // Draw
                Raylib.BeginDrawing();
                {
                    Raylib.ClearBackground(Raylib.RayWhite);

                    Vector2 mousePos = Raylib.GetMousePosition();
                    bool hovered = false;

                    for (int i = 0; i < examples.Length; i++)
                    {
                        int x = i % 2;
                        int y = i / 2;

                        int px = (x * previewWidth) + pad + (x == 0 ? -6 : 6);
                        int py = imagesStartHeight + (y * previewHeight) + (12 * y) + (int)Math.Floor(scroll);
                        RectangleF rect = new(px, py, previewWidth, previewHeight);

                        Raylib.DrawTexture(textures[i], new(px, py), 0, .5f, Raylib.White);

                        if (rect.Contains(mousePos.X, mousePos.Y))
                        {
                            Raylib.DrawRectangle(rect, Color.FromArgb(96, 200, 200, 200));
                            Raylib.DrawRectangleLines(rect, lineThick: 10, examples[i].Color);
                            hovered = true;
                            exampleIndex = i;
                        }
                        else
                        {
                            Raylib.DrawRectangleLines(rect, lineThick: 1, examples[i].Color);
                        }
                    }

                    if (hovered)
                    {
                        Raylib.SetMouseCursor(MouseCursor.PointingHand);
                    }
                    else
                    {
                        Raylib.SetMouseCursor(MouseCursor.Default);
                    }

                    if (Raylib.IsMouseButtonPressed(MouseButton.Left) && hovered)
                    {
                        exampleSelected = true;
                    }
                }
                Raylib.EndDrawing();

                close = Raylib.WindowShouldClose();
            }

            // Raylib.CloseWindow();

            if (close)
            {
                Environment.Exit(0);
            }

            IsExample = true;
            examples[exampleIndex].Entry.Invoke();
            IsExample = false;
        }
    }

    private static float Lerp(float firstFloat, float secondFloat, float by)
    {
        return (firstFloat * (1 - by)) + (secondFloat * by);
    }
}
