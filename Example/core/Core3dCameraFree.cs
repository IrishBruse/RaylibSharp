using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Program main entry point
public static int Core3dCameraFree()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - 3d camera free");

// Define the camera to look into our 3d world
Camera3D camera = new();
camera.position = (Vector3){ 10.0f, 10.0f, 10.0f }; // Camera position
camera.target = (Vector3){ 0.0f, 0.0f, 0.0f };      // Camera looking at point
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f };          // Camera up vector (rotation towards target)
camera.fovy = 45.0f;                                // Camera field-of-view Y
camera.projection = CAMERA_PERSPECTIVE;             // Camera projection type

Vector3 cubePosition = new( 0.0f, 0.0f, 0.0f );

DisableCursor();                    // Limit cursor to relative movement inside the window

SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())        // Detect window close button or ESC key
{
// Update
UpdateCamera(&camera, CAMERA_FREE);

if (IsKeyDown('Z')) camera.target = (Vector3){ 0.0f, 0.0f, 0.0f };

// Draw
BeginDrawing();

ClearBackground(RayWhite);

BeginMode3D(camera);

DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, Red);
DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, Maroon);

DrawGrid(10, 1.0f);

EndMode3D();

DrawRectangle( 10, 10, 320, 133, Fade(SkyBlue, 0.5f));
DrawRectangleLines( 10, 10, 320, 133, Blue);

DrawText("Free camera default controls:", 20, 20, 10, Black);
DrawText("- Mouse Wheel to Zoom in-out", 40, 40, 10, DarkGray);
DrawText("- Mouse Wheel Pressed to Pan", 40, 60, 10, DarkGray);
DrawText("- Alt + Mouse Wheel Pressed to Rotate", 40, 80, 10, DarkGray);
DrawText("- Alt + Ctrl + Mouse Wheel Pressed for Smooth Zoom", 40, 100, 10, DarkGray);
DrawText("- Z to zoom to (0, 0, 0)", 40, 120, 10, DarkGray);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
