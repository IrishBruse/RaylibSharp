using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersModelShader : ExampleHelper 
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

        SetConfigFlags(WindowFlag.Msaa4xHint);      // Enable Multi Sampling Anti Aliasing 4x (if available)

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - model shader");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(4.0f,4.0f, 4.0f);    // Camera position
        camera.Target = (Vector3)new(0.0f,1.0f, -1.0f);     // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        Model model = LoadModel("resources/models/watermill.obj");                   // Load OBJ model
        Texture texture = LoadTexture("resources/models/watermill_diffuse.png");   // Load model texture

        // Load shader for model
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(0, TextFormat("resources/shaders/glsl%i/grayscale.fs", GLSL_VERSION));

        model.Materials[0].shader = shader;                     // Set shader effect to 3d model
        model.Materials[0].Maps[MaterialMapIndex.Albedo].texture = texture; // Bind texture to model

        Vector3 position = new( 0.0f, 0.0f, 0.0f );    // Set model position

        DisableCursor();                    // Limit cursor to relative movement inside the window
        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                    DrawModel(model, position, 0.2f, White);   // Draw 3d model with texture

                    DrawGrid(10, 1.0f);     // Draw a grid

                }EndMode3D();

                DrawText("(c) Watermill 3D model by Alberto Cano", screenWidth - 210, screenHeight - 20, 10, Gray);

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader);       // Unload shader
        UnloadTexture(texture);     // Unload texture
        UnloadModel(model);         // Unload model

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
