using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsLoading : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - models loading");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(50.0f, 50.0f, 50.0f); // Camera3D position
        camera.Target = new(0.0f, 10.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D mode type

        Model model = LoadModel("resources/models/obj/castle.obj"); // Load model
        Texture texture = LoadTexture("resources/models/obj/castle_diffuse.png"); // Load model texture
        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture; // Set map diffuse texture

        Vector3 position = new(0.0f, 0.0f, 0.0f); // Set model position

        BoundingBox bounds = GetMeshBoundingBox(model.Meshes[0]); // Set model bounds

        // NOTE: bounds are calculated from the original size of the model,
        // if model is scaled on drawing, bounds must be also scaled

        bool selected = false; // Selected object flag

        DisableCursor(); // Limit cursor to relative movement inside the window

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            // Load new models/textures on dragref drop
            if (IsFileDropped())
            {
                FilePathList droppedFiles = LoadDroppedFiles();

                if (droppedFiles.Count == 1) // Only support one file dropped
                {
                    if (IsFileExtension(droppedFiles.Paths[0], ".obj") ||
                        IsFileExtension(droppedFiles.Paths[0], ".Gltf") ||
                        IsFileExtension(droppedFiles.Paths[0], ".Glb") ||
                        IsFileExtension(droppedFiles.Paths[0], ".vox") ||
                        IsFileExtension(droppedFiles.Paths[0], ".iqm") ||
                        IsFileExtension(droppedFiles.Paths[0], ".m3d"))       // Model file formats supported
                    {
                        UnloadModel(model); // Unload previous model
                        model = LoadModel(droppedFiles.Paths[0]); // Load new model
                        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture; // Set current map diffuse texture

                        bounds = GetMeshBoundingBox(model.Meshes[0]);

                        // TODO: Move camera position from target enough distance to visualize model propeRLGL.Y
                    }
                    else if (IsFileExtension(droppedFiles.Paths[0], ".png"))  // Texture file formats supported
                    {
                        // Unload current model texture and load new one
                        UnloadTexture(texture);
                        texture = LoadTexture(droppedFiles.Paths[0]);
                        model.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
                    }
                }

                UnloadDroppedFiles(droppedFiles); // Unload filepaths from memory
            }

            // Select model on mouse click
            if (IsMouseButtonPressed(MouseButton.Left))
            {
                // Check collision between ray and box
                if (GetRayCollisionBox(GetMouseRay(GetMousePosition(), camera), bounds).Hit)
                {
                    selected = !selected;
                }
                else
                {
                    selected = false;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    DrawModel(model, position, 1.0f, White); // Draw 3d model with texture

                    DrawGrid(20, 10.0f); // Draw a grid

                    if (selected)
                    {
                        DrawBoundingBox(bounds, Green); // Draw selection box
                    }
                }
                EndMode3D();

                DrawText("Drag ref  drop model to load mesh/texture.", 10, GetScreenHeight() - 20, 10, DarkGray);
                if (selected)
                {
                    DrawText("MODEL SELECTED", GetScreenWidth() - 110, 10, 10, Green);
                }

                DrawText("(c) Castle 3D model by Alberto Cano", screenWidth - 200, screenHeight - 20, 10, Gray);

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
