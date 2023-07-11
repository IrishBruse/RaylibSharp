using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

using Camera = RaylibSharp.Camera3D;

#pragma warning disable CS0618

public static class Core3dCameraFirstPerson
{
    private static readonly int MAX_COLUMNS = 20;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - Core - 3d camera first person");

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
            heights[i] = GetRandomValue(1, 12);
            positions[i] = new(GetRandomValue(-15, 15), heights[i] / 2.0f, GetRandomValue(-15, 15));
            colors[i] = Color.FromArgb(255, GetRandomValue(20, 255), GetRandomValue(10, 55), 30);
        }

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
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
                    camera.Yaw(-135 * DEG2RAD, true);
                    camera.Pitch(-45 * DEG2RAD, true, true, false);
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
            camera.Update(cameraMode);

            // Camera PRO usage example (EXPERIMENTAL)
            // This new camera function allows custom movement/rotation values to be directly provided
            // as input parameters, with this approach, rcamera module is internally independent of raylib inputs

            // Vector3 input = new(0);
            // input.X = ((IsKeyDown(Key.W) || IsKeyDown(Key.Up)) ? .1f : 0) - ((IsKeyDown(Key.S) || IsKeyDown(Key.Down)) ? .1f : 0);
            // input.Y = ((IsKeyDown(Key.D) || IsKeyDown(Key.Right)) ? .1f : 0) - ((IsKeyDown(Key.A) || IsKeyDown(Key.Left)) ? .1f : 0);
            // UpdateCameraPro(ref camera, input, new(GetMouseDelta() * .05f, 0), GetMouseWheelMove() * 2.0f);

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {
                    DrawPlane(new(0.0f, 0.0f, 0.0f), new(32.0f, 32.0f), LightGray); // Draw ground
                    DrawCube(new(-16.0f, 2.5f, 0.0f), 1.0f, 5.0f, 32.0f, Blue);     // Draw a blue wall
                    DrawCube(new(16.0f, 2.5f, 0.0f), 1.0f, 5.0f, 32.0f, Lime);      // Draw a green wall
                    DrawCube(new(0.0f, 2.5f, 16.0f), 32.0f, 5.0f, 1.0f, Gold);      // Draw a yellow wall

                    // Draw some cubes around
                    for (int i = 0; i < MAX_COLUMNS; i++)
                    {
                        DrawCube(positions[i], 2.0f, heights[i], 2.0f, colors[i]);
                        DrawCubeWires(positions[i], 2.0f, heights[i], 2.0f, Maroon);
                    }

                    // Draw player cube
                    if (cameraMode == CameraMode.ThirdPerson)
                    {
                        DrawCube(camera.Target, 0.5f, 0.5f, 0.5f, Purple);
                        DrawCubeWires(camera.Target, 0.5f, 0.5f, 0.5f, DarkPurple);
                    }
                }
                EndMode3D();

                // Draw info boxes
                DrawRectangle(5, 5, 330, 100, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(5, 5, 330, 100, Blue);

                DrawText("Camera controls:", 15, 15, 10, Black);
                DrawText("- Move keys: W, A, S, D, Space, Left-Ctrl", 15, 30, 10, Black);
                DrawText("- Look around: arrow keys or mouse", 15, 45, 10, Black);
                DrawText("- Camera mode keys: 1, 2, 3, 4", 15, 60, 10, Black);
                DrawText("- Zoom keys: num-plus, num-minus or mouse scroll", 15, 75, 10, Black);
                DrawText("- Camera projection key: P", 15, 90, 10, Black);

                DrawRectangle(600, 5, 195, 100, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(600, 5, 195, 100, Blue);

                DrawText("Camera status:", 610, 15, 10, Black);
                {
                    string mode = cameraMode switch
                    {
                        CameraMode.Free => "Free",
                        CameraMode.FirstPerson => "First Person",
                        CameraMode.ThirdPerson => "Third Person",
                        CameraMode.Orbital => "Orbital",
                        _ => "Unknown"
                    };

                    string projection = camera.Projection switch
                    {
                        CameraProjection.Perspective => "Perspective",
                        CameraProjection.Orthographic => "Orthographic",
                        _ => "Unknown"
                    };

                    // Better c# way
                    // DrawText("- Mode: " + mode, 610, 30, 10, Black);
                    // DrawText("- Projection: " + projection, 610, 45, 10, Black);
                    // DrawText("- Position: (" + camera.Position.ToString("0.000") + ")", 610, 60, 10, Black);
                    // DrawText("- Target: (" + camera.Target.ToString("0.000") + ")", 610, 75, 10, Black);
                    // DrawText("- Up: (" + camera.Up.ToString("0.000") + ")", 610, 90, 10, Black);

                    // Faithful to raylib example source
                    DrawText(TextFormat("- Mode: %s", mode), 610, 30, 10, Black);
                    DrawText(TextFormat("- Projection: %s", projection), 610, 45, 10, Black);
                    DrawText(TextFormat("- Position: (%06.3f, %06.3f, %06.3f)", camera.Position.X, camera.Position.Y, camera.Position.Z), 610, 60, 10, Black);
                    DrawText(TextFormat("- Target: (%06.3f, %06.3f, %06.3f)", camera.Target.X, camera.Target.Y, camera.Target.Z), 610, 75, 10, Black);
                    DrawText(TextFormat("- Up: (%06.3f, %06.3f, %06.3f)", camera.Up.X, camera.Up.Y, camera.Up.Z), 610, 90, 10, Black);
                }
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}

#pragma warning restore CS0618
