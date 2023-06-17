using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

// Program main entry point
public static int CoreSmoothPixelperfect()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

const int virtualScreenWidth = 160;
const int virtualScreenHeight = 90;

const float virtualRatio = (float)screenWidth/(float)virtualScreenWidth;

InitWindow(screenWidth, screenHeight, "raylib [core] example - smooth pixel-perfect camera");

Camera2D worldSpaceCamera = new();  // Game world camera
worldSpaceCamera.zoom = 1.0f;

Camera2D screenSpaceCamera = new(); // Smoothing camera
screenSpaceCamera.zoom = 1.0f;

RenderTexture2D target = LoadRenderTexture(virtualScreenWidth, virtualScreenHeight); // This is where we'll draw all our objects.

Rectangle rec01 = new( 70.0f, 35.0f, 20.0f, 20.0f );
Rectangle rec02 = new( 90.0f, 55.0f, 30.0f, 10.0f );
Rectangle rec03 = new( 80.0f, 65.0f, 15.0f, 25.0f );

// The target's height is flipped (in the source Rectangle), due to OpenGL reasons
Rectangle sourceRec = new( 0.0f, 0.0f, (float)target.texture.width, -(float)target.texture.height );
Rectangle destRec = new( -virtualRatio, -virtualRatio, screenWidth + (virtualRatio*2), screenHeight + (virtualRatio*2) );

Vector2 origin = new( 0.0f, 0.0f );

float rotation = 0.0f;

float cameraX = 0.0f;
float cameraY = 0.0f;

SetTargetFPS(60);

// Main game loop
while (!WindowShouldClose())    // Detect window close button or ESC key
{
// Update
rotation += 60.0f*GetFrameTime();   // Rotate the rectangles, 60 degrees per second

// Make the camera move to demonstrate the effect
cameraX = (sinf(GetTime())*50.0f) - 10.0f;
cameraY = cosf(GetTime())*30.0f;

// Set the camera's target to the values computed above
screenSpaceCamera.target = (Vector2){ cameraX, cameraY };

// Round worldSpace coordinates, keep decimals into screenSpace coordinates
worldSpaceCamera.target.X = (int)screenSpaceCamera.target.x;
screenSpaceCamera.target.X -= worldSpaceCamera.target.x;
screenSpaceCamera.target.X *= virtualRatio;

worldSpaceCamera.target.Y = (int)screenSpaceCamera.target.y;
screenSpaceCamera.target.Y -= worldSpaceCamera.target.y;
screenSpaceCamera.target.Y *= virtualRatio;

// Draw
BeginTextureMode(target);
ClearBackground(RayWhite);

BeginMode2D(worldSpaceCamera);
DrawRectanglePro(rec01, origin, rotation, Black);
DrawRectanglePro(rec02, origin, -rotation, Red);
DrawRectanglePro(rec03, origin, rotation + 45.0f, Blue);
EndMode2D();
EndTextureMode();

BeginDrawing();
ClearBackground(Red);

BeginMode2D(screenSpaceCamera);
DrawTexturePro(target.texture, sourceRec, destRec, origin, 0.0f, White);
EndMode2D();

DrawText(TextFormat("Screen resolution: %ix%i", screenWidth, screenHeight), 10, 10, 20, DarkBlue);
DrawText(TextFormat("World resolution: %ix%i", virtualScreenWidth, virtualScreenHeight), 10, 40, 20, DarkGreen);
DrawFPS(GetScreenWidth() - 95, 10);
EndDrawing();
}

// De-Initialization
UnloadRenderTexture(target);    // Unload render texture

CloseWindow();                  // Close window and OpenGL context

return 0;
}
}
