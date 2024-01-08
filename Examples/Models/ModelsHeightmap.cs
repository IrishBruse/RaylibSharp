using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsHeightmap : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - heightmap loading and drawing");

        // Define our custom camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(18.0f, 21.0f, 18.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        Image image = LoadImage("resources/heightmap.png"); // Load heightmap image (RAM)
        Texture texture = LoadTextureFromImage(image); // Convert image to texture (VRAM)

        Mesh mesh = GenMeshHeightmap(image, new(16, 8, 16)); // Generate heightmap mesh (RAM and VRAM)
        Model model = LoadModelFromMesh(mesh); // Load model from generated mesh

        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture; // Set map diffuse texture
        Vector3 mapPosition = new(-8.0f, 0.0f, -8.0f); // Define model position

        UnloadImage(image); // Unload heightmap image from RAM, already uploaded to VRAM

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {
                    DrawModel(model, mapPosition, 1.0f, Red);

                    DrawGrid(20, 1.0f);
                }
                EndMode3D();

                DrawTexture(texture, screenWidth - texture.Width - 20, 20, White);
                DrawRectangleLines(screenWidth - texture.Width - 20, 20, texture.Width, texture.Height, Green);

                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture); // Unload texture
        UnloadModel(model); // Unload model

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
