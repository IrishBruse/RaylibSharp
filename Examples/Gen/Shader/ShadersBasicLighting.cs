using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersBasicLighting : ExampleHelper 
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
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - basic lighting");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = (Vector3)new(2.0f,4.0f, 6.0f);    // Camera3D position
        camera.Target = (Vector3)new(0.0f,0.5f, 0.0f);      // Camera3D looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        // Load plane model from a generated mesh
        Model model = LoadModelFromMesh(GenMeshPlane(10.0f, 10.0f, 3, 3));
        Model cube = LoadModelFromMesh(GenMeshCube(2.0f, 4.0f, 2.0f));

        // Load basic lighting shader
        Shader shader = LoadShader(TextFormat("resources/shaders/glsl%i/lighting.vs", GLSL_VERSION),
                                   TextFormat("resources/shaders/glsl%i/lighting.fs", GLSL_VERSION));
        // Get some required shader locations
        shader.locs[SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(shader, "viewPos");
        // NOTE: "matModel" location name is automatically assigned on shader loading,
        // no need to get the location again if using that uniform name
        //shader.locs[SHADER_LOC_MATRIX_MODEL] = GetShaderLocation(shader, "matModel");

        // Ambient light level (some basic lighting)
        int ambientLoc = GetShaderLocation(shader, "ambient");
        SetShaderValue(shader, ambientLoc, (float[4])new(0.1f,0.1f, 0.1f, 1.0f), SHADER_UNIFORM_VEC4);

        // Assign out lighting shader to model
        model.Materials[0].shader = shader;
        cube.Materials[0].shader = shader;

        // Create lights
        Light lights[MAX_LIGHTS] = new();
        lights[0] = CreateLight(LIGHT_POINT, new( -2, 1, -2 ), Vector3Zero(), Yellow, shader);
        lights[1] = CreateLight(LIGHT_POINT, new( 2, 1, 2 ), Vector3Zero(), Red, shader);
        lights[2] = CreateLight(LIGHT_POINT, new( -2, 1, 2 ), Vector3Zero(), Green, shader);
        lights[3] = CreateLight(LIGHT_POINT, new( 2, 1, -2 ), Vector3Zero(), Blue, shader);

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Update the shader with the camera view vector (points towards new(0.0f,0.0f, 0.0f))
            float [] cameraPos = new float [3]new( camera.Position.X, camera.Position.Y, camera.Position.Z );
            SetShaderValue(shader, shader.locs[SHADER_LOC_VECTOR_VIEW], cameraPos, SHADER_UNIFORM_VEC3);

            // Check key inputs to enable/disable lights
            if (IsKeyPressed(Key.Y)) { lights[0].enabled = !lights[0].enabled; }
            if (IsKeyPressed(Key.R)) { lights[1].enabled = !lights[1].enabled; }
            if (IsKeyPressed(Key.G)) { lights[2].enabled = !lights[2].enabled; }
            if (IsKeyPressed(Key.B)) { lights[3].enabled = !lights[3].enabled; }

            // Update light values (actually, only enable/disable them)
            for (int i = 0; i < MAX_LIGHTS; i++) UpdateLightValues(shader, lights[i]);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                    DrawModel(model, Vector3Zero(), 1.0f, White);
                    DrawModel(cube, Vector3Zero(), 1.0f, White);

                    // Draw spheres to show where the lights are
                    for (int i = 0; i < MAX_LIGHTS; i++)
                    {
                        if (lights[i].enabled) DrawSphere(lights[i].position, 0.2f, 8, 8, lights[i].color);
                        else DrawSphereWires(lights[i].position, 0.2f, 8, 8, ColorAlpha(lights[i].color, 0.3f));
                    }

                    DrawGrid(10, 1.0f);

                }EndMode3D();

                DrawFPS(10, 10);

                DrawText("Use keys [Y][R][G][B] to toggle lights", 10, 40, 20, DarkGray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadModel(model);     // Unload the model
        UnloadModel(cube);      // Unload the model
        UnloadShader(shader);   // Unload shader

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }

}
