using System;
using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersCustomUniform : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint);      // Enable Multi Sampling Anti Aliasing 4x (if available)

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - custom uniform variable");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(8.0f, 8.0f, 8.0f);    // Camera3D position
        camera.Target = new(0.0f, 1.5f, 0.0f);      // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        Model model = LoadModel("resources/models/barracks.obj");                   // Load OBJ model
        Texture texture = LoadTexture("resources/models/barracks_diffuse.png");   // Load model texture (diffuse map)
        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;                     // Set model diffuse texture

        Vector3 position = new(0.0f, 0.0f, 0.0f);                                    // Set model position

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load postprocessing shader
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/swirl.fs");

        // Get variable (uniform) location on the shader to connect with the program
        // NOTE: If uniform variable could not be found in the shader, function returns -1
        int swirlCenterLoc = GetShaderLocation(shader, "center");

        Vector2 swirlCenter = new((float)screenWidth / 2, (float)screenHeight / 2);

        // Create a RenderTexture to be used for render to texture
        RenderTexture target = LoadRenderTexture(screenWidth, screenHeight);

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            Vector2 mousePosition = GetMousePosition();

            swirlCenter[0] = mousePosition.X;
            swirlCenter[1] = screenHeight - mousePosition.Y;

            // Send new value to the shader to be used on drawing
            SetShaderValue(shader, swirlCenterLoc, ref swirlCenter, ShaderUniformDataType.ShaderUniformVec2);

            // Draw
            BeginTextureMode(target);
            {       // Enable drawing to texture
                ClearBackground(RayWhite);  // Clear texture background

                BeginMode3D(camera);
                {        // Begin 3d mode drawing
                    DrawModel(model, position, 0.5f, White);   // Draw 3d model with texture
                    DrawGrid(10, 1.0f);     // Draw a grid
                }
                EndMode3D();                // End 3d mode drawing, returns to orthographic 2d mode

                DrawText("TEXT DRAWN IN RENDER TEXTURE", 200, 10, 30, Red);
            }
            EndTextureMode();               // End drawing to texture (now we have a texture available for next passes)

            BeginDrawing();
            {
                ClearBackground(RayWhite);  // Clear screen background

                // Enable shader using the custom uniform
                BeginShaderMode(shader);
                {
                    // NOTE: Render texture must be y-flipped due to default OpenGL coordinates (left-bottom)
                    DrawTexture(target.Texture, new RectangleF(0, 0, target.Texture.Width, -target.Texture.Height), new Vector2(0, 0), White);
                }
                EndShaderMode();

                // Draw some 2d text over drawn texture
                DrawText("(c) Barracks 3D model by Alberto Cano", screenWidth - 220, screenHeight - 20, 10, Gray);
                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);               // Unload shader
        UnloadTexture(texture);             // Unload texture
        UnloadModel(model);                 // Unload model
        UnloadRenderTexture(target);        // Unload render texture

        CloseWindow();                      // Close window and OpenGL context

        return 0;
    }
}
