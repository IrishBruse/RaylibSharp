using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersFog : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint); // Enable Multi Sampling Anti Aliasing 4x (if available)
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - fog");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(2.0f, 2.0f, 6.0f); // Camera3D position
        camera.Target = new(0.0f, 0.5f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        // Load models and texture
        Model modelA = LoadModelFromMesh(GenMeshTorus(0.4f, 1.0f, 16, 32));
        Model modelB = LoadModelFromMesh(GenMeshCube(1.0f, 1.0f, 1.0f));
        Model modelC = LoadModelFromMesh(GenMeshSphere(0.5f, 32, 32));
        Texture texture = LoadTexture("resources/texel_checker.png");

        // Assign texture to default model material
        modelA.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
        modelB.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
        modelC.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load shader and set up some uniforms
        Shader shader = LoadShader($"resources/shaders/glsl{glslVersion}/lighting.vs", $"resources/shaders/glsl{glslVersion}/fog.fs");
        shader.Locs[(int)ShaderLocationIndex.ShaderLocMatrixModel] = GetShaderLocation(shader, "matModel");
        shader.Locs[(int)ShaderLocationIndex.ShaderLocMatrixView] = GetShaderLocation(shader, "viewPos");

        // Ambient light level
        int ambientLoc = GetShaderLocation(shader, "ambient");
        Vector4 color = new(0.2f, 0.2f, 0.2f, 1.0f);
        SetShaderValue(shader, ambientLoc, color, ShaderUniformDataType.ShaderUniformVec4);

        float fogDensity = 0.15f;
        int fogDensityLoc = GetShaderLocation(shader, "fogDensity");
        SetShaderValue(shader, fogDensityLoc, fogDensity, ShaderUniformDataType.ShaderUniformFloat);

        // NOTE: All models share the same shader
        modelA.Materials[0].Shader = shader;
        modelB.Materials[0].Shader = shader;
        modelC.Materials[0].Shader = shader;

        // Using just 1 point lights
        RlLights.CreateLight(LightType.LightPoint, new(0, 2, 6), Vector3.Zero, White, shader);

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            if (IsKeyDown(Key.Up))
            {
                fogDensity += 0.001f;
                if (fogDensity > 1.0f)
                {
                    fogDensity = 1.0f;
                }
            }

            if (IsKeyDown(Key.Down))
            {
                fogDensity -= 0.001f;
                if (fogDensity < 0.0f)
                {
                    fogDensity = 0.0f;
                }
            }

            SetShaderValue(shader, fogDensityLoc, fogDensity, ShaderUniformDataType.ShaderUniformFloat);

            // Rotate the torus
            modelA.Transform = Matrix4x4.Multiply(modelA.Transform, Matrix4x4.CreateRotationX(-0.025f));
            modelA.Transform = Matrix4x4.Multiply(modelA.Transform, Matrix4x4.CreateRotationZ(0.012f));

            // Update the light shader with the camera view position
            SetShaderValue(shader, shader.Locs[(int)ShaderLocationIndex.ShaderLocVectorView], camera.Position.X, ShaderUniformDataType.ShaderUniformVec3);

            // Draw
            BeginDrawing();
            {

                ClearBackground(Gray);

                BeginMode3D(camera);
                {

                    // Draw the three models
                    DrawModel(modelA, Vector3.Zero, 1.0f, White);
                    DrawModel(modelB, new(-2.6f, 0, 0), 1.0f, White);
                    DrawModel(modelC, new(2.6f, 0, 0), 1.0f, White);

                    for (int i = -20; i < 20; i += 2)
                    {
                        DrawModel(modelA, new(i, 0, 2), 1.0f, White);
                    }
                }
                EndMode3D();

                DrawText($"Use Key.Up/Key.Down to change fog density [{fogDensity:0.00f}]", 10, 10, 20, RayWhite);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadModel(modelA); // Unload the model A
        UnloadModel(modelB); // Unload the model B
        UnloadModel(modelC); // Unload the model C
        UnloadTexture(texture); // Unload the texture
        UnloadShader(shader); // Unload shader

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
