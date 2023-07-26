using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class EasingsTestbed : ExampleHelper 
{

private const int FONT_SIZE = 20;

private const int D_STEP = 20.0f;
private const int D_STEP_FINE = 2.0f;
private const int D_MIN = 1.0f;
private const int D_MAX = 10000.0f;

    // Easing types
    enum EasingTypes {
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

    static float NoEase(float t, float b, float c, float d);  // NoEase function declaration, function used when "no easing" is selected for any axis

    // Easing functions reference data
    static const struct {
        string name;
        float (*func)(float, float, float, float);
    } Easings[] = {
        [EASE_LINEAR_NONE] = new( .name = "EaseLinearNone", .func = EaseLinearNone ),
        [EASE_LINEAR_IN] = new( .name = "EaseLinearIn", .func = EaseLinearIn ),
        [EASE_LINEAR_OUT] = new( .name = "EaseLinearOut", .func = EaseLinearOut ),
        [EASE_LINEAR_IN_OUT] = new( .name = "EaseLinearInOut", .func = EaseLinearInOut ),
        [EASE_SINE_IN] = new( .name = "EaseSineIn", .func = EaseSineIn ),
        [EASE_SINE_OUT] = new( .name = "EaseSineOut", .func = EaseSineOut ),
        [EASE_SINE_IN_OUT] = new( .name = "EaseSineInOut", .func = EaseSineInOut ),
        [EASE_CIRC_IN] = new( .name = "EaseCircIn", .func = EaseCircIn ),
        [EASE_CIRC_OUT] = new( .name = "EaseCircOut", .func = EaseCircOut ),
        [EASE_CIRC_IN_OUT] = new( .name = "EaseCircInOut", .func = EaseCircInOut ),
        [EASE_CUBIC_IN] = new( .name = "EaseCubicIn", .func = EaseCubicIn ),
        [EASE_CUBIC_OUT] = new( .name = "EaseCubicOut", .func = EaseCubicOut ),
        [EASE_CUBIC_IN_OUT] = new( .name = "EaseCubicInOut", .func = EaseCubicInOut ),
        [EASE_QUAD_IN] = new( .name = "EaseQuadIn", .func = EaseQuadIn ),
        [EASE_QUAD_OUT] = new( .name = "EaseQuadOut", .func = EaseQuadOut ),
        [EASE_QUAD_IN_OUT] = new( .name = "EaseQuadInOut", .func = EaseQuadInOut ),
        [EASE_EXPO_IN] = new( .name = "EaseExpoIn", .func = EaseExpoIn ),
        [EASE_EXPO_OUT] = new( .name = "EaseExpoOut", .func = EaseExpoOut ),
        [EASE_EXPO_IN_OUT] = new( .name = "EaseExpoInOut", .func = EaseExpoInOut ),
        [EASE_BACK_IN] = new( .name = "EaseBackIn", .func = EaseBackIn ),
        [EASE_BACK_OUT] = new( .name = "EaseBackOut", .func = EaseBackOut ),
        [EASE_BACK_IN_OUT] = new( .name = "EaseBackInOut", .func = EaseBackInOut ),
        [EASE_BOUNCE_OUT] = new( .name = "EaseBounceOut", .func = EaseBounceOut ),
        [EASE_BOUNCE_IN] = new( .name = "EaseBounceIn", .func = EaseBounceIn ),
        [EASE_BOUNCE_IN_OUT] = new( .name = "EaseBounceInOut", .func = EaseBounceInOut ),
        [EASE_ELASTIC_IN] = new( .name = "EaseElasticIn", .func = EaseElasticIn ),
        [EASE_ELASTIC_OUT] = new( .name = "EaseElasticOut", .func = EaseElasticOut ),
        [EASE_ELASTIC_IN_OUT] = new( .name = "EaseElasticInOut", .func = EaseElasticInOut ),
        [EASING_NONE] = new( .name = "None", .func = NoEase ),
    };

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - easings - easings testbed");

        Vector2 ballPosition = new( 100.0f, 200.0f );

        float t = 0.0f;             // Current time (in any unit measure, but same unit as duration)
        float d = 300.0f;           // Total time it should take to complete (duration)
        bool paused = true;
        bool boundedT = true;       // If true, t will stop when d >= td, otherwise t will keep adding td to its value every loop

        int easingX = EASING_NONE;  // Easing selected for x axis
        int easingY = EASING_NONE;  // Easing selected for y axis

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (IsKeyPressed(Key.T)) boundedT = !boundedT;

            // Choose easing for the X axis
            if (IsKeyPressed(Key.Right))
            {
                easingX++;

                if (easingX > EASING_NONE) easingX = 0;
            }
            else if (IsKeyPressed(Key.Left))
            {
                if (easingX == 0) easingX = EASING_NONE;
                else easingX--;
            }

            // Choose easing for the Y axis
            if (IsKeyPressed(Key.Down))
            {
                easingY++;

                if (easingY > EASING_NONE) easingY = 0;
            }
            else if (IsKeyPressed(Key.Up))
            {
                if (easingY == 0) easingY = EASING_NONE;
                else easingY--;
            }

            // Change d (duration) value
            if (IsKeyPressed(Key.W) && d < D_MAX - D_STEP) d += D_STEP;
            else if (IsKeyPressed(Key.Q) && d > D_MIN + D_STEP) d -= D_STEP;

            if (IsKeyDown(Key.S) && d < D_MAX - D_STEP_FINE) d += D_STEP_FINE;
            else if (IsKeyDown(Key.A) && d > D_MIN + D_STEP_FINE) d -= D_STEP_FINE;

            // Play, pause and restart controls
            if (IsKeyPressed(Key.Space) || IsKeyPressed(Key.T) ||
                IsKeyPressed(Key.Right) || IsKeyPressed(Key.Left) ||
                IsKeyPressed(Key.Down) || IsKeyPressed(Key.Up) ||
                IsKeyPressed(Key.W) || IsKeyPressed(Key.Q) ||
                IsKeyDown(Key.S)  || IsKeyDown(Key.A) ||
                (IsKeyPressed(Key.Enter) && (boundedT == true) && (t >= d)))
            {
                t = 0.0f;
                ballPosition.X = 100.0f;
                ballPosition.Y = 100.0f;
                paused = true;
            }

            if (IsKeyPressed(Key.Enter)) paused = !paused;

            // Movement computation
            if (!paused && ((boundedT && t < d) || !boundedT))
            {
                ballPosition.X = Easings[easingX].func(t, 100.0f, 700.0f - 100.0f, d);
                ballPosition.Y = Easings[easingY].func(t, 100.0f, 400.0f - 100.0f, d);
                t += 1.0f;
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                // Draw information text
                DrawText(TextFormat("Easing x: %s", Easings[easingX].name), 0, FONT_SIZE*2, FONT_SIZE, LightGray);
                DrawText(TextFormat("Easing y: %s", Easings[easingY].name), 0, FONT_SIZE*3, FONT_SIZE, LightGray);
                DrawText(TextFormat("t (%c) = %.2f d = %.2f", (boundedT == true)? 'b' : 'u', t, d), 0, FONT_SIZE*4, FONT_SIZE, LightGray);

                // Draw instructions text
                DrawText("Use ENTER to play or pause movement, use SPACE to restart", 0, GetScreenHeight() - FONT_SIZE*2, FONT_SIZE, LightGray);
                DrawText("Use D and W or A and S keys to change duration", 0, GetScreenHeight() - FONT_SIZE*3, FONT_SIZE, LightGray);
                DrawText("Use LEFT or RIGHT keys to choose easing for the x axis", 0, GetScreenHeight() - FONT_SIZE*4, FONT_SIZE, LightGray);
                DrawText("Use UP or DOWN keys to choose easing for the y axis", 0, GetScreenHeight() - FONT_SIZE*5, FONT_SIZE, LightGray);

                // Draw ball
                DrawCircle(ballPosition, 16.0f, Maroon);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();

        return 0;
    }

    // NoEase function, used when "no easing" is selected for any axis. It just ignores all parameters besides b.
    static float NoEase(float t, float b, float c, float d)
    {
        float burn = t + b + c + d;  // Hack to avoid compiler warning (about unused variables)
        d += burn;

        return b;
    }
}
