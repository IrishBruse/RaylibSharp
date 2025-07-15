/*******************************************************************************************
*
*   raylib [core] example - 3d camera first person
*
*   Example originally created with raylib 1.3, last time updated with raylib 1.3
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2015-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class Core3dCameraFirstPerson : ExampleHelper
{
    const int MAX_COLUMNS = 20;

    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - 3d camera first person");

        // Define the camera to look into our 3d world (position, target, up vector)
        Camera camera = new();
        camera.Position = new(0.0f, 2.0f, 4.0f);    // Camera position
        camera.Target = new(0.0f, 2.0f, 0.0f);      // Camera looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 60.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        CameraMode cameraMode = CameraMode.FirstPerson;

        // Generates some random columns
        float[] heights = new float[MAX_COLUMNS];
        Vector3[] positions = new Vector3[MAX_COLUMNS];
        Color[] colors = new Color[MAX_COLUMNS];

        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            heights[i] = (float)GetRandomValue(1, 12);
            positions[i] = new((float)GetRandomValue(-15, 15), heights[i]/2.0f, (float)GetRandomValue(-15, 15));
            colors[i] = new(GetRandomValue(20, 255), GetRandomValue(10, 55), 30, 255);
        }

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            // Switch camera mode
            if (IsKeyPressed(Key.One))
            {
                cameraMode = CameraMode.Free;
                camera.Up = new(0.0f, 1.0f, 0.0f); // Reset roll
            }

            if (IsKeyPressed(Key.Two))
            {
                cameraMode = CameraMode.FirstPerson;
                camera.Up = new(0.0f, 1.0f, 0.0f); // Reset roll
            }

            if (IsKeyPressed(Key.Three))
            {
                cameraMode = CameraMode.ThirdPerson;
                camera.Up = new(0.0f, 1.0f, 0.0f); // Reset roll
            }

            if (IsKeyPressed(Key.Four))
            {
                cameraMode = CameraMode.Orbital;
                camera.Up = new(0.0f, 1.0f, 0.0f); // Reset roll
            }

            // Switch camera projection
            if (IsKeyPressed(Key.P))
            {
                if (camera.Projection == CameraProjection.Perspective)
                {
                    // Create isometric view
                    cameraMode = CameraMode.ThirdPerson;
                    // Note: The target distance is related to the render distance in the orthographic projection
                    camera.Position = new(0.0f, 2.0f, -100.0f);
                    camera.Target = new(0.0f, 2.0f, 0.0f);
                    camera.Up = new(0.0f, 1.0f, 0.0f);
                    camera.Projection = CameraProjection.Orthographic;
                    camera.Fovy = 20.0f; // near plane width in CameraProjection.Orthographic
                    CameraYaw(ref camera, -135 * DEG2RAD, true);
                    CameraPitch(ref camera, -45 * DEG2RAD, true, true, false);
                }
                else if (camera.Projection == CameraProjection.Orthographic)
                {
                    // Reset to default view
                    cameraMode = CameraMode.ThirdPerson;
                    camera.Position = new(0.0f, 2.0f, 10.0f);
                    camera.Target = new(0.0f, 2.0f, 0.0f);
                    camera.Up = new(0.0f, 1.0f, 0.0f);
                    camera.Projection = CameraProjection.Perspective;
                    camera.Fovy = 60.0f;
                }
            }

            // Update camera computes movement internally depending on the camera mode
            // Some default standard keyboard/mouse inputs are hardcoded to simplify use
            // For advance camera controls, it's reecommended to compute camera movement manually
            UpdateCamera(ref camera, cameraMode);                  // Update camera

    /*
            // Camera PRO usage example (EXPERIMENTAL)
            // This new camera function allows custom movement/rotation values to be directly provided
            // as input parameters, with this approach, rcamera module is internally independent of raylib inputs
            UpdateCameraPro(ref camera,
                {
                    (IsKeyDown(Key.W) || IsKeyDown(Key.Up))*0.1f -      // Move forward-backward
                    (IsKeyDown(Key.S) || IsKeyDown(Key.Down))*0.1f,
                    (IsKeyDown(Key.D) || IsKeyDown(Key.Right))*0.1f -   // Move right-left
                    (IsKeyDown(Key.A) || IsKeyDown(Key.Left))*0.1f,
                    0.0f                                                // Move up-down
                },
                {
                    GetMouseDelta().X*0.05f,                            // Rotation: yaw
                    GetMouseDelta().Y*0.05f,                            // Rotation: pitch
                    0.0f                                                // Rotation: roll
                },
                GetMouseWheelMove()*2.0f);                              // Move to target (zoom)
    */
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                BeginMode3D(camera);

                    DrawPlane(new(0.0f, 0.0f, 0.0f), new(32.0f, 32.0f), LIGHTGRAY); // Draw ground
                    DrawCube(new(-16.0f, 2.5f, 0.0f), 1.0f, 5.0f, 32.0f, BLUE);     // Draw a blue wall
                    DrawCube(new(16.0f, 2.5f, 0.0f), 1.0f, 5.0f, 32.0f, LIME);      // Draw a green wall
                    DrawCube(new(0.0f, 2.5f, 16.0f), 32.0f, 5.0f, 1.0f, GOLD);      // Draw a yellow wall

                    // Draw some cubes around
                    for (int i = 0; i < MAX_COLUMNS; i++)
                    {
                        DrawCube(positions[i], 2.0f, heights[i], 2.0f, colors[i]);
                        DrawCubeWires(positions[i], 2.0f, heights[i], 2.0f, MAROON);
                    }

                    // Draw player cube
                    if (cameraMode == CameraMode.ThirdPerson)
                    {
                        DrawCube(camera.Target, 0.5f, 0.5f, 0.5f, PURPLE);
                        DrawCubeWires(camera.Target, 0.5f, 0.5f, 0.5f, DARKPURPLE);
                    }

                EndMode3D();

                // Draw info boxes
                DrawRectangle(5, 5, 330, 100, Fade(SKYBLUE, 0.5f));
                DrawRectangleLines(5, 5, 330, 100, BLUE);

                DrawText("Camera controls:", 15, 15, 10, BLACK);
                DrawText("- Move keys: W, A, S, D, Space, Left-Ctrl", 15, 30, 10, BLACK);
                DrawText("- Look around: arrow keys or mouse", 15, 45, 10, BLACK);
                DrawText("- Camera mode keys: 1, 2, 3, 4", 15, 60, 10, BLACK);
                DrawText("- Zoom keys: num-plus, num-minus or mouse scroll", 15, 75, 10, BLACK);
                DrawText("- Camera projection key: P", 15, 90, 10, BLACK);

                DrawRectangle(600, 5, 195, 100, Fade(SKYBLUE, 0.5f));
                DrawRectangleLines(600, 5, 195, 100, BLUE);

                DrawText("Camera status:", 610, 15, 10, BLACK);
                DrawText(TextFormat("- Mode: %s", (cameraMode == CameraMode.Free) ? "FREE" :
                                                  (cameraMode == CameraMode.FirstPerson) ? "FIRST_PERSON" :
                                                  (cameraMode == CameraMode.ThirdPerson) ? "THIRD_PERSON" :
                                                  (cameraMode == CameraMode.Orbital) ? "ORBITAL" : "CUSTOM"), 610, 30, 10, BLACK);
                DrawText(TextFormat("- Projection: %s", (camera.Projection == CameraProjection.Perspective) ? "PERSPECTIVE" :
                                                        (camera.Projection == CameraProjection.Orthographic) ? "ORTHOGRAPHIC" : "CUSTOM"), 610, 45, 10, BLACK);
                DrawText(TextFormat("- Position: (%06.3f, %06.3f, %06.3f)", camera.Position.X, camera.Position.Y, camera.Position.Z), 610, 60, 10, BLACK);
                DrawText(TextFormat("- Target: (%06.3f, %06.3f, %06.3f)", camera.Target.X, camera.Target.Y, camera.Target.Z), 610, 75, 10, BLACK);
                DrawText(TextFormat("- Up: (%06.3f, %06.3f, %06.3f)", camera.Up.X, camera.Up.Y, camera.Up.Z), 610, 90, 10, BLACK);

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

