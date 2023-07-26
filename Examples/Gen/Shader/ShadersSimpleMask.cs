using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersSimpleMask : ExampleHelper 
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - simple shader mask");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(0.0f,1.0f, 2.0f);    // Camera position
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);      // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        // Define our three models to show the shader on
        Mesh torus = GenMeshTorus(0.3f, 1, 16, 32);
        Model model1 = LoadModelFromMesh(torus);

        Mesh cube = GenMeshCube(0.8f,0.8f,0.8f);
        Model model2 = LoadModelFromMesh(cube);

        // Generate model to be shaded just to see the gaps in the other two
        Mesh sphere = GenMeshSphere(1, 16, 16);
        Model model3 = LoadModelFromMesh(sphere);

        // Load the shader
        Shader shader = LoadShader(0, TextFormat("resources/shaders/glsl%i/mask.fs", GLSL_VERSION));

        // Load and apply the diffuse texture (colour map)
        Texture texDiffuse = LoadTexture("resources/plasma.png");
        model1.Materials[0].Maps[MaterialMapIndex.Albedo].texture = texDiffuse;
        model2.Materials[0].Maps[MaterialMapIndex.Albedo].texture = texDiffuse;

        // Using MaterialMapIndex.Emission as a spare slot to use for 2nd texture
        // NOTE: Don't use MaterialMapIndex.Irradiance, MaterialMapIndex.Prefilter or  MaterialMapIndex.Cubemap as they are bound as cube maps
        Texture texMask = LoadTexture("resources/mask.png");
        model1.Materials[0].Maps[MaterialMapIndex.Emission].texture = texMask;
        model2.Materials[0].Maps[MaterialMapIndex.Emission].texture = texMask;
        shader.locs[SHADER_LOC_MAP_EMISSION] = GetShaderLocation(shader, "mask");

        // Frame is incremented each frame to animate the shader
        int shaderFrame = GetShaderLocation(shader, "frame");

        // Apply the shader to the two models
        model1.Materials[0].shader = shader;
        model2.Materials[0].shader = shader;

        int framesCounter = 0;
        Vector3 rotation = new();           // Model rotation angles

        DisableCursor();                    // Limit cursor to relative movement inside the window
        SetTargetFPS(60);                   // Set  to run at 60 frames-per-second

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
            SetShaderValue(shader, shaderFrame, &framesCounter, SHADER_UNIFORM_INT);

            // Rotate one of the models
            model1.transform = MatrixRotateXYZ(rotation);

            // Draw
            BeginDrawing();{

                ClearBackground(DarkBlue);

                BeginMode3D(camera);{

                    DrawModel(model1, (Vector3)new(0.5f,0.0f, 0.0f), 1, White);
                    DrawModel(model2, (Vector3)new(-0.5f,0.0f, 0.0f), (Vector3)new(1.0f,1.0f, 0.0f), 50, (Vector3)new(1.0f,1.0f, 1.0f), White);
                    DrawModel(model3,(Vector3)new(0.0f,0.0f, -1.5f), 1, White);
                    DrawGrid(10, 1.0f);        // Draw a grid

                }EndMode3D();

                DrawRectangle(16, 698, MeasureText(TextFormat("Frame: %i", framesCounter), 20) + 8, 42, Blue);
                DrawText(TextFormat("Frame: %i", framesCounter), 20, 700, 20, White);

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        UnloadModel(model1);
        UnloadModel(model2);
        UnloadModel(model3);

        UnloadTexture(texDiffuse);  // Unload default diffuse texture
        UnloadTexture(texMask);     // Unload texture mask

        UnloadShader(shader);       // Unload shader

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
