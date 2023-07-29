using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class AudioStreamEffects : ExampleHelper
{

    // Required delay effect variables
    private static float* delayBuffer = NULL;
    private static uint delayBufferSize;
    private static uint delayReadIndex = 2;
    private static uint delayWriteIndex;

    // Module Functions Declaration
    private static void AudioProcessEffectLPF(System.IntPtr buffer, uint frames);   // Audio effect: lowpass filter
    private static void AudioProcessEffectDelay(System.IntPtr buffer, uint frames); // Audio effect: delay

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "$1 false;stream effects");

        InitAudioDevice();              // Initialize audio device

        Music music = LoadMusicStream("resources/country.mp3");

        // Allocate buffer for the delay effect
        delayBufferSize = 48000 * 2;      // 1 second delay (device sampleRate*channels)
        delayBuffer = (float*)RL_CALLOC(delayBufferSize, sizeof(float));

        PlayMusicStream(music);

        float timePlayed = 0.0f;        // Time played normalized [0.0f..1.0f]
        bool pause = false;             // Music playing paused

        bool enableEffectLPF = false;   // Enable effect low-pass-filter
        bool enableEffectDelay = false; // Enable effect delay (1 second)

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateMusicStream(music);   // Update music buffer with new stream data

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
                    AttachAudioStreamProcessor(music.stream, AudioProcessEffectLPF);
                }
                else
                {
                    DetachAudioStreamProcessor(music.stream, AudioProcessEffectLPF);
                }
            }

            // Add/Remove effect: delay
            if (IsKeyPressed(Key.D))
            {
                enableEffectDelay = !enableEffectDelay;
                if (enableEffectDelay)
                {
                    AttachAudioStreamProcessor(music.stream, AudioProcessEffectDelay);
                }
                else
                {
                    DetachAudioStreamProcessor(music.stream, AudioProcessEffectDelay);
                }
            }

            // Get normalized time played for current music stream
            timePlayed = GetMusicTimePlayed(music) / GetMusicTimeLength(music);

            if (timePlayed > 1.0f)
            {
                timePlayed = 1.0f;   // Make sure time played is no longer than music
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

                DrawText(TextFormat("PRESS F TO TOGGLE LPF EFFECT: %s", enableEffectLPF ? "ON" : "OFF"), 200, 320, 20, Gray);
                DrawText(TextFormat("PRESS D TO TOGGLE DELAY EFFECT: %s", enableEffectDelay ? "ON" : "OFF"), 180, 350, 20, Gray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadMusicStream(music);   // Unload music stream buffers from RAM

        CloseAudioDevice();         // Close audio device (music streaming is automatically stopped)

        RL_FREE(delayBuffer);       // Free delay buffer

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }

    // Module Functions Definition
    // Audio effect: lowpass filter
    private static void AudioProcessEffectLPF(System.IntPtr buffer, uint frames)
    {
        static float[] low = new float[2];
        static const float cutoff = 70.0f / 44100.0f; // 70 Hz lowpass filter
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
    private static void AudioProcessEffectDelay(System.IntPtr buffer, uint frames)
    {
        for (uint i = 0; i < frames * 2; i += 2)
        {
            float leftDelay = delayBuffer[delayReadIndex++];    // ERROR: Reading buffer . WHY??? Maybe thread related???
            float rightDelay = delayBuffer[delayReadIndex++];

            if (delayReadIndex == delayBufferSize)
            {
                delayReadIndex = 0;
            } ((float*)buffer)[i] = (0.5f * ((float*)buffer)[i]) + (0.5f * leftDelay);
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
