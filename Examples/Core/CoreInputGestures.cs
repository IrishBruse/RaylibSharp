using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public class CoreInputGestures : ExampleHelper
{
    static readonly int MAX_GESTURE_STRINGS = 20;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - core - input gestures");
        RectangleF touchArea = new(220, 10, screenWidth - 230.0f, screenHeight - 20.0f);

        int gesturesCount = 0;
        string[] gestureStrings = new string[MAX_GESTURE_STRINGS];

        Gesture currentGesture = Gesture.None;

        //SetGesturesEnabled(0b0000000000001001); // Enable only some gestures to be detected

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            Gesture lastGesture = currentGesture;
            currentGesture = GetGestureDetected();
            Vector2 touchPosition = GetTouchPosition(0);

            if (CheckCollisionPoint(touchPosition, touchArea) && (currentGesture != Gesture.None))
            {
                if (currentGesture != lastGesture)
                {
                    // Store gesture string
                    gestureStrings[gesturesCount] = currentGesture switch
                    {
                        Gesture.Tap => "GESTURE TAP",
                        Gesture.Doubletap => "GESTURE DOUBLETAP",
                        Gesture.Hold => "GESTURE HOLD",
                        Gesture.Drag => "GESTURE DRAG",
                        Gesture.SwipeRight => "GESTURE SWIPE RIGHT",
                        Gesture.SwipeLeft => "GESTURE SWIPE LEFT",
                        Gesture.SwipeUp => "GESTURE SWIPE UP",
                        Gesture.SwipeDown => "GESTURE SWIPE DWON",
                        Gesture.PinchIn => "GESTURE PINCH IN",
                        Gesture.PinchOut => "GESTURE PINCH OUT",
                        _ => "GESTURE NONE",
                    };

                    gesturesCount++;

                    // Reset gestures strings
                    if (gesturesCount >= MAX_GESTURE_STRINGS)
                    {
                        for (int i = 0; i < MAX_GESTURE_STRINGS; i++)
                        {
                            gestureStrings[i] = string.Empty;
                        }

                        gesturesCount = 0;
                    }
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawRectangle(touchArea, Gray);
                DrawRectangle(225, 15, screenWidth - 240, screenHeight - 30, RayWhite);

                DrawText("GESTURES TEST AREA", screenWidth - 270, screenHeight - 40, 20, Fade(Gray, 0.5f));

                for (int i = 0; i < gesturesCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        DrawRectangle(10, 30 + (20 * i), 200, 20, Fade(LightGray, 0.5f));
                    }
                    else
                    {
                        DrawRectangle(10, 30 + (20 * i), 200, 20, Fade(LightGray, 0.3f));
                    }

                    if (i < gesturesCount - 1)
                    {
                        DrawText(gestureStrings[i], 35, 36 + (20 * i), 10, DarkGray);
                    }
                    else
                    {
                        DrawText(gestureStrings[i], 35, 36 + (20 * i), 10, Maroon);
                    }
                }

                DrawRectangleLines(10, 29, 200, screenHeight - 50, Gray);
                DrawText("DETECTED GESTURES", 50, 15, 10, Gray);

                if (currentGesture != Gesture.None)
                {
                    DrawCircle(touchPosition, 30, Maroon);
                }
            }
            EndDrawing();
        }

        CloseWindow();

        return 0;
    }
}
