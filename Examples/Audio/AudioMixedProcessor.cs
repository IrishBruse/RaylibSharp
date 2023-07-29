using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public static partial class AudioMixedProcessor
{
    private static float exponent = 1.0f;                 // Audio exponentiation value
    private static float[] averageVolume = new float[400];   // Average volume history

    // Audio processing function
    private static unsafe void ProcessAudio(IntPtr buffer, uint frames)
    {
        float* samples = (float*)buffer;   // Samples internally stored as <float>s
        float average = 0.0f;               // Temporary average volume

        for (uint frame = 0; frame < frames; frame++)
        {
            float* left = &samples[(frame * 2) + 0];
            float* right = &samples[(frame * 2) + 1];

            *left = MathF.Pow(MathF.Abs(*left), exponent) * ((*left < 0.0f) ? -1.0f : 1.0f);
            *right = MathF.Pow(MathF.Abs(*right), exponent) * ((*right < 0.0f) ? -1.0f : 1.0f);

            average += MathF.Abs(*left) / frames;   // accumulating average volume
            average += MathF.Abs(*right) / frames;
        }

        // Moving history to the left
        for (int i = 0; i < 399; i++)
        {
            averageVolume[i] = averageVolume[i + 1];
        }

        averageVolume[399] = average;         // Adding last average value
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [audio] example - processing mixed output");

        InitAudioDevice();              // Initialize audio device

        AttachAudioMixedProcessor(ProcessAudio);

        Music music = LoadMusicStream("resources/country.mp3");
        Sound sound = LoadSound("resources/coin.wav");

        PlayMusicStream(music);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateMusicStream(music);   // Update music buffer with new stream data

            // Modify processing variables
            if (IsKeyPressed(Key.Left))
            {
                exponent -= 0.05f;
            }

            if (IsKeyPressed(Key.Right))
            {
                exponent += 0.05f;
            }

            if (exponent <= 0.5f)
            {
                exponent = 0.5f;
            }

            if (exponent >= 3.0f)
            {
                exponent = 3.0f;
            }

            if (IsKeyPressed(Key.Space))
            {
                PlaySound(sound);
            }

            // Draw
            BeginDrawing();

            ClearBackground(RayWhite);

            DrawText("MUSIC SHOULD BE PLAYING!", 255, 150, 20, LightGray);

            DrawText("EXPONENT = " + exponent.ToString("0.00"), 215, 180, 20, LightGray);

            DrawRectangle(199, 199, 402, 34, LightGray);
            for (int i = 0; i < 400; i++)
            {
                DrawLine(201 + i, (int)(232 - (averageVolume[i] * 32)), 201 + i, 232, Maroon);
            }
            DrawRectangleLines(199, 199, 402, 34, Gray);

            DrawText("PRESS SPACE TO PLAY OTHER SOUND", 200, 250, 20, LightGray);
            DrawText("USE LEFT AND RIGHT ARROWS TO ALTER DISTORTION", 140, 280, 20, LightGray);

            EndDrawing();
        }

        // De-Initialization
        UnloadMusicStream(music);   // Unload music stream buffers from RAM

        DetachAudioMixedProcessor(ProcessAudio);  // Disconnect audio processor

        CloseAudioDevice();         // Close audio device (music streaming is automatically stopped)

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
