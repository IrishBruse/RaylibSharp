using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class AudioSoundLoading : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [audio] example - sound loading and playing");

        InitAudioDevice(); // Initialize audio device

        Sound fxWav = LoadSound("resources/sound.wav"); // Load WAV audio file
        Sound fxOgg = LoadSound("resources/target.ogg"); // Load OGG audio file

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.Space))
            {
                PlaySound(fxWav); // Play WAV sound
            }

            if (IsKeyPressed(Key.Enter))
            {
                PlaySound(fxOgg); // Play OGG sound
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawText("Press SPACE to PLAY the WAV sound!", 200, 180, 20, LightGray);
                DrawText("Press ENTER to PLAY the OGG sound!", 200, 220, 20, LightGray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadSound(fxWav); // Unload sound data
        UnloadSound(fxOgg); // Unload sound data

        CloseAudioDevice(); // Close audio device

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
