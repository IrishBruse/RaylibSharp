using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsFirstPersonMaze : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - first person maze");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(0.2f, 0.4f, 0.2f);    // Camera3D position
        camera.Target = new(0.185f, 0.4f, 0.0f);    // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type
        _ = new Vector3(0.0f, 0.0f, 0.0f);            // Set model position

        Image imMap = LoadImage("resources/cubicmap.png");      // Load cubicmap image (RAM)
        Texture cubicmap = LoadTextureFromImage(imMap);       // Convert image to texture to display (VRAM)
        Mesh mesh = GenMeshCubicmap(imMap, new(1.0f, 1.0f, 1.0f));
        Model model = LoadModelFromMesh(mesh);

        // NOTE: By default each cube is mapped to one part of texture atlas
        Texture texture = LoadTexture("resources/cubicmap_atlas.png");    // Load map texture
        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;    // Set map diffuse texture

        // Get map image data to be used for collision detection
        Color[] mapPixels = LoadImageColors(imMap);
        UnloadImage(imMap);             // Unload image from RAM

        Vector3 mapPosition = new(-16.0f, 0.0f, -8.0f);  // Set model position

        DisableCursor();                // Limit cursor to relative movement inside the window

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            Vector3 oldCamPos = camera.Position;    // Store old camera position

            UpdateCamera(ref camera, CameraMode.FirstPerson);

            // Check player collision (we simplify to 2D collision detection)
            Vector2 playerPos = new(camera.Position.X, camera.Position.Z);
            float playerRadius = 0.1f;  // Collision radius (player is modelled as a cilinder for collision)

            int playerCellX = (int)(playerPos.X - mapPosition.X + 0.5f);
            int playerCellY = (int)(playerPos.Y - mapPosition.Z + 0.5f);

            // Out-of-limits security check
            if (playerCellX < 0)
            {
                playerCellX = 0;
            }
            else if (playerCellX >= cubicmap.Width)
            {
                playerCellX = cubicmap.Width - 1;
            }

            if (playerCellY < 0)
            {
                playerCellY = 0;
            }
            else if (playerCellY >= cubicmap.Height)
            {
                playerCellY = cubicmap.Height - 1;
            }

            // Check map collisions using image data and player position
            // TODO: Improvement: Just check player surrounding cells for collision
            for (int y = 0; y < cubicmap.Height; y++)
            {
                for (int x = 0; x < cubicmap.Width; x++)
                {
                    if ((mapPixels[(y * cubicmap.Width) + x].R == 255) &&       // Collision: white pixel, only check R channel
                        CheckCollisionCircle(playerPos, playerRadius,
                        new(mapPosition.X - 0.5f + (x * 1.0f), mapPosition.Z - 0.5f + (y * 1.0f), 1.0f, 1.0f)))
                    {
                        // Collision detected, reset camera position
                        camera.Position = oldCamPos;
                    }
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {
                    DrawModel(model, mapPosition, 1.0f, White);                     // Draw maze map
                }
                EndMode3D();

                DrawTexture(cubicmap, new(GetScreenWidth() - (cubicmap.Width * 4.0f) - 20, 20.0f), 0.0f, 4.0f, White);
                DrawRectangleLines(GetScreenWidth() - (cubicmap.Width * 4) - 20, 20, cubicmap.Width * 4, cubicmap.Height * 4, Green);

                // Draw player position radar
                DrawRectangle(GetScreenWidth() - (cubicmap.Width * 4) - 20 + (playerCellX * 4), 20 + (playerCellY * 4), 4, 4, Red);

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        // UnloadImageColors(mapPixels);   // Unload color array

        UnloadTexture(cubicmap);        // Unload cubicmap texture
        UnloadTexture(texture);         // Unload map texture
        UnloadModel(model);             // Unload map model

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
