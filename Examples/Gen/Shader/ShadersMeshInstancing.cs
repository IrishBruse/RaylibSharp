using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersMeshInstancing : ExampleHelper 
{



    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

private const int MAX_INSTANCES = 10000;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - mesh instancing");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = (Vector3)new(-125.0f,125.0f, -125.0f);    // Camera3D position
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);              // Camera3D looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);                  // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                        // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;                     // Camera3D projection type

        // Define mesh to be instanced
        Mesh cube = GenMeshCube(1.0f, 1.0f, 1.0f);

        // Define transforms to be uploaded to GPU for instances
        Matrix *transforms = (Matrix *)RLGL.RlCalloc(MAX_INSTANCES, sizeof(Matrix));   // Pre-multiplied transformations passed to RLGL.Gl

        // Translate and rotate cubes randomly
        for (int i = 0; i < MAX_INSTANCES; i++)
        {
            Matrix translation = MatrixTranslate((float)GetRandomValue(-50, 50), (float)GetRandomValue(-50, 50), (float)GetRandomValue(-50, 50));
            Vector3 axis = Vector3Normalize(new( (float)GetRandomValue(0, 360), (float)GetRandomValue(0, 360), (float)GetRandomValue(0, 360) ));
            float angle = (float)GetRandomValue(0, 10)*DEG2RAD;
            Matrix rotation = MatrixRotate(axis, angle);

            transforms[i] = MatrixMultiply(rotation, translation);
        }

        // Load lighting shader
        Shader shader = LoadShader(TextFormat("resources/shaders/glsl%i/lighting_instancing.vs", GLSL_VERSION),
                                   TextFormat("resources/shaders/glsl%i/lighting.fs", GLSL_VERSION));
        // Get shader locations
        shader.locs[SHADER_LOC_MATRIX_MVP] = GetShaderLocation(shader, "mvp");
        shader.locs[SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(shader, "viewPos");
        shader.locs[SHADER_LOC_MATRIX_MODEL] = GetShaderLocationAttrib(shader, "instanceTransform");

        // Set shader value: ambient light level
        int ambientLoc = GetShaderLocation(shader, "ambient");
        SetShaderValue(shader, ambientLoc, (float[4])new(0.2f,0.2f, 0.2f, 1.0f), SHADER_UNIFORM_VEC4);

        // Create one light
        CreateLight(LIGHT_DIRECTIONAL, (Vector3)new(50.0f,50.0f, 0.0f), Vector3Zero(), White, shader);

        // NOTE: We are assigning the intancing shader to material.shader
        // to be used on mesh drawing with DrawMeshInstanced()
        Material matInstances = LoadMaterialDefault();
        matInstances.shader = shader;
        matInstances.Maps[(int)MaterialMapIndex.Albedo].color = Red;

        // Load default material (using raylib intenral default shader) for non-instanced mesh drawing
        // WARNING: Default shader enables vertex color attribute BUT GenMeshCube() does not generate vertex colors, so,
        // when drawing the color attribute is disabled and a default color value is provided as input for thevertex attribute
        Material matDefault = LoadMaterialDefault();
        matDefault.Maps[(int)MaterialMapIndex.Albedo].color = Blue;

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Update the light shader with the camera view position
            float [] cameraPos = new float [3]new( camera.Position.X, camera.Position.Y, camera.Position.Z );
            SetShaderValue(shader, shader.locs[SHADER_LOC_VECTOR_VIEW], cameraPos, SHADER_UNIFORM_VEC3);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                    // Draw cube mesh with default material (Blue)
                    DrawMesh(cube, matDefault, MatrixTranslate(-10.0f, 0.0f, 0.0f));

                    // Draw meshes instanced using material containing instancing shader (Red + lighting),
                    // transforms[] for the instances should be provided, they are dynamically
                    // updated in GPU every frame, so we can animate the different mesh instances
                    DrawMeshInstanced(cube, matInstances, transforms, MAX_INSTANCES);

                    // Draw cube mesh with default material (Blue)
                    DrawMesh(cube, matDefault, MatrixTranslate(10.0f, 0.0f, 0.0f));

                }EndMode3D();

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        RLGL.RlFree(transforms);    // Free transforms

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
