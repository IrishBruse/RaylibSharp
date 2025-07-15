/*******************************************************************************************
*
*   raylib [core] example - 2d camera split screen
*
*   Addapted from the core_3d_camera_split_screen example:
*       https://github.com/raysan5/raylib/blob/master/examples/core/core_3d_camera_split_screen.c
*
*   Example originally created with raylib 4.5, last time updated with raylib 4.5
*
*   Example contributed by Gabriel dos Santos Sanches (@gabrielssanches) and reviewed by Ramon Santamaria (@raysan5)
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2023 Gabriel dos Santos Sanches (@gabrielssanches)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class Core2dCameraSplitScreen : ExampleHelper
{
    static readonly int PLAYER_SIZE = 40;

    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 440;

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - 2d camera split screen");

        Rectangle player1 = new(200, 200, PLAYER_SIZE, PLAYER_SIZE);
        Rectangle player2 = new(250, 200, PLAYER_SIZE, PLAYER_SIZE);

        Camera2D camera1 = new();
        camera1.Target = new(player1.X, player1.Y);
        camera1.Offset = new(200.0f, 200.0f);
        camera1.Rotation = 0.0f;
        camera1.Zoom = 1.0f;

        Camera2D camera2 = new();
        camera2.Target = new(player2.X, player2.Y);
        camera2.Offset = new(200.0f, 200.0f);
        camera2.Rotation = 0.0f;
        camera2.Zoom = 1.0f;

        RenderTexture screenCamera1 = LoadRenderTexture(screenWidth/2, screenHeight);
        RenderTexture screenCamera2 = LoadRenderTexture(screenWidth/2, screenHeight);

        // Build a flipped rectangle the size of the split view to use for drawing later
        Rectangle splitScreenRect = new(0.0f, 0.0f, (float)screenCamera1.Texture.Width, (float)-screenCamera1.Texture.Height);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            if (IsKeyDown(Key.S)) player1.Y += 3.0f;
            else if (IsKeyDown(Key.W)) player1.Y -= 3.0f;
            if (IsKeyDown(Key.D)) player1.X += 3.0f;
            else if (IsKeyDown(Key.A)) player1.X -= 3.0f;

            if (IsKeyDown(Key.Up)) player2.Y -= 3.0f;
            else if (IsKeyDown(Key.Down)) player2.Y += 3.0f;
            if (IsKeyDown(Key.Right)) player2.X += 3.0f;
            else if (IsKeyDown(Key.Left)) player2.X -= 3.0f;

            camera1.Target = new(player1.X, player1.Y);
            camera2.Target = new(player2.X, player2.Y);
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginTextureMode(screenCamera1);
                ClearBackground(RAYWHITE);

                BeginMode2D(camera1);

                    // Draw full scene with first camera
                    for (int i = 0; i < screenWidth/PLAYER_SIZE + 1; i++)
                    {
                        DrawLine(new((float)PLAYER_SIZE*i, 0), new( (float)PLAYER_SIZE*i, (float)screenHeight), LIGHTGRAY);
                    }

                    for (int i = 0; i < screenHeight/PLAYER_SIZE + 1; i++)
                    {
                        DrawLine(new(0, (float)PLAYER_SIZE*i), new( (float)screenWidth, (float)PLAYER_SIZE*i), LIGHTGRAY);
                    }

                    for (int i = 0; i < screenWidth/PLAYER_SIZE; i++)
                    {
                        for (int j = 0; j < screenHeight/PLAYER_SIZE; j++)
                        {
                            DrawText(TextFormat("[%i,%i]", i, j), 10 + PLAYER_SIZE*i, 15 + PLAYER_SIZE*j, 10, LIGHTGRAY);
                        }
                    }

                    DrawRectangle(player1, RED);
                    DrawRectangle(player2, BLUE);
                EndMode2D();

                DrawRectangle(0, 0, GetScreenWidth()/2, 30, Fade(RAYWHITE, 0.6f));
                DrawText("PLAYER1: W/S/A/D to move", 10, 10, 10, MAROON);

            EndTextureMode();

            BeginTextureMode(screenCamera2);
                ClearBackground(RAYWHITE);

                BeginMode2D(camera2);

                    // Draw full scene with second camera
                    for (int i = 0; i < screenWidth/PLAYER_SIZE + 1; i++)
                    {
                        DrawLine(new( (float)PLAYER_SIZE*i, 0), new( (float)PLAYER_SIZE*i, (float)screenHeight), LIGHTGRAY);
                    }

                    for (int i = 0; i < screenHeight/PLAYER_SIZE + 1; i++)
                    {
                        DrawLine(new(0, (float)PLAYER_SIZE*i), new( (float)screenWidth, (float)PLAYER_SIZE*i), LIGHTGRAY);
                    }

                    for (int i = 0; i < screenWidth/PLAYER_SIZE; i++)
                    {
                        for (int j = 0; j < screenHeight/PLAYER_SIZE; j++)
                        {
                            DrawText(TextFormat("[%i,%i]", i, j), 10 + PLAYER_SIZE*i, 15 + PLAYER_SIZE*j, 10, LIGHTGRAY);
                        }
                    }

                    DrawRectangleRec(player1, RED);
                    DrawRectangleRec(player2, BLUE);

                EndMode2D();

                DrawRectangle(0, 0, GetScreenWidth()/2, 30, Fade(RAYWHITE, 0.6f));
                DrawText("PLAYER2: UP/DOWN/LEFT/RIGHT to move", 10, 10, 10, DARKBLUE);

            EndTextureMode();

            // Draw both views render textures to the screen side by side
            BeginDrawing();
                ClearBackground(BLACK);

                DrawTexture(screenCamera1.Texture, splitScreenRect, new(0, 0), WHITE);
                DrawTexture(screenCamera2.Texture, splitScreenRect, new(screenWidth/2.0f, 0), WHITE);

                DrawRectangle(GetScreenWidth()/2 - 2, 0, 4, GetScreenHeight(), LIGHTGRAY);
            EndDrawing();
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        UnloadRenderTexture(screenCamera1); // Unload render texture
        UnloadRenderTexture(screenCamera2); // Unload render texture

        CloseWindow();                      // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

