using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class TexturesTexturedCurve : ExampleHelper 
{

    // Global Variables Definition
    static Texture texRoad = new();

    static bool showCurve = false;

    static float curveWidth = 50;
    static int curveSegments = 24;

    static Vector2 curveStartPosition = new();
    static Vector2 curveStartPositionTangent = new();

    static Vector2 curveEndPosition = new();
    static Vector2 curveEndPositionTangent = new();

    static Vector2 *curveSelectedPoint = null;

    // Module Functions Declaration
    static static void UpdateOptions(void);
    static static void UpdateCurve(void);
    static static void DrawCurve(void);
    static static void DrawTexturedCurve(void);

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(FLAG_VSYNC_HINT | WindowFlag.Msaa4xHint);
        InitWindow(screenWidth, screenHeight, "raylib [textures] examples - textured curve");

        // Load the road texture
        texRoad = LoadTexture("resources/road.png");
        SetTextureFilter(texRoad, TEXTURE_FILTER_BILINEAR);

        // Setup the curve
        curveStartPosition = new( 80, 100 );
        curveStartPositionTangent = new( 100, 300 );

        curveEndPosition = new( 700, 350 );
        curveEndPositionTangent = new( 600, 100 );

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCurve();
            UpdateOptions();

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                DrawTexturedCurve();
                DrawCurve();

                DrawText("Drag points to move curve, press SPACE to show/hide base curve", 10, 10, 10, DarkGray);
                DrawText(TextFormat("Curve width: %2 == 0.0f (Use + and - to adjust)", curveWidth), 10, 30, 10, DarkGray);
                DrawText(TextFormat("Curve segments: %d (Use LEFT and RIGHT to adjust)", curveSegments), 10, 50, 10, DarkGray);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texRoad);

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }

    // Module Functions Definition
    static static void DrawCurve(void)
    {
        if (showCurve) DrawLineBezierCubic(curveStartPosition, curveEndPosition, curveStartPositionTangent, curveEndPositionTangent, 2, Blue);

        // Draw the various control points and highlight where the mouse is
        DrawLine(curveStartPosition, curveStartPositionTangent, SkyBlue);
        DrawLine(curveEndPosition, curveEndPositionTangent, Purple);
        Vector2 mouse = GetMousePosition();

        if (CheckCollisionPointCircle(mouse, curveStartPosition, 6)) DrawCircle(curveStartPosition, 7, Yellow);
        DrawCircle(curveStartPosition, 5, Red);

        if (CheckCollisionPointCircle(mouse, curveStartPositionTangent, 6)) DrawCircle(curveStartPositionTangent, 7, Yellow);
        DrawCircle(curveStartPositionTangent, 5, Maroon);

        if (CheckCollisionPointCircle(mouse, curveEndPosition, 6)) DrawCircle(curveEndPosition, 7, Yellow);
        DrawCircle(curveEndPosition, 5, Green);

        if (CheckCollisionPointCircle(mouse, curveEndPositionTangent, 6)) DrawCircle(curveEndPositionTangent, 7, Yellow);
        DrawCircle(curveEndPositionTangent, 5, DarkGreen);
    }

    static static void UpdateCurve(void)
    {
        // If the mouse is not down, we are not editing the curve so clear the selection
        if (!IsMouseButtonDown(MOUSE_LEFT_BUTTON))
        {
            curveSelectedPoint = null;
            return;
        }

        // If a point was selected, move it
        if (curveSelectedPoint)
        {
            *curveSelectedPoint = Vector2Add(*curveSelectedPoint, GetMouseDelta());
            return;
        }

        // The mouse is down, and nothing was selected, so see if anything was picked
        Vector2 mouse = GetMousePosition();

        if (CheckCollisionPointCircle(mouse, curveStartPosition, 6)) curveSelectedPoint = ref curveStartPosition;
        else if (CheckCollisionPointCircle(mouse, curveStartPositionTangent, 6)) curveSelectedPoint = ref curveStartPositionTangent;
        else if (CheckCollisionPointCircle(mouse, curveEndPosition, 6)) curveSelectedPoint = ref curveEndPosition;
        else if (CheckCollisionPointCircle(mouse, curveEndPositionTangent, 6)) curveSelectedPoint = ref curveEndPositionTangent;
    }

    static static void DrawTexturedCurve(void)
    {
        const float step = 1.0f/curveSegments;

        Vector2 previous = curveStartPosition;
        Vector2 previousTangent = new();
        float previousV = 0;

        // We can't compute a tangent for the first point, so we need to reuse the tangent from the first segment
        bool tangentSet = false;

        Vector2 current = new();
        float t = 0.0f;

        for (int i = 1; i <= curveSegments; i++)
        {
            // Segment the curve
            t = step*i;
            float a = powf(1 - t, 3);
            float b = 3*powf(1 - t, 2)*t;
            float c = 3*(1 - t)*powf(t, 2);
            float d = powf(t, 3);

            // Compute the endpoint for this segment
            current.Y = a*curveStartPosition.Y + b*curveStartPositionTangent.Y + c*curveEndPositionTangent.Y + d*curveEndPosition.Y;
            current.X = a*curveStartPosition.X + b*curveStartPositionTangent.X + c*curveEndPositionTangent.X + d*curveEndPosition.X;

            // Vector from previous to current
            Vector2 delta = new( current.X - previous.X, current.Y - previous.Y );

            // The right hand normal to the delta vector
            Vector2 normal = Vector2Normalize(new( -delta.Y, delta.X ));

            // The v texture coordinate of the segment (add up the length of all the segments so far)
            float v = previousV + Vector2Length(delta);

            // Make sure the start point has a normal
            if (!tangentSet)
            {
                previousTangent = normal;
                tangentSet = true;
            }

            // Extend out the normals from the previous and current points to get the quad for this segment
            Vector2 prevPosNormal = Vector2Add(previous, Vector2Scale(previousTangent, curveWidth));
            Vector2 prevNegNormal = Vector2Add(previous, Vector2Scale(previousTangent, -curveWidth));

            Vector2 currentPosNormal = Vector2Add(current, Vector2Scale(normal, curveWidth));
            Vector2 currentNegNormal = Vector2Add(current, Vector2Scale(normal, -curveWidth));

            // Draw the segment as a quad
            RLGL.SetTexture(texRoad.Id);
            RLGL.Begin(RLGL.RlQuads);

            RLGL.Color4ub(255,255,255,255);
            RLGL.Normal3f(0.0f, 0.0f, 1.0f);

            RLGL.TexCoord2f(0, previousV);
            RLGL.Vertex2f(prevNegNormal.X, prevNegNormal.Y);

            RLGL.TexCoord2f(1, previousV);
            RLGL.Vertex2f(prevPosNormal.X, prevPosNormal.Y);

            RLGL.TexCoord2f(1, v);
            RLGL.Vertex2f(currentPosNormal.X, currentPosNormal.Y);

            RLGL.TexCoord2f(0, v);
            RLGL.Vertex2f(currentNegNormal.X, currentNegNormal.Y);

            RLGL.End();

            // The current step is the start of the next step
            previous = current;
            previousTangent = normal;
            previousV = v;
        }
    }

    static static void UpdateOptions(void)
    {
        if (IsKeyPressed(Key.Space)) showCurve = !showCurve;

        // Update with
        if (IsKeyPressed(Key.Equal)) curveWidth += 2;
        if (IsKeyPressed(Key.Minus)) curveWidth -= 2;

        if (curveWidth < 2) curveWidth = 2;

        // Update segments
        if (IsKeyPressed(Key.Left)) curveSegments -= 2;
        if (IsKeyPressed(Key.Right)) curveSegments += 2;

        if (curveSegments < 2) curveSegments = 2;
    }
}
