using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TextFontSdf : ExampleHelper 
{

    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - text - SDF fonts");

        // NOTE: Textures/Fonts MUST be loaded after Window initialization (OpenGL context is required)

        const string msg = "Signed Distance Fields";

        // Loading file to memory
        uint fileSize = 0;
        unsigned char *fileData = LoadFileData("resources/anonymous_pro_bold.ttf", ref fileSize);

        // Default font generation from TTF font
        Font fontDefault = new();
        fontDefault.BaseSize = 16;
        fontDefault.GlyphCount = 95;

        // Loading font data from memory data
        // Parameters > font size: 16, no glyphs array provided (0), glyphs count: 95 (autogenerate chars array)
        fontDefault.Glyphs = LoadFontData(fileData, fileSize, 16, 0, 95, FONT_DEFAULT);
        // Parameters > glyphs count: 95, font size: 16, glyphs padding in image: 4 px, pack method: 0 (default)
        Image atlas = GenImageFontAtlas(fontDefault.Glyphs, ref fontDefault.Recs, 95, 16, 4, 0);
        fontDefault.Texture = LoadTextureFromImage(atlas);
        UnloadImage(atlas);

        // SDF font generation from TTF font
        Font fontSDF = new();
        fontSDF.BaseSize = 16;
        fontSDF.GlyphCount = 95;
        // Parameters > font size: 16, no glyphs array provided (0), glyphs count: 0 (defaults to 95)
        fontSDF.Glyphs = LoadFontData(fileData, fileSize, 16, 0, 0, FONT_SDF);
        // Parameters > glyphs count: 95, font size: 16, glyphs padding in image: 0 px, pack method: 1 (Skyline algorythm)
        atlas = GenImageFontAtlas(fontSDF.Glyphs, ref fontSDF.Recs, 95, 16, 0, 1);
        fontSDF.Texture = LoadTextureFromImage(atlas);
        UnloadImage(atlas);

        UnloadFileData(fileData);      // Free memory from loaded file

        // Load SDF required shader (we use default vertex shader)
        Shader shader = LoadShader(0, TextFormat("resources/shaders/glsl%i/sdf.fs", GLSL_VERSION));
        SetTextureFilter(fontSDF.Texture, TEXTURE_FILTER_BILINEAR);    // Required for SDF font

        Vector2 fontPosition = new( 40, screenHeight/2.0f - 50 );
        Vector2 textSize = new( 0.0f, 0.0f );
        float fontSize = 16.0f;
        int currentFont = 0;            // 0 - fontDefault, 1 - fontSDF

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            fontSize += GetMouseWheelMove()*8.0f;

            if (fontSize < 6) fontSize = 6;

            if (IsKeyDown(Key.Space)) currentFont = 1;
            else currentFont = 0;

            if (currentFont == 0) textSize = MeasureText(fontDefault, msg, fontSize, 0);
            else textSize = MeasureText(fontSDF, msg, fontSize, 0);

            fontPosition.X = GetScreenWidth()/2 - textSize.X/2;
            fontPosition.Y = GetScreenHeight()/2 - textSize.Y/2 + 80;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                if (currentFont == 1)
                {
                    // NOTE: SDF fonts require a custom SDf shader to compute fragment color
                    BeginShaderMode(shader);    // Activate SDF font shader
                        DrawText(fontSDF, msg, fontPosition, fontSize, 0, Black);
                    EndShaderMode();            // Activate our default shader for next drawings

                    DrawTexture(fontSDF.Texture, 10, 10, Black);
                }
                else
                {
                    DrawText(fontDefault, msg, fontPosition, fontSize, 0, Black);
                    DrawTexture(fontDefault.Texture, 10, 10, Black);
                }

                if (currentFont == 1) DrawText("SDF!", 320, 20, 80, Red);
                else DrawText("default font", 315, 40, 30, Gray);

                DrawText("FONT SIZE: 16.0", GetScreenWidth() - 240, 20, 20, DarkGray);
                DrawText(TextFormat("RENDER SIZE: %02.02f", fontSize), GetScreenWidth() - 240, 50, 20, DarkGray);
                DrawText("Use MOUSE WHEEL to SCALE TEXT!", GetScreenWidth() - 240, 90, 10, DarkGray);

                DrawText("HOLD SPACE to USE SDF FONT VERSION!", 340, GetScreenHeight() - 30, 20, Maroon);

            }EndDrawing();
        }

        // De-Initialization
        UnloadFont(fontDefault);    // Default font unloading
        UnloadFont(fontSDF);        // SDF font unloading

        UnloadShader(shader);       // Unload SDF shader

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
