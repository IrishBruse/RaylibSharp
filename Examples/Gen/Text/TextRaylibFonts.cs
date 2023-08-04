using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TextRaylibFonts : ExampleHelper 
{

private const int MAX_FONTS = 8;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - raylib fonts");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
        Font fonts[MAX_FONTS] = new();

        fonts[0] = LoadFont("resources/fonts/alagard.png");
        fonts[1] = LoadFont("resources/fonts/pixelplay.png");
        fonts[2] = LoadFont("resources/fonts/mecha.png");
        fonts[3] = LoadFont("resources/fonts/setback.png");
        fonts[4] = LoadFont("resources/fonts/romulus.png");
        fonts[5] = LoadFont("resources/fonts/pixantiqua.png");
        fonts[6] = LoadFont("resources/fonts/alpha_beta.png");
        fonts[7] = LoadFont("resources/fonts/jupiter_crash.png");

        string [] messages = new string [MAX_FONTS]{ "ALAGARD FONT designed by Hewett Tsoi",
                                    "PIXELPLAY FONT designed by Aleksander Shevchuk",
                                    "MECHA FONT designed by Captain Falcon",
                                    "SETBACK FONT designed by Brian Kent (AEnigma)",
                                    "ROMULUS FONT designed by Hewett Tsoi",
                                    "PIXANTIQUA FONT designed by Gerhard Grossmann",
                                    "ALPHA_BETA FONT designed by Brian Kent (AEnigma)",
                                    "JUPITER_CRASH FONT designed by Brian Kent (AEnigma)" };

        const int [] spacings = new int [MAX_FONTS]new( 2, 4, 8, 4, 3, 4, 4, 1 );

        Vector2 positions[MAX_FONTS] = new();

        for (int i = 0; i < MAX_FONTS; i++)
        {
            positions[i].X = screenWidth/2.0f - MeasureText(fonts[i], messages[i], fonts[i].BaseSize*2.0f, (float)spacings[i]).X/2.0f;
            positions[i].Y = 60.0f + fonts[i].BaseSize + 45.0f*i;
        }

        // Small Y position corrections
        positions[3].Y += 8;
        positions[4].Y += 2;
        positions[7].Y -= 8;

        Color [] colors = new Color [MAX_FONTS]new( Maroon, Orange, DarkGreen, DarkBlue, DarkPurple, Lime, Gold, Red );

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("free fonts included with raylib", 250, 20, 20, DarkGray);
                DrawLine(220, 50, 590, 50, DarkGray);

                for (int i = 0; i < MAX_FONTS; i++)
                {
                    DrawText(fonts[i], messages[i], positions[i], fonts[i].BaseSize*2.0f, (float)spacings[i], colors[i]);
                }

            }EndDrawing();
        }

        // De-Initialization

        // Fonts unloading
        for (int i = 0; i < MAX_FONTS; i++) UnloadFont(fonts[i]);

        CloseWindow();                 // Close window and OpenGL context

        return 0;
    }
}
