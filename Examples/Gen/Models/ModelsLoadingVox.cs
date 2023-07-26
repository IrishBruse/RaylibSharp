using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsLoadingVox : ExampleHelper 
{

private const int MAX_VOX_FILES = 3;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        string [] voxFileNames = new string []{
            "resources/models/vox/chr_knight.vox",
            "resources/models/vox/chr_sword.vox",
            "resources/models/vox/monu9.vox"
        };

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - magicavoxel loading");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(10.0f,10.0f, 10.0f); // Camera position
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);      // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        // Load MagicaVoxel files
        Model models[MAX_VOX_FILES] = new();

        for (int i = 0; i < MAX_VOX_FILES; i++)
        {
            // Load VOX file and measure time
            double t0 = GetTime()*1000.0;
            models[i] = LoadModel(voxFileNames[i]);
            double t1 = GetTime()*1000.0;

            TraceLog(LOG_WARNING, TextFormat("[%s] File loaded in %.3f ms", voxFileNames[i], t1 - t0));

            // Compute model translation matrix to center model on draw position (0, 0 , 0)
            BoundingBox bb = GetModelBoundingBox(models[i]);
            Vector3 center = new();
            center.X = bb.min.X  + (((bb.max.X - bb.min.X)/2));
            center.Z = bb.min.Z  + (((bb.max.Z - bb.min.Z)/2));

            Matrix matTranslate = MatrixTranslate(-center.X, 0, -center.Z);
            models[i].transform = matTranslate;
        }

        int currentModel = 0;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Cycle between models on mouse click
            if (IsMouseButtonPressed(MouseButton.Left)) currentModel = (currentModel + 1)%MAX_VOX_FILES;

            // Cycle between models on key pressed
            if (IsKeyPressed(Key.Right))
            {
                currentModel++;
                if (currentModel >= MAX_VOX_FILES) currentModel = 0;
            }
            else if (IsKeyPressed(Key.Left))
            {
                currentModel--;
                if (currentModel < 0) currentModel = MAX_VOX_FILES - 1;
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                // Draw 3D model
                BeginMode3D(camera);{

                    DrawModel(models[currentModel], new( 0, 0, 0 ), 1.0f, White);
                    DrawGrid(10, 1.0);

                }EndMode3D();

                // Display info
                DrawRectangle(10, 400, 310, 30, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(10, 400, 310, 30, Fade(DarkBlue, 0.5f));
                DrawText("MOUSE LEFT BUTTON to CYCLE VOX MODELS", 40, 410, 10, Blue);
                DrawText(TextFormat("File: %s", GetFileName(voxFileNames[currentModel])), 10, 10, 20, Gray);

            }EndDrawing();
        }

        // De-Initialization
        // Unload models data (GPU VRAM)
        for (int i = 0; i < MAX_VOX_FILES; i++) UnloadModel(models[i]);

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }

}
