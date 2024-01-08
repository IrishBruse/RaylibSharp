using System;
using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class Core2dCameraPlatformer : ExampleHelper
{
    static readonly int G = 400;
    static readonly float PLAYER_JUMP_SPD = 350.0f;
    static readonly float PLAYER_HOR_SPD = 200.0f;

    public class Player
    {
        public Vector2 Position;
        public float Speed;
        public bool CanJump;
    }

    public class EnvItem(RectangleF rect, bool blocking, Color color)
    {
        public RectangleF Rect = rect;
        public bool Blocking = blocking;
        public Color Color = color;
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - 2d camera");

        Player player = new();
        player.Position = new(400, 280);
        player.Speed = 0;
        player.CanJump = false;
        EnvItem[] envItems = {
            new(new( 0, 0, 1000, 400), false, LightGray ),
            new(new( 0, 400, 1000, 200), true, Gray ),
            new(new( 300, 200, 400, 10), true, Gray ),
            new(new( 250, 300, 100, 10), true, Gray ),
            new(new( 650, 300, 100, 10), true, Gray),
        };

        Camera2D camera = new();
        camera.Target = player.Position;
        camera.Offset = new(screenWidth / 2.0f, screenHeight / 2.0f);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        // Store pointers to the multiple update camera functions
        CameraAction[] cameraUpdaters = {
            UpdateCameraCenter,
            UpdateCameraCenterInsideMap,
            UpdateCameraCenterSmoothFollow,
            UpdateCameraEvenOutOnLanding,
            UpdateCameraPlayerBoundsPush
        };

        int cameraOption = 0;

        string[] cameraDescriptions = {
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

            UpdatePlayer(player, envItems, envItems.Length, deltaTime);

            camera.Zoom += GetMouseWheelMove().Y * 0.05f;

            if (camera.Zoom > 3.0f)
            {
                camera.Zoom = 3.0f;
            }
            else if (camera.Zoom < 0.25f)
            {
                camera.Zoom = 0.25f;
            }

            if (IsKeyPressed(Key.R))
            {
                camera.Zoom = 1.0f;
                player.Position = new(400, 280);
            }

            if (IsKeyPressed(Key.C))
            {
                cameraOption = (cameraOption + 1) % cameraUpdaters.Length;
            }

            // Call update camera function by its pointer
            cameraUpdaters[cameraOption].Invoke(ref camera, player, envItems, envItems.Length, deltaTime, screenWidth, screenHeight);

            // Draw
            BeginDrawing();
            {

                ClearBackground(LightGray);

                BeginMode2D(camera);
                {

                    for (int i = 0; i < envItems.Length; i++)
                    {
                        DrawRectangle(envItems[i].Rect, envItems[i].Color);
                    }

                    RectangleF playerRect = new(player.Position.X - 20, player.Position.Y - 40, 40, 40);
                    DrawRectangle(playerRect, Red);

                }
                EndMode2D();

                DrawText("Controls:", 20, 20, 10, Black);
                DrawText("- Right/Left to move", 40, 40, 10, DarkGray);
                DrawText("- Space to jump", 40, 60, 10, DarkGray);
                DrawText("- Mouse Wheel to Zoom in-out, R to reset zoom", 40, 80, 10, DarkGray);
                DrawText("- C to change camera mode", 40, 100, 10, DarkGray);
                DrawText("Current camera mode:", 20, 120, 10, Black);
                DrawText(cameraDescriptions[cameraOption], 40, 140, 10, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }

    static void UpdatePlayer(Player player, EnvItem[] envItems, int envItemsLength, float delta)
    {
        if (IsKeyDown(Key.Left))
        {
            player.Position.X -= PLAYER_HOR_SPD * delta;
        }

        if (IsKeyDown(Key.Right))
        {
            player.Position.X += PLAYER_HOR_SPD * delta;
        }

        if (IsKeyDown(Key.Space) && player.CanJump)
        {
            player.Speed = -PLAYER_JUMP_SPD;
            player.CanJump = false;
        }

        bool hitObstacle = false;
        for (int i = 0; i < envItemsLength; i++)
        {
            EnvItem ei = envItems[i];
            ref Vector2 p = ref player.Position;
            if (ei.Blocking && ei.Rect.X <= p.X && ei.Rect.X + ei.Rect.Width >= p.X && ei.Rect.Y >= p.Y && ei.Rect.Y <= p.Y + (player.Speed * delta))
            {
                hitObstacle = true;
                player.Speed = 0.0f;
                p.Y = ei.Rect.Y;
            }
        }

        if (!hitObstacle)
        {
            player.Position.Y += player.Speed * delta;
            player.Speed += G * delta;
            player.CanJump = false;
        }
        else
        {
            player.CanJump = true;
        }
    }

    static void UpdateCameraCenter(ref Camera2D camera, Player player, EnvItem[] envItems, int envItemsLength, float delta, int width, int height)
    {
        camera.Offset = new(width / 2.0f, height / 2.0f);
        camera.Target = player.Position;
    }

    static void UpdateCameraCenterInsideMap(ref Camera2D camera, Player player, EnvItem[] envItems, int envItemsLength, float delta, int width, int height)
    {
        camera.Target = player.Position;
        camera.Offset = new(width / 2.0f, height / 2.0f);
        float minX = 1000, minY = 1000, maxX = -1000, maxY = -1000;

        for (int i = 0; i < envItemsLength; i++)
        {
            EnvItem ei = envItems[i];
            minX = Math.Min(ei.Rect.X, minX);
            maxX = Math.Max(ei.Rect.X + ei.Rect.Width, maxX);
            minY = Math.Min(ei.Rect.Y, minY);
            maxY = Math.Max(ei.Rect.Y + ei.Rect.Height, maxY);
        }

        Vector2 max = GetWorldToScreen2D(new(maxX, maxY), camera);
        Vector2 min = GetWorldToScreen2D(new(minX, minY), camera);

        if (max.X < width)
        {
            camera.Offset.X = width - (max.X - (width / 2));
        }

        if (max.Y < height)
        {
            camera.Offset.Y = height - (max.Y - (height / 2));
        }

        if (min.X > 0)
        {
            camera.Offset.X = (width / 2) - min.X;
        }

        if (min.Y > 0)
        {
            camera.Offset.Y = (height / 2) - min.Y;
        }
    }

    static void UpdateCameraCenterSmoothFollow(ref Camera2D camera, Player player, EnvItem[] envItems, int envItemsLength, float delta, int width, int height)
    {
        const float minSpeed = 30;
        const float minEffectLength = 10;
        const float fractionSpeed = 0.8f;

        camera.Offset = new(width / 2.0f, height / 2.0f);
        Vector2 diff = player.Position - camera.Target;
        float length = diff.Length();

        if (length > minEffectLength)
        {
            float speed = Math.Max(fractionSpeed * length, minSpeed);
            camera.Target += diff * speed * delta / length;
        }
    }

    static float evenOutTarget;
    static float evenOutSpeed = 700;
    static bool eveningOut;

    static void UpdateCameraEvenOutOnLanding(ref Camera2D camera, Player player, EnvItem[] envItems, int envItemsLength, float delta, int width, int height)
    {

        camera.Offset = new(width / 2.0f, height / 2.0f);
        camera.Target.X = player.Position.X;

        if (eveningOut)
        {
            if (evenOutTarget > camera.Target.Y)
            {
                camera.Target.Y += evenOutSpeed * delta;

                if (camera.Target.Y > evenOutTarget)
                {
                    camera.Target.Y = evenOutTarget;
                    eveningOut = false;
                }
            }
            else
            {
                camera.Target.Y -= evenOutSpeed * delta;

                if (camera.Target.Y < evenOutTarget)
                {
                    camera.Target.Y = evenOutTarget;
                    eveningOut = false;
                }
            }
        }
        else
        {
            if (player.CanJump && (player.Speed == 0) && (player.Position.Y != camera.Target.Y))
            {
                eveningOut = true;
                evenOutTarget = player.Position.Y;
            }
        }
    }

    static void UpdateCameraPlayerBoundsPush(ref Camera2D camera, Player player, EnvItem[] envItems, int envItemsLength, float delta, int width, int height)
    {
        Vector2 bbox = new(0.2f, 0.2f);

        Vector2 bboxWorldMin = GetScreenToWorld2D(new((1 - bbox.X) * 0.5f * width, (1 - bbox.Y) * 0.5f * height), camera);
        Vector2 bboxWorldMax = GetScreenToWorld2D(new((1 + bbox.X) * 0.5f * width, (1 + bbox.Y) * 0.5f * height), camera);
        camera.Offset = new((1 - bbox.X) * 0.5f * width, (1 - bbox.Y) * 0.5f * height);

        if (player.Position.X < bboxWorldMin.X)
        {
            camera.Target.X = player.Position.X;
        }

        if (player.Position.Y < bboxWorldMin.Y)
        {
            camera.Target.Y = player.Position.Y;
        }

        if (player.Position.X > bboxWorldMax.X)
        {
            camera.Target.X = bboxWorldMin.X + (player.Position.X - bboxWorldMax.X);
        }

        if (player.Position.Y > bboxWorldMax.Y)
        {
            camera.Target.Y = bboxWorldMin.Y + (player.Position.Y - bboxWorldMax.Y);
        }
    }

    delegate void CameraAction(ref Camera2D camera, Player player, EnvItem[] envItems, int envItemsLength, float delta, int width, int height);
}
