using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreBasicScreenManager : ExampleHelper
{

    // Types and Structures Definition
    enum GameScreen
    {
        LOGO = 0,
        TITLE,
        GAMEPLAY,
        ENDING
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - basic screen manager");

        GameScreen currentScreen = GameScreen.LOGO;

        // TODO: Initialize all required variables and load all required data here!

        int framesCounter = 0; // Useful to count frames

        SetTargetFPS(60); // Set desired framerate (frames-per-second)

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            switch (currentScreen)
            {
                case GameScreen.LOGO:
                {
                    // TODO: Update LOGO screen variables here!

                    framesCounter++; // Count frames

                    // Wait for 2 seconds (120 frames) before jumping to TITLE screen
                    if (framesCounter > 120)
                    {
                        currentScreen = GameScreen.TITLE;
                    }
                }
                break;
                case GameScreen.TITLE:
                {
                    // TODO: Update TITLE screen variables here!

                    // Press enter to change to GAMEPLAY screen
                    if (IsKeyPressed(Key.Enter))// TODO add back || isgesturedetected(gestureTap)
                    {
                        currentScreen = GameScreen.GAMEPLAY;
                    }
                }
                break;
                case GameScreen.GAMEPLAY:
                {
                    // TODO: Update GAMEPLAY screen variables here!

                    // Press enter to change to ENDING screen
                    if (IsKeyPressed(Key.Enter))// TODO add back || isgesturedetected(gestureTap)
                    {
                        currentScreen = GameScreen.ENDING;
                    }
                }
                break;
                case GameScreen.ENDING:
                {
                    // TODO: Update ENDING screen variables here!

                    // Press enter to return to TITLE screen
                    if (IsKeyPressed(Key.Enter))// TODO add back || isgesturedetected(gestureTap)
                    {
                        currentScreen = GameScreen.TITLE;
                    }
                }
                break;
                default: break;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                switch (currentScreen)
                {
                    case GameScreen.LOGO:
                    // TODO: Draw LOGO screen here!
                    DrawText("LOGO SCREEN", 20, 20, 40, LightGray);
                    DrawText("WAIT for 2 SECONDS...", 290, 220, 20, Gray);
                    break;

                    case GameScreen.TITLE:
                    // TODO: Draw TITLE screen here!
                    DrawRectangle(0, 0, screenWidth, screenHeight, Green);
                    DrawText("TITLE SCREEN", 20, 20, 40, DarkGreen);
                    DrawText("PRESS ENTER or TAP to JUMP to GAMEPLAY SCREEN", 120, 220, 20, DarkGreen);
                    break;

                    case GameScreen.GAMEPLAY:
                    // TODO: Draw GAMEPLAY screen here!
                    DrawRectangle(0, 0, screenWidth, screenHeight, Purple);
                    DrawText("GAMEPLAY SCREEN", 20, 20, 40, Maroon);
                    DrawText("PRESS ENTER or TAP to JUMP to ENDING SCREEN", 130, 220, 20, Maroon);
                    break;

                    case GameScreen.ENDING:
                    // TODO: Draw ENDING screen here!
                    DrawRectangle(0, 0, screenWidth, screenHeight, Blue);
                    DrawText("ENDING SCREEN", 20, 20, 40, DarkBlue);
                    DrawText("PRESS ENTER or TAP to RETURN to TITLE SCREEN", 120, 220, 20, DarkBlue);
                    break;

                    default: break;
                }

            }
            EndDrawing();
        }

        // De-Initialization

        // TODO: Unload all loaded data (textures, fonts, audio) here!

        CloseWindow();

        return 0;
    }
}
