using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Custom logging function
void CustomLog(int msgType, const char *text, va_list args)
{
char timeStr[64] = new();
time_t now = time(NULL);
struct tm *tm_info = localtime(&now);

strftime(timeStr, sizeof(timeStr), "%Y-%m-%d %H:%M:%S", tm_info);
printf("[%s] ", timeStr);

switch (msgType)
{
case LOG_INFO: printf("[INFO] : "); break;
case LOG_ERROR: printf("[ERROR]: "); break;
case LOG_WARNING: printf("[WARN] : "); break;
case LOG_DEBUG: printf("[DEBUG]: "); break;
default: break;
}

vprintf(text, args);
printf("\n");
}

// Program main entry point
public static int CoreCustomLogging()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

// Set custom logger
SetTraceLogCallback(CustomLog);

InitWindow(screenWidth, screenHeight, "raylib [core] example - custom logging");

SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
// Update
// TODO: Update your variables here

// Draw
BeginDrawing();

ClearBackground(RayWhite);

DrawText("Check out the console output to see the custom logger in action!", 60, 200, 20, LightGray);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
