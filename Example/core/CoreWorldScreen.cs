using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Program main entry point
public static int CoreWorldScreen()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - core world screen");

// Define the camera to look into our 3d world
Camera camera = new();
camera.position = (Vector3){ 10.0f, 10.0f, 10.0f }; // Camera position
camera.target = (Vector3){ 0.0f, 0.0f, 0.0f };      // Camera looking at point
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f };          // Camera up vector (rotation towards target)
camera.fovy = 45.0f;                                // Camera field-of-view Y
camera.projection = CAMERA_PERSPECTIVE;             // Camera projection type

Vector3 cubePosition = new( 0.0f, 0.0f, 0.0f );
Vector2 cubeScreenPosition = new( 0.0f, 0.0f );

DisableCursor();                    // Limit cursor to relative movement inside the window

SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())        // Detect window close button or ESC key
{
// Update
UpdateCamera(&camera, CAMERA_THIRD_PERSON);

// Calculate cube screen space position (with a little offset to be in top)
cubeScreenPosition = GetWorldToScreen((Vector3){cubePosition.x, cubePosition.Y + 2.5f, cubePosition.z}, camera);

// Draw
BeginDrawing();

ClearBackground(RayWhite);

BeginMode3D(camera);

DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, Red);
DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, Maroon);

DrawGrid(10, 1.0f);

EndMode3D();

DrawText("Enemy: 100 / 100", (int)cubeScreenPosition.X - MeasureText("Enemy: 100/100", 20)/2, (int)cubeScreenPosition.y, 20, Black);

DrawText(TextFormat("Cube position in screen space coordinates: [%i, %i]", (int)cubeScreenPosition.x, (int)cubeScreenPosition.y), 10, 10, 20, Lime);
DrawText("Text 2d should be always on top of the cube", 10, 40, 20, Gray);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
