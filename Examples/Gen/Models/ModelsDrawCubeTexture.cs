using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsDrawCubeTexture : ExampleHelper 
{

    // Custom Functions Declaration
    static void DrawCubeTexture(Texture texture, Vector3 position, float width, float height, float length, Color color); // Draw cube textured
    static void DrawCubeTexture(Texture texture, RectangleF source, Vector3 position, float width, float height, float length, Color color); // Draw cube with a region of a texture

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - draw cube texture");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(0.0f,10.0f, 10.0f);
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);
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
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                    // Draw cube with an applied texture
                    DrawCubeTexture(texture, (Vector3)new(-2.0f,2.0f, 0.0f), 2.0f, 4.0f, 2.0f, White);

                    // Draw cube with an applied texture, but only a defined rectangle piece of the texture
                    DrawCubeTexture(texture, new( 0, texture.Height/2, texture.Width/2, texture.Height/2 ),
                        (Vector3)new(2.0f,1.0f, 0.0f), 2.0f, 2.0f, 2.0f, White);

                    DrawGrid(10, 1.0f);        // Draw a grid

                }EndMode3D();

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture); // Unload texture

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }

    // Custom Functions Definition
    // Draw cube textured
    // NOTE: Cube position is the center position
    static void DrawCubeTexture(Texture texture, Vector3 position, float width, float height, float length, Color color)
    {
        float x = position.X;
        float y = position.Y;
        float z = position.Z;

        // Set desired texture to be enabled while drawing following vertex data
        rlSetTexture(texture.id);

        // Vertex data transformation can be defined with the commented lines,
        // but in this example we calculate the transformed vertex data directly when calling rlVertex3f()
        //rlPushMatrix();
            // NOTE: Transformation is applied in inverse order (scale . rotate . translate)
            //rlTranslatef(2.0f, 0.0f, 0.0f);
            //rlRotatef(45, 0, 1, 0);
            //rlScalef(2.0f, 2.0f, 2.0f);

            rlBegin(RL_QUADS);
                rlColor4ub(color.r, color.g, color.b, color.a);
                // Front Face
                rlNormal3f(0.0f, 0.0f, 1.0f);       // Normal Pointing Towards Viewer
                rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
                rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad
                rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width/2, y + height/2, z + length/2);  // Top Right Of The Texture and Quad
                rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width/2, y + height/2, z + length/2);  // Top Left Of The Texture and Quad
                // Back Face
                rlNormal3f(0.0f, 0.0f, - 1.0f);     // Normal Pointing Away From Viewer
                rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width/2, y - height/2, z - length/2);  // Bottom Right Of The Texture and Quad
                rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad
                rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
                rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width/2, y - height/2, z - length/2);  // Bottom Left Of The Texture and Quad
                // Top Face
                rlNormal3f(0.0f, 1.0f, 0.0f);       // Normal Pointing Up
                rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
                rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width/2, y + height/2, z + length/2);  // Bottom Left Of The Texture and Quad
                rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width/2, y + height/2, z + length/2);  // Bottom Right Of The Texture and Quad
                rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad
                // Bottom Face
                rlNormal3f(0.0f, - 1.0f, 0.0f);     // Normal Pointing Down
                rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width/2, y - height/2, z - length/2);  // Top Right Of The Texture and Quad
                rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width/2, y - height/2, z - length/2);  // Top Left Of The Texture and Quad
                rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
                rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad
                // Right face
                rlNormal3f(1.0f, 0.0f, 0.0f);       // Normal Pointing Right
                rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x + width/2, y - height/2, z - length/2);  // Bottom Right Of The Texture and Quad
                rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x + width/2, y + height/2, z - length/2);  // Top Right Of The Texture and Quad
                rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x + width/2, y + height/2, z + length/2);  // Top Left Of The Texture and Quad
                rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x + width/2, y - height/2, z + length/2);  // Bottom Left Of The Texture and Quad
                // Left Face
                rlNormal3f( - 1.0f, 0.0f, 0.0f);    // Normal Pointing Left
                rlTexCoord2f(0.0f, 0.0f); rlVertex3f(x - width/2, y - height/2, z - length/2);  // Bottom Left Of The Texture and Quad
                rlTexCoord2f(1.0f, 0.0f); rlVertex3f(x - width/2, y - height/2, z + length/2);  // Bottom Right Of The Texture and Quad
                rlTexCoord2f(1.0f, 1.0f); rlVertex3f(x - width/2, y + height/2, z + length/2);  // Top Right Of The Texture and Quad
                rlTexCoord2f(0.0f, 1.0f); rlVertex3f(x - width/2, y + height/2, z - length/2);  // Top Left Of The Texture and Quad
            rlEnd();
        //rlPopMatrix();

        rlSetTexture(0);
    }

    // Draw cube with texture piece applied to all faces
    static void DrawCubeTexture(Texture texture, RectangleF source, Vector3 position, float width, float height, float length, Color color)
    {
        float x = position.X;
        float y = position.Y;
        float z = position.Z;
        float texWidth = (float)texture.Width;
        float texHeight = (float)texture.Height;

        // Set desired texture to be enabled while drawing following vertex data
        rlSetTexture(texture.id);

        // We calculate the normalized texture coordinates for the desired texture-source-rectangle
        // It means converting from (tex.Width, tex.Height) coordinates to [0.0f, 1.0f] equivalent
        rlBegin(RL_QUADS);
            rlColor4ub(color.r, color.g, color.b, color.a);

            // Front face
            rlNormal3f(0.0f, 0.0f, 1.0f);
            rlTexCoord2f(source.X/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x - width/2, y - height/2, z + length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x + width/2, y - height/2, z + length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, source.Y/texHeight);
            rlVertex3f(x + width/2, y + height/2, z + length/2);
            rlTexCoord2f(source.X/texWidth, source.Y/texHeight);
            rlVertex3f(x - width/2, y + height/2, z + length/2);

            // Back face
            rlNormal3f(0.0f, 0.0f, - 1.0f);
            rlTexCoord2f((source.X + source.Width)/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x - width/2, y - height/2, z - length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, source.Y/texHeight);
            rlVertex3f(x - width/2, y + height/2, z - length/2);
            rlTexCoord2f(source.X/texWidth, source.Y/texHeight);
            rlVertex3f(x + width/2, y + height/2, z - length/2);
            rlTexCoord2f(source.X/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x + width/2, y - height/2, z - length/2);

            // Top face
            rlNormal3f(0.0f, 1.0f, 0.0f);
            rlTexCoord2f(source.X/texWidth, source.Y/texHeight);
            rlVertex3f(x - width/2, y + height/2, z - length/2);
            rlTexCoord2f(source.X/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x - width/2, y + height/2, z + length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x + width/2, y + height/2, z + length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, source.Y/texHeight);
            rlVertex3f(x + width/2, y + height/2, z - length/2);

            // Bottom face
            rlNormal3f(0.0f, - 1.0f, 0.0f);
            rlTexCoord2f((source.X + source.Width)/texWidth, source.Y/texHeight);
            rlVertex3f(x - width/2, y - height/2, z - length/2);
            rlTexCoord2f(source.X/texWidth, source.Y/texHeight);
            rlVertex3f(x + width/2, y - height/2, z - length/2);
            rlTexCoord2f(source.X/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x + width/2, y - height/2, z + length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x - width/2, y - height/2, z + length/2);

            // Right face
            rlNormal3f(1.0f, 0.0f, 0.0f);
            rlTexCoord2f((source.X + source.Width)/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x + width/2, y - height/2, z - length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, source.Y/texHeight);
            rlVertex3f(x + width/2, y + height/2, z - length/2);
            rlTexCoord2f(source.X/texWidth, source.Y/texHeight);
            rlVertex3f(x + width/2, y + height/2, z + length/2);
            rlTexCoord2f(source.X/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x + width/2, y - height/2, z + length/2);

            // Left face
            rlNormal3f( - 1.0f, 0.0f, 0.0f);
            rlTexCoord2f(source.X/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x - width/2, y - height/2, z - length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, (source.Y + source.Height)/texHeight);
            rlVertex3f(x - width/2, y - height/2, z + length/2);
            rlTexCoord2f((source.X + source.Width)/texWidth, source.Y/texHeight);
            rlVertex3f(x - width/2, y + height/2, z + length/2);
            rlTexCoord2f(source.X/texWidth, source.Y/texHeight);
            rlVertex3f(x - width/2, y + height/2, z - length/2);

        rlEnd();

        rlSetTexture(0);
    }
}
