using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class Example
{
    private static readonly int MAX_FILEPATH_RECORDED = 4096;
    private static readonly int MAX_FILEPATH_SIZE = 2048;

    // Program main entry point
    public static int CoreDropFiles()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [core] example - drop files");

        int filePathCounter = 0;
        string[] filePaths = new string[MAX_FILEPATH_RECORDED]; // We will register a maximum of filepaths

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsFileDropped())
            {
                FilePathList droppedFiles = LoadDroppedFiles();

                for (int i = 0, offset = filePathCounter; i < droppedFiles.Count; i++)
                {
                    if (filePathCounter < (MAX_FILEPATH_RECORDED - 1))
                    {
                        filePaths[offset + i] = droppedFiles.Paths[i];
                        filePathCounter++;
                    }
                }

                UnloadDroppedFiles(droppedFiles);    // Unload filepaths from memory
            }

            // Draw
            BeginDrawing();

            ClearBackground(RayWhite);

            if (filePathCounter == 0)
            {
                DrawText("Drop your files to this window!", 100, 40, 20, DarkGray);
            }
            else
            {
                DrawText("Dropped files:", 100, 40, 20, DarkGray);

                for (int i = 0; i < filePathCounter; i++)
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

                DrawText("Drop new files...", 100, 110 + (40 * filePathCounter), 20, DarkGray);
            }

            EndDrawing();
        }

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
