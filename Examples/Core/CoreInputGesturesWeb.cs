/*******************************************************************************************
*
*   raylib [core] example - Input Gestures for Web
*
*   Example originally created with raylib 4.6-dev, last time updated with raylib 4.6-dev
*
*   Example contributed by ubkp (@ubkp) and reviewed by Ramon Santamaria (@raysan5)
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2023 ubkp (@ubkp)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreInputGesturesWeb : ExampleHelper
{
    #if defined(PLATFORM_WEB)
    #endif

    //--------------------------------------------------------------------------------------
    // Global definitions and declarations
    //--------------------------------------------------------------------------------------

    // Common variables definitions
    //--------------------------------------------------------------------------------------
    int screenWidth = 800;                  // Update depending on web canvas
    const int screenHeight = 450;
    Vector2 messagePosition = new(160, 7);

    // Last gesture variables definitions
    //--------------------------------------------------------------------------------------
    int lastGesture = 0;
    Vector2 lastGesturePosition = new(165, 130);

    // Gesture log variables definitions and functions declarations
    //--------------------------------------------------------------------------------------
    const int GESTURE_LOG_SIZE = 20;
    char[] gestureLog = new char[GESTURE_LOG_SIZE][12]new(""); // The gesture log uses an array (as an inverted circular queue) to store the performed gestures
    int gestureLogIndex = GESTURE_LOG_SIZE;         // The index for the inverted circular queue (moving from last to first direction, then looping around)
    int previousGesture = 0;

    char const *GetGestureName(int i)
    {
       switch (i)  {
          case 0:   return "None";        break;
          case 1:   return "Tap";         break;
          case 2:   return "Double Tap";  break;
          case 4:   return "Hold";        break;
          case 8:   return "Drag";        break;
          case 16:  return "Swipe Right"; break;
          case 32:  return "Swipe Left";  break;
          case 64:  return "Swipe Up";    break;
          case 128: return "Swipe Down";  break;
          case 256: return "Pinch In";    break;
          case 512: return "Pinch Out";   break;
          default:  return "Unknown";     break;
       }
    }

    Color GetGestureColor(int i)
    {
       switch (i)  {
          case 0:   return BLACK;   break;
          case 1:   return BLUE;    break;
          case 2:   return SKYBLUE; break;
          case 4:   return BLACK;   break;
          case 8:   return LIME;    break;
          case 16:  return RED;     break;
          case 32:  return RED;     break;
          case 64:  return RED;     break;
          case 128: return RED;     break;
          case 256: return VIOLET;  break;
          case 512: return ORANGE;  break;
          default:  return BLACK;   break;
       }
    }

    int logMode = 1; // Log mode values: 0 shows repeated events; 1 hides repeated events; 2 shows repeated events but hide hold events; 3 hides repeated events and hide hold events

    Color gestureColor = new(0, 0, 0, 255);
    Rectangle logButton1 = new(53, 7, 48, 26);
    Rectangle logButton2 = new(108, 7, 36, 26);
    Vector2 gestureLogPosition = new(10, 10);

    // Protractor variables definitions
    //--------------------------------------------------------------------------------------
    float angleLength = 90.0f;
    float currentAngleDegrees = 0.0f;
    Vector2 finalVector = new(0.0f, 0.0f);
    char[] currentAngleStr = new char[7]"";
    Vector2 protractorPosition = new(266.0f, 315.0f);

    // Update
    //--------------------------------------------------------------------------------------
    void Update(void)
    {
        // Handle common
        //--------------------------------------------------------------------------------------
        int i, ii; // Iterators that will be reused by all for loops
        const int currentGesture = GetGestureDetected();
        const float currentDragDegrees = GetGestureDragAngle();
        const float currentPitchDegrees = GetGesturePinchAngle();
        const int touchCount = GetTouchPointCount();

        // Handle last gesture
        //--------------------------------------------------------------------------------------
        if ((currentGesture != 0) && (currentGesture != 4) && (currentGesture != previousGesture)) lastGesture = currentGesture; // Filter the meaningful gestures (1, 2, 8 to 512) for the display

        // Handle gesture log
        //--------------------------------------------------------------------------------------
        if (IsMouseButtonReleased(MouseButton.Left))
        {
            if (CheckCollisionPointRec(GetMousePosition(), logButton1))
            {
                switch (logMode)
                {
                    case 3:  logMode=2; break;
                    case 2:  logMode=3; break;
                    case 1:  logMode=0; break;
                    default: logMode=1; break;
                }
            }
            else if (CheckCollisionPointRec(GetMousePosition(), logButton2))
            {
                switch (logMode)
                {
                    case 3:  logMode=1; break;
                    case 2:  logMode=0; break;
                    case 1:  logMode=3; break;
                    default: logMode=2; break;
                }
            }
        }

        int fillLog = 0; // Gate variable to be used to allow or not the gesture log to be filled
        if (currentGesture !=0)
        {
            if (logMode == 3) // 3 hides repeated events and hide hold events
            {
                if (((currentGesture != 4) && (currentGesture != previousGesture)) || (currentGesture < 3)) fillLog = 1;
            }
            else if (logMode == 2) // 2 shows repeated events but hide hold events
            {
                if (currentGesture != 4) fillLog = 1;
            }
            else if (logMode == 1) // 1 hides repeated events
            {
                if (currentGesture != previousGesture) fillLog = 1;
            }
            else  // 0 shows repeated events
            {
                fillLog = 1;
            }
        }

        if (fillLog) // If one of the conditions from logMode was met, fill the gesture log
        {
            previousGesture = currentGesture;
            gestureColor = GetGestureColor(currentGesture);
            if (gestureLogIndex <= 0) gestureLogIndex = GESTURE_LOG_SIZE;
            gestureLogIndex--;

            // Copy the gesture respective name to the gesture log array
            TextCopy(gestureLog[gestureLogIndex], GetGestureName(currentGesture));
        }

        // Handle protractor
        //--------------------------------------------------------------------------------------
        if (currentGesture > 255) // aka Pinch In and Pinch Out
        {
            currentAngleDegrees = currentPitchDegrees;
        }
        else if (currentGesture > 15) // aka Swipe Right, Swipe Left, Swipe Up and Swipe Down
        {
            currentAngleDegrees = currentDragDegrees;
        }
        else if (currentGesture > 0) // aka Tap, Doubletap, Hold and Grab
        {
            currentAngleDegrees = 0.0f;
        }

        float currentAngleRadians = ((currentAngleDegrees +90.0f)*PI/180); // Convert the current angle to Radians
        finalVector = new((angleLength*sinf(currentAngleRadians)) + protractorPosition.X, (angleLength*cosf(currentAngleRadians)) + protractorPosition.Y); // Calculate the final vector for display

        // Handle touch and mouse pointer points
        //--------------------------------------------------------------------------------------
    const int MAX_TOUCH_COUNT = 32;

        Vector2[] touchPosition = new Vector2[MAX_TOUCH_COUNT];
        Vector2 mousePosition = new(0, 0);
        if (currentGesture != Gesture.None)
        {
            if (touchCount != 0)
            {
                for (i = 0; i < touchCount; i++) touchPosition[i] = GetTouchPosition(i); // Fill the touch positions
            }
            else mousePosition = GetMousePosition();
        }

        // Draw
        //--------------------------------------------------------------------------------------
        BeginDrawing();

            ClearBackground(RAYWHITE);

            // Draw common
            //--------------------------------------------------------------------------------------
            DrawText("*", messagePosition.X + 5, messagePosition.Y + 5, 10, BLACK);
            DrawText("Example optimized for Web/HTML5\non Smartphones with Touch Screen.", messagePosition.X + 15, messagePosition.Y + 5, 10, BLACK);
            DrawText("*", messagePosition.X + 5, messagePosition.Y + 35, 10, BLACK);
            DrawText("While running on Desktop Web Browsers,\ninspect and turn on Touch Emulation.", messagePosition.X + 15,  messagePosition.Y + 35, 10, BLACK);

            // Draw last gesture
            //--------------------------------------------------------------------------------------
            DrawText("Last gesture", lastGesturePosition.X + 33, lastGesturePosition.Y - 47, 20, BLACK);
            DrawText("Swipe         Tap       Pinch  Touch", lastGesturePosition.X + 17, lastGesturePosition.Y - 18, 10, BLACK);
            DrawRectangle(lastGesturePosition.X + 20, lastGesturePosition.Y, 20, 20, lastGesture == GESTURE_SWIPE_UP ? RED : LIGHTGRAY);
            DrawRectangle(lastGesturePosition.X, lastGesturePosition.Y + 20, 20, 20, lastGesture == GESTURE_SWIPE_LEFT ? RED : LIGHTGRAY);
            DrawRectangle(lastGesturePosition.X + 40, lastGesturePosition.Y + 20, 20, 20, lastGesture == GESTURE_SWIPE_RIGHT ? RED : LIGHTGRAY);
            DrawRectangle(lastGesturePosition.X + 20, lastGesturePosition.Y + 40, 20, 20, lastGesture == GESTURE_SWIPE_DOWN ? RED : LIGHTGRAY);
            DrawCircle(lastGesturePosition.X + 80, lastGesturePosition.Y + 16, 10, lastGesture == Gesture.Tap ? BLUE : LIGHTGRAY);
            DrawRing( new(lastGesturePosition.X + 103, lastGesturePosition.Y + 16), 6.0f, 11.0f, 0.0f, 360.0f, 0, lastGesture == Gesture.Drag ? LIME : LIGHTGRAY);
            DrawCircle(lastGesturePosition.X + 80, lastGesturePosition.Y + 43, 10, lastGesture == Gesture.Doubletap ? SKYBLUE : LIGHTGRAY);
            DrawCircle(lastGesturePosition.X + 103, lastGesturePosition.Y + 43, 10, lastGesture == Gesture.Doubletap ? SKYBLUE : LIGHTGRAY);
            DrawTriangle(new(lastGesturePosition.X + 122, lastGesturePosition.Y + 16), new(lastGesturePosition.X + 137, lastGesturePosition.Y + 26), new(lastGesturePosition.X + 137, lastGesturePosition.Y + 6), lastGesture == GESTURE_PINCH_OUT? ORANGE : LIGHTGRAY);
            DrawTriangle(new(lastGesturePosition.X + 147, lastGesturePosition.Y + 6), new(lastGesturePosition.X + 147, lastGesturePosition.Y + 26), new(lastGesturePosition.X + 162, lastGesturePosition.Y + 16), lastGesture == GESTURE_PINCH_OUT? ORANGE : LIGHTGRAY);
            DrawTriangle(new(lastGesturePosition.X + 125, lastGesturePosition.Y + 33), new(lastGesturePosition.X + 125, lastGesturePosition.Y + 53), new(lastGesturePosition.X + 140, lastGesturePosition.Y + 43), lastGesture == GESTURE_PINCH_IN? VIOLET : LIGHTGRAY);
            DrawTriangle(new(lastGesturePosition.X + 144, lastGesturePosition.Y + 43), new(lastGesturePosition.X + 159, lastGesturePosition.Y + 53), new(lastGesturePosition.X + 159, lastGesturePosition.Y + 33), lastGesture == GESTURE_PINCH_IN? VIOLET : LIGHTGRAY);
            for (i = 0; i < 4; i++) DrawCircle(lastGesturePosition.X + 180, lastGesturePosition.Y + 7 + i*15, 5, touchCount <= i? LIGHTGRAY : gestureColor);

            // Draw gesture log
            //--------------------------------------------------------------------------------------
            DrawText("Log", gestureLogPosition.X, gestureLogPosition.Y, 20, BLACK);

            // Loop in both directions to print the gesture log array in the inverted order (and looping around if the index started somewhere in the middle)
            for (i = 0, ii = gestureLogIndex; i < GESTURE_LOG_SIZE; i++, ii = (ii + 1) % GESTURE_LOG_SIZE) DrawText(gestureLog[ii], gestureLogPosition.X, gestureLogPosition.Y + 410 - i*20, 20, (i == 0 ? gestureColor : LIGHTGRAY));
            Color logButton1Color, logButton2Color;
            switch (logMode)
            {
                case 3:  logButton1Color=MAROON; logButton2Color=MAROON; break;
                case 2:  logButton1Color=GRAY;   logButton2Color=MAROON; break;
                case 1:  logButton1Color=MAROON; logButton2Color=GRAY;   break;
                default: logButton1Color=GRAY;   logButton2Color=GRAY;   break;
            }
            DrawRectangleRec(logButton1, logButton1Color);
            DrawText("Hide", logButton1.X + 7, logButton1.Y + 3, 10, WHITE);
            DrawText("Repeat", logButton1.X + 7, logButton1.Y + 13, 10, WHITE);
            DrawRectangleRec(logButton2, logButton2Color);
            DrawText("Hide", logButton1.X + 62, logButton1.Y + 3, 10, WHITE);
            DrawText("Hold", logButton1.X + 62, logButton1.Y + 13, 10, WHITE);

            // Draw protractor
            //--------------------------------------------------------------------------------------
            DrawText("Angle", protractorPosition.X + 55, protractorPosition.Y + 76, 10, BLACK);
            const char *angleString = TextFormat("%f", currentAngleDegrees);
            const int angleStringDot = TextFindIndex(angleString, ".");
            const char *angleStringTrim = TextSubtext(angleString, 0, angleStringDot + 3);
            DrawText( angleStringTrim, protractorPosition.X + 55, protractorPosition.Y + 92, 20, gestureColor);
            DrawCircle(protractorPosition.X, protractorPosition.Y, 80.0f, WHITE);
            DrawLineEx(new(protractorPosition.X - 90, protractorPosition.Y), new(protractorPosition.X + 90, protractorPosition.Y), 3.0f, LIGHTGRAY);
            DrawLineEx(new(protractorPosition.X, protractorPosition.Y - 90), new(protractorPosition.X, protractorPosition.Y + 90), 3.0f, LIGHTGRAY);
            DrawLineEx(new(protractorPosition.X - 80, protractorPosition.Y - 45), new(protractorPosition.X + 80, protractorPosition.Y + 45), 3.0f, GREEN);
            DrawLineEx(new(protractorPosition.X - 80, protractorPosition.Y + 45), new(protractorPosition.X + 80, protractorPosition.Y - 45), 3.0f, GREEN);
            DrawText("0", protractorPosition.X + 96, protractorPosition.Y - 9, 20, BLACK);
            DrawText("30", protractorPosition.X + 74, protractorPosition.Y - 68, 20, BLACK);
            DrawText("90", protractorPosition.X - 11, protractorPosition.Y - 110, 20, BLACK);
            DrawText("150", protractorPosition.X - 100, protractorPosition.Y - 68, 20, BLACK);
            DrawText("180", protractorPosition.X - 124, protractorPosition.Y - 9, 20, BLACK);
            DrawText("210", protractorPosition.X - 100, protractorPosition.Y + 50, 20, BLACK);
            DrawText("270", protractorPosition.X - 18, protractorPosition.Y + 92, 20, BLACK);
            DrawText("330", protractorPosition.X + 72, protractorPosition.Y + 50, 20, BLACK);
            if (currentAngleDegrees != 0.0f) DrawLineEx(protractorPosition, finalVector, 3.0f, gestureColor);

            // Draw touch and mouse pointer points
            //--------------------------------------------------------------------------------------
            if (currentGesture != Gesture.None)
            {
                if ( touchCount != 0 )
                {
                    for (i = 0; i < touchCount; i++)
                    {
                        DrawCircleV(touchPosition[i], 50.0f, Fade(gestureColor, 0.5f));
                        DrawCircleV(touchPosition[i], 5.0f, gestureColor);
                    }

                    if (touchCount == 2) DrawLineEx(touchPosition[0], touchPosition[1], ((currentGesture == 512)? 8 : 12), gestureColor);
                }
                else
                {
                    DrawCircleV(mousePosition, 35.0f, Fade(gestureColor, 0.5f));
                    DrawCircleV(mousePosition, 5.0f, gestureColor);
                }
            }

        EndDrawing();
        //--------------------------------------------------------------------------------------

    }

    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - input gestures web");
        //--------------------------------------------------------------------------------------

        // Main game loop
        //--------------------------------------------------------------------------------------
        #if defined(PLATFORM_WEB)
            emscripten_set_main_loop(Update, 0, 1);
        #else
            SetTargetFPS(60);
            while (!WindowShouldClose()) Update(); // Detect window close button or ESC key
        #endif
        //--------------------------------------------------------------------------------------

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow(); // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

