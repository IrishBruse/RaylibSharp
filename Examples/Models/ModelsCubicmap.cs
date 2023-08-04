using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsCubicmap : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - cubesmap loading and drawing");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(16.0f, 14.0f, 16.0f);     // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f);          // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);              // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                    // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;                 // Camera3D projection type

        Image image = LoadImage("resources/cubicmap.png");      // Load cubicmap image (RAM)
        Texture cubicmap = LoadTextureFromImage(image);       // Convert image to texture to display (VRAM)

        Mesh mesh = GenMeshCubicmap(image, new(1.0f, 1.0f, 1.0f));
        Model model = LoadModelFromMesh(mesh);

        // NOTE: By default each cube is mapped to one part of texture atlas
        Texture texture = LoadTexture("resources/cubicmap_atlas.png");    // Load map texture
        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;    // Set map diffuse texture

        Vector3 mapPosition = new(-16.0f, 0.0f, -8.0f);          // Set model position

        UnloadImage(image);     // Unload cubesmap image from RAM, already uploaded to VRAM

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

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

                    DrawModel(model, mapPosition, 1.0f, White);

                }
                EndMode3D();

                DrawTexture(cubicmap, new(screenWidth - (cubicmap.Width * 4.0f) - 20, 20.0f), 0.0f, 4.0f, White);
                DrawRectangleLines(screenWidth - (cubicmap.Width * 4) - 20, 20, cubicmap.Width * 4, cubicmap.Height * 4, Green);

                DrawText("cubicmap image used to", 658, 90, 10, Gray);
                DrawText("generate map 3d model", 658, 104, 10, Gray);

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(cubicmap);    // Unload cubicmap texture
        UnloadTexture(texture);     // Unload map texture
        UnloadModel(model);         // Unload map model

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
