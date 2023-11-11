using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersMeshInstancing : ExampleHelper
{
    private const int MAX_INSTANCES = 10000;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - mesh instancing");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(-125.0f, 125.0f, -125.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        // Define mesh to be instanced
        Mesh cube = GenMeshCube(1.0f, 1.0f, 1.0f);

        // Define transforms to be uploaded to GPU for instances
        Matrix4x4[] transforms = new Matrix4x4[MAX_INSTANCES]; // Pre-multiplied transformations passed toRLGL.Gl

        // Translate and rotate cubes randomly
        for (int i = 0; i < MAX_INSTANCES; i++)
        {
            Matrix4x4 translation = Matrix4x4.CreateTranslation(GetRandomValue(-50, 50), GetRandomValue(-50, 50), GetRandomValue(-50, 50));
            Vector3 axis = Vector3.Normalize(new(GetRandomValue(0, 360), GetRandomValue(0, 360), GetRandomValue(0, 360)));
            float angle = GetRandomValue(0, 10) * DEG2RAD;
            Matrix4x4 rotation = Matrix4x4.CreateFromAxisAngle(axis, angle);

            transforms[i] = Matrix4x4.Multiply(rotation, translation);
        }

        // Load lighting shader
        Shader shader = LoadShader($"resources/shaders/glsl{glslVersion}/lighting_instancing.vs", $"resources/shaders/glsl{glslVersion}/lighting.fs");
        // Get shader locations
        shader.Locs[(int)ShaderLocationIndex.ShaderLocMatrixMvp] = GetShaderLocation(shader, "mvp");
        shader.Locs[(int)ShaderLocationIndex.ShaderLocVectorView] = GetShaderLocation(shader, "viewPos");
        shader.Locs[(int)ShaderLocationIndex.ShaderLocMatrixModel] = GetShaderLocationAttrib(shader, "instanceTransform");

        // Set shader value: ambient light level
        int ambientLoc = GetShaderLocation(shader, "ambient");
        Vector4 color = new(0.2f, 0.2f, 0.2f, 1.0f);
        SetShaderValue(shader, ambientLoc, ref color, ShaderUniformDataType.ShaderUniformVec4);

        // Create one light
        RlLights.CreateLight(LightType.LightDirectional, new(50.0f, 50.0f, 0.0f), Vector3.Zero, White, shader);

        // NOTE: We are assigning the intancing shader to material.Shader
        // to be used on mesh drawing with DrawMeshInstanced()
        Material matInstances = LoadMaterialDefault();
        matInstances.Shader = shader;
        matInstances.Maps[(int)MaterialMapIndex.Albedo].Color = Red;

        // Load default material (using raylib intenral default shader) for non-instanced mesh drawing
        // WARNING: Default shader enables vertex color attribute BUT GenMeshCube() does not generate vertex colors, so,
        // when drawing the color attribute is disabled and a default color value is provided as input for thevertex attribute
        Material matDefault = LoadMaterialDefault();
        matDefault.Maps[(int)MaterialMapIndex.Albedo].Color = Blue;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Update the light shader with the camera view position
            Vector3 cameraPos = new(camera.Position.X, camera.Position.Y, camera.Position.Z);
            SetShaderValue(shader, shader.Locs[(int)ShaderLocationIndex.ShaderLocVectorView], ref cameraPos, ShaderUniformDataType.ShaderUniformVec3);

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {
                    // Draw cube mesh with default material (Blue)
                    DrawMesh(cube, matDefault, Matrix4x4.CreateTranslation(-10.0f, 0.0f, 0.0f));

                    // Draw meshes instanced using material containing instancing shader (Red + lighting),
                    // transforms[] for the instances should be provided, they are dynamically
                    // updated in GPU every frame, so we can animate the different mesh instances
                    DrawMeshInstanced(cube, matInstances, transforms, MAX_INSTANCES);

                    // Draw cube mesh with default material (Blue)
                    DrawMesh(cube, matDefault, Matrix4x4.CreateTranslation(10.0f, 0.0f, 0.0f));
                }
                EndMode3D();

                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        // RLGL.RlFree(transforms); // Free transforms

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
