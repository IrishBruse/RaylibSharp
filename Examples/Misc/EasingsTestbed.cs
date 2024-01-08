using System.Collections.Generic;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class EasingsTestbed : ExampleHelper
{
    const int FONT_SIZE = 20;

    const float D_STEP = 20.0f;
    const float D_STEP_FINE = 2.0f;
    const float D_MIN = 1.0f;
    const float D_MAX = 10000.0f;
    static Dictionary<EasingTypes, Easing> Easings = new()
    {
        {EasingTypes.EASE_LINEAR_NONE ,new Easing("EaseLinearNone", EaseLinearNone)},
        {EasingTypes.EASE_LINEAR_IN ,new Easing("EaseLinearIn", EaseLinearIn)},
        {EasingTypes.EASE_LINEAR_OUT ,new Easing("EaseLinearOut", EaseLinearOut)},
        {EasingTypes.EASE_LINEAR_IN_OUT ,new Easing("EaseLinearInOut", EaseLinearInOut)},
        {EasingTypes.EASE_SINE_IN ,new Easing("EaseSineIn", EaseSineIn)},
        {EasingTypes.EASE_SINE_OUT ,new Easing("EaseSineOut", EaseSineOut)},
        {EasingTypes.EASE_SINE_IN_OUT ,new Easing("EaseSineInOut", EaseSineInOut)},
        {EasingTypes.EASE_CIRC_IN ,new Easing("EaseCircIn", EaseCircIn)},
        {EasingTypes.EASE_CIRC_OUT ,new Easing("EaseCircOut", EaseCircOut)},
        {EasingTypes.EASE_CIRC_IN_OUT ,new Easing("EaseCircInOut", EaseCircInOut)},
        {EasingTypes.EASE_CUBIC_IN ,new Easing("EaseCubicIn", EaseCubicIn)},
        {EasingTypes.EASE_CUBIC_OUT ,new Easing("EaseCubicOut", EaseCubicOut)},
        {EasingTypes.EASE_CUBIC_IN_OUT ,new Easing("EaseCubicInOut", EaseCubicInOut)},
        {EasingTypes.EASE_QUAD_IN ,new Easing("EaseQuadIn", EaseQuadIn)},
        {EasingTypes.EASE_QUAD_OUT ,new Easing("EaseQuadOut", EaseQuadOut)},
        {EasingTypes.EASE_QUAD_IN_OUT ,new Easing("EaseQuadInOut", EaseQuadInOut)},
        {EasingTypes.EASE_EXPO_IN ,new Easing("EaseExpoIn", EaseExpoIn)},
        {EasingTypes.EASE_EXPO_OUT ,new Easing("EaseExpoOut", EaseExpoOut)},
        {EasingTypes.EASE_EXPO_IN_OUT ,new Easing("EaseExpoInOut", EaseExpoInOut)},
        {EasingTypes.EASE_BACK_IN ,new Easing("EaseBackIn", EaseBackIn)},
        {EasingTypes.EASE_BACK_OUT ,new Easing("EaseBackOut", EaseBackOut)},
        {EasingTypes.EASE_BACK_IN_OUT ,new Easing("EaseBackInOut", EaseBackInOut)},
        {EasingTypes.EASE_BOUNCE_OUT ,new Easing("EaseBounceOut", EaseBounceOut)},
        {EasingTypes.EASE_BOUNCE_IN ,new Easing("EaseBounceIn", EaseBounceIn)},
        {EasingTypes.EASE_BOUNCE_IN_OUT ,new Easing("EaseBounceInOut", EaseBounceInOut)},
        {EasingTypes.EASE_ELASTIC_IN ,new Easing("EaseElasticIn", EaseElasticIn)},
        {EasingTypes.EASE_ELASTIC_OUT ,new Easing("EaseElasticOut", EaseElasticOut)},
        {EasingTypes.EASE_ELASTIC_IN_OUT ,new Easing("EaseElasticInOut", EaseElasticInOut)},
        {EasingTypes.EASING_NONE ,new Easing("None", NoEase)},
    };

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - easings - easings testbed");

        Vector2 ballPosition = new(100.0f, 200.0f);

        float t = 0.0f; // Current time (in any unit measure, but same unit as duration)
        float d = 300.0f; // Total time it should take to complete (duration)
        bool paused = true;
        bool boundedT = true; // If true, t will stop when d >= td, otherwise t will keep adding td to its value every loop

        EasingTypes easingX = EasingTypes.EASING_NONE; // Easing selected for x axis
        EasingTypes easingY = EasingTypes.EASING_NONE; // Easing selected for y axis

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.T))
            {
                boundedT = !boundedT;
            }

            // Choose easing for the X axis
            if (IsKeyPressed(Key.Right))
            {
                easingX++;

                if (easingX > EasingTypes.EASING_NONE)
                {
                    easingX = 0;
                }
            }
            else if (IsKeyPressed(Key.Left))
            {
                if (easingX == 0)
                {
                    easingX = EasingTypes.EASING_NONE;
                }
                else
                {
                    easingX--;
                }
            }

            // Choose easing for the Y axis
            if (IsKeyPressed(Key.Down))
            {
                easingY++;

                if (easingY > EasingTypes.EASING_NONE)
                {
                    easingY = 0;
                }
            }
            else if (IsKeyPressed(Key.Up))
            {
                if (easingY == 0)
                {
                    easingY = EasingTypes.EASING_NONE;
                }
                else
                {
                    easingY--;
                }
            }

            // Change d (duration) value
            if (IsKeyPressed(Key.W) && d < D_MAX - D_STEP)
            {
                d += D_STEP;
            }
            else if (IsKeyPressed(Key.Q) && d > D_MIN + D_STEP)
            {
                d -= D_STEP;
            }

            if (IsKeyDown(Key.S) && d < D_MAX - D_STEP_FINE)
            {
                d += D_STEP_FINE;
            }
            else if (IsKeyDown(Key.A) && d > D_MIN + D_STEP_FINE)
            {
                d -= D_STEP_FINE;
            }

            // Play, pause and restart controls
            if (IsKeyPressed(Key.Space) || IsKeyPressed(Key.T) ||
                IsKeyPressed(Key.Right) || IsKeyPressed(Key.Left) ||
                IsKeyPressed(Key.Down) || IsKeyPressed(Key.Up) ||
                IsKeyPressed(Key.W) || IsKeyPressed(Key.Q) ||
                IsKeyDown(Key.S) || IsKeyDown(Key.A) ||
                (IsKeyPressed(Key.Enter) && boundedT && (t >= d)))
            {
                t = 0.0f;
                ballPosition.X = 100.0f;
                ballPosition.Y = 100.0f;
                paused = true;
            }

            if (IsKeyPressed(Key.Enter))
            {
                paused = !paused;
            }

            // Movement computation
            if (!paused && ((boundedT && t < d) || !boundedT))
            {
                ballPosition.X = Easings[easingX].func(t, 100.0f, 700.0f - 100.0f, d);
                ballPosition.Y = Easings[easingY].func(t, 100.0f, 400.0f - 100.0f, d);
                t += 1.0f;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // Draw information text
                DrawText("Easing x: " + Easings[easingX].name, 0, FONT_SIZE * 2, FONT_SIZE, LightGray);
                DrawText("Easing y: " + Easings[easingY].name, 0, FONT_SIZE * 3, FONT_SIZE, LightGray);
                char c = boundedT ? 'b' : 'u';
                DrawText($"t ({c}) = {t} d = {d}", 0, FONT_SIZE * 4, FONT_SIZE, LightGray);

                // Draw instructions text
                DrawText("Use ENTER to play or pause movement, use SPACE to restart", 0, GetScreenHeight() - (FONT_SIZE * 2), FONT_SIZE, LightGray);
                DrawText("Use D and W or A and S keys to change duration", 0, GetScreenHeight() - (FONT_SIZE * 3), FONT_SIZE, LightGray);
                DrawText("Use LEFT or RIGHT keys to choose easing for the x axis", 0, GetScreenHeight() - (FONT_SIZE * 4), FONT_SIZE, LightGray);
                DrawText("Use UP or DOWN keys to choose easing for the y axis", 0, GetScreenHeight() - (FONT_SIZE * 5), FONT_SIZE, LightGray);

                // Draw ball
                DrawCircle(ballPosition, 16.0f, Maroon);

            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }

    // NoEase function, used when "no easing" is selected for any axis. It just ignores all parameters besides b.
    static float NoEase(float t, float b, float c, float d)
    {
        return b;
    }

    struct Easing(string name, EasingFunction func)
    {
        public string name = name;
        public EasingFunction func = func;
    }

    // Easing types
    enum EasingTypes
    {
        EASE_LINEAR_NONE = 0,
        EASE_LINEAR_IN,
        EASE_LINEAR_OUT,
        EASE_LINEAR_IN_OUT,
        EASE_SINE_IN,
        EASE_SINE_OUT,
        EASE_SINE_IN_OUT,
        EASE_CIRC_IN,
        EASE_CIRC_OUT,
        EASE_CIRC_IN_OUT,
        EASE_CUBIC_IN,
        EASE_CUBIC_OUT,
        EASE_CUBIC_IN_OUT,
        EASE_QUAD_IN,
        EASE_QUAD_OUT,
        EASE_QUAD_IN_OUT,
        EASE_EXPO_IN,
        EASE_EXPO_OUT,
        EASE_EXPO_IN_OUT,
        EASE_BACK_IN,
        EASE_BACK_OUT,
        EASE_BACK_IN_OUT,
        EASE_BOUNCE_OUT,
        EASE_BOUNCE_IN,
        EASE_BOUNCE_IN_OUT,
        EASE_ELASTIC_IN,
        EASE_ELASTIC_OUT,
        EASE_ELASTIC_IN_OUT,
        NUM_EASING_TYPES,
        EASING_NONE = NUM_EASING_TYPES
    };
}
