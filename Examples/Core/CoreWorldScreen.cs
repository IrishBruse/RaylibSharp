/*******************************************************************************************
*
*   raylib [core] example - World to screen
*
*   Example originally created with raylib 1.3, last time updated with raylib 1.4
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2015-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreWorldScreen : ExampleHelper
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - core world screen");

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = new(10.0f, 10.0f, 10.0f); // Camera position
        camera.Target = new(0.0f, 0.0f, 0.0f);      // Camera looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);
        Vector2 cubeScreenPosition = new(0.0f, 0.0f);

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            UpdateCamera(ref camera, CameraMode.ThirdPerson);

            // Calculate cube screen space position (with a little offset to be in top)
            cubeScreenPosition = GetWorldToScreen(new(cubePosition.X, cubePosition.Y + 2.5f, cubePosition.Z), camera);
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                BeginMode3D(camera);

                    DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, RED);
                    DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, MAROON);

                    DrawGrid(10, 1.0f);

                EndMode3D();

                DrawText("Enemy: 100 / 100", (int)cubeScreenPosition.X - MeasureText("Enemy: 100/100", 20)/2, (int)cubeScreenPosition.Y, 20, BLACK);

                DrawText(TextFormat("Cube position in screen space coordinates: [%i, %i]", (int)cubeScreenPosition.X, (int)cubeScreenPosition.Y), 10, 10, 20, LIME);
                DrawText("Text 2d should be always on top of the cube", 10, 40, 20, GRAY);

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

