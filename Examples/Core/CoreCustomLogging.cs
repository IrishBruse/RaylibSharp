using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreCustomLogging : ExampleHelper
{
    // Custom logging function
    private static void CustomLog(TraceLogLevel msgType, string text)
    {
        Console.Write(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

        switch (msgType)
        {
            case TraceLogLevel.Info: Console.Write(" [INFO]: "); break;
            case TraceLogLevel.Error: Console.Write(" [ERROR]: "); break;
            case TraceLogLevel.Warning: Console.Write(" [WARN]: "); break;
            case TraceLogLevel.Debug: Console.Write(" [DEBUG]: "); break;
            default: break;
        }

        Console.WriteLine(text);
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        // Set custom logger
        SetTraceLogCallback(CustomLog);

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - custom logging");

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // TODO: Update your variables here

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawText("Check out the console output to see the custom logger in action!", 60, 200, 20, LightGray);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }
}
