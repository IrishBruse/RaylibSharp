using System;
using System.Drawing;
using System.Numerics;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersHybridRender : ExampleHelper
{
    // Declare custom Structs
    private struct RayLocs
    {
        public int camPos;
        public int camDir;
        public int screenCenter;
    };

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - write depth buffer");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // This Shader calculates pixel depth and color using raymarch
        Shader shdrRaymarch = LoadShader(null, $"resources/shaders/glsl{glslVersion}/hybrid_raymarch.fs");

        // This Shader is a standard rasterization fragment shader with the addition of depth writing
        // You are required to write depth for all shaders if one shader does it
        Shader shdrRaster = LoadShader(null, $"resources/shaders/glsl{glslVersion}/hybrid_raster.fs");

        // Declare Struct used to store camera locs.
        RayLocs marchLocs = new();

        // Fill the struct with shader locs.
        marchLocs.camPos = GetShaderLocation(shdrRaymarch, "camPos");
        marchLocs.camDir = GetShaderLocation(shdrRaymarch, "camDir");
        marchLocs.screenCenter = GetShaderLocation(shdrRaymarch, "screenCenter");

        // Transfer screenCenter position to shader. Which is used to calculate ray direction.
        Vector2 screenCenter = new(screenWidth / 2.0f, screenHeight / 2.0f);
        SetShaderValue(shdrRaymarch, marchLocs.screenCenter, ref screenCenter, ShaderUniformDataType.ShaderUniformVec2);

        // Use Customized function to create writable depth texture buffer
        RenderTexture target = LoadRenderTextureDepthTex(screenWidth, screenHeight);

        // Define the camera to look into our 3d world
        Camera3D camera = new()
        {
            Position = new(0.5f, 1.0f, 1.5f),    // Camera3D position
            Target = new(0.0f, 0.5f, 0.0f),      // Camera3D looking at point
            Up = new(0.0f, 1.0f, 0.0f),          // Camera3D up vector (rotation towards target)
            Fovy = 45.0f,                                // Camera3D field-of-view Y
            Projection = CameraProjection.Perspective              // Camera3D projection type
        };

        // Camera3D FOV is pre-calculated in the camera Distance.
        double camDist = 1.0 / Math.Tan(camera.Fovy * 0.5 * DEG2RAD);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Update Camera3D Postion in the ray march shader.
            SetShaderValue(shdrRaymarch, marchLocs.camPos, ref camera.Position, ShaderUniformDataType.ShaderUniformVec3);

            // Update Camera3D Looking Vector. Vector length determines FOV.
            Vector3 camDir = Vector3.Multiply(Vector3.Normalize(Vector3.Subtract(camera.Target, camera.Position)), (float)camDist);
            SetShaderValue(shdrRaymarch, marchLocs.camDir, ref camDir, ShaderUniformDataType.ShaderUniformVec3);

            // Draw
            // Draw into our custom render texture (framebuffer)
            BeginTextureMode(target);
            {
                ClearBackground(White);

                // Raymarch Scene
                RLGL.EnableDepthTest(); //Manually enable Depth Test to handle multiple rendering methods.
                BeginShaderMode(shdrRaymarch);
                DrawRectangle(new(0, 0, screenWidth, screenHeight), White);
            }
            EndShaderMode();

            // Raserize Scene
            BeginMode3D(camera);
            {
                BeginShaderMode(shdrRaster);
                {
                    DrawCubeWires(new(0.0f, 0.5f, 1.0f), new(1.0f, 1.0f, 1.0f), Red);
                    DrawCube(new(0.0f, 0.5f, 1.0f), new(1.0f, 1.0f, 1.0f), Purple);
                    DrawCubeWires(new(0.0f, 0.5f, -1.0f), new(1.0f, 1.0f, 1.0f), DarkGreen);
                    DrawCube(new(0.0f, 0.5f, -1.0f), new(1.0f, 1.0f, 1.0f), Yellow);
                    DrawGrid(10, 1.0f);
                }
                EndShaderMode();
            }
            EndMode3D();
        }
        EndTextureMode();

        // Draw into screen our custom render texture
        BeginDrawing();
        {
            ClearBackground(RayWhite);

            DrawTexture(target.Texture, new RectangleF(0, 0, screenWidth, -screenHeight), new(0, 0), White);
            DrawFPS(10, 10);
        }
        EndDrawing();


        // De-Initialization
        UnloadRenderTextureDepthTex(target);
        UnloadShader(shdrRaymarch);
        UnloadShader(shdrRaster);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    // Define custom functions required for the example
    // Load custom render texture, create a writable depth texture buffer
    private static RenderTexture LoadRenderTextureDepthTex(int width, int height)
    {
        RenderTexture target = new();

        target.Id = RLGL.LoadFramebuffer(width, height);   // Load an empty framebuffer

        if (target.Id > 0)
        {
            RLGL.EnableFramebuffer(target.Id);

            // Create color texture (default to RGBA)
            target.Texture.Id = RLGL.LoadTexture(0, width, height, PixelFormat.UncompressedR8g8b8a8, 1);
            target.Texture.Width = width;
            target.Texture.Height = height;
            target.Texture.Format = PixelFormat.UncompressedR8g8b8a8;
            target.Texture.Mipmaps = 1;

            // Create depth texture buffer (instead of raylib default renderbuffer)
            target.Depth.Id = RLGL.LoadTextureDepth(width, height, false);
            target.Depth.Width = width;
            target.Depth.Height = height;
            target.Depth.Format = PixelFormat.CompressedPvrtRgba;       //DEPTH_COMPONENT_24BIT?
            target.Depth.Mipmaps = 1;

            // Attach color texture and depth texture to FBO
            RLGL.FramebufferAttach(target.Id, target.Texture.Id, FramebufferAttachType.ColorChannel0, FramebufferAttachTextureType.Texture2d, 0);
            RLGL.FramebufferAttach(target.Id, target.Depth.Id, FramebufferAttachType.Depth, FramebufferAttachTextureType.Texture2d, 0);

            // Check if fbo is complete with attachments (valid)
            if (RLGL.FramebufferComplete(target.Id))
            {
                TraceLog(TraceLogLevel.Info, $"FBO: [ID {target.Id}] Framebuffer object created successfully");
            }

            RLGL.DisableFramebuffer();
        }
        else
        {
            TraceLog(TraceLogLevel.Warning, "FBO: Framebuffer object can not be created");
        }

        return target;
    }

    // Unload render texture from GPU memory (VRAM)
    private static void UnloadRenderTextureDepthTex(RenderTexture target)
    {
        if (target.Id > 0)
        {
            // Color texture attached to FBO is deleted
            RLGL.UnloadTexture(target.Texture.Id);
            RLGL.UnloadTexture(target.Depth.Id);

            // NOTE: Depth texture is automatically
            // queried and deleted before deleting framebuffer
            RLGL.UnloadFramebuffer(target.Id);
        }
    }
}
