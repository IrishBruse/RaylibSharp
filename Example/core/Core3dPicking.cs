using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Program main entry point
public static int Core3dPicking()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - 3d picking");

// Define the camera to look into our 3d world
Camera camera = new();
camera.position = (Vector3){ 10.0f, 10.0f, 10.0f }; // Camera position
camera.target = (Vector3){ 0.0f, 0.0f, 0.0f };      // Camera looking at point
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f };          // Camera up vector (rotation towards target)
camera.fovy = 45.0f;                                // Camera field-of-view Y
camera.projection = CAMERA_PERSPECTIVE;             // Camera projection type

Vector3 cubePosition = new( 0.0f, 1.0f, 0.0f );
Vector3 cubeSize = new( 2.0f, 2.0f, 2.0f );

Ray ray = new();                    // Picking line ray
RayCollision collision = new();     // Ray collision hit info

SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())        // Detect window close button or ESC key
{
// Update
if (IsCursorHidden()) UpdateCamera(&camera, CAMERA_FIRST_PERSON);

// Toggle camera controls
if (IsMouseButtonPressed(MOUSE_BUTTON_RIGHT))
{
if (IsCursorHidden()) EnableCursor();
else DisableCursor();
}

if (IsMouseButtonPressed(MOUSE_BUTTON_LEFT))
{
if (!collision.hit)
{
ray = GetMouseRay(GetMousePosition(), camera);

// Check collision between ray and box
collision = GetRayCollisionBox(ray,
(BoundingBox){(Vector3){ cubePosition.X - cubeSize.x/2, cubePosition.Y - cubeSize.y/2, cubePosition.z - cubeSize.z/2 },
(Vector3){ cubePosition.X + cubeSize.x/2, cubePosition.Y + cubeSize.y/2, cubePosition.z + cubeSize.z/2 }});
}
else collision.hit = false;
}

// Draw
BeginDrawing();

ClearBackground(RayWhite);

BeginMode3D(camera);

if (collision.hit)
{
DrawCube(cubePosition, cubeSize.x, cubeSize.y, cubeSize.z, Red);
DrawCubeWires(cubePosition, cubeSize.x, cubeSize.y, cubeSize.z, Maroon);

DrawCubeWires(cubePosition, cubeSize.X + 0.2f, cubeSize.Y + 0.2f, cubeSize.z + 0.2f, Green);
}
else
{
DrawCube(cubePosition, cubeSize.x, cubeSize.y, cubeSize.z, Gray);
DrawCubeWires(cubePosition, cubeSize.x, cubeSize.y, cubeSize.z, DarkGray);
}

DrawRay(ray, Maroon);
DrawGrid(10, 1.0f);

EndMode3D();

DrawText("Try clicking on the box with your mouse!", 240, 10, 20, DarkGray);

if (collision.hit) DrawText("BOX SELECTED", (screenWidth - MeasureText("BOX SELECTED", 30)) / 2, (int)(screenHeight * 0.1f), 30, Green);

DrawText("Right click mouse to toggle camera controls", 10, 430, 10, Gray);

DrawFPS(10, 10);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
