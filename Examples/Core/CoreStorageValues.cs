using System;
using System.IO;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreStorageValues : ExampleHelper
{
    private const int STORAGE_POSITION_SCORE = 0;
    private const int STORAGE_POSITION_HISCORE = 1;
    private const string STORAGE_DATA_FILE = "storage.data";

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - storage save/load values");

        int score = 0;
        int hiscore = 0;
        int framesCounter = 0;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.R))
            {
                score = GetRandomValue(1000, 2000);
                hiscore = GetRandomValue(2000, 4000);
            }

            if (IsKeyPressed(Key.Enter))
            {
                SaveStorageValue(STORAGE_POSITION_SCORE, score);
                SaveStorageValue(STORAGE_POSITION_HISCORE, hiscore);
            }
            else if (IsKeyPressed(Key.Space))
            {
                // NOTE: If requested position could not be found, value 0 is returned
                score = LoadStorageValue(STORAGE_POSITION_SCORE);
                hiscore = LoadStorageValue(STORAGE_POSITION_HISCORE);
            }

            framesCounter++;

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawText("SCORE: " + score, 280, 130, 40, Maroon);
                DrawText("HI-SCORE: " + hiscore, 210, 200, 50, Black);

                DrawText("frames: " + framesCounter, 10, 10, 20, Lime);

                DrawText("Press R to generate random numbers", 220, 40, 20, LightGray);
                DrawText("Press ENTER to SAVE values", 250, 310, 20, LightGray);
                DrawText("Press SPACE to LOAD values", 252, 350, 20, LightGray);
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }

    // Save integer value to storage file (to defined position)
    // NOTE: Storage positions is directly related to file memory layout (4 bytes each integer)
    private static bool SaveStorageValue(int position, int value)
    {
        try
        {
            using BinaryWriter file = new(File.Open(STORAGE_DATA_FILE, FileMode.OpenOrCreate));
            file.BaseStream.Seek(position * sizeof(int), SeekOrigin.Begin);
            file.Write(value);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Load integer value from storage file (from defined position)
    // NOTE: If requested position could not be found, value 0 is returned
    private static int LoadStorageValue(int position)
    {
        if (!File.Exists(STORAGE_DATA_FILE))
        {
            return 0;
        }
        using BinaryReader file = new(File.Open(STORAGE_DATA_FILE, FileMode.OpenOrCreate));
        file.BaseStream.Seek(position * sizeof(int), SeekOrigin.Begin);
        return file.ReadInt32();
    }
}
