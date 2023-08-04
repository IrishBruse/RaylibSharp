using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TextFontLoading : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - font loading");

        // Define characters to draw
        // NOTE: raylib supports UTF-8 encoding, following list is actually codified as UTF8 internally
        const string msg = "!\"#$%ref '()*+,-./0123456789:;<=>?@ABCDEFGHI\nJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmn\nopqrstuvwxyz{|}~¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓ\nÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷\nøùúûüýþÿ";

        // NOTE: Textures/Fonts MUST be loaded after Window initialization (OpenGL context is required)

        // BMFont (AngelCode) : Font data and image atlas have been generated using external program
        Font fontBm = LoadFont("resources/pixantiqua.fnt");

        // TTF font : Font data and atlas are generated directly from TTF
        // NOTE: We define a font base size of 32 pixels tall and up-to 250 characters
        Font fontTtf = LoadFont("resources/pixantiqua.ttf", 32, 0, 250);

        bool useTtf = false;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyDown(Key.Space)) useTtf = true;
            else useTtf = false;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("Hold SPACE to use TTF generated font", 20, 20, 20, LightGray);

                if (!useTtf)
                {
                    DrawText(fontBm, msg, new( 20.0f, 100.0f ), (float)fontBm.BaseSize, 2, Maroon);
                    DrawText("Using BMFont (Angelcode) imported", 20, GetScreenHeight() - 30, 20, Gray);
                }
                else
                {
                    DrawText(fontTtf, msg, new( 20.0f, 100.0f ), (float)fontTtf.BaseSize, 2, Lime);
                    DrawText("Using TTF font generated", 20, GetScreenHeight() - 30, 20, Gray);
                }

            }EndDrawing();
        }

        // De-Initialization
        UnloadFont(fontBm);     // AngelCode Font unloading
        UnloadFont(fontTtf);    // TTF Font unloading

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
