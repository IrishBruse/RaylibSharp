using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreDropFiles : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - Core - drop files");

        string[] filePaths = Array.Empty<string>();

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsFileDropped())
            {
                FilePathList droppedFiles = LoadDroppedFiles();

                filePaths = droppedFiles.Paths;

                UnloadDroppedFiles(droppedFiles);    // Unload filepaths from memory
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                if (filePaths.Length == 0)
                {
                    DrawText("Drop your files to this window!", 100, 40, 20, DarkGray);
                }
                else
                {
                    DrawText("Dropped files:", 100, 40, 20, DarkGray);

                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            DrawRectangle(0, 85 + (40 * i), screenWidth, 40, Fade(LightGray, 0.5f));
                        }
                        else
                        {
                            DrawRectangle(0, 85 + (40 * i), screenWidth, 40, Fade(LightGray, 0.3f));
                        }

                        DrawText(filePaths[i], 120, 100 + (40 * i), 10, Gray);
                    }

                    DrawText("Drop new files...", 100, 110 + (40 * filePaths.Length), 20, DarkGray);
                }

            }
            EndDrawing();
        }

        CloseWindow();

        return 0;
    }
}
