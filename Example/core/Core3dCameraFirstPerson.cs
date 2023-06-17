using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

private static readonly int MAX_COLUMNS = 20;

// Program main entry point
public static int Core3dCameraFirstPerson()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - 3d camera first person");

// Define the camera to look into our 3d world (position, target, up vector)
Camera camera = new();
camera.position = (Vector3){ 0.0f, 2.0f, 4.0f };    // Camera position
camera.target = (Vector3){ 0.0f, 2.0f, 0.0f };      // Camera looking at point
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f };          // Camera up vector (rotation towards target)
camera.fovy = 60.0f;                                // Camera field-of-view Y
camera.projection = CAMERA_PERSPECTIVE;             // Camera projection type

int cameraMode = CAMERA_FIRST_PERSON;

// Generates some random columns
float heights[MAX_COLUMNS] = new();
Vector3 positions[MAX_COLUMNS] = new();
Color colors[MAX_COLUMNS] = new();

for (int i = 0; i < MAX_COLUMNS; i++)
{
heights[i] = (float)GetRandomValue(1, 12);
positions[i] = (Vector3){ (float)GetRandomValue(-15, 15), heights[i]/2.0f, (float)GetRandomValue(-15, 15) };
colors[i] = (Color){ GetRandomValue(20, 255), GetRandomValue(10, 55), 30, 255 };
}

DisableCursor();                    // Limit cursor to relative movement inside the window

SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())        // Detect window close button or ESC key
{
// Update
// Switch camera mode
if (IsKeyPressed(Key.One))
{
cameraMode = CAMERA_FREE;
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f }; // Reset roll
}

if (IsKeyPressed(Key.Two))
{
cameraMode = CAMERA_FIRST_PERSON;
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f }; // Reset roll
}

if (IsKeyPressed(Key.Three))
{
cameraMode = CAMERA_THIRD_PERSON;
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f }; // Reset roll
}

if (IsKeyPressed(Key.Four))
{
cameraMode = CAMERA_ORBITAL;
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f }; // Reset roll
}

// Switch camera projection
if (IsKeyPressed(Key.P))
{
if (camera.projection == CAMERA_PERSPECTIVE)
{
// Create isometric view
cameraMode = CAMERA_THIRD_PERSON;
// Note: The target distance is related to the render distance in the orthographic projection
camera.position = (Vector3){ 0.0f, 2.0f, -100.0f };
camera.target = (Vector3){ 0.0f, 2.0f, 0.0f };
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f };
camera.projection = CAMERA_ORTHOGRAPHIC;
camera.fovy = 20.0f; // near plane width in CAMERA_ORTHOGRAPHIC
CameraYaw(&camera, -135 * DEG2RAD, true);
CameraPitch(&camera, -45 * DEG2RAD, true, true, false);
}
else if (camera.projection == CAMERA_ORTHOGRAPHIC)
{
// Reset to default view
cameraMode = CAMERA_THIRD_PERSON;
camera.position = (Vector3){ 0.0f, 2.0f, 10.0f };
camera.target = (Vector3){ 0.0f, 2.0f, 0.0f };
camera.up = (Vector3){ 0.0f, 1.0f, 0.0f };
camera.projection = CAMERA_PERSPECTIVE;
camera.fovy = 60.0f;
}
}

// Update camera computes movement internally depending on the camera mode
// Some default standard keyboard/mouse inputs are hardcoded to simplify use
// For advance camera controls, it's reecommended to compute camera movement manually
UpdateCamera(&camera, cameraMode);                  // Update camera

/*
// Camera PRO usage example (EXPERIMENTAL)
// This new camera function allows custom movement/rotation values to be directly provided
// as input parameters, with this approach, rcamera module is internally independent of raylib inputs
UpdateCameraPro(&camera,
(Vector3){
(IsKeyDown(Key.W) || iskeydown(keyUp))*0.1f -      // Move forward-backward
(IsKeyDown(Key.S) || iskeydown(keyDown))*0.1f,
(IsKeyDown(Key.D) || iskeydown(keyRight))*0.1f -   // Move right-left
(IsKeyDown(Key.A) || iskeydown(keyLeft))*0.1f,
0.0f                                                // Move up-down
},
(Vector3){
GetMouseDelta().x*0.05f,                            // Rotation: yaw
GetMouseDelta().y*0.05f,                            // Rotation: pitch
0.0f                                                // Rotation: roll
},
GetMouseWheelMove()*2.0f);                              // Move to target (zoom)
*/

// Draw
BeginDrawing();

ClearBackground(RayWhite);

BeginMode3D(camera);

DrawPlane((Vector3){ 0.0f, 0.0f, 0.0f }, (Vector2){ 32.0f, 32.0f }, LightGray); // Draw ground
DrawCube((Vector3){ -16.0f, 2.5f, 0.0f }, 1.0f, 5.0f, 32.0f, Blue);     // Draw a blue wall
DrawCube((Vector3){ 16.0f, 2.5f, 0.0f }, 1.0f, 5.0f, 32.0f, Lime);      // Draw a green wall
DrawCube((Vector3){ 0.0f, 2.5f, 16.0f }, 32.0f, 5.0f, 1.0f, Gold);      // Draw a yellow wall

// Draw some cubes around
for (int i = 0; i < MAX_COLUMNS; i++)
{
DrawCube(positions[i], 2.0f, heights[i], 2.0f, colors[i]);
DrawCubeWires(positions[i], 2.0f, heights[i], 2.0f, Maroon);
}

// Draw player cube
if (cameraMode == CAMERA_THIRD_PERSON)
{
DrawCube(camera.target, 0.5f, 0.5f, 0.5f, Purple);
DrawCubeWires(camera.target, 0.5f, 0.5f, 0.5f, DarkPurple);
}

EndMode3D();

// Draw info boxes
DrawRectangle(5, 5, 330, 100, Fade(SkyBlue, 0.5f));
DrawRectangleLines(5, 5, 330, 100, Blue);

DrawText("Camera controls:", 15, 15, 10, Black);
DrawText("- Move keys: W, A, S, D, Space, Left-Ctrl", 15, 30, 10, Black);
DrawText("- Look around: arrow keys or mouse", 15, 45, 10, Black);
DrawText("- Camera mode keys: 1, 2, 3, 4", 15, 60, 10, Black);
DrawText("- Zoom keys: num-plus, num-minus or mouse scroll", 15, 75, 10, Black);
DrawText("- Camera projection key: P", 15, 90, 10, Black);

DrawRectangle(600, 5, 195, 100, Fade(SkyBlue, 0.5f));
DrawRectangleLines(600, 5, 195, 100, Blue);

DrawText("Camera status:", 610, 15, 10, Black);
DrawText(TextFormat("- Mode: %s", (cameraMode == CAMERA_FREE) ? "FREE" :
(cameraMode == CAMERA_FIRST_PERSON) ? "FIRST_PERSON" :
(cameraMode == CAMERA_THIRD_PERSON) ? "THIRD_PERSON" :
(cameraMode == CAMERA_ORBITAL) ? "ORBITAL" : "CUSTOM"), 610, 30, 10, Black);
DrawText(TextFormat("- Projection: %s", (camera.projection == CAMERA_PERSPECTIVE) ? "PERSPECTIVE" :
(camera.projection == CAMERA_ORTHOGRAPHIC) ? "ORTHOGRAPHIC" : "CUSTOM"), 610, 45, 10, Black);
DrawText(TextFormat("- Position: (%06.3f, %06.3f, %06.3f)", camera.position.x, camera.position.y, camera.position.z), 610, 60, 10, Black);
DrawText(TextFormat("- Target: (%06.3f, %06.3f, %06.3f)", camera.target.x, camera.target.y, camera.target.z), 610, 75, 10, Black);
DrawText(TextFormat("- Up: (%06.3f, %06.3f, %06.3f)", camera.up.x, camera.up.y, camera.up.z), 610, 90, 10, Black);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}
}
