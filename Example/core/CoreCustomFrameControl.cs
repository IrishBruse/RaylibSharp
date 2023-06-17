using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Program main entry point
public static int CoreCustomFrameControl()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - custom frame control");

// Custom timming variables
double previousTime = GetTime();    // Previous time measure
double currentTime = 0.0;           // Current time measure
double updateDrawTime = 0.0;        // Update + Draw time
double waitTime = 0.0;              // Wait time (if target fps required)
float deltaTime = 0.0f;             // Frame time (Update + Draw + Wait time)

float timeCounter = 0.0f;           // Accumulative time counter (seconds)
float position = 0.0f;              // Circle position
bool pause = false;                 // Pause control flag

int targetFPS = 60;                 // Our initial target fps

// Main game loop
while (!WindowShouldClose())        // Detect window close button or ESC key
{
// Update
PollInputEvents();              // Poll input events (SUPPORT_CUSTOM_FRAME_CONTROL)

if (IsKeyPressed(Key.Space)) pause = !pause;

if (IsKeyPressed(Key.Up)) targetFPS += 20;
else if (IsKeyPressed(Key.Down)) targetFPS -= 20;

if (targetFPS < 0) targetFPS = 0;

if (!pause)
{
position += 200*deltaTime;  // We move at 200 pixels per second
if (position >= GetScreenWidth()) position = 0;
timeCounter += deltaTime;   // We count time (seconds)
}

// Draw
BeginDrawing();

ClearBackground(RayWhite);

for (int i = 0; i < GetScreenWidth()/200; i++) DrawRectangle(200*i, 0, 1, GetScreenHeight(), SkyBlue);

DrawCircle((int)position, GetScreenHeight()/2 - 25, 50, Red);

DrawText(TextFormat("%03.0f ms", timeCounter*1000.0f), (int)position - 40, GetScreenHeight()/2 - 100, 20, Maroon);
DrawText(TextFormat("PosX: %03.0f", position), (int)position - 50, GetScreenHeight()/2 + 40, 20, Black);

DrawText("Circle is moving at a constant 200 pixels/sec,\nindependently of the frame rate.", 10, 10, 20, DarkGray);
DrawText("PRESS SPACE to PAUSE MOVEMENT", 10, GetScreenHeight() - 60, 20, Gray);
DrawText("PRESS UP | DOWN to CHANGE TARGET FPS", 10, GetScreenHeight() - 30, 20, Gray);
DrawText(TextFormat("TARGET FPS: %i", targetFPS), GetScreenWidth() - 220, 10, 20, Lime);
DrawText(TextFormat("CURRENT FPS: %i", (int)(1.0f/deltaTime)), GetScreenWidth() - 220, 40, 20, Green);

EndDrawing();

// NOTE: In case raylib is configured to SUPPORT_CUSTOM_FRAME_CONTROL,
// Events polling, screen buffer swap and frame time control must be managed by the user

SwapScreenBuffer();         // Flip the back buffer to screen (front buffer)

currentTime = GetTime();
updateDrawTime = currentTime - previousTime;

if (targetFPS > 0)          // We want a fixed frame rate
{
waitTime = (1.0f/(float)targetFPS) - updateDrawTime;
if (waitTime > 0.0)
{
WaitTime((float)waitTime);
currentTime = GetTime();
deltaTime = (float)(currentTime - previousTime);
}
}
else deltaTime = (float)updateDrawTime;    // Framerate could be variable

previousTime = currentTime;
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
