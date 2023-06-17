using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

private static readonly string STORAGE_DATA_FILE = "storage.data";

// NOTE: Storage positions must start with 0, directly related to file memory layout
typedef enum {
STORAGE_POSITION_SCORE      = 0,
STORAGE_POSITION_HISCORE    = 1
} StorageData;

// Persistent storage functions
static bool SaveStorageValue(int position, int value);
static int LoadStorageValue(int position);

// Program main entry point
public static int CoreStorageValues()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - storage save/load values");

int score = 0;
int hiscore = 0;
int framesCounter = 0;

SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

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

ClearBackground(RayWhite);

DrawText(TextFormat("SCORE: %i", score), 280, 130, 40, Maroon);
DrawText(TextFormat("HI-SCORE: %i", hiscore), 210, 200, 50, Black);

DrawText(TextFormat("frames: %i", framesCounter), 10, 10, 20, Lime);

DrawText("Press R to generate random numbers", 220, 40, 20, LightGray);
DrawText("Press ENTER to SAVE values", 250, 310, 20, LightGray);
DrawText("Press SPACE to LOAD values", 252, 350, 20, LightGray);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}

// Save integer value to storage file (to defined position)
// NOTE: Storage positions is directly related to file memory layout (4 bytes each integer)
bool SaveStorageValue(int position, int value)
{
bool success = false;
int dataSize = 0;
int newDataSize = 0;
char *fileData = LoadFileData(STORAGE_DATA_FILE, &dataSize);
char *newFileData = NULL;

if (fileData != NULL)
{
if (dataSize <= (position*sizeof(int)))
{
// Increase data size up to position and store value
newDataSize = (position + 1)*sizeof(int);
newFileData = (char *)RL_REALLOC(fileData, newDataSize);

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

TraceLog(LOG_INFO, "FILEIO: [%s] Saved storage value: %i", STORAGE_DATA_FILE, value);
}
else
{
TraceLog(LOG_INFO, "FILEIO: [%s] File created successfully", STORAGE_DATA_FILE);

dataSize = (position + 1)*sizeof(int);
fileData = (char *)RL_MALLOC(dataSize);
int *dataPtr = (int *)fileData;
dataPtr[position] = value;

success = SaveFileData(STORAGE_DATA_FILE, fileData, dataSize);
UnloadFileData(fileData);

TraceLog(LOG_INFO, "FILEIO: [%s] Saved storage value: %i", STORAGE_DATA_FILE, value);
}

return success;
}

// Load integer value from storage file (from defined position)
// NOTE: If requested position could not be found, value 0 is returned
int LoadStorageValue(int position)
{
int value = 0;
int dataSize = 0;
char *fileData = LoadFileData(STORAGE_DATA_FILE, &dataSize);

if (fileData != NULL)
{
if (dataSize < (position*4)) TraceLog(LOG_WARNING, "FILEIO: [%s] Failed to find storage position: %i", STORAGE_DATA_FILE, position);
else
{
int *dataPtr = (int *)fileData;
value = dataPtr[position];
}

UnloadFileData(fileData);

TraceLog(LOG_INFO, "FILEIO: [%s] Loaded storage value: %i", STORAGE_DATA_FILE, value);
}

return value;
}
}
