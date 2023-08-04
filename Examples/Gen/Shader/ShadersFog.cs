using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersFog : ExampleHelper 
{



    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint);  // Enable Multi Sampling Anti Aliasing 4x (if available)
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - fog");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = (Vector3)new(2.0f,2.0f, 6.0f);    // Camera3D position
        camera.Target = (Vector3)new(0.0f,0.5f, 0.0f);      // Camera3D looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        // Load models and texture
        Model modelA = LoadModelFromMesh(GenMeshTorus(0.4f, 1.0f, 16, 32));
        Model modelB = LoadModelFromMesh(GenMeshCube(1.0f, 1.0f, 1.0f));
        Model modelC = LoadModelFromMesh(GenMeshSphere(0.5f, 32, 32));
        Texture texture = LoadTexture("resources/texel_checker.png");

        // Assign texture to default model material
        modelA.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
        modelB.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
        modelC.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;

        // Load shader and set up some uniforms
        Shader shader = LoadShader(TextFormat("resources/shaders/glsl%i/lighting.vs", GLSL_VERSION),
                                   TextFormat("resources/shaders/glsl%i/fog.fs", GLSL_VERSION));
        shader.locs[SHADER_LOC_MATRIX_MODEL] = GetShaderLocation(shader, "matModel");
        shader.locs[SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(shader, "viewPos");

        // Ambient light level
        int ambientLoc = GetShaderLocation(shader, "ambient");
        SetShaderValue(shader, ambientLoc, (float[4])new(0.2f,0.2f, 0.2f, 1.0f), SHADER_UNIFORM_VEC4);

        float fogDensity = 0.15f;
        int fogDensityLoc = GetShaderLocation(shader, "fogDensity");
        SetShaderValue(shader, fogDensityLoc, ref fogDensity, SHADER_UNIFORM_FLOAT);

        // NOTE: All models share the same shader
        modelA.Materials[0].shader = shader;
        modelB.Materials[0].shader = shader;
        modelC.Materials[0].shader = shader;

        // Using just 1 point lights
        CreateLight(LIGHT_POINT, new( 0, 2, 6 ), Vector3Zero(), White, shader);

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            if (IsKeyDown(Key.Up))
            {
                fogDensity += 0.001f;
                if (fogDensity > 1.0f) fogDensity = 1.0f;
            }

            if (IsKeyDown(Key.Down))
            {
                fogDensity -= 0.001f;
                if (fogDensity < 0.0f) fogDensity = 0.0f;
            }

            SetShaderValue(shader, fogDensityLoc, ref fogDensity, SHADER_UNIFORM_FLOAT);

            // Rotate the torus
            modelA.transform = MatrixMultiply(modelA.transform, MatrixRotateX(-0.025f));
            modelA.transform = MatrixMultiply(modelA.transform, MatrixRotateZ(0.012f));

            // Update the light shader with the camera view position
            SetShaderValue(shader, shader.locs[SHADER_LOC_VECTOR_VIEW], ref camera.Position.X, SHADER_UNIFORM_VEC3);

            // Draw
            BeginDrawing();{

                ClearBackground(Gray);

                BeginMode3D(camera);{

                    // Draw the three models
                    DrawModel(modelA, Vector3Zero(), 1.0f, White);
                    DrawModel(modelB, new( -2.6f, 0, 0 ), 1.0f, White);
                    DrawModel(modelC, new( 2.6f, 0, 0 ), 1.0f, White);

                    for (int i = -20; i < 20; i += 2) DrawModel(modelA,new( (float)i, 0, 2 ), 1.0f, White);

                }EndMode3D();

                DrawText(TextFormat("Use Key.Up/Key.Down to change fog density [%.2f]", fogDensity), 10, 10, 20, RayWhite);

            }EndDrawing();
        }

        // De-Initialization
        UnloadModel(modelA);        // Unload the model A
        UnloadModel(modelB);        // Unload the model B
        UnloadModel(modelC);        // Unload the model C
        UnloadTexture(texture);     // Unload the texture
        UnloadShader(shader);       // Unload shader

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
