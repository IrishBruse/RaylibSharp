using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersWriteDepth : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - write depth buffer");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // The shader inverts the depth buffer by writing into it by `gl_FragDepth = 1 - gl_FragCoord.Z;`
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/write_depth.fs");

        // Use Customized function to create writable depth texture buffer
        RenderTexture target = LoadRenderTextureDepthTex(screenWidth, screenHeight);

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(2.0f, 2.0f, 3.0f); // Camera3D position
        camera.Target = new(0.0f, 0.5f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Draw

            // Draw into our custom render texture (framebuffer)
            BeginTextureMode(target);
            {
                ClearBackground(White);

                BeginMode3D(camera);
                {
                    BeginShaderMode(shader);
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

                DrawTexture(target.Texture, new Rectangle(0, 0, screenWidth, -screenHeight), new(0, 0), White);
                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadRenderTextureDepthTex(target);
        UnloadShader(shader);

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

    // Define custom functions required for the example
    // Load custom render texture, create a writable depth texture buffer
    static RenderTexture LoadRenderTextureDepthTex(int width, int height)
    {
        RenderTexture target = new();

        target.Id = RLGL.LoadFramebuffer(width, height); // Load an empty framebuffer

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
            target.Depth.Format = PixelFormat.CompressedPvrtRgba; //DEPTH_COMPONENT_24BIT?
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
    static void UnloadRenderTextureDepthTex(RenderTexture target)
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
