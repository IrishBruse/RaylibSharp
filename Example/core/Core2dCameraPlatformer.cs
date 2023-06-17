using System.Numerics;
using RaylibSharp;
using static RaylibSharp.Raylib;
using static Utility;

public static partial class Example
{

private static readonly int G = 400;
private static readonly int PLAYER_JUMP_SPD = 350.0f;
private static readonly int PLAYER_HOR_SPD = 200.0f;

typedef struct Player {
Vector2 position;
float speed;
bool canJump;
} Player;

typedef struct EnvItem {
Rectangle rect;
int blocking;
Color color;
} EnvItem;

// Module functions declaration
void UpdatePlayer(Player *player, EnvItem *envItems, int envItemsLength, float delta);
void UpdateCameraCenter(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height);
void UpdateCameraCenterInsideMap(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height);
void UpdateCameraCenterSmoothFollow(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height);
void UpdateCameraEvenOutOnLanding(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height);
void UpdateCameraPlayerBoundsPush(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height);

// Program main entry point
public static int Core2dCameraPlatformer()
{
// Initialization
const int screenWidth = 800;
const int screenHeight = 450;

InitWindow(screenWidth, screenHeight, "raylib [core] example - 2d camera");

Player player = new();
player.position = (Vector2){ 400, 280 };
player.speed = 0;
player.canJump = false;
EnvItem envItems[] = {
{{ 0, 0, 1000, 400 }, 0, LightGray },
{{ 0, 400, 1000, 200 }, 1, Gray },
{{ 300, 200, 400, 10 }, 1, Gray },
{{ 250, 300, 100, 10 }, 1, Gray },
{{ 650, 300, 100, 10 }, 1, Gray }
};

int envItemsLength = sizeof(envItems)/sizeof(envItems[0]);

Camera2D camera = new();
camera.target = player.position;
camera.offset = (Vector2){ screenWidth/2.0f, screenHeight/2.0f };
camera.rotation = 0.0f;
camera.zoom = 1.0f;

// Store pointers to the multiple update camera functions
void (*cameraUpdaters[])(Camera2D*, Player*, EnvItem*, int, float, int, int) = {
UpdateCameraCenter,
UpdateCameraCenterInsideMap,
UpdateCameraCenterSmoothFollow,
UpdateCameraEvenOutOnLanding,
UpdateCameraPlayerBoundsPush
};

int cameraOption = 0;
int cameraUpdatersLength = sizeof(cameraUpdaters)/sizeof(cameraUpdaters[0]);

char *cameraDescriptions[] = {
"Follow player center",
"Follow player center, but clamp to map edges",
"Follow player center; smoothed",
"Follow player center horizontally; update player center vertically after landing",
"Player push camera on getting too close to screen edge"
};

SetTargetFPS(60);

// Main game loop
while (!WindowShouldClose())
{
// Update
float deltaTime = GetFrameTime();

UpdatePlayer(&player, envItems, envItemsLength, deltaTime);

camera.zoom += ((float)GetMouseWheelMove()*0.05f);

if (camera.zoom > 3.0f) camera.zoom = 3.0f;
else if (camera.zoom < 0.25f) camera.zoom = 0.25f;

if (IsKeyPressed(Key.R))
{
camera.zoom = 1.0f;
player.position = (Vector2){ 400, 280 };
}

if (IsKeyPressed(Key.C)) cameraoption = (cameraoption + 1)%cameraUpdatersLength;

// Call update camera function by its pointer
cameraUpdaters[cameraOption](&camera, &player, envItems, envItemsLength, deltaTime, screenWidth, screenHeight);

// Draw
BeginDrawing();

ClearBackground(LightGray);

BeginMode2D(camera);

for (int i = 0; i < envItemsLength; i++) DrawRectangleRec(envItems[i].rect, envItems[i].color);

Rectangle playerRect = { player.position.X - 20, player.position.Y - 40, 40, 40 };
DrawRectangleRec(playerRect, Red);

EndMode2D();

DrawText("Controls:", 20, 20, 10, Black);
DrawText("- Right/Left to move", 40, 40, 10, DarkGray);
DrawText("- Space to jump", 40, 60, 10, DarkGray);
DrawText("- Mouse Wheel to Zoom in-out, R to reset zoom", 40, 80, 10, DarkGray);
DrawText("- C to change camera mode", 40, 100, 10, DarkGray);
DrawText("Current camera mode:", 20, 120, 10, Black);
DrawText(cameraDescriptions[cameraOption], 40, 140, 10, DarkGray);

EndDrawing();
}

// De-Initialization
CloseWindow();        // Close window and OpenGL context

return 0;
}

void UpdatePlayer(Player *player, EnvItem *envItems, int envItemsLength, float delta)
{
if (IsKeyDown(Key.Left)) player->position.X -= PLAYER_HOR_SPD*delta;
if (IsKeyDown(Key.Right)) player->position.X += PLAYER_HOR_SPD*delta;
if (IsKeyDown(Key.Space) && player->canjump)
{
player->speed = -PLAYER_JUMP_SPD;
player->canJump = false;
}

int hitObstacle = 0;
for (int i = 0; i < envItemsLength; i++)
{
EnvItem *ei = envItems + i;
Vector2 *p = &(player->position);
if (ei->blocking &&
ei->rect.X <= p->x &&
ei->rect.X + ei->rect.width >= p->x &&
ei->rect.Y >= p->y &&
ei->rect.Y <= p->y + player->speed*delta)
{
hitObstacle = 1;
player->speed = 0.0f;
p->y = ei->rect.y;
}
}

if (!hitObstacle)
{
player->position.Y += player->speed*delta;
player->speed += G*delta;
player->canJump = false;
}
else player->canJump = true;
}

void UpdateCameraCenter(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height)
{
camera->offset = (Vector2){ width/2.0f, height/2.0f };
camera->target = player->position;
}

void UpdateCameraCenterInsideMap(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height)
{
camera->target = player->position;
camera->offset = (Vector2){ width/2.0f, height/2.0f };
float minX = 1000, minY = 1000, maxX = -1000, maxY = -1000;

for (int i = 0; i < envItemsLength; i++)
{
EnvItem *ei = envItems + i;
minX = fminf(ei->rect.x, minX);
maxX = fmaxf(ei->rect.X + ei->rect.width, maxX);
minY = fminf(ei->rect.y, minY);
maxY = fmaxf(ei->rect.Y + ei->rect.height, maxY);
}

Vector2 max = GetWorldToScreen2D((Vector2){ maxX, maxY }, *camera);
Vector2 min = GetWorldToScreen2D((Vector2){ minX, minY }, *camera);

if (max.X < width) camera->offset.X = width - (max.X - width/2);
if (max.Y < height) camera->offset.Y = height - (max.Y - height/2);
if (min.X > 0) camera->offset.X = width/2 - min.x;
if (min.Y > 0) camera->offset.Y = height/2 - min.y;
}

void UpdateCameraCenterSmoothFollow(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height)
{
static float minSpeed = 30;
static float minEffectLength = 10;
static float fractionSpeed = 0.8f;

camera->offset = (Vector2){ width/2.0f, height/2.0f };
Vector2 diff = Vector2Subtract(player->position, camera->target);
float length = Vector2Length(diff);

if (length > minEffectLength)
{
float speed = fmaxf(fractionSpeed*length, minSpeed);
camera->target = Vector2Add(camera->target, Vector2Scale(diff, speed*delta/length));
}
}

void UpdateCameraEvenOutOnLanding(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height)
{
static float evenOutSpeed = 700;
static int eveningOut = false;
static float evenOutTarget;

camera->offset = (Vector2){ width/2.0f, height/2.0f };
camera->target.X = player->position.x;

if (eveningOut)
{
if (evenOutTarget > camera->target.y)
{
camera->target.Y += evenOutSpeed*delta;

if (camera->target.Y > evenOutTarget)
{
camera->target.Y = evenOutTarget;
eveningOut = 0;
}
}
else
{
camera->target.Y -= evenOutSpeed*delta;

if (camera->target.Y < evenOutTarget)
{
camera->target.Y = evenOutTarget;
eveningOut = 0;
}
}
}
else
{
if (player->canJump && (player->speed == 0) && (player->position.Y != camera->target.y))
{
eveningOut = 1;
evenOutTarget = player->position.y;
}
}
}

void UpdateCameraPlayerBoundsPush(Camera2D *camera, Player *player, EnvItem *envItems, int envItemsLength, float delta, int width, int height)
{
static Vector2 bbox = new( 0.2f, 0.2f );

Vector2 bboxWorldMin = GetScreenToWorld2D((Vector2){ (1 - bbox.x)*0.5f*width, (1 - bbox.y)*0.5f*height }, *camera);
Vector2 bboxWorldMax = GetScreenToWorld2D((Vector2){ (1 + bbox.x)*0.5f*width, (1 + bbox.y)*0.5f*height }, *camera);
camera->offset = (Vector2){ (1 - bbox.x)*0.5f * width, (1 - bbox.y)*0.5f*height };

if (player->position.X < bboxWorldMin.x) camera->target.X = player->position.x;
if (player->position.Y < bboxWorldMin.y) camera->target.Y = player->position.y;
if (player->position.X > bboxWorldMax.x) camera->target.X = bboxWorldMin.X + (player->position.X - bboxWorldMax.x);
if (player->position.Y > bboxWorldMax.y) camera->target.Y = bboxWorldMin.Y + (player->position.Y - bboxWorldMax.y);
}
}
