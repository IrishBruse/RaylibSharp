using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersHybridRender : ExampleHelper 
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

    // Declare custom Structs

    typedef struct {
        uint camPos, camDir, screenCenter;
    }RayLocs ;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - write depth buffer");

        // This Shader calculates pixel depth and color using raymarch
        Shader shdrRaymarch = LoadShader(0, TextFormat("resources/shaders/glsl%i/hybrid_raymarch.fs", GLSL_VERSION));

        // This Shader is a standard rasterization fragment shader with the addition of depth writing
        // You are required to write depth for all shaders if one shader does it
        Shader shdrRaster = LoadShader(0, TextFormat("resources/shaders/glsl%i/hybrid_raster.fs", GLSL_VERSION));

        // Declare Struct used to store camera locs.
        RayLocs marchLocs = {0};

        // Fill the struct with shader locs.
        marchLocs.camPos = GetShaderLocation(shdrRaymarch, "camPos");
        marchLocs.camDir = GetShaderLocation(shdrRaymarch, "camDir");
        marchLocs.screenCenter = GetShaderLocation(shdrRaymarch, "screenCenter");

        // Transfer screenCenter position to shader. Which is used to calculate ray direction.
        Vector2 screenCenter = new(.X = screenWidth/2.0, .Y = screenHeight/2.0);
        SetShaderValue(shdrRaymarch, marchLocs.screenCenter , ref screenCenter , SHADER_UNIFORM_VEC2);

        // Use Customized function to create writable depth texture buffer
        RenderTexture target = LoadRenderTextureDepthTex(screenWidth, screenHeight);

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = {
            .position = (Vector3)new(0.5f,1.0f, 1.5f),    // Camera3D position
            .target = (Vector3)new(0.0f,0.5f, 0.0f),      // Camera3D looking at point
            .up = (Vector3)new(0.0f,1.0f, 0.0f),          // Camera3D up vector (rotation towards target)
            .fovy = 45.0f,                                // Camera3D field-of-view Y
            .projection = CameraProjection.Perspective              // Camera3D projection type
        };

        // Camera3D FOV is pre-calculated in the camera Distance.
        double camDist = 1.0/(tan(camera.Fovy*0.5*DEG2RAD));

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Update Camera3D Postion in the ray march shader.
            SetShaderValue(shdrRaymarch, marchLocs.camPos, ref (camera.Position), RLGL.RlShaderUniformVec3);

            // Update Camera3D Looking Vector. Vector length determines FOV.
            Vector3 camDir = Vector3Scale( Vector3Normalize( Vector3Subtract(camera.Target, camera.Position)) , camDist);
            SetShaderValue(shdrRaymarch, marchLocs.camDir, ref (camDir), RLGL.RlShaderUniformVec3);

            // Draw
            // Draw into our custom render texture (framebuffer)
            BeginTextureMode(target);
                ClearBackground(White);

                // Raymarch Scene
                RLGL.EnableDepthTest(); //Manually enable Depth Test to handle multiple rendering methods.
                BeginShaderMode(shdrRaymarch);
                    DrawRectangle(new(0,0,screenWidth,screenHeight),White);
                EndShaderMode();

                // Raserize Scene
                BeginMode3D(camera);{
                    BeginShaderMode(shdrRaster);
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
        UnloadShader(shdrRaymarch);
        UnloadShader(shdrRaster);

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
