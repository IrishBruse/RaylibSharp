using System;
using System.IO;
using System.Numerics;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

using TextureCubemap = RaylibSharp.Texture;

public partial class ModelsSkybox : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - skybox loading and drawing");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(1.0f, 1.0f, 1.0f); // Camera3D position
        camera.Target = new(4.0f, 1.0f, 4.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        // Load skybox model
        Mesh cube = GenMeshCube(1.0f, 1.0f, 1.0f);
        Model skybox = LoadModelFromMesh(cube);

        bool useHDR = true;

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load skybox shader and set required locations
        // NOTE: Some locations are automatically set at shader loading
        skybox.Materials[0].Shader = LoadShader($"resources/shaders/glsl{glslVersion}/skybox.vs", $"resources/shaders/gls{glslVersion}/skybox.fs");

        int matMap = (int)MaterialMapIndex.Cubemap;
        SetShaderValue(skybox.Materials[0].Shader, GetShaderLocation(skybox.Materials[0].Shader, "environmentMap"), matMap, ShaderUniformDataType.ShaderUniformInt);
        SetShaderValue(skybox.Materials[0].Shader, GetShaderLocation(skybox.Materials[0].Shader, "doGamma"), useHDR, ShaderUniformDataType.ShaderUniformInt);
        SetShaderValue(skybox.Materials[0].Shader, GetShaderLocation(skybox.Materials[0].Shader, "vflipped"), useHDR, ShaderUniformDataType.ShaderUniformInt);

        // Load cubemap shader and setup required shader locations
        Shader shdrCubemap = LoadShader($"resources/shaders/glsl{glslVersion}/cubemap.vs", $"resources/shaders/glsl{glslVersion}/cubemap.fs");

        SetShaderValue(shdrCubemap, GetShaderLocation(shdrCubemap, "equirectangularMap"), 0, ShaderUniformDataType.ShaderUniformInt);

        TextureCubemap panorama;

        if (useHDR)
        {
            // Load HDR panorama (sphere) texture
            panorama = LoadTexture("resources/dresden_square_2k.hdr");

            // Generate cubemap (texture with 6 quads-cube-mapping) from panorama HDR texture
            // NOTE 1: New texture is generated rendering to texture, shader calculates the sphere.cube coordinates mapping
            // NOTE 2: It seems on some Android devices WebGL, fbo does not propeRLGL.Y support a FLOAT-based attachment,
            // despite texture can be successfully created.. so using PIXELFORMAT_UNCOMPRESSED_R8G8B8A8 instead of PIXELFORMAT_UNCOMPRESSED_R32G32B32A32
            skybox.Materials[0].Maps[(int)MaterialMapIndex.Cubemap].Texture = GenTextureCubemap(shdrCubemap, panorama, 1024, PixelFormat.UncompressedR8g8b8a8);

            //UnloadTexture(panorama); // Texture not required anymore, cubemap already generated
        }
        else
        {
            Image img = LoadImage("resources/skybox.png");
            skybox.Materials[0].Maps[(int)MaterialMapIndex.Cubemap].Texture = LoadTextureCubemap(img, CubemapLayout.AutoDetect); // CUBEMAP_LAYOUT_PANORAMA
            UnloadImage(img);
        }

        DisableCursor(); // Limit cursor to relative movement inside the window

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            string skyboxFileName = string.Empty;

            // Load new cubemap texture on dragref drop
            if (IsFileDropped())
            {
                FilePathList droppedFiles = LoadDroppedFiles();

                if (droppedFiles.Count == 1)         // Only support one file dropped
                {
                    if (IsFileExtension(droppedFiles.Paths[0], ".png;.jpg;.hdr;.Bmp;.tga"))
                    {
                        // Unload current cubemap texture and load new one
                        UnloadTexture(skybox.Materials[0].Maps[(int)MaterialMapIndex.Cubemap].Texture);
                        if (useHDR)
                        {
                            panorama = LoadTexture(droppedFiles.Paths[0]);

                            // Generate cubemap from panorama texture
                            skybox.Materials[0].Maps[(int)MaterialMapIndex.Cubemap].Texture = GenTextureCubemap(shdrCubemap, panorama, 1024, PixelFormat.UncompressedR8g8b8a8);
                            UnloadTexture(panorama);
                        }
                        else
                        {
                            Image img = LoadImage(droppedFiles.Paths[0]);
                            skybox.Materials[0].Maps[(int)MaterialMapIndex.Cubemap].Texture = LoadTextureCubemap(img, CubemapLayout.AutoDetect);
                            UnloadImage(img);
                        }

                        skyboxFileName = droppedFiles.Paths[0];
                    }
                }

                UnloadDroppedFiles(droppedFiles); // Unload filepaths from memory
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    // We are inside the cube, we need to disable backface culling!
                    RLGL.DisableBackfaceCulling();
                    RLGL.DisableDepthMask();
                    DrawModel(skybox, new(0, 0, 0), 1.0f, White);
                    RLGL.EnableBackfaceCulling();
                    RLGL.EnableDepthMask();

                    DrawGrid(10, 1.0f);

                }
                EndMode3D();

                //DrawTexture(panorama, new( 0, 0 ), 0.0f, 0.5f, White);

                if (useHDR)
                {
                    DrawText("Panorama image from hdrihaven.com: " + Path.GetFileName(skyboxFileName), 10, GetScreenHeight() - 20, 10, Black);
                }
                else
                {
                    DrawText(": " + Path.GetFileName(skyboxFileName), 10, GetScreenHeight() - 20, 10, Black);
                }

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(skybox.Materials[0].Shader);
        UnloadTexture(skybox.Materials[0].Maps[(int)MaterialMapIndex.Cubemap].Texture);

        UnloadModel(skybox); // Unload skybox model

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

    // Generate cubemap texture from HDR texture
    static TextureCubemap GenTextureCubemap(Shader shader, TextureCubemap panorama, int size, PixelFormat format)
    {
        TextureCubemap cubemap = new();

        RLGL.DisableBackfaceCulling(); // Disable backface culling to render inside the cube

        // STEP 1: Setup framebuffer
        uint rbo = RLGL.LoadTextureDepth(size, size, true);
        cubemap.Id = RLGL.LoadTextureCubemap(0, size, format);

        uint fbo = RLGL.LoadFramebuffer(size, size);
        RLGL.FramebufferAttach(fbo, rbo, FramebufferAttachType.Depth, FramebufferAttachTextureType.Renderbuffer, 0);
        RLGL.FramebufferAttach(fbo, cubemap.Id, FramebufferAttachType.ColorChannel0, FramebufferAttachTextureType.CubemapPositiveX, 0);

        // Check if framebuffer is complete with attachments (valid)
        if (RLGL.FramebufferComplete(fbo))
        {
            TraceLog(TraceLogLevel.Info, $"FBO: [ID {fbo}] Framebuffer object created successfully");
        }

        // STEP 2: Draw to framebuffer
        // NOTE: Shader is used to convert HDR equirectangular environment map to cubemap equivalent (6 faces)
        RLGL.EnableShader(shader.Id);

        // Define projection matrix and send it to shader
        Matrix4x4 matFboProjection = Matrix4x4.CreatePerspectiveFieldOfView(90.0f * MathF.PI / 180f, 1.0f, (float)RLGL.RlCullDistanceNear, (float)RLGL.RlCullDistanceFar);
        RLGL.SetUniformMatrix(shader.Locs[(int)ShaderLocationIndex.ShaderLocMatrixProjection], matFboProjection);

        // Define view matrix for every side of the cubemap
        Matrix4x4[] fboViews = new Matrix4x4[6] {
            Matrix4x4.CreateLookAt(new(0.0f,0.0f, 0.0f), new( 1.0f, 0.0f,  0.0f), new(0.0f,-1.0f,  0.0f)),
            Matrix4x4.CreateLookAt(new(0.0f,0.0f, 0.0f), new(-1.0f, 0.0f,  0.0f), new(0.0f,-1.0f,  0.0f)),
            Matrix4x4.CreateLookAt(new(0.0f,0.0f, 0.0f), new( 0.0f, 1.0f,  0.0f), new(0.0f, 0.0f,  1.0f)),
            Matrix4x4.CreateLookAt(new(0.0f,0.0f, 0.0f), new( 0.0f,-1.0f,  0.0f), new(0.0f, 0.0f, -1.0f)),
            Matrix4x4.CreateLookAt(new(0.0f,0.0f, 0.0f), new( 0.0f, 0.0f,  1.0f), new(0.0f,-1.0f,  0.0f)),
            Matrix4x4.CreateLookAt(new(0.0f,0.0f, 0.0f), new( 0.0f, 0.0f, -1.0f), new(0.0f,-1.0f,  0.0f))
        };

        RLGL.Viewport(0, 0, size, size); // Set viewport to current fbo dimensions

        // Activate and enable texture for drawing to cubemap faces
        RLGL.ActiveTextureSlot(0);
        RLGL.EnableTexture(panorama.Id);

        for (int i = 0; i < 6; i++)
        {
            // Set the view matrix for the current cube face
            RLGL.SetUniformMatrix(shader.Locs[(int)ShaderLocationIndex.ShaderLocMatrixView], fboViews[i]);

            // Select the current cubemap face attachment for the fbo
            // WARNING: This function by default enables.Attach.disables fbo!!!
            RLGL.FramebufferAttach(fbo, cubemap.Id, FramebufferAttachType.ColorChannel0, FramebufferAttachTextureType.CubemapPositiveX + i, 0);
            RLGL.EnableFramebuffer(fbo);

            // Load and draw a cube, it uses the current enabled texture
            RLGL.ClearScreenBuffers();
            RLGL.LoadDrawCube();

            // ALTERNATIVE: Try to use internal batch system to draw the cube instead of RLGL.LoadDrawCube
            // for some reason this method does not work, maybe due to cube triangles definition? normals pointing out?
            // TODO: Investigate this issue...
            //RLGL.SetTexture(panorama.Id); // WARNING: It must be called after enabling current framebuffer if using internal batch system!
            //RLGL.ClearScreenBuffers();
            //DrawCube(Vector3Zero(), Vector3One(), White);
            //RLGL.DrawRenderBatchActive();
        }

        // STEP 3: Unload framebuffer and reset state
        RLGL.DisableShader(); // Unbind shader
        RLGL.DisableTexture(); // Unbind texture
        RLGL.DisableFramebuffer(); // Unbind framebuffer
        RLGL.UnloadFramebuffer(fbo); // Unload framebuffer (and automatically attached depth texture/renderbuffer)

        // Reset viewport dimensions to default
        RLGL.Viewport(0, 0, RLGL.GetFramebufferWidth(), RLGL.GetFramebufferHeight());
        RLGL.EnableBackfaceCulling();

        cubemap.Width = size;
        cubemap.Height = size;
        cubemap.Mipmaps = 1;
        cubemap.Format = format;

        return cubemap;
    }
}
