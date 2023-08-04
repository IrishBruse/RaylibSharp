using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsAnimation : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - model animation");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(10.0f, 10.0f, 10.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f);      // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D mode type

        Model model = LoadModel("resources/models/iqm/guy.iqm");                    // Load the animated model mesh and basic data
        Texture texture = LoadTexture("resources/models/iqm/guytex.png");         // Load model texture and set material
        SetMaterialTexture(ref model.Materials[0], MaterialMapIndex.Albedo, texture);     // Set model material map texture

        Vector3 position = new(0.0f, 0.0f, 0.0f);            // Set model position

        // Load animation data
        uint animsCount = 0;
        ModelAnimation[] anims = LoadModelAnimations("resources/models/iqm/guyanim.iqm", ref animsCount);
        int animFrameCounter = 0;

        DisableCursor();                    // Catch cursor
        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            // Play animation when spacebar is held down
            if (IsKeyDown(Key.Space))
            {
                animFrameCounter++;
                UpdateModelAnimation(model, anims[0], animFrameCounter);
                if (animFrameCounter >= anims[0].FrameCount)
                {
                    animFrameCounter = 0;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    DrawModel(model, position, new(1.0f, 0.0f, 0.0f), -90.0f, new(1.0f, 1.0f, 1.0f), White);

                    for (int i = 0; i < model.BoneCount; i++)
                    {
                        DrawCube(anims[0].FramePoses[animFrameCounter][i].Translation, 0.2f, 0.2f, 0.2f, Red);
                    }

                    DrawGrid(10, 1.0f);         // Draw a grid

                }
                EndMode3D();

                DrawText("PRESS SPACE to PLAY MODEL ANIMATION", 10, 10, 20, Maroon);
                DrawText("(c) Guy IQM 3D model by @culacant", screenWidth - 200, screenHeight - 20, 10, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture);                     // Unload texture
        // UnloadModelAnimations(anims, animsCount);   // Unload model animations data
        UnloadModel(model);                         // Unload model

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
