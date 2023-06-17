using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

//#define PLATFORM_WEB

#if defined(PLATFORM_WEB)
#endif

// Global Variables Definition
const int screenWidth = 800;
const int screenHeight = 450;

// Module functions declaration
void UpdateDrawFrame(void);     // Update and Draw one frame

// Program main entry point
public static int CoreBasicWindowWeb()
{
// Initialization
InitWindow(screenWidth, screenHeight, "raylib [core] example - basic window");

#if defined(PLATFORM_WEB)
emscripten_set_main_loop(UpdateDrawFrame, 0, 1);
#else
SetTargetFPS(60);   // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
UpdateDrawFrame();
}
#endif

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}

// Module Functions Definition
void UpdateDrawFrame(void)
{
// Update
// TODO: Update your variables here

// Draw
BeginDrawing();

ClearBackground(RayWhite);

DrawText("Congrats! You created your first window!", 190, 200, 20, LightGray);

EndDrawing();
}
}
