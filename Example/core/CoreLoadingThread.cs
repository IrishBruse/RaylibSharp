using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// WARNING: This example does not build on Windows with MSVC compiler

// Using C11 atomics for synchronization
// NOTE: A plain bool (or any plain data type for that matter) can't be used for inter-thread synchronization
static atomic_bool dataLoaded = false; // Data Loaded completion indicator
static void *LoadDataThread(void *arg);     // Loading data thread function declaration

static atomic_int dataProgress = 0;                // Data progress accumulator

// Program main entry point
public static int CoreLoadingThread()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - loading thread");

pthread_t threadId;             // Loading data thread id

enum { STATE_WAITING, STATE_LOADING, STATE_FINISHED } state = STATE_WAITING;
int framesCounter = 0;

SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
// Update
switch (state)
{
case STATE_WAITING:
{
if (IsKeyPressed(Key.Enter))
{
int error = pthread_create(&threadId, NULL, &LoadDataThread, NULL);
if (error != 0) TraceLog(LOG_ERROR, "Error creating loading thread");
else TraceLog(LOG_INFO, "Loading thread initialized successfully");

state = STATE_LOADING;
}
} break;
case STATE_LOADING:
{
framesCounter++;
if (atomic_load_explicit(&dataLoaded, memory_order_relaxed))
{
framesCounter = 0;
int error = pthread_join(threadId, NULL);
if (error != 0) TraceLog(LOG_ERROR, "Error joining loading thread");
else TraceLog(LOG_INFO, "Loading thread terminated successfully");

state = STATE_FINISHED;
}
} break;
case STATE_FINISHED:
{
if (IsKeyPressed(Key.Enter))
{
// Reset everything to launch again
atomic_store_explicit(&dataLoaded, false, memory_order_relaxed);
atomic_store_explicit(&dataProgress, 0, memory_order_relaxed);
state = STATE_WAITING;
}
} break;
default: break;
}

// Draw
BeginDrawing();

ClearBackground(RayWhite);

switch (state)
{
case STATE_WAITING: DrawText("PRESS ENTER to START LOADING DATA", 150, 170, 20, DarkGray); break;
case STATE_LOADING:
{
DrawRectangle(150, 200, atomic_load_explicit(&dataProgress, memory_order_relaxed), 60, SkyBlue);
if ((framesCounter/15)%2) DrawText("LOADING DATA...", 240, 210, 40, DarkBlue);

} break;
case STATE_FINISHED:
{
DrawRectangle(150, 200, 500, 60, Lime);
DrawText("DATA LOADED!", 250, 210, 40, Green);

} break;
default: break;
}

DrawRectangleLines(150, 200, 500, 60, DarkGray);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}

// Loading data thread function definition
static void *LoadDataThread(void *arg)
{
int timeCounter = 0;            // Time counted in ms
clock_t prevTime = clock();     // Previous time

// We simulate data loading with a time counter for 5 seconds
while (timeCounter < 5000)
{
clock_t currentTime = clock() - prevTime;
timeCounter = currentTime*1000/CLOCKS_PER_SEC;

// We accumulate time over a global variable to be used in
// main thread as a progress bar
atomic_store_explicit(&dataProgress, timeCounter/10, memory_order_relaxed);
}

// When data has finished loading, we set global variable
atomic_store_explicit(&dataLoaded, true, memory_order_relaxed);

return NULL;
}
}
