using System.IO;
using System.Numerics;

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

        string[] voxFileNames = new string[]{
            "resources/models/vox/chr_knight.vox",
            "resources/models/vox/chr_sword.vox",
            "resources/models/vox/monu9.vox"
        };

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - magicavoxel loading");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(10.0f, 10.0f, 10.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        // Load MagicaVoxel files
        Model[] models = new Model[MAX_VOX_FILES];

        for (int i = 0; i < MAX_VOX_FILES; i++)
        {
            // Load VOX file and measure time
            double t0 = GetTime() * 1000.0;
            models[i] = LoadModel(voxFileNames[i]);
            double t1 = GetTime() * 1000.0;

            TraceLog(TraceLogLevel.Warning, $"[{voxFileNames[i]}] File loaded in {t1 - t0:0.000} ms");

            // Compute model translation matrix to center model on draw position (0, 0 , 0)
            BoundingBox bb = GetModelBoundingBox(models[i]);
            Vector3 center = new();
            center.X = bb.Min.X + ((bb.Max.X - bb.Min.X) / 2);
            center.Z = bb.Min.Z + ((bb.Max.Z - bb.Min.Z) / 2);

            Matrix4x4 matTranslate = Matrix4x4.CreateTranslation(-center.X, 0, -center.Z);
            models[i].Transform = matTranslate;
        }

        int currentModel = 0;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            // Cycle between models on mouse click
            if (IsMouseButtonPressed(MouseButton.Left))
            {
                currentModel = (currentModel + 1) % MAX_VOX_FILES;
            }

            // Cycle between models on key pressed
            if (IsKeyPressed(Key.Right))
            {
                currentModel++;
                if (currentModel >= MAX_VOX_FILES)
                {
                    currentModel = 0;
                }
            }
            else if (IsKeyPressed(Key.Left))
            {
                currentModel--;
                if (currentModel < 0)
                {
                    currentModel = MAX_VOX_FILES - 1;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // Draw 3D model
                BeginMode3D(camera);
                {

                    DrawModel(models[currentModel], new(0, 0, 0), 1.0f, White);
                    DrawGrid(10, 1.0f);

                }
                EndMode3D();

                // Display info
                DrawRectangle(10, 400, 310, 30, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(10, 400, 310, 30, Fade(DarkBlue, 0.5f));
                DrawText("MOUSE LEFT BUTTON to CYCLE VOX MODELS", 40, 410, 10, Blue);
                DrawText("File:" + Path.GetFileName(voxFileNames[currentModel]), 10, 10, 20, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        // Unload models data (GPU VRAM)
        for (int i = 0; i < MAX_VOX_FILES; i++)
        {
            UnloadModel(models[i]);
        }

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

}
