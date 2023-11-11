using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class AudioMusicStream : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [audio] example - music playing (streaming)");

        InitAudioDevice(); // Initialize audio device

        Music music = LoadMusicStream("resources/country.mp3");

        PlayMusicStream(music);
        bool pause = false; // Music playing paused

        SetTargetFPS(30); // Set our game to run at 30 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateMusicStream(music); // Update music buffer with new stream data

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

            // Get normalized time played for current music stream
            float timePlayed = GetMusicTimePlayed(music) / GetMusicTimeLength(music);

            if (timePlayed > 1.0f)
            {
                timePlayed = 1.0f; // Make sure time played is no longer than music
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawText("MUSIC SHOULD BE PLAYING!", 255, 150, 20, LightGray);

                DrawRectangle(200, 200, 400, 12, LightGray);
                DrawRectangle(200, 200, (int)(timePlayed * 400.0f), 12, Maroon);
                DrawRectangleLines(200, 200, 400, 12, Gray);

                DrawText("PRESS SPACE TO RESTART MUSIC", 215, 250, 20, LightGray);
                DrawText("PRESS P TO PAUSE/RESUME MUSIC", 208, 280, 20, LightGray);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadMusicStream(music); // Unload music stream buffers from RAM

        CloseAudioDevice(); // Close audio device (music streaming is automatically stopped)

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
