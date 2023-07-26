using RaylibSharp;

using static RaylibSharp.Raylib;

using Texture2D = RaylibSharp.Texture;

public class CoreInputGamepad : ExampleHelper
{
    private const string XBOX360_NAME_ID = "Xbox 360 Controller";
    private const string PS3_NAME_ID = "PLAYSTATION(R)3 Controller";

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.Msaa4xHint);  // Set MSAA 4X hint before windows creation

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - gamepad input");

        Texture2D texPs3Pad = LoadTexture("resources/ps3.png");
        Texture2D texXboxPad = LoadTexture("resources/xbox.png");

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // ...

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                if (IsGamepadAvailable(0))
                {
                    DrawText("GP1: " + GetGamepadName(0), 10, 10, 10, Black);

                    if (GetGamepadName(0) == XBOX360_NAME_ID)
                    {
                        DrawTexture(texXboxPad, 0, 0, DarkGray);

                        // Draw buttons: xbox home
                        if (IsGamepadButtonDown(0, GamepadButton.Middle))
                        {
                            DrawCircle(394, 89, 19, Red);
                        }

                        // Draw buttons: basic
                        if (IsGamepadButtonDown(0, GamepadButton.MiddleRight))
                        {
                            DrawCircle(436, 150, 9, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.MiddleLeft))
                        {
                            DrawCircle(352, 150, 9, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceLeft))
                        {
                            DrawCircle(501, 151, 15, Blue);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceDown))
                        {
                            DrawCircle(536, 187, 15, Lime);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceRight))
                        {
                            DrawCircle(572, 151, 15, Maroon);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceUp))
                        {
                            DrawCircle(536, 115, 15, Gold);
                        }

                        // Draw buttons: d-pad
                        DrawRectangle(317, 202, 19, 71, Black);
                        DrawRectangle(293, 228, 69, 19, Black);
                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceUp))
                        {
                            DrawRectangle(317, 202, 19, 26, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceDown))
                        {
                            DrawRectangle(317, 202 + 45, 19, 26, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceLeft))
                        {
                            DrawRectangle(292, 228, 25, 19, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceRight))
                        {
                            DrawRectangle(292 + 44, 228, 26, 19, Red);
                        }

                        // Draw buttons: left-right back
                        if (IsGamepadButtonDown(0, GamepadButton.LeftTrigger1))
                        {
                            DrawCircle(259, 61, 20, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightTrigger1))
                        {
                            DrawCircle(536, 61, 20, Red);
                        }

                        // Draw axis: left joystick
                        DrawCircle(259, 152, 39, Black);
                        DrawCircle(259, 152, 34, LightGray);
                        DrawCircle(259 + (int)(GetGamepadAxisMovement(0, GamepadAxis.LeftX) * 20),
                        152 + (int)(GetGamepadAxisMovement(0, GamepadAxis.LeftY) * 20), 25, Black);

                        // Draw axis: right joystick
                        DrawCircle(461, 237, 38, Black);
                        DrawCircle(461, 237, 33, LightGray);
                        DrawCircle(461 + (int)(GetGamepadAxisMovement(0, GamepadAxis.RightX) * 20),
                        237 + (int)(GetGamepadAxisMovement(0, GamepadAxis.RightY) * 20), 25, Black);

                        // Draw axis: left-right triggers
                        DrawRectangle(170, 30, 15, 70, Gray);
                        DrawRectangle(604, 30, 15, 70, Gray);
                        DrawRectangle(170, 30, 15, (int)((1 + GetGamepadAxisMovement(0, GamepadAxis.LeftTrigger)) / 2 * 70), Red);
                        DrawRectangle(604, 30, 15, (int)((1 + GetGamepadAxisMovement(0, GamepadAxis.RightTrigger)) / 2 * 70), Red);

                        //DrawText(TextFormat("Xbox axis LT: %02.02f", GetGamepadAxisMovement(0, GamepadAxis.LEFT_TRIGGER)), 10, 40, 10, Black);
                        //DrawText(TextFormat("Xbox axis RT: %02.02f", GetGamepadAxisMovement(0, GamepadAxis.RIGHT_TRIGGER)), 10, 60, 10, Black);
                    }
                    else if (GetGamepadName(0) == PS3_NAME_ID)
                    {
                        DrawTexture(texPs3Pad, 0, 0, DarkGray);

                        // Draw buttons: ps
                        if (IsGamepadButtonDown(0, GamepadButton.Middle))
                        {
                            DrawCircle(396, 222, 13, Red);
                        }

                        // Draw buttons: basic
                        if (IsGamepadButtonDown(0, GamepadButton.MiddleLeft))
                        {
                            DrawRectangle(328, 170, 32, 13, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.MiddleRight))
                        {
                            DrawTriangle(new(436, 168), new(436, 185), new(464, 177), Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceUp))
                        {
                            DrawCircle(557, 144, 13, Lime);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceRight))
                        {
                            DrawCircle(586, 173, 13, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceDown))
                        {
                            DrawCircle(557, 203, 13, Violet);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightFaceLeft))
                        {
                            DrawCircle(527, 173, 13, Pink);
                        }

                        // Draw buttons: d-pad
                        DrawRectangle(225, 132, 24, 84, Black);
                        DrawRectangle(195, 161, 84, 25, Black);
                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceUp))
                        {
                            DrawRectangle(225, 132, 24, 29, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceDown))
                        {
                            DrawRectangle(225, 132 + 54, 24, 30, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceLeft))
                        {
                            DrawRectangle(195, 161, 30, 25, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.LeftFaceRight))
                        {
                            DrawRectangle(195 + 54, 161, 30, 25, Red);
                        }

                        // Draw buttons: left-right back buttons
                        if (IsGamepadButtonDown(0, GamepadButton.LeftTrigger1))
                        {
                            DrawCircle(239, 82, 20, Red);
                        }

                        if (IsGamepadButtonDown(0, GamepadButton.RightTrigger1))
                        {
                            DrawCircle(557, 82, 20, Red);
                        }

                        // Draw axis: left joystick
                        DrawCircle(319, 255, 35, Black);
                        DrawCircle(319, 255, 31, LightGray);
                        DrawCircle(319 + (int)(GetGamepadAxisMovement(0, GamepadAxis.LeftX) * 20),
                        255 + (int)(GetGamepadAxisMovement(0, GamepadAxis.LeftY) * 20), 25, Black);

                        // Draw axis: right joystick
                        DrawCircle(475, 255, 35, Black);
                        DrawCircle(475, 255, 31, LightGray);
                        DrawCircle(475 + (int)(GetGamepadAxisMovement(0, GamepadAxis.RightX) * 20),
                        255 + (int)(GetGamepadAxisMovement(0, GamepadAxis.RightY) * 20), 25, Black);

                        // Draw axis: left-right triggers
                        DrawRectangle(169, 48, 15, 70, Gray);
                        DrawRectangle(611, 48, 15, 70, Gray);
                        DrawRectangle(169, 48, 15, (int)((1 - GetGamepadAxisMovement(0, GamepadAxis.LeftTrigger)) / 2 * 70), Red);
                        DrawRectangle(611, 48, 15, (int)((1 - GetGamepadAxisMovement(0, GamepadAxis.RightTrigger)) / 2 * 70), Red);
                    }
                    else
                    {
                        DrawText("- GENERIC GAMEPAD -", 280, 180, 20, Gray);

                        // TODO: Draw generic gamepad
                    }

                    DrawText($"DETECTED AXIS [{GetGamepadAxisCount(0)}]:", 10, 50, 10, Maroon);

                    for (int i = 0; i < GetGamepadAxisCount(0); i++)
                    {
                        DrawText($"AXIS {i}: {GetGamepadAxisMovement(0, (GamepadAxis)i)}", 20, 70 + (20 * i), 10, DarkGray);
                    }

                    if (GetGamepadButtonPressed() != GamepadButton.Unknown)
                    {
                        DrawText("DETECTED BUTTON: " + GetGamepadButtonPressed(), 10, 430, 10, Red);
                    }
                    else
                    {
                        DrawText("DETECTED BUTTON: NONE", 10, 430, 10, Gray);
                    }
                }
                else
                {
                    DrawText("GP1: NOT DETECTED", 10, 10, 10, Gray);

                    DrawTexture(texXboxPad, 0, 0, LightGray);
                }

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texPs3Pad);
        UnloadTexture(texXboxPad);

        CloseWindow();

        return 0;
    }
}
