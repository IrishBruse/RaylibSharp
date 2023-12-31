using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersModelShader : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint); // Enable Multi Sampling Anti Aliasing 4x (if available)

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - model shader");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(4.0f, 4.0f, 4.0f); // Camera3D position
        camera.Target = new(0.0f, 1.0f, -1.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        Model model = LoadModel("resources/models/watermill.obj"); // Load OBJ model
        Texture texture = LoadTexture("resources/models/watermill_diffuse.png"); // Load model texture

        // Load shader for model
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/grayscale.fs");

        model.Materials[0].Shader = shader; // Set shader effect to 3d model
        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture; // Bind texture to model

        Vector3 position = new(0.0f, 0.0f, 0.0f); // Set model position

        DisableCursor(); // Limit cursor to relative movement inside the window
        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    DrawModel(model, position, 0.2f, White); // Draw 3d model with texture

                    DrawGrid(10, 1.0f); // Draw a grid

                }
                EndMode3D();

                DrawText("(c) Watermill 3D model by Alberto Cano", screenWidth - 210, screenHeight - 20, 10, Gray);

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader); // Unload shader
        UnloadTexture(texture); // Unload texture
        UnloadModel(model); // Unload model

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
