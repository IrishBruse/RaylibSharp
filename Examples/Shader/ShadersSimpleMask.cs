using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersSimpleMask : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - simple shader mask");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(0.0f, 1.0f, 2.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        // Define our three models to show the shader on
        Mesh torus = GenMeshTorus(0.3f, 1, 16, 32);
        Model model1 = LoadModelFromMesh(torus);

        Mesh cube = GenMeshCube(0.8f, 0.8f, 0.8f);
        Model model2 = LoadModelFromMesh(cube);

        // Generate model to be shaded just to see the gaps in the other two
        Mesh sphere = GenMeshSphere(1, 16, 16);
        Model model3 = LoadModelFromMesh(sphere);

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load the shader
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/mask.fs");

        // Load and apply the diffuse texture (colour map)
        Texture texDiffuse = LoadTexture("resources/plasma.png");
        model1.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texDiffuse;
        model2.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texDiffuse;

        // Using MaterialMapIndex.Emission as a spare slot to use for 2nd texture
        // NOTE: Don't use MaterialMapIndex.Irradiance, MaterialMapIndex.Prefilter or  MaterialMapIndex.Cubemap as they are bound as cube maps
        Texture texMask = LoadTexture("resources/mask.png");
        model1.Materials[0].Maps[(int)MaterialMapIndex.Emission].Texture = texMask;
        model2.Materials[0].Maps[(int)MaterialMapIndex.Emission].Texture = texMask;
        shader.Locs[(int)ShaderLocationIndex.ShaderLocMapEmission] = GetShaderLocation(shader, "mask");

        // Frame is incremented each frame to animate the shader
        int shaderFrame = GetShaderLocation(shader, "frame");

        // Apply the shader to the two models
        model1.Materials[0].Shader = shader;
        model2.Materials[0].Shader = shader;

        int framesCounter = 0;
        Vector3 rotation = new(); // Model rotation angles

        DisableCursor(); // Limit cursor to relative movement inside the window
        SetTargetFPS(60); // Set  to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            framesCounter++;
            rotation.X += 0.01f;
            rotation.Y += 0.005f;
            rotation.Z -= 0.0025f;

            // Send frames counter to shader for animation
            SetShaderValue(shader, shaderFrame, ref framesCounter, ShaderUniformDataType.ShaderUniformInt);

            // Rotate one of the models
            model1.Transform = Matrix4x4.CreateRotationX(rotation.X) * Matrix4x4.CreateRotationY(rotation.Y) * Matrix4x4.CreateRotationZ(rotation.Z);

            // Draw
            BeginDrawing();
            {
                ClearBackground(DarkBlue);

                BeginMode3D(camera);
                {
                    DrawModel(model1, new(0.5f, 0.0f, 0.0f), 1, White);
                    DrawModel(model2, new(-0.5f, 0.0f, 0.0f), new(1.0f, 1.0f, 0.0f), 50, new(1.0f, 1.0f, 1.0f), White);
                    DrawModel(model3, new(0.0f, 0.0f, -1.5f), 1, White);
                    DrawGrid(10, 1.0f); // Draw a grid
                }
                EndMode3D();

                DrawRectangle(16, 698, MeasureText("Frame: " + framesCounter, 20) + 8, 42, Blue);
                DrawText("Frame: " + framesCounter, 20, 700, 20, White);

                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadModel(model1);
        UnloadModel(model2);
        UnloadModel(model3);

        UnloadTexture(texDiffuse); // Unload default diffuse texture
        UnloadTexture(texMask); // Unload texture mask

        UnloadShader(shader); // Unload shader

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
