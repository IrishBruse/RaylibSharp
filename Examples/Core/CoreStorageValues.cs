/*******************************************************************************************
*
*   raylib [core] example - Storage save/load values
*
*   Example originally created with raylib 1.4, last time updated with raylib 4.2
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2015-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreStorageValues : ExampleHelper
{
    const float STORAGE_DATA_FILE = "storage.data";

    // NOTE: Storage positions must start with 0, directly related to file memory layout
    typedef enum {
        STORAGE_POSITION_SCORE      = 0,
        STORAGE_POSITION_HISCORE    = 1
    } StorageData;

    // Persistent storage functions
    static bool SaveStorageValue(uint position, int value);
    static int LoadStorageValue(uint position);

    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - storage save/load values");

        int score = 0;
        int hiscore = 0;
        int framesCounter = 0;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
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
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(RAYWHITE);

                DrawText(TextFormat("SCORE: %i", score), 280, 130, 40, MAROON);
                DrawText(TextFormat("HI-SCORE: %i", hiscore), 210, 200, 50, BLACK);

                DrawText(TextFormat("frames: %i", framesCounter), 10, 10, 20, LIME);

                DrawText("Press R to generate random numbers", 220, 40, 20, LIGHTGRAY);
                DrawText("Press ENTER to SAVE values", 250, 310, 20, LIGHTGRAY);
                DrawText("Press SPACE to LOAD values", 252, 350, 20, LIGHTGRAY);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }

    // Save integer value to storage file (to defined position)
    // NOTE: Storage positions is directly related to file memory layout (4 bytes each integer)
    bool SaveStorageValue(uint position, int value)
    {
        bool success = false;
        int dataSize = 0;
        uint newDataSize = 0;
        unsigned char *fileData = LoadFileData(STORAGE_DATA_FILE, &dataSize);
        unsigned char *newFileData = NULL;

        if (fileData != NULL)
        {
            if (dataSize <= (position*sizeof(int)))
            {
                // Increase data size up to position and store value
                newDataSize = (position + 1)*sizeof(int);
                newFileData = (unsigned char *)RL_REALLOC(fileData, newDataSize);

                if (newFileData != NULL)
                {
                    // RL_REALLOC succeded
                    int *dataPtr = (int *)newFileData;
                    dataPtr[position] = value;
                }
                else
                {
                    // RL_REALLOC failed
                    TraceLog(LOG_WARNING, "FILEIO: [%s] Failed to realloc data (%u), position in bytes (%u) bigger than actual file size", STORAGE_DATA_FILE, dataSize, position*sizeof(int));

                    // We store the old size of the file
                    newFileData = fileData;
                    newDataSize = dataSize;
                }
            }
            else
            {
                // Store the old size of the file
                newFileData = fileData;
                newDataSize = dataSize;

                // Replace value on selected position
                int *dataPtr = (int *)newFileData;
                dataPtr[position] = value;
            }

            success = SaveFileData(STORAGE_DATA_FILE, newFileData, newDataSize);
            RL_FREE(newFileData);

            TraceLog(TraceLogLevel.Info, "FILEIO: [%s] Saved storage value: %i", STORAGE_DATA_FILE, value);
        }
        else
        {
            TraceLog(TraceLogLevel.Info, "FILEIO: [%s] File created successfully", STORAGE_DATA_FILE);

            dataSize = (position + 1)*sizeof(int);
            fileData = (unsigned char *)RL_MALLOC(dataSize);
            int *dataPtr = (int *)fileData;
            dataPtr[position] = value;

            success = SaveFileData(STORAGE_DATA_FILE, fileData, dataSize);
            UnloadFileData(fileData);

            TraceLog(TraceLogLevel.Info, "FILEIO: [%s] Saved storage value: %i", STORAGE_DATA_FILE, value);
        }

        return success;
    }

    // Load integer value from storage file (from defined position)
    // NOTE: If requested position could not be found, value 0 is returned
    int LoadStorageValue(uint position)
    {
        int value = 0;
        int dataSize = 0;
        unsigned char *fileData = LoadFileData(STORAGE_DATA_FILE, &dataSize);

        if (fileData != NULL)
        {
            if (dataSize < ((int)(position*4))) TraceLog(LOG_WARNING, "FILEIO: [%s] Failed to find storage position: %i", STORAGE_DATA_FILE, position);
            else
            {
                int *dataPtr = (int *)fileData;
                value = dataPtr[position];
            }

            UnloadFileData(fileData);

            TraceLog(TraceLogLevel.Info, "FILEIO: [%s] Loaded storage value: %i", STORAGE_DATA_FILE, value);
        }

        return value;
    }
}

