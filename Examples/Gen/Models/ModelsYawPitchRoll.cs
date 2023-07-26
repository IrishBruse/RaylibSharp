using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsYawPitchRoll : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        //SetConfigFlags(WindowFlag.Msaa4xHint | FLAG_WINDOW_HIGHDPI);
        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - plane rotations (yaw, pitch, roll)");

        Camera camera = new();
        camera.Position = (Vector3)new(0.0f,50.0f, -120.0f);// Camera position perspective
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);      // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 30.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera type

        Model model = LoadModel("resources/models/obj/plane.obj");                  // Load model
        Texture texture = LoadTexture("resources/models/obj/plane_diffuse.png");  // Load model texture
        model.Materials[0].Maps[MaterialMapIndex.Albedo].texture = texture;            // Set map diffuse texture

        float pitch = 0.0f;
        float roll = 0.0f;
        float yaw = 0.0f;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // Plane pitch (x-axis) controls
            if (IsKeyDown(Key.Down)) pitch += 0.6f;
            else if (IsKeyDown(Key.Up)) pitch -= 0.6f;
            else
            {
                if (pitch > 0.3f) pitch -= 0.3f;
                else if (pitch < -0.3f) pitch += 0.3f;
            }

            // Plane yaw (y-axis) controls
            if (IsKeyDown(Key.S)) yaw -= 1.0f;
            else if (IsKeyDown(Key.A)) yaw += 1.0f;
            else
            {
                if (yaw > 0.0f) yaw -= 0.5f;
                else if (yaw < 0.0f) yaw += 0.5f;
            }

            // Plane roll (z-axis) controls
            if (IsKeyDown(Key.Left)) roll -= 1.0f;
            else if (IsKeyDown(Key.Right)) roll += 1.0f;
            else
            {
                if (roll > 0.0f) roll -= 0.5f;
                else if (roll < 0.0f) roll += 0.5f;
            }

            // Tranformation matrix for rotations
            model.transform = MatrixRotateXYZ(new( DEG2RAD*pitch, DEG2RAD*yaw, DEG2RAD*roll ));

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                // Draw 3D model (recomended to draw 3D always before 2D)
                BeginMode3D(camera);{

                    DrawModel(model, (Vector3)new(0.0f,-8.0f, 0.0f), 1.0f, White);   // Draw 3d model with texture
                    DrawGrid(10, 10.0f);

                }EndMode3D();

                // Draw controls info
                DrawRectangle(30, 370, 260, 70, Fade(Green, 0.5f));
                DrawRectangleLines(30, 370, 260, 70, Fade(DarkGreen, 0.5f));
                DrawText("Pitch controlled with: Key.Up / Key.Down", 40, 380, 10, DarkGray);
                DrawText("Roll controlled with: Key.Left / Key.Right", 40, 400, 10, DarkGray);
                DrawText("Yaw controlled with: Key.A / Key.S", 40, 420, 10, DarkGray);

                DrawText("(c) WWI Plane Model created by GiaHanLam", screenWidth - 240, screenHeight - 20, 10, DarkGray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadModel(model);     // Unload model data

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
