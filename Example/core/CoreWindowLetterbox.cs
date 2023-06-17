using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

private static readonly int MAX(a, = b);
private static readonly int MIN(a, = b);

// Program main entry point
public static int CoreWindowLetterbox()
{
const int windowWidth = 800;
const int windowHeight = 450;

// Enable config flags for resizable window and vertical synchro
SetConfigFlags(FLAG_WINDOW_RESIZABLE | FLAG_VSYNC_HINT);
InitWindow(windowWidth, windowHeight, "raylib [core] example - window scale letterbox");
SetWindowMinSize(320, 240);

int gameScreenWidth = 640;
int gameScreenHeight = 480;

// Render texture initialization, used to hold the rendering result so we can easily resize it
RenderTexture2D target = LoadRenderTexture(gameScreenWidth, gameScreenHeight);
SetTextureFilter(target.texture, TEXTURE_FILTER_BILINEAR);  // Texture scale filter to use

Color colors[10] = new();
for (int i = 0; i < 10; i++) colors[i] = (Color){ GetRandomValue(100, 250), GetRandomValue(50, 150), GetRandomValue(10, 100), 255 };

SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second

// Main game loop
while (!WindowShouldClose())        // Detect window close button or ESC key
{
// Update
// Compute required framebuffer scaling
float scale = MIN((float)GetScreenWidth()/gameScreenWidth, (float)GetScreenHeight()/gameScreenHeight);

if (IsKeyPressed(Key.Space))
{
// Recalculate random colors for the bars
for (int i = 0; i < 10; i++) colors[i] = (Color){ GetRandomValue(100, 250), GetRandomValue(50, 150), GetRandomValue(10, 100), 255 };
}

// Update virtual mouse (clamped mouse value behind game screen)
Vector2 mouse = GetMousePosition();
Vector2 virtualMouse = new();
virtualMouse.X = (mouse.X - (GetScreenWidth() - (gameScreenWidth*scale))*0.5f)/scale;
virtualMouse.Y = (mouse.Y - (GetScreenHeight() - (gameScreenHeight*scale))*0.5f)/scale;
virtualMouse = Vector2Clamp(virtualMouse, (Vector2){ 0, 0 }, (Vector2){ (float)gameScreenWidth, (float)gameScreenHeight });

// Apply the same transformation as the virtual mouse to the real mouse (i.e. to work with raygui)
//SetMouseOffset(-(GetScreenWidth() - (gameScreenWidth*scale))*0.5f, -(GetScreenHeight() - (gameScreenHeight*scale))*0.5f);
//SetMouseScale(1/scale, 1/scale);

// Draw
// Draw everything in the render texture, note this will not be rendered on screen, yet
BeginTextureMode(target);
ClearBackground(RayWhite);  // Clear render texture background color

for (int i = 0; i < 10; i++) DrawRectangle(0, (gameScreenHeight/10)*i, gameScreenWidth, gameScreenHeight/10, colors[i]);

DrawText("If executed inside a window,\nyou can resize the window,\nand see the screen scaling!", 10, 25, 20, White);
DrawText(TextFormat("Default Mouse: [%i , %i]", (int)mouse.x, (int)mouse.y), 350, 25, 20, Green);
DrawText(TextFormat("Virtual Mouse: [%i , %i]", (int)virtualMouse.x, (int)virtualMouse.y), 350, 55, 20, Yellow);
EndTextureMode();

BeginDrawing();
ClearBackground(Black);     // Clear screen background

// Draw render texture to screen, properly scaled
DrawTexturePro(target.texture, (Rectangle){ 0.0f, 0.0f, (float)target.texture.width, (float)-target.texture.height },
(Rectangle){ (GetScreenWidth() - ((float)gameScreenWidth*scale))*0.5f, (GetScreenHeight() - ((float)gameScreenHeight*scale))*0.5f,
(float)gameScreenWidth*scale, (float)gameScreenHeight*scale }, (Vector2){ 0, 0 }, 0.0f, White);
EndDrawing();
}

// De-Initialization
UnloadRenderTexture(target);        // Unload render texture

CloseWindow();                      // Close window and OpenGL context

return 0;
}
}
