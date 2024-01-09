using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesDrawTiled : ExampleHelper
{
    const int OPT_WIDTH = 220;
    const int MARGIN_SIZE = 8;
    const int COLOR_SIZE = 16;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Resizable); // Make the window resizable
        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - Draw part of a texture tiled");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
        Texture texPattern = LoadTexture("resources/patterns.png");
        SetTextureFilter(texPattern, TextureFilter.Trilinear); // Makes the texture smoother when upscaled

        // Coordinates for all patterns inside the texture
        Rectangle[] recPattern = new Rectangle[]
        {
            new( 3, 3, 66, 66 ),
            new( 75, 3, 100, 100 ),
            new( 3, 75, 66, 66 ),
            new( 7, 156, 50, 50 ),
            new( 85, 106, 90, 45 ),
            new( 75, 154, 100, 60)
        };

        // Setup colors
        Color[] colors = [Black, Maroon, Orange, Blue, Purple, Beige, Lime, Red, DarkGray, SkyBlue];
        Rectangle[] colorRec = new Rectangle[colors.Length];

        // Calculate rectangle for each color
        for (int i = 0, x = 0, y = 0; i < colors.Length; i++)
        {
            colorRec[i].X = 2.0f + MARGIN_SIZE + x;
            colorRec[i].Y = 22.0f + 256.0f + MARGIN_SIZE + y;
            colorRec[i].Width = COLOR_SIZE * 2.0f;
            colorRec[i].Height = COLOR_SIZE;

            if (i == ((colors.Length / 2) - 1))
            {
                x = 0;
                y += COLOR_SIZE + MARGIN_SIZE;
            }
            else
            {
                x += (COLOR_SIZE * 2) + MARGIN_SIZE;
            }
        }

        int activePattern = 0, activeCol = 0;
        float scale = 1.0f, rotation = 0.0f;

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // Handle mouse
            if (IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mouse = GetMousePosition();

                // Check which pattern was clicked and set it as the active pattern
                for (int i = 0; i < recPattern.Length; i++)
                {
                    if (CheckCollisionPoint(mouse, new(2 + MARGIN_SIZE + recPattern[i].X, 40 + MARGIN_SIZE + recPattern[i].Y, recPattern[i].Width, recPattern[i].Height)))
                    {
                        activePattern = i;
                        break;
                    }
                }

                // Check to see which color was clicked and set it as the active color
                for (int i = 0; i < colors.Length; ++i)
                {
                    if (CheckCollisionPoint(mouse, colorRec[i]))
                    {
                        activeCol = i;
                        break;
                    }
                }
            }

            // Handle keys

            // Change scale
            if (IsKeyPressed(Key.Up))
            {
                scale += 0.25f;
            }

            if (IsKeyPressed(Key.Down))
            {
                scale -= 0.25f;
            }

            if (scale > 10.0f)
            {
                scale = 10.0f;
            }
            else if (scale <= 0.0f)
            {
                scale = 0.25f;
            }

            // Change rotation
            if (IsKeyPressed(Key.Left))
            {
                rotation -= 25.0f;
            }

            if (IsKeyPressed(Key.Right))
            {
                rotation += 25.0f;
            }

            // Reset
            if (IsKeyPressed(Key.Space)) { rotation = 0.0f; scale = 1.0f; }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                // Draw the tiled area
                DrawTextureTiled(texPattern, recPattern[activePattern], new((float)OPT_WIDTH + MARGIN_SIZE, MARGIN_SIZE, GetScreenWidth() - OPT_WIDTH - (2.0f * MARGIN_SIZE), GetScreenHeight() - (2.0f * MARGIN_SIZE)),
                    new(0.0f, 0.0f), rotation, scale, colors[activeCol]);

                // Draw options
                DrawRectangle(MARGIN_SIZE, MARGIN_SIZE, OPT_WIDTH - MARGIN_SIZE, GetScreenHeight() - (2 * MARGIN_SIZE), ColorAlpha(LightGray, 0.5f));

                DrawText("Select Pattern", 2 + MARGIN_SIZE, 30 + MARGIN_SIZE, 10, Black);
                DrawTexture(texPattern, 2 + MARGIN_SIZE, 40 + MARGIN_SIZE, Black);
                DrawRectangle(2 + MARGIN_SIZE + (int)recPattern[activePattern].X, 40 + MARGIN_SIZE + (int)recPattern[activePattern].Y, (int)recPattern[activePattern].Width, (int)recPattern[activePattern].Height, ColorAlpha(DarkBlue, 0.3f));

                DrawText("Select Color", 2 + MARGIN_SIZE, 10 + 256 + MARGIN_SIZE, 10, Black);
                for (int i = 0; i < colors.Length; i++)
                {
                    DrawRectangle(colorRec[i], colors[i]);
                    if (activeCol == i)
                    {
                        DrawRectangleLines(colorRec[i], 3, ColorAlpha(White, 0.5f));
                    }
                }
                DrawText(scale.ToString("0.00"), 2 + MARGIN_SIZE, 92 + 256 + MARGIN_SIZE, 20, Black);

                DrawText("Rotation (LEFT/RIGHT to change)", 2 + MARGIN_SIZE, 122 + 256 + MARGIN_SIZE, 10, Black);
                DrawText(rotation.ToString("0.0") + " degrees", 2 + MARGIN_SIZE, 134 + 256 + MARGIN_SIZE, 20, Black);

                DrawText("Press [SPACE] to reset", 2 + MARGIN_SIZE, 164 + 256 + MARGIN_SIZE, 10, DarkBlue);

                // Draw FPS
                DrawText(GetFPS() + " FPS", 2 + MARGIN_SIZE, 2 + MARGIN_SIZE, 20, Black);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texPattern);        // Unload texture

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }

    // Draw part of a texture (defined by a rectangle) with rotation and scale tiled into dest.
    static void DrawTextureTiled(Texture texture, Rectangle source, Rectangle dest, Vector2 origin, float rotation, float scale, Color tint)
    {
        if ((texture.Id <= 0) || (scale <= 0.0f))
        {
            return; // Wanna see a infinite loop?!...just delete this line!
        }

        if ((source.Width == 0) || (source.Height == 0))
        {
            return;
        }

        int tileWidth = (int)(source.Width * scale), tileHeight = (int)(source.Height * scale);
        if ((dest.Width < tileWidth) && (dest.Height < tileHeight))
        {
            // Can fit only one tile
            DrawTexture(
                texture,
                new Rectangle(source.X, source.Y, (float)(dest.Width / tileWidth * source.Width), (float)(dest.Height / tileHeight * source.Height)),
                new Rectangle(dest.X, dest.Y, dest.Width, dest.Height),
                origin,
                rotation,
                tint
            );
        }
        else if (dest.Width <= tileWidth)
        {
            // Tiled vertically (one column)
            int dy = 0;
            for (; dy + tileHeight < dest.Height; dy += tileHeight)
            {
                DrawTexture(texture, new Rectangle(source.X, source.Y, dest.Width / tileWidth * source.Width, source.Height), new Rectangle(dest.X, dest.Y + dy, dest.Width, tileHeight), origin, rotation, tint);
            }

            // Fit last tile
            if (dy < dest.Height)
            {
                DrawTexture(texture, new Rectangle(source.X, source.Y, dest.Width / tileWidth * source.Width, (float)(dest.Height - dy) / tileHeight * source.Height),
                        new Rectangle(dest.X, dest.Y + dy, dest.Width, dest.Height - dy), origin, rotation, tint);
            }
        }
        else if (dest.Height <= tileHeight)
        {
            // Tiled horizontally (one row)
            int dx = 0;
            for (; dx + tileWidth < dest.Width; dx += tileWidth)
            {
                DrawTexture(
                    texture,
                    new Rectangle(
                        source.X,
                        source.Y,
                        source.Width,
                        dest.Height / tileHeight * source.Height),
                    new Rectangle(
                        dest.X + dx,
                        dest.Y,
                        tileWidth,
                        dest.Height
                    ),
                    origin,
                    rotation,
                    tint
                );
            }

            // Fit last tile
            if (dx < dest.Width)
            {
                DrawTexture(
                    texture,
                    new Rectangle(
                        source.X,
                        source.Y,
                        (float)(dest.Width - dx) / tileWidth * source.Width,
                        dest.Height / tileHeight * source.Height
                    ),
                    new Rectangle(
                        dest.X + dx,
                        dest.Y,
                        dest.Width - dx,
                        dest.Height
                    ),
                    origin,
                    rotation,
                    tint
                );
            }
        }
        else
        {
            // Tiled both horizontally and vertically (rows and columns)
            int dx = 0;
            for (; dx + tileWidth < dest.Width; dx += tileWidth)
            {
                int dy = 0;
                for (; dy + tileHeight < dest.Height; dy += tileHeight)
                {
                    DrawTexture(texture, source, new Rectangle(dest.X + dx, dest.Y + dy, tileWidth, tileHeight), origin, rotation, tint);
                }

                if (dy < dest.Height)
                {
                    DrawTexture(texture, new Rectangle(source.X, source.Y, source.Width, (float)(dest.Height - dy) / tileHeight * source.Height),
                                new Rectangle(dest.X + dx, dest.Y + dy, tileWidth, dest.Height - dy), origin, rotation, tint);
                }
            }

            // Fit last column of tiles
            if (dx < dest.Width)
            {
                int dy = 0;
                for (; dy + tileHeight < dest.Height; dy += tileHeight)
                {
                    DrawTexture(texture, new Rectangle(source.X, source.Y, (float)(dest.Width - dx) / tileWidth * source.Width, source.Height),
                            new Rectangle(dest.X + dx, dest.Y + dy, dest.Width - dx, tileHeight), origin, rotation, tint);
                }

                // Draw final tile in the bottom right corner
                if (dy < dest.Height)
                {
                    DrawTexture(texture, new Rectangle(source.X, source.Y, (float)(dest.Width - dx) / tileWidth * source.Width, (float)(dest.Height - dy) / tileHeight * source.Height),
                                        new Rectangle(dest.X + dx, dest.Y + dy, dest.Width - dx, dest.Height - dy), origin, rotation, tint);
                }
            }
        }
    }
}
