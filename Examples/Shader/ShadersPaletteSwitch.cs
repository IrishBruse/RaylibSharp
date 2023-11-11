using System;
using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersPaletteSwitch : ExampleHelper
{
    private const int MAX_PALETTES = 3;
    private const int COLORS_PER_PALETTE = 8;
    private const int VALUES_PER_COLOR = 3;
    private static byte[][] palettes = {
        new byte[] {   // 3-BIT RGB
            0, 0, 0,
            255, 0, 0,
            0, 255, 0,
            0, 0, 255,
            0, 255, 255,
            255, 0, 255,
            255, 255, 0,
            255, 255, 255
        },
        new byte[] {   // AMMO-8 (GameBoy-like)
            4, 12, 6,
            17, 35, 24,
            30, 58, 41,
            48, 93, 66,
            77, 128, 97,
            137, 162, 87,
            190, 220, 127,
            238, 255, 204
        },
        new byte[] {   // RKBV (2-strip film)
            21, 25, 26,
            138, 76, 88,
            217, 98, 117,
            230, 184, 193,
            69, 107, 115,
            75, 151, 166,
            165, 189, 194,
            255, 245, 247
        }
    };
    private static string[] paletteText = new string[]{
        "3-BIT RGB",
        "AMMO-8 (GameBoy-like)",
        "RKBV (2-strip film)"
    };

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - color palette switch");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load shader to be used on some parts drawing
        // NOTE 1: Using GLSL 330 shader version, on OpenGL ES 2.0 use GLSL 100 shader version
        // NOTE 2: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/palette_switch.fs");

        // Get variable (uniform) location on the shader to connect with the program
        // NOTE: If uniform variable could not be found in the shader, function returns -1
        int paletteLoc = GetShaderLocation(shader, "palette");

        int currentPalette = 0;
        int lineHeight = screenHeight / COLORS_PER_PALETTE;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())            // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.Right))
            {
                currentPalette++;
            }
            else if (IsKeyPressed(Key.Left))
            {
                currentPalette--;
            }

            if (currentPalette >= MAX_PALETTES)
            {
                currentPalette = 0;
            }
            else if (currentPalette < 0)
            {
                currentPalette = MAX_PALETTES - 1;
            }

            // Send new value to the shader to be used on drawing.
            // NOTE: We are sending RGB triplets w/o the alpha channel
            SetShaderValue(shader, paletteLoc, palettes[currentPalette], ShaderUniformDataType.ShaderUniformIvec3, COLORS_PER_PALETTE);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginShaderMode(shader);
                {

                    for (int i = 0; i < COLORS_PER_PALETTE; i++)
                    {
                        // Draw horizontal screen-wide rectangles with increasing "palette index"
                        // The used palette index is encoded in the RGB components of the pixel
                        DrawRectangle(0, lineHeight * i, GetScreenWidth(), lineHeight, Color.FromArgb(255, i, i, i));
                    }

                }
                EndShaderMode();

                DrawText("< >", 10, 10, 30, DarkBlue);
                DrawText("CURRENT PALETTE:", 60, 15, 20, RayWhite);
                DrawText(paletteText[currentPalette], 300, 15, 20, Red);

                DrawFPS(700, 15);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader); // Unload shader

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
