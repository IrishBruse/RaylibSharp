using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsLoadingGltf : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - loading gltf");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(5.0f,5.0f, 5.0f);    // Camera position
        camera.Target = (Vector3)new(0.0f,2.0f, 0.0f);      // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        // Load gltf model
        Model model = LoadModel("resources/models/gltf/robot.glb");

        // Load gltf model animations
        uint animsCount = 0;
        uint animIndex = 0;
        uint animCurrentFrame = 0;
        ModelAnimation *modelAnimations = LoadModelAnimations("resources/models/gltf/robot.glb", &animsCount);

        Vector3 position = new( 0.0f, 0.0f, 0.0f );    // Set model position

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.ThirdPerson);
            // Select current animation
            if (IsKeyPressed(Key.Up)) animIndex = (animIndex + 1)%animsCount;
            else if (IsKeyPressed(Key.Down)) animIndex = (animIndex + animsCount - 1)%animsCount;

            // Update model animation
            ModelAnimation anim = modelAnimations[animIndex];
            animCurrentFrame = (animCurrentFrame + 1)%anim.frameCount;
            UpdateModelAnimation(model, anim, animCurrentFrame);

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                    DrawModel(model, position, 1.0f, White);    // Draw animated model
                    DrawGrid(10, 1.0f);

                }EndMode3D();

                DrawText("Use the UP/DOWN arrow keys to switch animation", 10, 10, 20, Gray);
                DrawText(TextFormat("Animation: %s", anim.name), 10, GetScreenHeight() - 20, 10, DarkGray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadModel(model);         // Unload model and meshes/material

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
