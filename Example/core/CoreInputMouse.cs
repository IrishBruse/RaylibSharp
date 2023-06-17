using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Program main entry point
public static int CoreInputMouse()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - mouse input");

Vector2 ballPosition = new( -100.0f, -100.0f );
Color ballColor = DarkBlue;

SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
// Update
ballPosition = GetMousePosition();

if (IsMouseButtonPressed(MOUSE_BUTTON_LEFT)) ballColor = Maroon;
else if (IsMouseButtonPressed(MOUSE_BUTTON_MIDDLE)) ballColor = Lime;
else if (IsMouseButtonPressed(MOUSE_BUTTON_RIGHT)) ballColor = DarkBlue;
else if (IsMouseButtonPressed(MOUSE_BUTTON_SIDE)) ballColor = Purple;
else if (IsMouseButtonPressed(MOUSE_BUTTON_EXTRA)) ballColor = Yellow;
else if (IsMouseButtonPressed(MOUSE_BUTTON_FORWARD)) ballColor = Orange;
else if (IsMouseButtonPressed(MOUSE_BUTTON_BACK)) ballColor = Beige;

// Draw
BeginDrawing();

ClearBackground(RayWhite);

DrawCircleV(ballPosition, 40, ballColor);

DrawText("move ball with mouse and click mouse button to change color", 10, 10, 20, DarkGray);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
