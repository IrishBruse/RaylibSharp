using System;
using System.Numerics;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersLightmap : ExampleHelper
{

    private const int MAP_SIZE = 10;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint); // Enable Multi Sampling Anti Aliasing 4x (if available)
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - lightmap");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.Position = new(4.0f, 6.0f, 8.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        Mesh mesh = GenMeshPlane(MAP_SIZE, MAP_SIZE, 1, 1);

        // GenMeshPlane doesn't generate texcoords2 so we will upload them separately
        mesh.Texcoords2 = new float[mesh.VertexCount * 2];

        // X                          // Y
        mesh.Texcoords2[0] = 0.0f; mesh.Texcoords2[1] = 0.0f;
        mesh.Texcoords2[2] = 1.0f; mesh.Texcoords2[3] = 0.0f;
        mesh.Texcoords2[4] = 0.0f; mesh.Texcoords2[5] = 1.0f;
        mesh.Texcoords2[6] = 1.0f; mesh.Texcoords2[7] = 1.0f;

        // Load a new texcoords2 attributes buffer
        mesh.VboId[(int)ShaderLocationIndex.ShaderLocVertexTexcoord02] = RLGL.LoadVertexBuffer(mesh.Texcoords2, mesh.VertexCount * 2 * sizeof(float), false);
        RLGL.EnableVertexArray(mesh.VaoId);

        // Index 5 is for texcoords2
        RLGL.SetVertexAttribute(5, 2, RLGL.RlFloat, false, 0, 0);
        RLGL.EnableVertexAttribute(5);
        RLGL.DisableVertexArray();

        // Load lightmap shader
        Shader shader = LoadShader($"resources/shaders/glsl{glslVersion}/lightmap.vs", $"resources/shaders/glsl{glslVersion}/lightmap.fs");

        Texture texture = LoadTexture("resources/cubicmap_atlas.png");
        Texture light = LoadTexture("resources/spark_flame.png");

        GenTextureMipmaps(ref texture);
        SetTextureFilter(texture, TextureFilter.Trilinear);

        RenderTexture lightmap = LoadRenderTexture(MAP_SIZE, MAP_SIZE);

        SetTextureFilter(lightmap.Texture, TextureFilter.Trilinear);

        Material material = LoadMaterialDefault();
        material.Shader = shader;
        material.Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
        material.Maps[(int)MaterialMapIndex.Metalness].Texture = lightmap.Texture;

        // Drawing to lightmap
        BeginTextureMode(lightmap);
        ClearBackground(Black);

        BeginBlendMode(BlendMode.Additive);
        {
            DrawTexture(
                light,
                new(0, 0, light.Width, light.Height),
                new(0, 0, 20, 20),
                new(10.0f, 10.0f),
                0.0f,
                Red
            );
            DrawTexture(
                light,
                new(0, 0, light.Width, light.Height),
                new(8, 4, 20, 20),
                new(10.0f, 10.0f),
                0.0f,
                Blue
            );
            DrawTexture(
                light,
                new(0, 0, light.Width, light.Height),
                new(8, 8, 10, 10),
                new(5.0f, 5.0f),
                0.0f,
                Green
            );
            BeginBlendMode(BlendMode.Alpha);
        }
        EndTextureMode();

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {
                    DrawMesh(mesh, material, Matrix4x4.Identity);
                }
                EndMode3D();

                DrawFPS(10, 10);

                DrawTexture(
                    lightmap.Texture,
                    new(0, 0, -MAP_SIZE, -MAP_SIZE),
                    new(GetRenderWidth() - (MAP_SIZE * 8) - 10, 10, MAP_SIZE * 8, MAP_SIZE * 8),
                    new(0.0f, 0.0f),
                    0.0f,
                    White);

                DrawText("lightmap", GetRenderWidth() - 66, 16 + (MAP_SIZE * 8), 10, Gray);
                DrawText("10x10 pixels", GetRenderWidth() - 76, 30 + (MAP_SIZE * 8), 10, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadMesh(mesh); // Unload the mesh
        UnloadShader(shader); // Unload shader

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

}
