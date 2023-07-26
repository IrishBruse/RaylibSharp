using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesImageGeneration : ExampleHelper 
{

private const int NUM_TEXTURES = 9;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - procedural images generation");

        Image verticalGradient = GenImageGradientLinear(screenWidth, screenHeight, 0, Red, Blue);
        Image horizontalGradient = GenImageGradientLinear(screenWidth, screenHeight, 90, Red, Blue);
        Image diagonalGradient = GenImageGradientLinear(screenWidth, screenHeight, 45, Red, Blue);
        Image radialGradient = GenImageGradientRadial(screenWidth, screenHeight, 0.0f, White, Black);
        Image squareGradient = GenImageGradientSquare(screenWidth, screenHeight, 0.0f, White, Black);
        Image checked = GenImageChecked(screenWidth, screenHeight, 32, 32, Red, Blue);
        Image whiteNoise = GenImageWhiteNoise(screenWidth, screenHeight, 0.5f);
        Image perlinNoise = GenImagePerlinNoise(screenWidth, screenHeight, 50, 50, 4.0f);
        Image cellular = GenImageCellular(screenWidth, screenHeight, 32);

        Texture textures[NUM_TEXTURES] = new();

        textures[0] = LoadTextureFromImage(verticalGradient);
        textures[1] = LoadTextureFromImage(horizontalGradient);
        textures[2] = LoadTextureFromImage(diagonalGradient);
        textures[3] = LoadTextureFromImage(radialGradient);
        textures[4] = LoadTextureFromImage(squareGradient);
        textures[5] = LoadTextureFromImage(checked);
        textures[6] = LoadTextureFromImage(whiteNoise);
        textures[7] = LoadTextureFromImage(perlinNoise);
        textures[8] = LoadTextureFromImage(cellular);

        // Unload image data (CPU RAM)
        UnloadImage(verticalGradient);
        UnloadImage(horizontalGradient);
        UnloadImage(diagonalGradient);
        UnloadImage(radialGradient);
        UnloadImage(squareGradient);
        UnloadImage(checked);
        UnloadImage(whiteNoise);
        UnloadImage(perlinNoise);
        UnloadImage(cellular);

        int currentTexture = 0;

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            if (IsMouseButtonPressed(MouseButton.Left) || IsKeyPressed(Key.Right))
            {
                currentTexture = (currentTexture + 1)%NUM_TEXTURES; // Cycle between the textures
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawTexture(textures[currentTexture], 0, 0, White);

                DrawRectangle(30, 400, 325, 30, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(30, 400, 325, 30, Fade(White, 0.5f));
                DrawText("MOUSE LEFT BUTTON to CYCLE PROCEDURAL TEXTURES", 40, 410, 10, White);

                switch(currentTexture)
                {
                    case 0: DrawText("VERTICAL GRADIENT", 560, 10, 20, RayWhite); break;
                    case 1: DrawText("HORIZONTAL GRADIENT", 540, 10, 20, RayWhite); break;
                    case 2: DrawText("DIAGONAL GRADIENT", 540, 10, 20, RayWhite); break;
                    case 3: DrawText("RADIAL GRADIENT", 580, 10, 20, LightGray); break;
                    case 4: DrawText("SQUARE GRADIENT", 580, 10, 20, LightGray); break;
                    case 5: DrawText("CHECKED", 680, 10, 20, RayWhite); break;
                    case 6: DrawText("White NOISE", 640, 10, 20, Red); break;
                    case 7: DrawText("PERLIN NOISE", 640, 10, 20, Red); break;
                    case 8: DrawText("CELLULAR", 670, 10, 20, RayWhite); break;
                    default: break;
                }

            }EndDrawing();
        }

        // De-Initialization

        // Unload textures data (GPU VRAM)
        for (int i = 0; i < NUM_TEXTURES; i++) UnloadTexture(textures[i]);

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
