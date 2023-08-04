using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TextInputBox : ExampleHelper 
{

private const int MAX_INPUT_CHARS = 9;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - input box");

        char name[MAX_INPUT_CHARS + 1] = "\0";      // NOTE: One extra space required for null terminator char '\0'
        int letterCount = 0;

        RectangleF textBox = new( screenWidth/2.0f - 100, 180, 225, 50 );
        bool mouseOnText = false;

        int framesCounter = 0;

        SetTargetFPS(10);               // Set our game to run at 10 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (CheckCollisionPoint(GetMousePosition(), textBox)) mouseOnText = true;
            else mouseOnText = false;

            if (mouseOnText)
            {
                // Set the window's cursor to the I-Beam
                SetMouseCursor(MOUSE_CURSOR_IBEAM);

                // Get char pressed (unicode character) on the queue
                int key = GetCharPressed();

                // Check if more characters have been pressed on the same frame
                while (key > 0)
                {
                    // NOTE: Only allow keys in range [32..125]
                    if ((key >= 32) && (key <= 125) && (letterCount < MAX_INPUT_CHARS))
                    {
                        name[letterCount] = (char)key;
                        name[letterCount+1] = '\0'; // Add null terminator at the end of the string.
                        letterCount++;
                    }

                    key = GetCharPressed();  // Check next character in the queue
                }

                if (IsKeyPressed(Key.Backspace))
                {
                    letterCount--;
                    if (letterCount < 0) letterCount = 0;
                    name[letterCount] = '\0';
                }
            }
            else SetMouseCursor(MOUSE_CURSOR_DEFAULT);

            if (mouseOnText) framesCounter++;
            else framesCounter = 0;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("PLACE MOUSE OVER INPUT BOX!", 240, 140, 20, Gray);

                DrawRectangle(textBox, LightGray);
                if (mouseOnText) DrawRectangleLines((int)textBox.X, (int)textBox.Y, (int)textBox.Width, (int)textBox.Height, Red);
                else DrawRectangleLines((int)textBox.X, (int)textBox.Y, (int)textBox.Width, (int)textBox.Height, DarkGray);

                DrawText(name, (int)textBox.X + 5, (int)textBox.Y + 8, 40, Maroon);

                DrawText(TextFormat("INPUT CHARS: %i/%i", letterCount, MAX_INPUT_CHARS), 315, 250, 20, DarkGray);

                if (mouseOnText)
                {
                    if (letterCount < MAX_INPUT_CHARS)
                    {
                        // Draw blinking underscore char
                        if (((framesCounter/20)%2 == 0) == 0) DrawText("_", (int)textBox.X + 8 + MeasureText(name, 40), (int)textBox.Y + 12, 40, Maroon);
                    }
                    else DrawText("Press BACKSPACE to delete chars...", 230, 300, 20, Gray);
                }

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    // Check if any key is pressed
    // NOTE: We limit keys check to keys between 32 (Key.Space) and 126
    bool IsAnyKeyPressed()
    {
        bool keyPressed = false;
        int key = GetKeyPressed();

        if ((key >= 32) && (key <= 126)) keyPressed = true;

        return keyPressed;
    }
}
