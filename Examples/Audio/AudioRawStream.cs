using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class AudioRawStream : ExampleHelper
{
    static readonly int MAX_SAMPLES = 512;
    static readonly int MAX_SAMPLES_PER_UPDATE = 4096;

    // Cycles per second (hz)
    static float frequency = 440.0f;

    // Audio frequency, for smoothing
    static float audioFrequency = 440.0f;

    // Previous value, used to test if sine needs to be rewritten, and to smoothly modulate frequency
    static float oldFrequency = 1.0f;

    // Index for audio rendering
    static float sineIdx;

    // Audio input processing callback
    static unsafe void AudioInputCallback(IntPtr buffer, uint frames)
    {
        audioFrequency = frequency + ((audioFrequency - frequency) * 0.95f);
        audioFrequency += 1.0f;
        audioFrequency -= 1.0f;
        float incr = audioFrequency / 44100.0f;
        short* d = (short*)buffer;

        for (uint i = 0; i < frames; i++)
        {
            d[i] = (short)(32000.0f * MathF.Sin(2 * MathF.PI * sineIdx));
            sineIdx += incr;
            if (sineIdx > 1.0f)
            {
                sineIdx -= 1.0f;
            }
        }
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [audio] example - raw audio streaming");

        InitAudioDevice(); // Initialize audio device

        SetAudioStreamBufferSizeDefault(MAX_SAMPLES_PER_UPDATE);

        // Init raw audio stream (sample rate: 44100, sample size: 16bit-short, channels: 1-mono)
        AudioStream stream = LoadAudioStream(44100, 16, 1);

        SetAudioStreamCallback(stream, AudioInputCallback);

        // Buffer for the single cycle waveform we are synthesizing
        short[] data = new short[MAX_SAMPLES];

        // Frame buffer, describing the waveform when repeated over the course of a frame
        short[] writeBuf = new short[MAX_SAMPLES_PER_UPDATE];

        PlayAudioStream(stream); // Start processing stream buffer (no data loaded currently)

        // Position read in to determine next frequency
        Vector2 mousePosition = new(-100.0f, -100.0f);

        // Computed size in samples of the sine wave
        int waveLength = 1;

        Vector2 position = new(0, 0);

        SetTargetFPS(30); // Set our game to run at 30 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update

            // Sample mouse input.
            mousePosition = GetMousePosition();

            if (IsMouseButtonDown(MouseButton.Left))
            {
                float fp = mousePosition.Y;
                frequency = 40.0f + (float)fp;

                float pan = mousePosition.X / screenWidth;
                SetAudioStreamPan(stream, pan);
            }

            // Rewrite the sine wave
            // Compute two cycles to allow the buffer padding, simplifying any modulation, resampling, etc.
            if (frequency != oldFrequency)
            {
                // Compute wavelength. Limit size in both directions.
                //int oldWavelength = waveLength;
                waveLength = (int)(22050 / frequency);
                if (waveLength > MAX_SAMPLES / 2)
                {
                    waveLength = MAX_SAMPLES / 2;
                }

                if (waveLength < 1)
                {
                    waveLength = 1;
                }

                // Write sine wave
                for (int i = 0; i < waveLength * 2; i++)
                {
                    data[i] = (short)(MathF.Sin(2 * MathF.PI * i / waveLength) * 32000);
                }
                // Make sure the rest of the line is flat
                for (int j = waveLength * 2; j < MAX_SAMPLES; j++)
                {
                    data[j] = 0;
                }

                // Scale read cursor's position to minimize transition artifacts
                //readCursor = (int)(readCursor * ((float)waveLength / (float)oldWavelength));
                oldFrequency = frequency;
            }

            /*
            // Refill audio stream if required
            if (IsAudioStreamProcessed(stream))
            {
                // Synthesize a buffer that is exactly the requested size
                int writeCursor = 0;

                while (writeCursor < MAX_SAMPLES_PER_UPDATE)
                {
                    // Start by trying to write the whole chunk at once
                    int writeLength = MAX_SAMPLES_PER_UPDATE-writeCursor;

                    // Limit to the maximum readable size
                    int readLength = waveLength-readCursor;

                    if (writeLength > readLength) writeLength = readLength;

                    // Write the slice
                    memcpy(writeBuf + writeCursor, data + readCursor, writeLength*sizeof(short));

                    // Update cursors and loop audio
                    readCursor = (readCursor + writeLength) % waveLength;

                    writeCursor += writeLength;
                }

                // Copy finished frame to audio stream
                UpdateAudioStream(stream, writeBuf, MAX_SAMPLES_PER_UPDATE);
            }
            */

            // Draw
            BeginDrawing();

            ClearBackground(RayWhite);

            DrawText("sine frequency: " + (int)frequency, GetScreenWidth() - 220, 10, 20, Red);
            DrawText("click mouse button to change frequency or pan", 10, 10, 20, DarkGray);

            // Draw the current buffer state proportionate to the screen
            for (int i = 0; i < screenWidth; i++)
            {
                position.X = i;
                position.Y = 250 + (50 * data[i * MAX_SAMPLES / screenWidth] / 32000.0f);

                DrawPixel(position, Red);
            }

            EndDrawing();
        }

        // De-Initialization
        UnloadAudioStream(stream); // Close raw audio stream and delete buffers from RAM
        CloseAudioDevice(); // Close audio device (music streaming is automatically stopped)

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
