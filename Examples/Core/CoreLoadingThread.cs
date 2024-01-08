using System.Threading;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreLoadingThread : ExampleHelper
{
    enum State
    {
        Waiting,
        Loading,
        Finished
    }

    static int done;
    static int progress;
    static Thread? thread;

    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        State state = State.Waiting;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - loading thread");

        int framesCounter = 0;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            switch (state)
            {
                case State.Waiting:
                {
                    if (IsKeyPressed(Key.Enter))
                    {
                        try
                        {
                            thread = new(LoadDataThread);
                            thread.Start();
                            TraceLog(TraceLogLevel.Info, "Loading thread initialized successfully");
                        }
                        catch (System.Exception)
                        {
                            TraceLog(TraceLogLevel.Error, "Error creating loading thread");
                        }

                        state = State.Loading;
                    }
                }
                break;
                case State.Loading:
                {
                    framesCounter++;
                    if (done == 1)
                    {
                        framesCounter = 0;
                        try
                        {
                            thread?.Join();
                            TraceLog(TraceLogLevel.Info, "Loading thread terminated successfully");
                        }
                        catch (System.Exception)
                        {
                            TraceLog(TraceLogLevel.Error, "Error joining loading thread");
                        }

                        state = State.Finished;
                    }
                }
                break;
                case State.Finished:
                {
                    if (IsKeyPressed(Key.Enter))
                    {
                        // Reset everything to launch again
                        progress = 0;
                        done = 0;
                        state = State.Waiting;
                    }
                }
                break;
                default: break;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                switch (state)
                {
                    case State.Waiting: DrawText("PRESS ENTER to START LOADING DATA", 150, 170, 20, DarkGray); break;
                    case State.Loading:
                    {
                        DrawRectangle(150, 200, progress * 5, 60, SkyBlue);
                        if (framesCounter / 15 % 2 == 0)
                        {
                            DrawText("LOADING DATA...", 240, 210, 40, DarkBlue);
                        }
                    }
                    break;
                    case State.Finished:
                    {
                        DrawRectangle(150, 200, 500, 60, Lime);
                        DrawText("DATA LOADED!", 250, 210, 40, Green);

                    }
                    break;
                    default: break;
                }

                DrawRectangleLines(150, 200, 500, 60, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }

    // Loading data thread function definition
    static void LoadDataThread()
    {
        // We simulate data loading with a time counter for 5 seconds
        for (int i = 0; i < 5000; i++)
        {
            Thread.Sleep(1);
            Interlocked.Exchange(ref progress, i / 50);
        }

        Interlocked.Exchange(ref done, 1);
    }

}
