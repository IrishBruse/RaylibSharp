/*******************************************************************************************
*
*   raylib [core] example - Picking in 3d mode
*
*   Example originally created with raylib 1.3, last time updated with raylib 4.0
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2015-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class Core3dPicking : ExampleHelper
{
    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - 3d picking");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = new(10.0f, 10.0f, 10.0f); // Camera position
        camera.Target = new(0.0f, 0.0f, 0.0f);      // Camera looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        Vector3 cubePosition = new(0.0f, 1.0f, 0.0f);
        Vector3 cubeSize = new(2.0f, 2.0f, 2.0f);

        Ray ray = new();                    // Picking line ray
        RayCollision collision = new();     // Ray collision hit info

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            if (IsCursorHidden()) UpdateCamera(ref camera, CameraMode.FirstPerson);

            // Toggle camera controls
            if (IsMouseButtonPressed(MouseButton.Right))
            {
                if (IsCursorHidden()) EnableCursor();
                else DisableCursor();
            }

            if (IsMouseButtonPressed(MouseButton.Left))
            {
                if (!collision.Hit)
                {
                    ray = GetScreenToWorldRay(GetMousePosition(), camera);

                    // Check collision between ray and box
                    collision = GetRayCollisionBox(ray,
                                new(new( cubePosition.X - cubeSize.X/2, cubePosition.Y - cubeSize.Y/2, cubePosition.Z - cubeSize.Z/2),
                                              new(cubePosition.X + cubeSize.X/2, cubePosition.Y + cubeSize.Y/2, cubePosition.Z + cubeSize.Z/2)));
                }
                else collision.Hit = false;
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                BeginMode3D(camera);

                    if (collision.Hit)
                    {
                        DrawCube(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, RED);
                        DrawCubeWires(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, MAROON);

                        DrawCubeWires(cubePosition, cubeSize.X + 0.2f, cubeSize.Y + 0.2f, cubeSize.Z + 0.2f, GREEN);
                    }
                    else
                    {
                        DrawCube(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, GRAY);
                        DrawCubeWires(cubePosition, cubeSize.X, cubeSize.Y, cubeSize.Z, DARKGRAY);
                    }

                    DrawRay(ray, MAROON);
                    DrawGrid(10, 1.0f);

                EndMode3D();

                DrawText("Try clicking on the box with your mouse!", 240, 10, 20, DARKGRAY);

                if (collision.Hit) DrawText("BOX SELECTED", (screenWidth - MeasureText("BOX SELECTED", 30)) / 2, (int)(screenHeight * 0.1f), 30, GREEN);

                DrawText("Right click mouse to toggle camera controls", 10, 430, 10, GRAY);

                DrawFPS(10, 10);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

