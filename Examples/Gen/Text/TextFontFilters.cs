using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TextFontFilters : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - font filters");

        const string msg = "Loaded Font";

        // NOTE: Textures/Fonts MUST be loaded after Window initialization (OpenGL context is required)

        // TTF Font loading with custom generation parameters
        Font font = LoadFont("resources/KAISG.ttf", 96, 0, 0);

        // Generate mipmap levels to use trilinear filtering
        // NOTE: On 2D drawing it won't be noticeable, it looks like FILTER_BILINEAR
        GenTextureMipmaps(ref font.Texture);

        float fontSize = (float)font.BaseSize;
        Vector2 fontPosition = new( 40.0f, screenHeight/2.0f - 80.0f );
        Vector2 textSize = new( 0.0f, 0.0f );

        // Setup texture scaling filter
        SetTextureFilter(font.Texture, TEXTURE_FILTER_POINT);
        int currentFontFilter = 0;      // TEXTURE_FILTER_POINT

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            fontSize += GetMouseWheelMove()*4.0f;

            // Choose font texture filter method
            if (IsKeyPressed(Key.One))
            {
                SetTextureFilter(font.Texture, TEXTURE_FILTER_POINT);
                currentFontFilter = 0;
            }
            else if (IsKeyPressed(Key.Two))
            {
                SetTextureFilter(font.Texture, TEXTURE_FILTER_BILINEAR);
                currentFontFilter = 1;
            }
            else if (IsKeyPressed(Key.Three))
            {
                // NOTE: Trilinear filter won't be noticed on 2D drawing
                SetTextureFilter(font.Texture, TEXTURE_FILTER_TRILINEAR);
                currentFontFilter = 2;
            }

            textSize = MeasureText(font, msg, fontSize, 0);

            if (IsKeyDown(Key.Left)) fontPosition.X -= 10;
            else if (IsKeyDown(Key.Right)) fontPosition.X += 10;

            // Load a dropped TTF file dynamically (at current fontSize)
            if (IsFileDropped())
            {
                FilePathList droppedFiles = LoadDroppedFiles();

                // NOTE: We only support first ttf file dropped
                if (IsFileExtension(droppedFiles.Paths[0], ".ttf"))
                {
                    UnloadFont(font);
                    font = LoadFont(droppedFiles.Paths[0], (int)fontSize, 0, 0);
                }

                UnloadDroppedFiles(droppedFiles);    // Unload filepaths from memory
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("Use mouse wheel to change font size", 20, 20, 10, Gray);
                DrawText("Use Key.Right and Key.Left to move text", 20, 40, 10, Gray);
                DrawText("Use 1, 2, 3 to change texture filter", 20, 60, 10, Gray);
                DrawText("Drop a new TTF font for dynamic loading", 20, 80, 10, DarkGray);

                DrawText(font, msg, fontPosition, fontSize, 0, Black);

                // TODO: It seems texSize measurement is not accurate due to chars offsets...
                //DrawRectangleLines(fontPosition.X, fontPosition.Y, textSize.X, textSize.Y, Red);

                DrawRectangle(0, screenHeight - 80, screenWidth, 80, LightGray);
                DrawText(TextFormat("Font size: %02.02f", fontSize), 20, screenHeight - 50, 10, DarkGray);
                DrawText(TextFormat("Text size: [%02.02f, %02.02f]", textSize.X, textSize.Y), 20, screenHeight - 30, 10, DarkGray);
                DrawText("CURRENT TEXTURE FILTER:", 250, 400, 20, Gray);

                if (currentFontFilter == 0) DrawText("POINT", 570, 400, 20, Black);
                else if (currentFontFilter == 1) DrawText("BILINEAR", 570, 400, 20, Black);
                else if (currentFontFilter == 2) DrawText("TRILINEAR", 570, 400, 20, Black);

            }EndDrawing();
        }

        // De-Initialization
        UnloadFont(font);           // Font unloading

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
