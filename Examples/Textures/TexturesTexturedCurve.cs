using System;
using System.Numerics;

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

    static Vector2? curveSelectedPoint = null;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(WindowFlag.VsyncHint | WindowFlag.Msaa4xHint);
        InitWindow(screenWidth, screenHeight, "raylib [textures] examples - textured curve");

        // Load the road texture
        texRoad = LoadTexture("resources/road.png");
        SetTextureFilter(texRoad, TextureFilter.Bilinear);

        // Setup the curve
        curveStartPosition = new(80, 100);
        curveStartPositionTangent = new(100, 300);

        curveEndPosition = new(700, 350);
        curveEndPositionTangent = new(600, 100);

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCurve();
            UpdateOptions();

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                DrawTexturedCurve();
                DrawCurve();

                DrawText("Drag points to move curve, press SPACE to show/hide base curve", 10, 10, 10, DarkGray);
                DrawText($"Curve width: {curveWidth} == 0.0f (Use + and - to adjust)", 10, 30, 10, DarkGray);
                DrawText($"Curve segments: {curveSegments} (Use LEFT and RIGHT to adjust)", 10, 50, 10, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texRoad);

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }

    // Module Functions Definition
    static void DrawCurve()
    {
        if (showCurve)
        {
            DrawLineBezierCubic(curveStartPosition, curveEndPosition, curveStartPositionTangent, curveEndPositionTangent, 2, Blue);
        }

        // Draw the various control points and highlight where the mouse is
        DrawLine(curveStartPosition, curveStartPositionTangent, SkyBlue);
        DrawLine(curveEndPosition, curveEndPositionTangent, Purple);
        Vector2 mouse = GetMousePosition();

        if (CheckCollisionPointCircle(mouse, curveStartPosition, 6))
        {
            DrawCircle(curveStartPosition, 7, Yellow);
        }

        DrawCircle(curveStartPosition, 5, Red);

        if (CheckCollisionPointCircle(mouse, curveStartPositionTangent, 6))
        {
            DrawCircle(curveStartPositionTangent, 7, Yellow);
        }

        DrawCircle(curveStartPositionTangent, 5, Maroon);

        if (CheckCollisionPointCircle(mouse, curveEndPosition, 6))
        {
            DrawCircle(curveEndPosition, 7, Yellow);
        }

        DrawCircle(curveEndPosition, 5, Green);

        if (CheckCollisionPointCircle(mouse, curveEndPositionTangent, 6))
        {
            DrawCircle(curveEndPositionTangent, 7, Yellow);
        }

        DrawCircle(curveEndPositionTangent, 5, DarkGreen);
    }

    static void UpdateCurve()
    {
        // If the mouse is not down, we are not editing the curve so clear the selection
        if (!IsMouseButtonDown(MouseButton.Left))
        {
            curveSelectedPoint = null;
            return;
        }

        // If a point was selected, move it
        if (curveSelectedPoint.HasValue)
        {
            curveSelectedPoint = Vector2.Add(curveSelectedPoint.Value, GetMouseDelta());
            return;
        }

        // The mouse is down, and nothing was selected, so see if anything was picked
        Vector2 mouse = GetMousePosition();

        if (CheckCollisionPointCircle(mouse, curveStartPosition, 6))
        {
            curveSelectedPoint = curveStartPosition;
        }
        else if (CheckCollisionPointCircle(mouse, curveStartPositionTangent, 6))
        {
            curveSelectedPoint = curveStartPositionTangent;
        }
        else if (CheckCollisionPointCircle(mouse, curveEndPosition, 6))
        {
            curveSelectedPoint = curveEndPosition;
        }
        else if (CheckCollisionPointCircle(mouse, curveEndPositionTangent, 6))
        {
            curveSelectedPoint = curveEndPositionTangent;
        }
    }

    static void DrawTexturedCurve()
    {
        float step = 1.0f / curveSegments;

        Vector2 previous = curveStartPosition;
        Vector2 previousTangent = new();
        float previousV = 0;

        // We can't compute a tangent for the first point, so we need to reuse the tangent from the first segment
        bool tangentSet = false;

        Vector2 current = new();
        for (int i = 1; i <= curveSegments; i++)
        {
            // Segment the curve
            float t = step * i;
            float a = MathF.Pow(1 - t, 3);
            float b = 3 * MathF.Pow(1 - t, 2) * t;
            float c = 3 * (1 - t) * MathF.Pow(t, 2);
            float d = MathF.Pow(t, 3);

            // Compute the endpoint for this segment
            current.Y = (a * curveStartPosition.Y) + (b * curveStartPositionTangent.Y) + (c * curveEndPositionTangent.Y) + (d * curveEndPosition.Y);
            current.X = (a * curveStartPosition.X) + (b * curveStartPositionTangent.X) + (c * curveEndPositionTangent.X) + (d * curveEndPosition.X);

            // Vector from previous to current
            Vector2 delta = new(current.X - previous.X, current.Y - previous.Y);

            // The right hand normal to the delta vector
            Vector2 normal = Vector2.Normalize(new(-delta.Y, delta.X));

            // The v texture coordinate of the segment (add up the length of all the segments so far)
            float v = previousV + delta.Length();

            // Make sure the start point has a normal
            if (!tangentSet)
            {
                previousTangent = normal;
                tangentSet = true;
            }

            // Extend out the normals from the previous and current points to get the quad for this segment
            Vector2 prevPosNormal = Vector2.Add(previous, Vector2.Multiply(previousTangent, curveWidth));
            Vector2 prevNegNormal = Vector2.Add(previous, Vector2.Multiply(previousTangent, -curveWidth));

            Vector2 currentPosNormal = Vector2.Add(current, Vector2.Multiply(normal, curveWidth));
            Vector2 currentNegNormal = Vector2.Add(current, Vector2.Multiply(normal, -curveWidth));

            // Draw the segment as a quad
            RLGL.SetTexture(texRoad.Id);
            RLGL.Begin(RLGL.RlQuads);

            RLGL.Color4ub(255, 255, 255, 255);
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

    static void UpdateOptions()
    {
        if (IsKeyPressed(Key.Space))
        {
            showCurve = !showCurve;
        }

        // Update with
        if (IsKeyPressed(Key.Equal))
        {
            curveWidth += 2;
        }

        if (IsKeyPressed(Key.Minus))
        {
            curveWidth -= 2;
        }

        if (curveWidth < 2)
        {
            curveWidth = 2;
        }

        // Update segments
        if (IsKeyPressed(Key.Left))
        {
            curveSegments -= 2;
        }

        if (IsKeyPressed(Key.Right))
        {
            curveSegments += 2;
        }

        if (curveSegments < 2)
        {
            curveSegments = 2;
        }
    }
}
