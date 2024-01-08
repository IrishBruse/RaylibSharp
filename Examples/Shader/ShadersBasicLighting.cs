using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersBasicLighting : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint); // Enable Multi Sampling Anti Aliasing 4x (if available)
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - basic lighting");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(2.0f, 4.0f, 6.0f); // Camera3D position
        camera.Target = new(0.0f, 0.5f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        // Load plane model from a generated mesh
        Model model = LoadModelFromMesh(GenMeshPlane(10.0f, 10.0f, 3, 3));
        Model cube = LoadModelFromMesh(GenMeshCube(2.0f, 4.0f, 2.0f));

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load basic lighting shader
        Shader shader = LoadShader($"resources/shaders/glsl{glslVersion}/lighting.vs", $"resources/shaders/glsl{glslVersion}/lighting.fs");
        // Get some required shader locations
        shader.Locs[(int)ShaderLocationIndex.ShaderLocVectorView] = GetShaderLocation(shader, "viewPos");
        // NOTE: "matModel" location name is automatically assigned on shader loading,
        // no need to get the location again if using that uniform name
        //shader.locs[SHADER_LOC_MATRIX_MODEL] = GetShaderLocation(shader, "matModel");

        // Ambient light level (some basic lighting)
        int ambientLoc = GetShaderLocation(shader, "ambient");
        Vector4 color = new(1.0f, 0.1f, 0.1f, 0.1f);
        SetShaderValue(shader, ambientLoc, color, ShaderUniformDataType.ShaderUniformVec4);

        // Assign out lighting shader to model
        model.Materials[0].Shader = shader;
        cube.Materials[0].Shader = shader;

        // Create lights
        Light[] lights = new Light[RlLights.MAXLIGHTS];
        lights[0] = RlLights.CreateLight(LightType.LightPoint, new(-2, 1, -2), Vector3.Zero, Yellow, shader);
        lights[1] = RlLights.CreateLight(LightType.LightPoint, new(2, 1, 2), Vector3.Zero, Red, shader);
        lights[2] = RlLights.CreateLight(LightType.LightPoint, new(-2, 1, 2), Vector3.Zero, Green, shader);
        lights[3] = RlLights.CreateLight(LightType.LightPoint, new(2, 1, -2), Vector3.Zero, Blue, shader);

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Update the shader with the camera view vector (points towards new(0.0f,0.0f, 0.0f))
            Vector3 cameraPos = new(camera.Position.X, camera.Position.Y, camera.Position.Z);
            SetShaderValue(shader, shader.Locs[(int)ShaderLocationIndex.ShaderLocVectorView], cameraPos, ShaderUniformDataType.ShaderUniformVec3);

            // Check key inputs to enable/disable lights
            if (IsKeyPressed(Key.Y)) { lights[0].enabled = !lights[0].enabled; }
            if (IsKeyPressed(Key.R)) { lights[1].enabled = !lights[1].enabled; }
            if (IsKeyPressed(Key.G)) { lights[2].enabled = !lights[2].enabled; }
            if (IsKeyPressed(Key.B)) { lights[3].enabled = !lights[3].enabled; }

            // Update light values (actually, only enable/disable them)
            for (int i = 0; i < RlLights.MAXLIGHTS; i++)
            {
                RlLights.UpdateLightValues(shader, lights[i]);
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    DrawModel(model, Vector3.Zero, 1.0f, White);
                    DrawModel(cube, Vector3.Zero, 1.0f, White);

                    // Draw spheres to show where the lights are
                    for (int i = 0; i < RlLights.MAXLIGHTS; i++)
                    {
                        if (lights[i].enabled)
                        {
                            DrawSphere(lights[i].position, 0.2f, 8, 8, lights[i].color);
                        }
                        else
                        {
                            DrawSphereWires(lights[i].position, 0.2f, 8, 8, ColorAlpha(lights[i].color, 0.3f));
                        }
                    }

                    DrawGrid(10, 1.0f);

                }
                EndMode3D();

                DrawFPS(10, 10);

                DrawText("Use keys [Y][R][G][B] to toggle lights", 10, 40, 20, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadModel(model); // Unload the model
        UnloadModel(cube); // Unload the model
        UnloadShader(shader); // Unload shader

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

}
