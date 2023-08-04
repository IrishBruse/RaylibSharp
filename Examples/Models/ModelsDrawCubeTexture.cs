using System.Drawing;
using System.Numerics;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ModelsDrawCubeTexture : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - draw cube texture");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(0.0f, 10.0f, 10.0f);
        camera.Target = new(0.0f, 0.0f, 0.0f);
        camera.Up = new(0.0f, 1.0f, 0.0f);
        camera.Fovy = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        // Load texture to be applied to the cubes sides
        Texture texture = LoadTexture("resources/cubicmap_atlas.png");

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    // Draw cube with an applied texture
                    DrawCubeTexture(texture, new(-2.0f, 2.0f, 0.0f), 2.0f, 4.0f, 2.0f, White);

                    // Draw cube with an applied texture, but only a defined rectangle piece of the texture
                    DrawCubeTexture(texture, new(0, texture.Height / 2, texture.Width / 2, texture.Height / 2),
                        new(2.0f, 1.0f, 0.0f), 2.0f, 2.0f, 2.0f, White);

                    DrawGrid(10, 1.0f);        // Draw a grid

                }
                EndMode3D();

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture); // Unload texture

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }

    // Custom Functions Definition
    // Draw cube textured
    // NOTE: Cube position is the center position
    private static void DrawCubeTexture(Texture texture, Vector3 position, float width, float height, float length, Color color)
    {
        float x = position.X;
        float y = position.Y;
        float z = position.Z;

        // Set desired texture to be enabled while drawing following vertex data
        RLGL.SetTexture(texture.Id);

        // Vertex data transformation can be defined with the commented lines,
        // but in this example we calculate the transformed vertex data directly when calling RLGL.Vertex3f()
        //RLGL.PushMatrix();
        // NOTE: Transformation is applied in inverse order (scale . rotate . translate)
        //RLGL.Translatef(2.0f, 0.0f, 0.0f);
        //RLGL.Rotatef(45, 0, 1, 0);
        //RLGL.Scalef(2.0f, 2.0f, 2.0f);

        RLGL.Begin(RLGL.RlQuads);
        RLGL.Color4ub(color.R, color.G, color.B, color.A);
        // Front Face
        RLGL.Normal3f(0.0f, 0.0f, 1.0f);       // Normal Pointing Towards Viewer
        RLGL.TexCoord2f(0.0f, 0.0f); RLGL.Vertex3f(x - (width / 2), y - (height / 2), z + (length / 2));  // Bottom Left Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 0.0f); RLGL.Vertex3f(x + (width / 2), y - (height / 2), z + (length / 2));  // Bottom Right Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 1.0f); RLGL.Vertex3f(x + (width / 2), y + (height / 2), z + (length / 2));  // Top Right Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 1.0f); RLGL.Vertex3f(x - (width / 2), y + (height / 2), z + (length / 2));  // Top Left Of The Texture and Quad
                                                                                                          // Back Face
        RLGL.Normal3f(0.0f, 0.0f, -1.0f);     // Normal Pointing Away From Viewer
        RLGL.TexCoord2f(1.0f, 0.0f); RLGL.Vertex3f(x - (width / 2), y - (height / 2), z - (length / 2));  // Bottom Right Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 1.0f); RLGL.Vertex3f(x - (width / 2), y + (height / 2), z - (length / 2));  // Top Right Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 1.0f); RLGL.Vertex3f(x + (width / 2), y + (height / 2), z - (length / 2));  // Top Left Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 0.0f); RLGL.Vertex3f(x + (width / 2), y - (height / 2), z - (length / 2));  // Bottom Left Of The Texture and Quad
                                                                                                          // Top Face
        RLGL.Normal3f(0.0f, 1.0f, 0.0f);       // Normal Pointing Up
        RLGL.TexCoord2f(0.0f, 1.0f); RLGL.Vertex3f(x - (width / 2), y + (height / 2), z - (length / 2));  // Top Left Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 0.0f); RLGL.Vertex3f(x - (width / 2), y + (height / 2), z + (length / 2));  // Bottom Left Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 0.0f); RLGL.Vertex3f(x + (width / 2), y + (height / 2), z + (length / 2));  // Bottom Right Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 1.0f); RLGL.Vertex3f(x + (width / 2), y + (height / 2), z - (length / 2));  // Top Right Of The Texture and Quad
                                                                                                          // Bottom Face
        RLGL.Normal3f(0.0f, -1.0f, 0.0f);     // Normal Pointing Down
        RLGL.TexCoord2f(1.0f, 1.0f); RLGL.Vertex3f(x - (width / 2), y - (height / 2), z - (length / 2));  // Top Right Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 1.0f); RLGL.Vertex3f(x + (width / 2), y - (height / 2), z - (length / 2));  // Top Left Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 0.0f); RLGL.Vertex3f(x + (width / 2), y - (height / 2), z + (length / 2));  // Bottom Left Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 0.0f); RLGL.Vertex3f(x - (width / 2), y - (height / 2), z + (length / 2));  // Bottom Right Of The Texture and Quad
                                                                                                          // Right face
        RLGL.Normal3f(1.0f, 0.0f, 0.0f);       // Normal Pointing Right
        RLGL.TexCoord2f(1.0f, 0.0f); RLGL.Vertex3f(x + (width / 2), y - (height / 2), z - (length / 2));  // Bottom Right Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 1.0f); RLGL.Vertex3f(x + (width / 2), y + (height / 2), z - (length / 2));  // Top Right Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 1.0f); RLGL.Vertex3f(x + (width / 2), y + (height / 2), z + (length / 2));  // Top Left Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 0.0f); RLGL.Vertex3f(x + (width / 2), y - (height / 2), z + (length / 2));  // Bottom Left Of The Texture and Quad
                                                                                                          // Left Face
        RLGL.Normal3f(-1.0f, 0.0f, 0.0f);    // Normal Pointing Left
        RLGL.TexCoord2f(0.0f, 0.0f); RLGL.Vertex3f(x - (width / 2), y - (height / 2), z - (length / 2));  // Bottom Left Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 0.0f); RLGL.Vertex3f(x - (width / 2), y - (height / 2), z + (length / 2));  // Bottom Right Of The Texture and Quad
        RLGL.TexCoord2f(1.0f, 1.0f); RLGL.Vertex3f(x - (width / 2), y + (height / 2), z + (length / 2));  // Top Right Of The Texture and Quad
        RLGL.TexCoord2f(0.0f, 1.0f); RLGL.Vertex3f(x - (width / 2), y + (height / 2), z - (length / 2));  // Top Left Of The Texture and Quad
        RLGL.End();
        //RLGL.PopMatrix();

        RLGL.SetTexture(0);
    }

    // Draw cube with texture piece applied to all faces
    private static void DrawCubeTexture(Texture texture, RectangleF source, Vector3 position, float width, float height, float length, Color color)
    {
        float x = position.X;
        float y = position.Y;
        float z = position.Z;
        float texWidth = texture.Width;
        float texHeight = texture.Height;

        // Set desired texture to be enabled while drawing following vertex data
        RLGL.SetTexture(texture.Id);

        // We calculate the normalized texture coordinates for the desired texture-source-rectangle
        // It means converting from (tex.Width, tex.Height) coordinates to [0.0f, 1.0f] equivalent
        RLGL.Begin(RLGL.RlQuads);
        RLGL.Color4ub(color.R, color.G, color.B, color.A);

        // Front face
        RLGL.Normal3f(0.0f, 0.0f, 1.0f);
        RLGL.TexCoord2f(source.X / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x - (width / 2), y - (height / 2), z + (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x + (width / 2), y - (height / 2), z + (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x + (width / 2), y + (height / 2), z + (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x - (width / 2), y + (height / 2), z + (length / 2));

        // Back face
        RLGL.Normal3f(0.0f, 0.0f, -1.0f);
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x - (width / 2), y - (height / 2), z - (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x - (width / 2), y + (height / 2), z - (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x + (width / 2), y + (height / 2), z - (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x + (width / 2), y - (height / 2), z - (length / 2));

        // Top face
        RLGL.Normal3f(0.0f, 1.0f, 0.0f);
        RLGL.TexCoord2f(source.X / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x - (width / 2), y + (height / 2), z - (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x - (width / 2), y + (height / 2), z + (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x + (width / 2), y + (height / 2), z + (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x + (width / 2), y + (height / 2), z - (length / 2));

        // Bottom face
        RLGL.Normal3f(0.0f, -1.0f, 0.0f);
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x - (width / 2), y - (height / 2), z - (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x + (width / 2), y - (height / 2), z - (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x + (width / 2), y - (height / 2), z + (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x - (width / 2), y - (height / 2), z + (length / 2));

        // Right face
        RLGL.Normal3f(1.0f, 0.0f, 0.0f);
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x + (width / 2), y - (height / 2), z - (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x + (width / 2), y + (height / 2), z - (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x + (width / 2), y + (height / 2), z + (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x + (width / 2), y - (height / 2), z + (length / 2));

        // Left face
        RLGL.Normal3f(-1.0f, 0.0f, 0.0f);
        RLGL.TexCoord2f(source.X / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x - (width / 2), y - (height / 2), z - (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, (source.Y + source.Height) / texHeight);
        RLGL.Vertex3f(x - (width / 2), y - (height / 2), z + (length / 2));
        RLGL.TexCoord2f((source.X + source.Width) / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x - (width / 2), y + (height / 2), z + (length / 2));
        RLGL.TexCoord2f(source.X / texWidth, source.Y / texHeight);
        RLGL.Vertex3f(x - (width / 2), y + (height / 2), z - (length / 2));

        RLGL.End();

        RLGL.SetTexture(0);
    }
}
