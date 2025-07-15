/*******************************************************************************************
*
*   raylib [core] example - 3d cmaera split screen
*
*   Example originally created with raylib 3.7, last time updated with raylib 4.0
*
*   Example contributed by Jeffery Myers (@JeffM2501) and reviewed by Ramon Santamaria (@raysan5)
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2021-2024 Jeffery Myers (@JeffM2501)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class Core3dCameraSplitScreen : ExampleHelper
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

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - 3d camera split screen");

        // Setup player 1 camera and screen
        Camera cameraPlayer1 = new();
        cameraPlayer1.Fovy = 45.0f;
        cameraPlayer1.Up.Y = 1.0f;
        cameraPlayer1.Target.Y = 1.0f;
        cameraPlayer1.Position.Z = -3.0f;
        cameraPlayer1.Position.Y = 1.0f;

        RenderTexture screenPlayer1 = LoadRenderTexture(screenWidth/2, screenHeight);

        // Setup player two camera and screen
        Camera cameraPlayer2 = new();
        cameraPlayer2.Fovy = 45.0f;
        cameraPlayer2.Up.Y = 1.0f;
        cameraPlayer2.Target.Y = 3.0f;
        cameraPlayer2.Position.X = -3.0f;
        cameraPlayer2.Position.Y = 3.0f;

        RenderTexture screenPlayer2 = LoadRenderTexture(screenWidth / 2, screenHeight);

        // Build a flipped rectangle the size of the split view to use for drawing later
        Rectangle splitScreenRect = new(0.0f, 0.0f, (float)screenPlayer1.Texture.Width, (float)-screenPlayer1.Texture.Height);

        // Grid data
        int count = 5;
        float spacing = 4;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            // If anyone moves this frame, how far will they move based on the time since the last frame
            // this moves thigns at 10 world units per second, regardless of the actual FPS
            float offsetThisFrame = 10.0f*GetFrameTime();

            // Move Player1 forward and backwards (no turning)
            if (IsKeyDown(Key.W))
            {
                cameraPlayer1.Position.Z += offsetThisFrame;
                cameraPlayer1.Target.Z += offsetThisFrame;
            }
            else if (IsKeyDown(Key.S))
            {
                cameraPlayer1.Position.Z -= offsetThisFrame;
                cameraPlayer1.Target.Z -= offsetThisFrame;
            }

            // Move Player2 forward and backwards (no turning)
            if (IsKeyDown(Key.Up))
            {
                cameraPlayer2.Position.X += offsetThisFrame;
                cameraPlayer2.Target.X += offsetThisFrame;
            }
            else if (IsKeyDown(Key.Down))
            {
                cameraPlayer2.Position.X -= offsetThisFrame;
                cameraPlayer2.Target.X -= offsetThisFrame;
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            // Draw Player1 view to the render texture
            BeginTextureMode(screenPlayer1);
                ClearBackground(SKYBLUE);

                BeginMode3D(cameraPlayer1);

                    // Draw scene: grid of cube trees on a plane to make a "world"
                    DrawPlane(new(0, 0, 0), new(50, 50), BEIGE); // Simple world plane

                    for (float x = -count*spacing; x <= count*spacing; x += spacing)
                    {
                        for (float z = -count*spacing; z <= count*spacing; z += spacing)
                        {
                            DrawCube( new(x, 1.5f, z), 1, 1, 1, LIME);
                            DrawCube( new(x, 0.5f, z), 0.25f, 1, 0.25f, BROWN);
                        }
                    }

                    // Draw a cube at each player's position
                    DrawCube(cameraPlayer1.Position, 1, 1, 1, RED);
                    DrawCube(cameraPlayer2.Position, 1, 1, 1, BLUE);

                EndMode3D();

                DrawRectangle(0, 0, GetScreenWidth()/2, 40, Fade(RAYWHITE, 0.8f));
                DrawText("PLAYER1: W/S to move", 10, 10, 20, MAROON);

            EndTextureMode();

            // Draw Player2 view to the render texture
            BeginTextureMode(screenPlayer2);
                ClearBackground(SKYBLUE);

                BeginMode3D(cameraPlayer2);

                    // Draw scene: grid of cube trees on a plane to make a "world"
                    DrawPlane(new(0, 0, 0), new(50, 50), BEIGE); // Simple world plane

                    for (float x = -count*spacing; x <= count*spacing; x += spacing)
                    {
                        for (float z = -count*spacing; z <= count*spacing; z += spacing)
                        {
                            DrawCube( new(x, 1.5f, z), 1, 1, 1, LIME);
                            DrawCube( new(x, 0.5f, z), 0.25f, 1, 0.25f, BROWN);
                        }
                    }

                    // Draw a cube at each player's position
                    DrawCube(cameraPlayer1.Position, 1, 1, 1, RED);
                    DrawCube(cameraPlayer2.Position, 1, 1, 1, BLUE);

                EndMode3D();

                DrawRectangle(0, 0, GetScreenWidth()/2, 40, Fade(RAYWHITE, 0.8f));
                DrawText("PLAYER2: UP/DOWN to move", 10, 10, 20, DARKBLUE);

            EndTextureMode();

            // Draw both views render textures to the screen side by side
            BeginDrawing();
                ClearBackground(BLACK);

                DrawTextureRec(screenPlayer1.Texture, splitScreenRect, new(0, 0), WHITE);
                DrawTextureRec(screenPlayer2.Texture, splitScreenRect, new(screenWidth/2.0f, 0), WHITE);

                DrawRectangle(GetScreenWidth()/2 - 2, 0, 4, GetScreenHeight(), LIGHTGRAY);
            EndDrawing();
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        UnloadRenderTexture(screenPlayer1); // Unload render texture
        UnloadRenderTexture(screenPlayer2); // Unload render texture

        CloseWindow();                      // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

