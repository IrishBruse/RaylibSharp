using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersPostprocessing : ExampleHelper 
{

    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

private const int MAX_POSTPRO_SHADERS = 12;

    typedef enum {
        FX_GraySCALE = 0,
        FX_POSTERIZATION,
        FX_DREAM_VISION,
        FX_PIXELIZER,
        FX_CROSS_HATCHING,
        FX_CROSS_STITCHING,
        FX_PRedATOR_VIEW,
        FX_SCANLINES,
        FX_FISHEYE,
        FX_SOBEL,
        FX_BLOOM,
        FX_BLUR,
        //FX_FXAA
    } PostproShader;

    static string [] postproShaderText = new string []{
        "GraySCALE",
        "POSTERIZATION",
        "DREAM_VISION",
        "PIXELIZER",
        "CROSS_HATCHING",
        "CROSS_STITCHING",
        "PRedATOR_VIEW",
        "SCANLINES",
        "FISHEYE",
        "SOBEL",
        "BLOOM",
        "BLUR",
        //"FXAA"
    };

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint);      // Enable Multi Sampling Anti Aliasing 4x (if available)

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - postprocessing shader");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = (Vector3)new(2.0f,3.0f, 2.0f);    // Camera3D position
        camera.Target = (Vector3)new(0.0f,1.0f, 0.0f);      // Camera3D looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        Model model = LoadModel("resources/models/church.obj");                 // Load OBJ model
        Texture texture = LoadTexture("resources/models/church_diffuse.png"); // Load model texture (diffuse map)
        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;        // Set model diffuse texture

        Vector3 position = new( 0.0f, 0.0f, 0.0f );            // Set model position

        // Load all postpro shaders
        // NOTE 1: All postpro shader use the base vertex shader (DEFAULT_VERTEX_SHADER)
        // NOTE 2: We load the correct shader depending on GLSL version
        Shader shaders[MAX_POSTPRO_SHADERS] = new();

        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        shaders[FX_GraySCALE] = LoadShader(0, TextFormat("resources/shaders/glsl%i/grayscale.fs", GLSL_VERSION));
        shaders[FX_POSTERIZATION] = LoadShader(0, TextFormat("resources/shaders/glsl%i/posterization.fs", GLSL_VERSION));
        shaders[FX_DREAM_VISION] = LoadShader(0, TextFormat("resources/shaders/glsl%i/dream_vision.fs", GLSL_VERSION));
        shaders[FX_PIXELIZER] = LoadShader(0, TextFormat("resources/shaders/glsl%i/pixelizer.fs", GLSL_VERSION));
        shaders[FX_CROSS_HATCHING] = LoadShader(0, TextFormat("resources/shaders/glsl%i/cross_hatching.fs", GLSL_VERSION));
        shaders[FX_CROSS_STITCHING] = LoadShader(0, TextFormat("resources/shaders/glsl%i/cross_stitching.fs", GLSL_VERSION));
        shaders[FX_PRedATOR_VIEW] = LoadShader(0, TextFormat("resources/shaders/glsl%i/predator.fs", GLSL_VERSION));
        shaders[FX_SCANLINES] = LoadShader(0, TextFormat("resources/shaders/glsl%i/scanlines.fs", GLSL_VERSION));
        shaders[FX_FISHEYE] = LoadShader(0, TextFormat("resources/shaders/glsl%i/fisheye.fs", GLSL_VERSION));
        shaders[FX_SOBEL] = LoadShader(0, TextFormat("resources/shaders/glsl%i/sobel.fs", GLSL_VERSION));
        shaders[FX_BLOOM] = LoadShader(0, TextFormat("resources/shaders/glsl%i/bloom.fs", GLSL_VERSION));
        shaders[FX_BLUR] = LoadShader(0, TextFormat("resources/shaders/glsl%i/blur.fs", GLSL_VERSION));

        int currentShader = FX_GraySCALE;

        // Create a RenderTexture to be used for render to texture
        RenderTexture target = LoadRenderTexture(screenWidth, screenHeight);

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            if (IsKeyPressed(Key.Right)) currentShader++;
            else if (IsKeyPressed(Key.Left)) currentShader--;

            if (currentShader >= MAX_POSTPRO_SHADERS) currentShader = 0;
            else if (currentShader < 0) currentShader = MAX_POSTPRO_SHADERS - 1;

            // Draw
            BeginTextureMode(target);       // Enable drawing to texture
                ClearBackground(RayWhite);  // Clear texture background

                BeginMode3D(camera);{        // Begin 3d mode drawing
                    DrawModel(model, position, 0.1f, White);   // Draw 3d model with texture
                    DrawGrid(10, 1.0f);     // Draw a grid
                }EndMode3D();                // End 3d mode drawing, returns to orthographic 2d mode
            EndTextureMode();               // End drawing to texture (now we have a texture available for next passes)

            BeginDrawing();{
                ClearBackground(RayWhite);  // Clear screen background

                // Render generated texture using selected postprocessing shader
                BeginShaderMode(shaders[currentShader]);
                    // NOTE: Render texture must be y-flipped due to default OpenGL coordinates (left-bottom)
                    DrawTexture(target.Texture, new( 0, 0, (float)target.Texture.Width, (float)-target.Texture.Height ), new( 0, 0 ), White);
                EndShaderMode();

                // Draw 2d shapes and text over drawn texture
                DrawRectangle(0, 9, 580, 30, Fade(LightGray, 0.7f));

                DrawText("(c) Church 3D model by Alberto Cano", screenWidth - 200, screenHeight - 20, 10, Gray);
                DrawText("CURRENT POSTPRO SHADER:", 10, 15, 20, Black);
                DrawText(postproShaderText[currentShader], 330, 15, 20, Red);
                DrawText("< >", 540, 10, 30, DarkBlue);
                DrawFPS(700, 15);
            }EndDrawing();
        }

        // De-Initialization
        // Unload all postpro shaders
        for (int i = 0; i < MAX_POSTPRO_SHADERS; i++) UnloadShader(shaders[i]);

        UnloadTexture(texture);         // Unload texture
        UnloadModel(model);             // Unload model
        UnloadRenderTexture(target);    // Unload render texture

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
