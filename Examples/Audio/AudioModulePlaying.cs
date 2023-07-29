using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class AudioModulePlaying
{
    private static readonly int MAX_CIRCLES = 64;

    private sealed class CircleWave
    {
        public Vector2 position;
        public float radius;
        public float alpha;
        public float speed;
        public Color color;
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint);  // NOTE: Try to enable MSAA 4X

        InitWindow(screenWidth, screenHeight, "raylib [audio] example - module playing (streaming)");

        InitAudioDevice();                  // Initialize audio device

        Color[] colors = { Orange, Red, Gold, Lime, Blue, Violet, Brown, LightGray, Pink, Yellow, Green, SkyBlue, Purple, Beige };

        // Creates some circles for visual effect
        CircleWave[] circles = new CircleWave[MAX_CIRCLES];

        for (int i = MAX_CIRCLES - 1; i >= 0; i--)
        {
            circles[i].alpha = 0.0f;
            circles[i].radius = GetRandomValue(10, 40);
            circles[i].position.X = GetRandomValue((int)circles[i].radius, (int)(screenWidth - circles[i].radius));
            circles[i].position.Y = GetRandomValue((int)circles[i].radius, (int)(screenHeight - circles[i].radius));
            circles[i].speed = GetRandomValue(1, 100) / 2000.0f;
            circles[i].color = colors[GetRandomValue(0, 13)];
        }

        Music music = LoadMusicStream("resources/mini1111.xm");
        music.Looping = false;
        float pitch = 1.0f;

        PlayMusicStream(music);
        bool pause = false;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateMusicStream(music);      // Update music buffer with new stream data

            // Restart music playing (stop and play)
            if (IsKeyPressed(Key.Space))
            {
                StopMusicStream(music);
                PlayMusicStream(music);
            }

            // Pause/Resume music playing
            if (IsKeyPressed(Key.P))
            {
                pause = !pause;

                if (pause)
                {
                    PauseMusicStream(music);
                }
                else
                {
                    ResumeMusicStream(music);
                }
            }

            if (IsKeyDown(Key.Down))
            {
                pitch -= 0.01f;
            }
            else if (IsKeyDown(Key.Up))
            {
                pitch += 0.01f;
            }

            SetMusicPitch(music, pitch);

            // Get timePlayed scaled to bar dimensions
            float timePlayed = GetMusicTimePlayed(music) / GetMusicTimeLength(music) * (screenWidth - 40);

            // Color circles animation
            for (int i = MAX_CIRCLES - 1; (i >= 0) && !pause; i--)
            {
                circles[i].alpha += circles[i].speed;
                circles[i].radius += circles[i].speed * 10.0f;

                if (circles[i].alpha > 1.0f)
                {
                    circles[i].speed *= -1;
                }

                if (circles[i].alpha <= 0.0f)
                {
                    circles[i].alpha = 0.0f;
                    circles[i].radius = GetRandomValue(10, 40);
                    circles[i].position.X = GetRandomValue((int)circles[i].radius, (int)(screenWidth - circles[i].radius));
                    circles[i].position.Y = GetRandomValue((int)circles[i].radius, (int)(screenHeight - circles[i].radius));
                    circles[i].color = colors[GetRandomValue(0, 13)];
                    circles[i].speed = GetRandomValue(1, 100) / 2000.0f;
                }
            }

            // Draw
            BeginDrawing();

            ClearBackground(RayWhite);

            for (int i = MAX_CIRCLES - 1; i >= 0; i--)
            {
                DrawCircle(circles[i].position, circles[i].radius, Fade(circles[i].color, circles[i].alpha));
            }

            // Draw time bar
            DrawRectangle(20, screenHeight - 20 - 12, screenWidth - 40, 12, LightGray);
            DrawRectangle(20, screenHeight - 20 - 12, (int)timePlayed, 12, Maroon);
            DrawRectangleLines(20, screenHeight - 20 - 12, screenWidth - 40, 12, Gray);

            EndDrawing();
        }

        // De-Initialization
        UnloadMusicStream(music);          // Unload music stream buffers from RAM

        CloseAudioDevice();     // Close audio device (music streaming is automatically stopped)

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
