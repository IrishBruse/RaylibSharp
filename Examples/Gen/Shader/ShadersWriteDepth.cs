using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersWriteDepth : ExampleHelper 
{

    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

    // Declare custom functions required for the example
    // Load custom render texture, create a writable depth texture buffer
    static RenderTexture LoadRenderTextureDepthTex(int width, int height);

    // Unload render texture from GPU memory (VRAM)
    static static void UnloadRenderTextureDepthTex(RenderTexture target);

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - write depth buffer");

        // The shader inverts the depth buffer by writing into it by `gl_FragDepth = 1 - gl_FragCoord.Z;`
        Shader shader = LoadShader(0, TextFormat("resources/shaders/glsl%i/write_depth.fs", GLSL_VERSION));

        // Use Customized function to create writable depth texture buffer
        RenderTexture target = LoadRenderTextureDepthTex(screenWidth, screenHeight);

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = {
            .position = (Vector3)new(2.0f,2.0f, 3.0f),    // Camera3D position
            .target = (Vector3)new(0.0f,0.5f, 0.0f),      // Camera3D looking at point
            .up = (Vector3)new(0.0f,1.0f, 0.0f),          // Camera3D up vector (rotation towards target)
            .fovy = 45.0f,                                // Camera3D field-of-view Y
            .projection = CameraProjection.Perspective              // Camera3D projection type
        };

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Draw

            // Draw into our custom render texture (framebuffer)
            BeginTextureMode(target);
                ClearBackground(White);

                BeginMode3D(camera);{
                    BeginShaderMode(shader);
                        DrawCubeWires((Vector3)new(0.0f,0.5f, 1.0f), (Vector3)new(1.0f,1.0f, 1.0f), Red);
                        DrawCube((Vector3)new(0.0f,0.5f, 1.0f), (Vector3)new(1.0f,1.0f, 1.0f), Purple);
                        DrawCubeWires((Vector3)new(0.0f,0.5f, -1.0f), (Vector3)new(1.0f,1.0f, 1.0f), DarkGreen);
                        DrawCube((Vector3) new(0.0f,0.5f, -1.0f), (Vector3)new(1.0f,1.0f, 1.0f), Yellow);
                        DrawGrid(10, 1.0f);
                    EndShaderMode();
                }EndMode3D();
            EndTextureMode();

            // Draw into screen our custom render texture
            BeginDrawing();{
                ClearBackground(RayWhite);

                DrawTexture(target.Texture, (Rectangle) { 0, 0, screenWidth, -screenHeight }, new( 0, 0 ), White);
                DrawFPS(10, 10);
            }EndDrawing();
        }

        // De-Initialization
        UnloadRenderTextureDepthTex(target);
        UnloadShader(shader);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    // Define custom functions required for the example
    // Load custom render texture, create a writable depth texture buffer
    RenderTexture LoadRenderTextureDepthTex(int width, int height)
    {
        RenderTexture target = new();

        target.Id = RLGL.LoadFramebuffer(width, height);   // Load an empty framebuffer

        if (target.Id > 0)
        {
            RLGL.EnableFramebuffer(target.Id);

            // Create color texture (default to RGBA)
            target.Texture.Id = RLGL.LoadTexture(0, width, height, PIXELFORMAT_UNCOMPRESSED_R8G8B8A8, 1);
            target.Texture.Width = width;
            target.Texture.Height = height;
            target.Texture.format = PIXELFORMAT_UNCOMPRESSED_R8G8B8A8;
            target.Texture.mipmaps = 1;

            // Create depth texture buffer (instead of raylib default renderbuffer)
            target.depth.Id = RLGL.LoadTextureDepth(width, height, false);
            target.depth.Width = width;
            target.depth.Height = height;
            target.depth.format = 19;       //DEPTH_COMPONENT_24BIT?
            target.depth.mipmaps = 1;

            // Attach color texture and depth texture to FBO
            RLGL.FramebufferAttach(target.Id, target.Texture.Id, RLGL.RlAttachmentColorChannel0, RLGL.RlAttachmentTexture2d, 0);
            RLGL.FramebufferAttach(target.Id, target.depth.Id, RLGL.RlAttachmentDepth, RLGL.RlAttachmentTexture2d, 0);

            // Check if fbo is complete with attachments (valid)
            if (RLGL.FramebufferComplete(target.Id)) TRACELOG(LOG_INFO, "FBO: [ID %i] Framebuffer object created successfully", target.Id);

            RLGL.DisableFramebuffer();
        }
        else TRACELOG(LOG_WARNING, "FBO: Framebuffer object can not be created");

        return target;
    }

    // Unload render texture from GPU memory (VRAM)
    static void UnloadRenderTextureDepthTex(RenderTexture target)
    {
        if (target.Id > 0)
        {
            // Color texture attached to FBO is deleted
            RLGL.UnloadTexture(target.Texture.Id);
            RLGL.UnloadTexture(target.depth.Id);

            // NOTE: Depth texture is automatically
            // queried and deleted before deleting framebuffer
            RLGL.UnloadFramebuffer(target.Id);
        }
    }
}
