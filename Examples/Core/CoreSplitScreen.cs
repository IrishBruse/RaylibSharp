using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;

using Camera = RaylibSharp.Camera3D;

public class Core3dSplitScreen : ExampleHelper
{
    static Camera cameraPlayer1;
    static Camera cameraPlayer2;

    // Scene drawing
    static void DrawScene()
    {
        int count = 5;
        float spacing = 4;

        // Grid of cube trees on a plane to make a "world"
        DrawPlane(new(0, 0, 0), new(50, 50), Beige); // Simple world plane

        for (float x = -count * spacing; x <= count * spacing; x += spacing)
        {
            for (float z = -count * spacing; z <= count * spacing; z += spacing)
            {
                DrawCube(new(x, 1.5f, z), 1, 1, 1, Lime);
                DrawCube(new(x, 0.5f, z), 0.25f, 1, 0.25f, Brown);
            }
        }

        // Draw a cube at each player's position
        DrawCube(cameraPlayer1.Position, 1, 1, 1, Red);
        DrawCube(cameraPlayer2.Position, 1, 1, 1, Blue);
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - split screen");

        // Setup player 1 camera and screen
        cameraPlayer1.Fovy = 45.0f;
        cameraPlayer1.Up.Y = 1.0f;
        cameraPlayer1.Target.Y = 1.0f;
        cameraPlayer1.Position.Z = -3.0f;
        cameraPlayer1.Position.Y = 1.0f;

        RenderTexture screenPlayer1 = LoadRenderTexture(screenWidth / 2, screenHeight);

        // Setup player two camera and screen
        cameraPlayer2.Fovy = 45.0f;
        cameraPlayer2.Up.Y = 1.0f;
        cameraPlayer2.Target.Y = 3.0f;
        cameraPlayer2.Position.X = -3.0f;
        cameraPlayer2.Position.Y = 3.0f;

        RenderTexture screenPlayer2 = LoadRenderTexture(screenWidth / 2, screenHeight);

        // Build a flipped rectangle the size of the split view to use for drawing later
        RectangleF splitScreenRect = new(0.0f, 0.0f, screenPlayer1.Texture.Width, -screenPlayer1.Texture.Height);

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // If anyone moves this frame, how far will they move based on the time since the last frame
            // this moves thigns at 10 world units per second, regardless of the actual FPS
            float offsetThisFrame = 10.0f * GetFrameTime();

            // Move Player1 forward and backwards (no turning)
            if (IsKeyDown(Key.W))
            {
                cameraPlayer1.Position.Z += offsetThisFrame;
                cameraPlayer1.Target.Z += offsetThisFrame;
            }
            else if (IsKeyDown(Key.S))
            {
                cameraPlayer1.Position.Z -= offsetThisFrame;
                cameraPlayer1.Target.Z -= offsetThisFrame;
            }

            // Move Player2 forward and backwards (no turning)
            if (IsKeyDown(Key.Up))
            {
                cameraPlayer2.Position.X += offsetThisFrame;
                cameraPlayer2.Target.X += offsetThisFrame;
            }
            else if (IsKeyDown(Key.Down))
            {
                cameraPlayer2.Position.X -= offsetThisFrame;
                cameraPlayer2.Target.X -= offsetThisFrame;
            }

            // Draw
            // Draw Player1 view to the render texture
            BeginTextureMode(screenPlayer1);
            ClearBackground(SkyBlue);
            BeginMode3D(cameraPlayer1);
            DrawScene();
            EndMode3D();
            DrawText("PLAYER1 W/S to move", 10, 10, 20, Red);
            EndTextureMode();

            // Draw Player2 view to the render texture
            BeginTextureMode(screenPlayer2);
            ClearBackground(SkyBlue);
            BeginMode3D(cameraPlayer2);
            DrawScene();
            EndMode3D();
            DrawText("PLAYER2 UP/DOWN to move", 10, 10, 20, Blue);
            EndTextureMode();

            // Draw both views render textures to the screen side by side
            BeginDrawing();
            {
                ClearBackground(Black);
                DrawTexture(screenPlayer1.Texture, splitScreenRect, new(0, 0), White);
                DrawTexture(screenPlayer2.Texture, splitScreenRect, new(screenWidth / 2.0f, 0), White);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadRenderTexture(screenPlayer1); // Unload render texture
        UnloadRenderTexture(screenPlayer2); // Unload render texture

        CloseWindow();

        return 0;
    }
}
