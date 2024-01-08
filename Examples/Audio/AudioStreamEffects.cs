using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class AudioStreamEffects : ExampleHelper
{
    // Required delay effect variables
    static int delayBufferSize = 48000 * 2;

    // Allocate buffer for the delay effect
    // 1 second delay (device sampleRate*channels)
    static float[] delayBuffer = new float[delayBufferSize];
    static uint delayReadIndex = 2;
    static uint delayWriteIndex;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [audio] example - stream effects");

        InitAudioDevice(); // Initialize audio device

        Music music = LoadMusicStream("resources/country.mp3");

        PlayMusicStream(music);

        float timePlayed = 0.0f; // Time played normalized [0.0f..1.0f]
        bool pause = false; // Music playing paused

        bool enableEffectLPF = false; // Enable effect low-pass-filter
        bool enableEffectDelay = false; // Enable effect delay (1 second)

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

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

            // Add/Remove effect: lowpass filter
            if (IsKeyPressed(Key.F))
            {
                enableEffectLPF = !enableEffectLPF;
                if (enableEffectLPF)
                {
                    AttachAudioStreamProcessor(music.Stream, AudioProcessEffectLPF);
                }
                else
                {
                    DetachAudioStreamProcessor(music.Stream, AudioProcessEffectLPF);
                }
            }

            // Add/Remove effect: delay
            if (IsKeyPressed(Key.D))
            {
                enableEffectDelay = !enableEffectDelay;
                if (enableEffectDelay)
                {
                    AttachAudioStreamProcessor(music.Stream, AudioProcessEffectDelay);
                }
                else
                {
                    DetachAudioStreamProcessor(music.Stream, AudioProcessEffectDelay);
                }
            }

            // Get normalized time played for current music stream
            timePlayed = GetMusicTimePlayed(music) / GetMusicTimeLength(music);

            if (timePlayed > 1.0f)
            {
                timePlayed = 1.0f; // Make sure time played is no longer than music
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                DrawText("MUSIC SHOULD BE PLAYING!", 245, 150, 20, LightGray);

                DrawRectangle(200, 180, 400, 12, LightGray);
                DrawRectangle(200, 180, (int)(timePlayed * 400.0f), 12, Maroon);
                DrawRectangleLines(200, 180, 400, 12, Gray);

                DrawText("PRESS SPACE TO RESTART MUSIC", 215, 230, 20, LightGray);
                DrawText("PRESS P TO PAUSE/RESUME MUSIC", 208, 260, 20, LightGray);

                DrawText("PRESS F TO TOGGLE LPF EFFECT: " + (enableEffectLPF ? "ON" : "OFF"), 200, 320, 20, Gray);
                DrawText("PRESS D TO TOGGLE DELAY EFFECT: " + (enableEffectDelay ? "ON" : "OFF"), 180, 350, 20, Gray);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadMusicStream(music); // Unload music stream buffers from RAM

        CloseAudioDevice(); // Close audio device (music streaming is automatically stopped)

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

    // Module Functions Definition
    // Audio effect: lowpass filter
    static unsafe void AudioProcessEffectLPF(nint buffer, uint frames)
    {
        float[] low = new float[2];
        const float cutoff = 70.0f / 44100.0f; // 70 Hz lowpass filter
        const float k = cutoff / (cutoff + 0.1591549431f); // RC filter formula

        for (uint i = 0; i < frames * 2; i += 2)
        {
            float l = ((float*)buffer)[i], r = ((float*)buffer)[i + 1];
            low[0] += k * (l - low[0]);
            low[1] += k * (r - low[1]);
            ((float*)buffer)[i] = low[0];
            ((float*)buffer)[i + 1] = low[1];
        }
    }

    // Audio effect: delay
    static unsafe void AudioProcessEffectDelay(nint buffer, uint frames)
    {
        for (uint i = 0; i < frames * 2; i += 2)
        {
            float leftDelay = delayBuffer[delayReadIndex++]; // ERROR: Reading buffer . WHY??? Maybe thread related???
            float rightDelay = delayBuffer[delayReadIndex++];

            if (delayReadIndex == delayBufferSize)
            {
                delayReadIndex = 0;
            }
            ((float*)buffer)[i] = (0.5f * ((float*)buffer)[i]) + (0.5f * leftDelay);
            ((float*)buffer)[i + 1] = (0.5f * ((float*)buffer)[i + 1]) + (0.5f * rightDelay);

            delayBuffer[delayWriteIndex++] = ((float*)buffer)[i];
            delayBuffer[delayWriteIndex++] = ((float*)buffer)[i + 1];
            if (delayWriteIndex == delayBufferSize)
            {
                delayWriteIndex = 0;
            }
        }
    }
}
