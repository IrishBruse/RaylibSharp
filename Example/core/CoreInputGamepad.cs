using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// NOTE: Gamepad name ID depends on drivers and OS
private static readonly string XBOX360_LEGACY_NAME_ID = "Xbox;
#if defined(PLATFORM_RPI)
private static readonly string XBOX360_NAME_ID = "Microsoft;
private static readonly string PS3_NAME_ID = "PLAYSTATION(R)3;
#else
private static readonly string XBOX360_NAME_ID = "Xbox;
private static readonly string PS3_NAME_ID = "PLAYSTATION(R)3;
#endif

// Program main entry point
public static int CoreInputGamepad()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

SetConfigFlags(FLAG_MSAA_4X_HINT);  // Set MSAA 4X hint before windows creation

InitWindow(screenWidth, screenHeight, "raylib [core] example - gamepad input");

Texture2D texPs3Pad = LoadTexture("resources/ps3.png");
Texture2D texXboxPad = LoadTexture("resources/xbox.png");

SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
// Update
// ...

// Draw
BeginDrawing();

ClearBackground(RayWhite);

if (IsGamepadAvailable(0))
{
DrawText(TextFormat("GP1: %s", GetGamepadName(0)), 10, 10, 10, Black);

if (TextIsEqual(GetGamepadName(0), XBOX360_NAME_ID) || TextIsEqual(GetGamepadName(0), XBOX360_LEGACY_NAME_ID))
{
DrawTexture(texXboxPad, 0, 0, DarkGray);

// Draw buttons: xbox home
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE)) DrawCircle(394, 89, 19, Red);

// Draw buttons: basic
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_RIGHT)) DrawCircle(436, 150, 9, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_LEFT)) DrawCircle(352, 150, 9, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_LEFT)) DrawCircle(501, 151, 15, Blue);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_DOWN)) DrawCircle(536, 187, 15, Lime);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT)) DrawCircle(572, 151, 15, Maroon);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_UP)) DrawCircle(536, 115, 15, Gold);

// Draw buttons: d-pad
DrawRectangle(317, 202, 19, 71, Black);
DrawRectangle(293, 228, 69, 19, Black);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_UP)) DrawRectangle(317, 202, 19, 26, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_DOWN)) DrawRectangle(317, 202 + 45, 19, 26, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_LEFT)) DrawRectangle(292, 228, 25, 19, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_RIGHT)) DrawRectangle(292 + 44, 228, 26, 19, Red);

// Draw buttons: left-right back
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_TRIGGER_1)) DrawCircle(259, 61, 20, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_TRIGGER_1)) DrawCircle(536, 61, 20, Red);

// Draw axis: left joystick
DrawCircle(259, 152, 39, Black);
DrawCircle(259, 152, 34, LightGray);
DrawCircle(259 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_X)*20),
152 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_Y)*20), 25, Black);

// Draw axis: right joystick
DrawCircle(461, 237, 38, Black);
DrawCircle(461, 237, 33, LightGray);
DrawCircle(461 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_X)*20),
237 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_Y)*20), 25, Black);

// Draw axis: left-right triggers
DrawRectangle(170, 30, 15, 70, Gray);
DrawRectangle(604, 30, 15, 70, Gray);
DrawRectangle(170, 30, 15, (int)(((1 + GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_TRIGGER))/2)*70), Red);
DrawRectangle(604, 30, 15, (int)(((1 + GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_TRIGGER))/2)*70), Red);

//DrawText(TextFormat("Xbox axis LT: %02.02f", GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_TRIGGER)), 10, 40, 10, Black);
//DrawText(TextFormat("Xbox axis RT: %02.02f", GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_TRIGGER)), 10, 60, 10, Black);
}
else if (TextIsEqual(GetGamepadName(0), PS3_NAME_ID))
{
DrawTexture(texPs3Pad, 0, 0, DarkGray);

// Draw buttons: ps
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE)) DrawCircle(396, 222, 13, Red);

// Draw buttons: basic
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_LEFT)) DrawRectangle(328, 170, 32, 13, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_RIGHT)) DrawTriangle((Vector2){ 436, 168 }, (Vector2){ 436, 185 }, (Vector2){ 464, 177 }, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_UP)) DrawCircle(557, 144, 13, Lime);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT)) DrawCircle(586, 173, 13, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_DOWN)) DrawCircle(557, 203, 13, Violet);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_LEFT)) DrawCircle(527, 173, 13, Pink);

// Draw buttons: d-pad
DrawRectangle(225, 132, 24, 84, Black);
DrawRectangle(195, 161, 84, 25, Black);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_UP)) DrawRectangle(225, 132, 24, 29, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_DOWN)) DrawRectangle(225, 132 + 54, 24, 30, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_LEFT)) DrawRectangle(195, 161, 30, 25, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_RIGHT)) DrawRectangle(195 + 54, 161, 30, 25, Red);

// Draw buttons: left-right back buttons
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_TRIGGER_1)) DrawCircle(239, 82, 20, Red);
if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_TRIGGER_1)) DrawCircle(557, 82, 20, Red);

// Draw axis: left joystick
DrawCircle(319, 255, 35, Black);
DrawCircle(319, 255, 31, LightGray);
DrawCircle(319 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_X) * 20),
255 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_Y) * 20), 25, Black);

// Draw axis: right joystick
DrawCircle(475, 255, 35, Black);
DrawCircle(475, 255, 31, LightGray);
DrawCircle(475 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_X) * 20),
255 + (int)(GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_Y) * 20), 25, Black);

// Draw axis: left-right triggers
DrawRectangle(169, 48, 15, 70, Gray);
DrawRectangle(611, 48, 15, 70, Gray);
DrawRectangle(169, 48, 15, (int)(((1 - GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_TRIGGER)) / 2) * 70), Red);
DrawRectangle(611, 48, 15, (int)(((1 - GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_TRIGGER)) / 2) * 70), Red);
}
else
{
DrawText("- GENERIC GAMEPAD -", 280, 180, 20, Gray);

// TODO: Draw generic gamepad
}

DrawText(TextFormat("DETECTED AXIS [%i]:", GetGamepadAxisCount(0)), 10, 50, 10, Maroon);

for (int i = 0; i < GetGamepadAxisCount(0); i++)
{
DrawText(TextFormat("AXIS %i: %.02f", i, GetGamepadAxisMovement(0, i)), 20, 70 + 20*i, 10, DarkGray);
}

if (GetGamepadButtonPressed() != GAMEPAD_BUTTON_UNKNOWN) DrawText(TextFormat("DETECTED BUTTON: %i", GetGamepadButtonPressed()), 10, 430, 10, Red);
else DrawText("DETECTED BUTTON: NONE", 10, 430, 10, Gray);
}
else
{
DrawText("GP1: NOT DETECTED", 10, 10, 10, Gray);

DrawTexture(texXboxPad, 0, 0, LightGray);
}

EndDrawing();
}

// De-Initialization
UnloadTexture(texPs3Pad);
UnloadTexture(texXboxPad);

CloseWindow();        // Close window and OpenGL context

return 0;
}
}
