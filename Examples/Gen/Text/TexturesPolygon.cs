using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TexturesPolygon : ExampleHelper 
{

private const int MAX_POINTS = 11;

    // Draw textured polygon, defined by vertex and texture coordinates
    static void DrawTexturePoly(Texture texture, Vector2 center, Vector2 *points, Vector2 *texcoords, int pointCount, Color tint);

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - textured polygon");

        // Define texture coordinates to map our texture to poly
        Vector2 texcoords[MAX_POINTS] = {
            new( 0.75f, 0.0f ),
            new( 0.25f, 0.0f ),
            new( 0.0f, 0.5f ),
            new( 0.0f, 0.75f ),
            new( 0.25f, 1.0f),
            new( 0.375f, 0.875f),
            new( 0.625f, 0.875f),
            new( 0.75f, 1.0f),
            new( 1.0f, 0.75f),
            new( 1.0f, 0.5f),
            new( 0.75f, 0.0f)  // Close the poly
        };

        // Define the base poly vertices from the UV's
        // NOTE: They can be specified in any other way
        Vector2 points[MAX_POINTS] = new();
        for (int i = 0; i < MAX_POINTS; i++)
        {
            points[i].X = (texcoords[i].X - 0.5f)*256.0f;
            points[i].Y = (texcoords[i].Y - 0.5f)*256.0f;
        }

        // Define the vertices drawing position
        // NOTE: Initially same as points but updated every frame
        Vector2 positions[MAX_POINTS] = new();
        for (int i = 0; i < MAX_POINTS; i++) positions[i] = points[i];

        // Load texture to be mapped to poly
        Texture texture = LoadTexture("resources/cat.png");

        float angle = 0.0f;             // Rotation angle (in degrees)

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // Update points rotation with an angle transform
            // NOTE: Base points position are not modified
            angle++;
            for (int i = 0; i < MAX_POINTS; i++) positions[i] = Vector2Rotate(points[i], angle*DEG2RAD);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawText("textured polygon", 20, 20, 20, DarkGray);

                DrawTexturePoly(texture, new( GetScreenWidth()/2.0f, GetScreenHeight()/2.0f ),
                                positions, texcoords, MAX_POINTS, White);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture); // Unload texture

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }

    // Draw textured polygon, defined by vertex and texture coordinates
    // NOTE: Polygon center must have straight line path to all points
    // without crossing perimeter, points must be in anticlockwise order
    static void DrawTexturePoly(Texture texture, Vector2 center, Vector2 *points, Vector2 *texcoords, int pointCount, Color tint)
    {
        RLGL.SetTexture(texture.Id);

        // Texturing is only supported on RLGL.RlQuads
        RLGL.Begin(RLGL.RlQuads);

            RLGL.Color4ub(tint.R, tint.G, tint.B, tint.A);

            for (int i = 0; i < pointCount - 1; i++)
            {
                RLGL.TexCoord2f(0.5f, 0.5f);
                RLGL.Vertex2f(center.X, center.Y);

                RLGL.TexCoord2f(texcoords[i].X, texcoords[i].Y);
                RLGL.Vertex2f(points[i].X + center.X, points[i].Y + center.Y);

                RLGL.TexCoord2f(texcoords[i + 1].X, texcoords[i + 1].Y);
                RLGL.Vertex2f(points[i + 1].X + center.X, points[i + 1].Y + center.Y);

                RLGL.TexCoord2f(texcoords[i + 1].X, texcoords[i + 1].Y);
                RLGL.Vertex2f(points[i + 1].X + center.X, points[i + 1].Y + center.Y);
            }
        RLGL.End();

        RLGL.SetTexture(0);
    }
}
