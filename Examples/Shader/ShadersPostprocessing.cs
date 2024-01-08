using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersPostprocessing : ExampleHelper
{
    const int MAX_POSTPRO_SHADERS = 12;

    enum PostproShader
    {
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
    }

    static string[] postproShaderText = new string[]{
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

        SetConfigFlags(WindowFlag.Msaa4xHint); // Enable Multi Sampling Anti Aliasing 4x (if available)

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - postprocessing shader");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(2.0f, 3.0f, 2.0f); // Camera3D position
        camera.Target = new(0.0f, 1.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        Model model = LoadModel("resources/models/church.obj"); // Load OBJ model
        Texture texture = LoadTexture("resources/models/church_diffuse.png"); // Load model texture (diffuse map)
        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture; // Set model diffuse texture

        Vector3 position = new(0.0f, 0.0f, 0.0f); // Set model position

        // Load all postpro shaders
        // NOTE 1: All postpro shader use the base vertex shader (DEFAULT_VERTEX_SHADER)
        // NOTE 2: We load the correct shader depending on GLSL version
        Shader[] shaders = new Shader[MAX_POSTPRO_SHADERS];

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        shaders[(int)PostproShader.FX_GraySCALE] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/grayscale.fs");
        shaders[(int)PostproShader.FX_POSTERIZATION] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/posterization.fs");
        shaders[(int)PostproShader.FX_DREAM_VISION] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/dream_vision.fs");
        shaders[(int)PostproShader.FX_PIXELIZER] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/pixelizer.fs");
        shaders[(int)PostproShader.FX_CROSS_HATCHING] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/cross_hatching.fs");
        shaders[(int)PostproShader.FX_CROSS_STITCHING] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/cross_stitching.fs");
        shaders[(int)PostproShader.FX_PRedATOR_VIEW] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/predator.fs");
        shaders[(int)PostproShader.FX_SCANLINES] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/scanlines.fs");
        shaders[(int)PostproShader.FX_FISHEYE] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/fisheye.fs");
        shaders[(int)PostproShader.FX_SOBEL] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/sobel.fs");
        shaders[(int)PostproShader.FX_BLOOM] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/bloom.fs");
        shaders[(int)PostproShader.FX_BLUR] = LoadShader(null, $"resources/shaders/glsl{glslVersion}/blur.fs");

        int currentShader = (int)PostproShader.FX_GraySCALE;

        // Create a RenderTexture to be used for render to texture
        RenderTexture target = LoadRenderTexture(screenWidth, screenHeight);

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            if (IsKeyPressed(Key.Right))
            {
                currentShader++;
            }
            else if (IsKeyPressed(Key.Left))
            {
                currentShader--;
            }

            if (currentShader >= MAX_POSTPRO_SHADERS)
            {
                currentShader = 0;
            }
            else if (currentShader < 0)
            {
                currentShader = MAX_POSTPRO_SHADERS - 1;
            }

            // Draw
            BeginTextureMode(target);
            {       // Enable drawing to texture
                ClearBackground(RayWhite); // Clear texture background

                BeginMode3D(camera);
                {        // Begin 3d mode drawing
                    DrawModel(model, position, 0.1f, White); // Draw 3d model with texture
                    DrawGrid(10, 1.0f); // Draw a grid
                }
                EndMode3D(); // End 3d mode drawing, returns to orthographic 2d mode
            }
            EndTextureMode(); // End drawing to texture (now we have a texture available for next passes)

            BeginDrawing();
            {
                ClearBackground(RayWhite); // Clear screen background

                // Render generated texture using selected postprocessing shader
                BeginShaderMode(shaders[currentShader]);
                {
                    // NOTE: Render texture must be y-flipped due to default OpenGL coordinates (left-bottom)
                    DrawTexture(target.Texture, new(0, 0, target.Texture.Width, -target.Texture.Height), new Vector2(0, 0), White);
                }
                EndShaderMode();

                // Draw 2d shapes and text over drawn texture
                DrawRectangle(0, 9, 580, 30, Fade(LightGray, 0.7f));

                DrawText("(c) Church 3D model by Alberto Cano", screenWidth - 200, screenHeight - 20, 10, Gray);
                DrawText("CURRENT POSTPRO SHADER:", 10, 15, 20, Black);
                DrawText(postproShaderText[currentShader], 330, 15, 20, Red);
                DrawText("< >", 540, 10, 30, DarkBlue);
                DrawFPS(700, 15);
            }
            EndDrawing();
        }

        // De-Initialization
        // Unload all postpro shaders
        for (int i = 0; i < MAX_POSTPRO_SHADERS; i++)
        {
            UnloadShader(shaders[i]);
        }

        UnloadTexture(texture); // Unload texture
        UnloadModel(model); // Unload model
        UnloadRenderTexture(target); // Unload render texture

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
