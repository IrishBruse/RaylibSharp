using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Types and Structures Definition
typedef enum GameScreen { LOGO = 0, TITLE, GAMEPLAY, ENDING } GameScreen;

// Program main entry point
public static int CoreBasicScreenManager()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - basic screen manager");

GameScreen currentScreen = LOGO;

// TODO: Initialize all required variables and load all required data here!

int framesCounter = 0;          // Useful to count frames

SetTargetFPS(60);               // Set desired framerate (frames-per-second)

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
// Update
switch(currentScreen)
{
case LOGO:
{
// TODO: Update LOGO screen variables here!

framesCounter++;    // Count frames

// Wait for 2 seconds (120 frames) before jumping to TITLE screen
if (framesCounter > 120)
{
currentScreen = TITLE;
}
} break;
case TITLE:
{
// TODO: Update TITLE screen variables here!

// Press enter to change to GAMEPLAY screen
if (IsKeyPressed(Key.Enter) || isgesturedetected(gestureTap))
{
currentScreen = GAMEPLAY;
}
} break;
case GAMEPLAY:
{
// TODO: Update GAMEPLAY screen variables here!

// Press enter to change to ENDING screen
if (IsKeyPressed(Key.Enter) || isgesturedetected(gestureTap))
{
currentScreen = ENDING;
}
} break;
case ENDING:
{
// TODO: Update ENDING screen variables here!

// Press enter to return to TITLE screen
if (IsKeyPressed(Key.Enter) || isgesturedetected(gestureTap))
{
currentScreen = TITLE;
}
} break;
default: break;
}

// Draw
BeginDrawing();

ClearBackground(RayWhite);

switch(currentScreen)
{
case LOGO:
{
// TODO: Draw LOGO screen here!
DrawText("LOGO SCREEN", 20, 20, 40, LightGray);
DrawText("WAIT for 2 SECONDS...", 290, 220, 20, Gray);

} break;
case TITLE:
{
// TODO: Draw TITLE screen here!
DrawRectangle(0, 0, screenWidth, screenHeight, Green);
DrawText("TITLE SCREEN", 20, 20, 40, DarkGreen);
DrawText("PRESS ENTER or TAP to JUMP to GAMEPLAY SCREEN", 120, 220, 20, DarkGreen);

} break;
case GAMEPLAY:
{
// TODO: Draw GAMEPLAY screen here!
DrawRectangle(0, 0, screenWidth, screenHeight, Purple);
DrawText("GAMEPLAY SCREEN", 20, 20, 40, Maroon);
DrawText("PRESS ENTER or TAP to JUMP to ENDING SCREEN", 130, 220, 20, Maroon);

} break;
case ENDING:
{
// TODO: Draw ENDING screen here!
DrawRectangle(0, 0, screenWidth, screenHeight, Blue);
DrawText("ENDING SCREEN", 20, 20, 40, DarkBlue);
DrawText("PRESS ENTER or TAP to RETURN to TITLE SCREEN", 120, 220, 20, DarkBlue);

} break;
default: break;
}

EndDrawing();
}

// De-Initialization

// TODO: Unload all loaded data (textures, fonts, audio) here!

CloseWindow();        // Close window and OpenGL context

return 0;
}
}
