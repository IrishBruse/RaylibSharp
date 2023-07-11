namespace Examples;

using System;
using System.Drawing;
using System.IO;
using System.Numerics;

using RaylibSharp;

public static class ExamplePicker
{
    public static void Run()
    {
        // Uncomment to run all examples one after another
        // for (int i = 0; i < examples.Length; i++)
        // {
        //     examples[i].example.Invoke();
        // }

        (string path, Func<int> example)[] examples = ExamplesList.Examples;

        int exampleIndex = 0;

        const int screenWidth = 800 + 24;
        const int screenHeight = 450 + 24;

        const int previewWidth = 400;
        const int previewHeight = 225;

        Texture[] textures = new Texture[examples.Length];

        float scroll = 0;
        float scrollTarget = 0;

        while (true)
        {
            bool exampleSelected = false;
            Raylib.InitWindow(screenWidth, screenHeight, "RaylibSharp Example Picker");

            for (int i = 0; i < examples.Length; i++)
            {
                byte[] data = File.ReadAllBytes(examples[i].path);
                textures[i] = Raylib.LoadTextureFromImage(Raylib.LoadImageFromMemory(".png", data, data.Length));
            }

            bool close = false;

            string text = "raylib examples are organized by colors depending on the raylib module they focus.\nClick on the module to filter examples:";

            // Main game loop
            while (!close && !exampleSelected)        // Detect window close button or ESC key
            {
                Raylib.DrawText(text, 6, (int)scroll + 6, 19, Raylib.Black);

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

                if (scroll < (-(MathF.Ceiling(examples.Length / 2f) - 1) * previewHeight) + 24)
                {
                    scrollTarget = scroll = (-(MathF.Ceiling(examples.Length / 2f) - 1) * previewHeight) + 24;
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

                        int px = (x * previewWidth) + ((screenWidth / 2) - previewWidth) + (x == 0 ? -6 : 6);
                        int py = 100 + (y * previewHeight) + (12 * y) + (int)Math.Floor(scroll);
                        RectangleF rect = new(px, py, previewWidth, previewHeight);

                        Raylib.DrawTexture(textures[i], new(px, py), 0, .5f, Raylib.White);

                        if (rect.Contains(mousePos.X, mousePos.Y))
                        {
                            Raylib.DrawRectangle(rect, Color.FromArgb(96, 200, 200, 200));
                            Raylib.DrawRectangleLines(rect, lineThick: 10, Raylib.Gray);
                            hovered = true;
                            exampleIndex = i;
                        }
                        else
                        {
                            Raylib.DrawRectangleLines(rect, lineThick: 1, Raylib.Gray);
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

            if (close)
            {
                Raylib.CloseWindow();        // Close window and OpenGL context
                Environment.Exit(0);
            }

            // De-Initialization
            Raylib.CloseWindow();        // Close window and OpenGL context

            examples[exampleIndex].example.Invoke();
        }
    }

    private static float Lerp(float firstFloat, float secondFloat, float by)
    {
        return (firstFloat * (1 - by)) + (secondFloat * by);
    }
}
