using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsLoadingM3d : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - M3D model loading");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(1.5f, 1.5f, 1.5f);    // Camera3D position
        camera.Target = new(0.0f, 0.4f, 0.0f);      // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type

        Vector3 position = new(0.0f, 0.0f, 0.0f);            // Set model position

        string modelFileName = "resources/models/m3d/cesium_man.m3d";
        bool drawMesh = true;
        bool drawSkeleton = true;
        bool animPlaying = false;   // Store anim state, what to draw

        // Load model
        Model model = LoadModel(modelFileName); // Load the bind-pose model mesh and basic data

        // Load animations
        uint animsCount = 0;
        int animFrameCounter = 0, animId = 0;
        ModelAnimation[] anims = LoadModelAnimations(modelFileName, ref animsCount); // Load skeletal animation data

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            if (animsCount != 0)
            {
                // Play animation when spacebar is held down (or step one frame with N)
                if (IsKeyDown(Key.Space) || IsKeyPressed(Key.N))
                {
                    animFrameCounter++;

                    if (animFrameCounter >= anims[animId].FrameCount)
                    {
                        animFrameCounter = 0;
                    }

                    UpdateModelAnimation(model, anims[animId], animFrameCounter);
                    animPlaying = true;
                }

                // Select animation by pressing A
                if (IsKeyPressed(Key.A))
                {
                    animFrameCounter = 0;
                    animId++;

                    if (animId >= animsCount)
                    {
                        animId = 0;
                    }

                    UpdateModelAnimation(model, anims[animId], 0);
                    animPlaying = true;
                }
            }

            // Toggle skeleton drawing
            if (IsKeyPressed(Key.S))
            {
                drawSkeleton ^= true;
            }

            // Toggle mesh drawing
            if (IsKeyPressed(Key.M))
            {
                drawMesh ^= true;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    // Draw 3d model with texture
                    if (drawMesh)
                    {
                        DrawModel(model, position, 1.0f, White);
                    }

                    // Draw the animated skeleton
                    if (drawSkeleton)
                    {
                        // Loop to (boneCount - 1) because the last one is a special "no bone" bone,
                        // needed to workaround buggy models
                        // without a -1, we would always draw a cube at the origin
                        for (int i = 0; i < model.BoneCount - 1; i++)
                        {
                            // By default the model is loaded in bind-pose by LoadModel().
                            // But if UpdateModelAnimation() has been called at least once
                            // then the model is already in animation pose, so we need the animated skeleton
                            if (!animPlaying || animsCount == 0)
                            {
                                // Display the bind-pose skeleton
                                DrawCube(model.BindPose[i].Translation, 0.04f, 0.04f, 0.04f, Red);

                                if (model.Bones[i].Parent >= 0)
                                {
                                    DrawLine3D(model.BindPose[i].Translation,
                                        model.BindPose[model.Bones[i].Parent].Translation, Red);
                                }
                            }
                            else
                            {
                                // Display the frame-pose skeleton
                                DrawCube(anims[animId].FramePoses[animFrameCounter][i].Translation, 0.05f, 0.05f, 0.05f, Red);

                                if (anims[animId].Bones[i].Parent >= 0)
                                {
                                    DrawLine3D(anims[animId].FramePoses[animFrameCounter][i].Translation,
                                        anims[animId].FramePoses[animFrameCounter][anims[animId].Bones[i].Parent].Translation, Red);
                                }
                            }
                        }
                    }

                    DrawGrid(10, 1.0f);         // Draw a grid

                }
                EndMode3D();

                DrawText("PRESS SPACE to PLAY MODEL ANIMATION", 10, GetScreenHeight() - 60, 10, Maroon);
                DrawText("PRESS A to CYCLE THROUGH ANIMATIONS", 10, GetScreenHeight() - 40, 10, DarkGray);
                DrawText("PRESS M to toggle MESH, S to toggle SKELETON DRAWING", 10, GetScreenHeight() - 20, 10, DarkGray);
                DrawText("(c) CesiumMan model by KhronosGroup", GetScreenWidth() - 210, GetScreenHeight() - 20, 10, Gray);

            }
            EndDrawing();
        }

        // De-Initialization

        // Unload model animations data
        // UnloadModelAnimations(anims, animsCount);

        UnloadModel(model);         // Unload model

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
