using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Program main entry point
public static int CoreWindowFlags()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

// Possible window flags
/*
FLAG_VSYNC_HINT
FLAG_FULLSCREEN_MODE    -> not working properly -> wrong scaling!
FLAG_WINDOW_RESIZABLE
FLAG_WINDOW_UNDECORATED
FLAG_WINDOW_TRANSPARENT
FLAG_WINDOW_HIDDEN
FLAG_WINDOW_MINIMIZED   -> Not supported on window creation
FLAG_WINDOW_MAXIMIZED   -> Not supported on window creation
FLAG_WINDOW_UNFOCUSED
FLAG_WINDOW_TOPMOST
FLAG_WINDOW_HIGHDPI     -> errors after minimize-resize, fb size is recalculated
FLAG_WINDOW_ALWAYS_RUN
FLAG_MSAA_4X_HINT
*/

// Set configuration flags for window creation
//SetConfigFlags(FLAG_VSYNC_HINT | FLAG_MSAA_4X_HINT | FLAG_WINDOW_HIGHDPI);
InitWindow(screenWidth, screenHeight, "raylib [core] example - window flags");

Vector2 ballPosition = new( GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f );
Vector2 ballSpeed = new( 5.0f, 4.0f );
float ballRadius = 20;

int framesCounter = 0;

//SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
// Update
if (IsKeyPressed(Key.F)) togglefullscreen();  // modifies window size when scaling!

if (IsKeyPressed(Key.R))
{
if (IsWindowState(FLAG_WINDOW_RESIZABLE)) ClearWindowState(FLAG_WINDOW_RESIZABLE);
else SetWindowState(FLAG_WINDOW_RESIZABLE);
}

if (IsKeyPressed(Key.D))
{
if (IsWindowState(FLAG_WINDOW_UNDECORATED)) ClearWindowState(FLAG_WINDOW_UNDECORATED);
else SetWindowState(FLAG_WINDOW_UNDECORATED);
}

if (IsKeyPressed(Key.H))
{
if (!IsWindowState(FLAG_WINDOW_HIDDEN)) SetWindowState(FLAG_WINDOW_HIDDEN);

framesCounter = 0;
}

if (IsWindowState(FLAG_WINDOW_HIDDEN))
{
framesCounter++;
if (framesCounter >= 240) ClearWindowState(FLAG_WINDOW_HIDDEN); // Show window after 3 seconds
}

if (IsKeyPressed(Key.N))
{
if (!IsWindowState(FLAG_WINDOW_MINIMIZED)) MinimizeWindow();

framesCounter = 0;
}

if (IsWindowState(FLAG_WINDOW_MINIMIZED))
{
framesCounter++;
if (framesCounter >= 240) RestoreWindow(); // Restore window after 3 seconds
}

if (IsKeyPressed(Key.M))
{
// NOTE: Requires FLAG_WINDOW_RESIZABLE enabled!
if (IsWindowState(FLAG_WINDOW_MAXIMIZED)) RestoreWindow();
else MaximizeWindow();
}

if (IsKeyPressed(Key.U))
{
if (IsWindowState(FLAG_WINDOW_UNFOCUSED)) ClearWindowState(FLAG_WINDOW_UNFOCUSED);
else SetWindowState(FLAG_WINDOW_UNFOCUSED);
}

if (IsKeyPressed(Key.T))
{
if (IsWindowState(FLAG_WINDOW_TOPMOST)) ClearWindowState(FLAG_WINDOW_TOPMOST);
else SetWindowState(FLAG_WINDOW_TOPMOST);
}

if (IsKeyPressed(Key.A))
{
if (IsWindowState(FLAG_WINDOW_ALWAYS_RUN)) ClearWindowState(FLAG_WINDOW_ALWAYS_RUN);
else SetWindowState(FLAG_WINDOW_ALWAYS_RUN);
}

if (IsKeyPressed(Key.V))
{
if (IsWindowState(FLAG_VSYNC_HINT)) ClearWindowState(FLAG_VSYNC_HINT);
else SetWindowState(FLAG_VSYNC_HINT);
}

// Bouncing ball logic
ballPosition.X += ballSpeed.x;
ballPosition.Y += ballSpeed.y;
if ((ballPosition.X >= (GetScreenWidth() - ballRadius)) || (ballPosition.X <= ballRadius)) ballSpeed.X *= -1.0f;
if ((ballPosition.Y >= (GetScreenHeight() - ballRadius)) || (ballPosition.Y <= ballRadius)) ballSpeed.Y *= -1.0f;

// Draw
BeginDrawing();

if (IsWindowState(FLAG_WINDOW_TRANSPARENT)) ClearBackground(Blank);
else ClearBackground(RayWhite);

DrawCircleV(ballPosition, ballRadius, Maroon);
DrawRectangleLinesEx((Rectangle) { 0, 0, (float)GetScreenWidth(), (float)GetScreenHeight() }, 4, RayWhite);

DrawCircleV(GetMousePosition(), 10, DarkBlue);

DrawFPS(10, 10);

DrawText(TextFormat("Screen Size: [%i, %i]", GetScreenWidth(), GetScreenHeight()), 10, 40, 10, Green);

// Draw window state info
DrawText("Following flags can be set after window creation:", 10, 60, 10, Gray);
if (IsWindowState(FLAG_FULLSCREEN_MODE)) DrawText("[F] FLAG_FULLSCREEN_MODE: on", 10, 80, 10, Lime);
else DrawText("[F] FLAG_FULLSCREEN_MODE: off", 10, 80, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_RESIZABLE)) DrawText("[R] FLAG_WINDOW_RESIZABLE: on", 10, 100, 10, Lime);
else DrawText("[R] FLAG_WINDOW_RESIZABLE: off", 10, 100, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_UNDECORATED)) DrawText("[D] FLAG_WINDOW_UNDECORATED: on", 10, 120, 10, Lime);
else DrawText("[D] FLAG_WINDOW_UNDECORATED: off", 10, 120, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_HIDDEN)) DrawText("[H] FLAG_WINDOW_HIDDEN: on", 10, 140, 10, Lime);
else DrawText("[H] FLAG_WINDOW_HIDDEN: off", 10, 140, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_MINIMIZED)) DrawText("[N] FLAG_WINDOW_MINIMIZED: on", 10, 160, 10, Lime);
else DrawText("[N] FLAG_WINDOW_MINIMIZED: off", 10, 160, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_MAXIMIZED)) DrawText("[M] FLAG_WINDOW_MAXIMIZED: on", 10, 180, 10, Lime);
else DrawText("[M] FLAG_WINDOW_MAXIMIZED: off", 10, 180, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_UNFOCUSED)) DrawText("[G] FLAG_WINDOW_UNFOCUSED: on", 10, 200, 10, Lime);
else DrawText("[U] FLAG_WINDOW_UNFOCUSED: off", 10, 200, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_TOPMOST)) DrawText("[T] FLAG_WINDOW_TOPMOST: on", 10, 220, 10, Lime);
else DrawText("[T] FLAG_WINDOW_TOPMOST: off", 10, 220, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_ALWAYS_RUN)) DrawText("[A] FLAG_WINDOW_ALWAYS_RUN: on", 10, 240, 10, Lime);
else DrawText("[A] FLAG_WINDOW_ALWAYS_RUN: off", 10, 240, 10, Maroon);
if (IsWindowState(FLAG_VSYNC_HINT)) DrawText("[V] FLAG_VSYNC_HINT: on", 10, 260, 10, Lime);
else DrawText("[V] FLAG_VSYNC_HINT: off", 10, 260, 10, Maroon);

DrawText("Following flags can only be set before window creation:", 10, 300, 10, Gray);
if (IsWindowState(FLAG_WINDOW_HIGHDPI)) DrawText("FLAG_WINDOW_HIGHDPI: on", 10, 320, 10, Lime);
else DrawText("FLAG_WINDOW_HIGHDPI: off", 10, 320, 10, Maroon);
if (IsWindowState(FLAG_WINDOW_TRANSPARENT)) DrawText("FLAG_WINDOW_TRANSPARENT: on", 10, 340, 10, Lime);
else DrawText("FLAG_WINDOW_TRANSPARENT: off", 10, 340, 10, Maroon);
if (IsWindowState(FLAG_MSAA_4X_HINT)) DrawText("FLAG_MSAA_4X_HINT: on", 10, 360, 10, Lime);
else DrawText("FLAG_MSAA_4X_HINT: off", 10, 360, 10, Maroon);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
