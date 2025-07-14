/*******************************************************************************************
*
*   raylib [core] example - loading thread
*
*   NOTE: This example requires linking with pthreads library on MinGW,
*   it can be accomplished passing -static parameter to compiler
*
*   Example originally created with raylib 2.5, last time updated with raylib 3.0
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*
*   Copyright (c) 2014-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using System.Numerics;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using Camera = RaylibSharp.Camera3D;
using RenderTexture2D = RaylibSharp.RenderTexture;

using static RaylibSharp.Raylib;
using System.Threading.Tasks;
using System.Threading;

public class LoadingThreadExample
{
    // C# equivalent of C11 atomics using Interlocked for thread-safe operations
    private static int _dataLoaded = 0; // 0 for false, 1 for true
    private static int _dataProgress = 0;

    // Enum for game states
    private enum GameState
    {
        Waiting,
        Loading,
        Finished
    }

    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static void Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - loading thread (C#)");

        GameState state = GameState.Waiting;
        int framesCounter = 0;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            //----------------------------------------------------------------------------------
            switch (state)
            {
                case GameState.Waiting:
                {
                    if (IsKeyPressed(Key.Enter))
                    {
                        // Start the loading task in a new thread
                        Task.Run(() => LoadDataThread());
                        TraceLog(TraceLogLevel.Info, "Loading thread initialized successfully");
                        state = GameState.Loading;
                    }
                } break;
                case GameState.Loading:
                {
                    framesCounter++;
                    // Check if dataLoaded is true
                    if (Interlocked.CompareExchange(ref _dataLoaded, 0, 1) == 1) // Atomically checks if _dataLoaded is 1 and if so sets it to 0
                    {
                        framesCounter = 0;
                        TraceLog(TraceLogLevel.Info, "Loading thread terminated successfully");
                        state = GameState.Finished;
                    }
                } break;
                case GameState.Finished:
                {
                    if (IsKeyPressed(Key.Enter))
                    {
                        // Reset everything to launch again
                        Interlocked.Exchange(ref _dataLoaded, 0); // Atomically sets _dataLoaded to 0
                        Interlocked.Exchange(ref _dataProgress, 0); // Atomically sets _dataProgress to 0
                        state = GameState.Waiting;
                    }
                } break;
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RayWhite);

                switch (state)
                {
                    case GameState.Waiting: DrawText("PRESS ENTER to START LOADING DATA", 150, 170, 20, DarkGray); break;
                    case GameState.Loading:
                    {
                        DrawRectangle(150, 200, Interlocked.CompareExchange(ref _dataProgress, 0, 0), 60, SkyBlue);
                        if ((framesCounter / 15) % 2 == 0)
                        {
                            DrawText("LOADING DATA...", 240, 210, 40, DarkBlue);
                        }
                    } break;
                    case GameState.Finished:
                    {
                        DrawRectangle(150, 200, 500, 60, Lime);
                        DrawText("DATA LOADED!", 250, 210, 40, Green);
                    } break;
                    default: break;
                }

                DrawRectangleLines(150, 200, 500, 60, DarkGray);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------
    }

    // Loading data thread function definition
    private static void LoadDataThread()
    {
        long timeCounter = 0; // Time counted in ms
        long prevTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(); // Previous time in milliseconds

        // We simulate data loading for 5 seconds
        while (timeCounter < 5000)
        {
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            timeCounter = currentTime - prevTime;

            // We accumulate time over a global variable to be used in
            // main thread as a progress bar
            Interlocked.Exchange(ref _dataProgress, (int)(timeCounter / 10)); // Atomically sets _dataProgress
            Thread.Sleep(1); // Simulate some work and yield CPU
        }

        // When data has finished loading, we set global variable
        Interlocked.Exchange(ref _dataLoaded, 1); // Atomically sets _dataLoaded to 1
    }
}
