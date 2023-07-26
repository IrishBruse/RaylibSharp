using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShapesTopDownLights : ExampleHelper 
{

    // Custom Blend Modes
private const int RLGL_SRC_ALPHA = 0x0302;
private const int RLGL_MIN = 0x8007;
private const int RLGL_MAX = 0x8008;

private const int MAX_BOXES = 20;
private const int MAX_SHADOWS = MAX_BOXES*3;
private const int MAX_LIGHTS = 16;

    // Shadow geometry type
    typedef struct ShadowGeometry {
        Vector2 vertices[4];
    } ShadowGeometry;

    // Light info type
    typedef struct LightInfo {
        bool active;                // Is this light slot active?
        bool dirty;                 // Does this light need to be updated?
        bool valid;                 // Is this light in a valid position?

        Vector2 position;           // Light position
        RenderTexture mask;         // Alpha mask for the light
        float outerRadius;          // The distance the light touches
        RectangleF bounds;           // A cached rectangle of the light bounds to help with culling

        ShadowGeometry shadows[MAX_SHADOWS];
        int shadowCount;
    } LightInfo;

    LightInfo lights[MAX_LIGHTS] = new();

    // Move a light and mark it as dirty so that we update it's mask next frame
    static void MoveLight(int slot, float x, float y)
    {
        lights[slot].dirty = true;
        lights[slot].position.X = x;
        lights[slot].position.Y = y;

        // update the cached bounds
        lights[slot].bounds.X = x - lights[slot].outerRadius;
        lights[slot].bounds.Y = y - lights[slot].outerRadius;
    }

    // Compute a shadow volume for the edge
    // It takes the edge and projects it back by the light radius and turns it into a quad
    static void ComputeShadowVolumeForEdge(int slot, Vector2 sp, Vector2 ep)
    {
        if (lights[slot].shadowCount >= MAX_SHADOWS) return;

        float extension = lights[slot].outerRadius*2;

        Vector2 spVector = Vector2Normalize(Vector2Subtract(sp, lights[slot].position));
        Vector2 spProjection = Vector2Add(sp, Vector2Scale(spVector, extension));

        Vector2 epVector = Vector2Normalize(Vector2Subtract(ep, lights[slot].position));
        Vector2 epProjection = Vector2Add(ep, Vector2Scale(epVector, extension));

        lights[slot].shadows[lights[slot].shadowCount].vertices[0] = sp;
        lights[slot].shadows[lights[slot].shadowCount].vertices[1] = ep;
        lights[slot].shadows[lights[slot].shadowCount].vertices[2] = epProjection;
        lights[slot].shadows[lights[slot].shadowCount].vertices[3] = spProjection;

        lights[slot].shadowCount++;
    }

    // Draw the light and shadows to the mask for a light
    static void DrawLightMask(int slot)
    {
        // Use the light mask
        BeginTextureMode(lights[slot].mask);

            ClearBackground(White);

            // Force the blend mode to only set the alpha of the destination
            rlSetBlendFactors(RLGL_SRC_ALPHA, RLGL_SRC_ALPHA, RLGL_MIN);
            rlSetBlendMode(BLEND_CUSTOM);

            // If we are valid, then draw the light radius to the alpha mask
            if (lights[slot].valid) DrawCircleGradient((int)lights[slot].position.X, (int)lights[slot].position.Y, lights[slot].outerRadius, ColorAlpha(White, 0), White);

            rlDrawRenderBatchActive();

            // Cut out the shadows from the light radius by forcing the alpha to maximum
            rlSetBlendMode(BLEND_ALPHA);
            rlSetBlendFactors(RLGL_SRC_ALPHA, RLGL_SRC_ALPHA, RLGL_MAX);
            rlSetBlendMode(BLEND_CUSTOM);

            // Draw the shadows to the alpha mask
            for (int i = 0; i < lights[slot].shadowCount; i++)
            {
                DrawTriangleFan(lights[slot].shadows[i].vertices, 4, White);
            }

            rlDrawRenderBatchActive();

            // Go back to normal blend mode
            rlSetBlendMode(BLEND_ALPHA);

        EndTextureMode();
    }

    // Setup a light
    static void SetupLight(int slot, float x, float y, float radius)
    {
        lights[slot].active = true;
        lights[slot].valid = false;  // The light must prove it is valid
        lights[slot].mask = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());
        lights[slot].outerRadius = radius;

        lights[slot].bounds.Width = radius * 2;
        lights[slot].bounds.Height = radius * 2;

        MoveLight(slot, x, y);

        // Force the render texture to have something in it
        DrawLightMask(slot);
    }

    // See if a light needs to update it's mask
    bool UpdateLight(int slot, Rectangle* boxes, int count)
    {
        if (!lights[slot].active || !lights[slot].dirty) return false;

        lights[slot].dirty = false;
        lights[slot].shadowCount = 0;
        lights[slot].valid = false;

        for (int i = 0; i < count; i++)
        {
            // Are we in a box? if so we are not valid
            if (CheckCollisionPoint(lights[slot].position, boxes[i])) return false;

            // If this box is outside our bounds, we can skip it
            if (!CheckCollisionRecs(lights[slot].bounds, boxes[i])) continue;

            // Check the edges that are on the same side we are, and cast shadow volumes out from them

            // Top
            Vector2 sp = new( boxes[i].X, boxes[i].Y );
            Vector2 ep = new( boxes[i].X + boxes[i].Width, boxes[i].Y );

            if (lights[slot].position.Y > ep.Y) ComputeShadowVolumeForEdge(slot, sp, ep);

            // Right
            sp = ep;
            ep.Y += boxes[i].Height;
            if (lights[slot].position.X < ep.X) ComputeShadowVolumeForEdge(slot, sp, ep);

            // Bottom
            sp = ep;
            ep.X -= boxes[i].Width;
            if (lights[slot].position.Y < ep.Y) ComputeShadowVolumeForEdge(slot, sp, ep);

            // Left
            sp = ep;
            ep.Y -= boxes[i].Height;
            if (lights[slot].position.X > ep.X) ComputeShadowVolumeForEdge(slot, sp, ep);

            // The box itself
            lights[slot].shadows[lights[slot].shadowCount].vertices[0] = new( boxes[i].X, boxes[i].Y );
            lights[slot].shadows[lights[slot].shadowCount].vertices[1] = new( boxes[i].X, boxes[i].Y + boxes[i].Height );
            lights[slot].shadows[lights[slot].shadowCount].vertices[2] = new( boxes[i].X + boxes[i].Width, boxes[i].Y + boxes[i].Height );
            lights[slot].shadows[lights[slot].shadowCount].vertices[3] = new( boxes[i].X + boxes[i].Width, boxes[i].Y );
            lights[slot].shadowCount++;
        }

        lights[slot].valid = true;

        DrawLightMask(slot);

        return true;
    }

    // Set up some boxes
    static void SetupBoxes(RectangleF *boxes, int *count)
    {
        boxes[0] = new( 150,80, 40, 40 );
        boxes[1] = new( 1200, 700, 40, 40 );
        boxes[2] = new( 200, 600, 40, 40 );
        boxes[3] = new( 1000, 50, 40, 40 );
        boxes[4] = new( 500, 350, 40, 40 );

        for (int i = 5; i < MAX_BOXES; i++)
        {
            boxes[i] = new((float)GetRandomValue(0,GetScreenWidth()), (float)GetRandomValue(0,GetScreenHeight()), (float)GetRandomValue(10,100), (float)GetRandomValue(10,100) );
        }

        *count = MAX_BOXES;
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - top down lights");

        // Initialize our 'world' of boxes
        int boxCount = 0;
        RectangleF [] boxes = new RectangleF [MAX_BOXES];
        SetupBoxes(boxes, &boxCount);

        // Create a checkerboard ground texture
        Image img = GenImageChecked(64, 64, 32, 32, DarkBrown, DarkGray);
        Texture backgroundTexture = LoadTextureFromImage(img);
        UnloadImage(img);

        // Create a global light mask to hold all the blended lights
        RenderTexture lightMask = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());

        // Setup initial light
        SetupLight(0, 600, 400, 300);
        int nextLight = 1;

        bool showLines = false;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            // Drag light 0
            if (IsMouseButtonDown(MouseButton.Left)) MoveLight(0, GetMousePosition().X, GetMousePosition().Y);

            // Make a new light
            if (IsMouseButtonPressed(MouseButton.Right) && (nextLight < MAX_LIGHTS))
            {
                SetupLight(nextLight, GetMousePosition().X, GetMousePosition().Y, 200);
                nextLight++;
            }

            // Toggle debug info
            if (IsKeyPressed(Key.F1)) showLines = !showLines;

            // Update the lights and keep track if any were dirty so we know if we need to update the master light mask
            bool dirtyLights = false;
            for (int i = 0; i < MAX_LIGHTS; i++)
            {
                if (UpdateLight(i, boxes, boxCount)) dirtyLights = true;
            }

            // Update the light mask
            if (dirtyLights)
            {
                // Build up the light mask
                BeginTextureMode(lightMask);

                    ClearBackground(Black);

                    // Force the blend mode to only set the alpha of the destination
                    rlSetBlendFactors(RLGL_SRC_ALPHA, RLGL_SRC_ALPHA, RLGL_MIN);
                    rlSetBlendMode(BLEND_CUSTOM);

                    // Merge in all the light masks
                    for (int i = 0; i < MAX_LIGHTS; i++)
                    {
                        if (lights[i].active) DrawTexture(lights[i].mask.texture, new( 0, 0, (float)GetScreenWidth(), -(float)GetScreenHeight() ), Vector2Zero(), White);
                    }

                    rlDrawRenderBatchActive();

                    // Go back to normal blend
                    rlSetBlendMode(BLEND_ALPHA);
                EndTextureMode();
            }

            // Draw
            BeginDrawing();{

                ClearBackground(Black);

                // Draw the tile background
                DrawTexture(backgroundTexture, new( 0, 0, (float)GetScreenWidth(), (float)GetScreenHeight() ), Vector2Zero(), White);

                // Overlay the shadows from all the lights
                DrawTexture(lightMask.texture, new( 0, 0, (float)GetScreenWidth(), -(float)GetScreenHeight() ), Vector2Zero(), ColorAlpha(White, showLines? 0.75f : 1.0f));

                // Draw the lights
                for (int i = 0; i < MAX_LIGHTS; i++)
                {
                    if (lights[i].active) DrawCircle((int)lights[i].position.X, (int)lights[i].position.Y, 10, (i == 0)? Yellow : White);
                }

                if (showLines)
                {
                    for (int s = 0; s < lights[0].shadowCount; s++)
                    {
                        DrawTriangleFan(lights[0].shadows[s].vertices, 4, DarkPurple);
                    }

                    for (int b = 0; b < boxCount; b++)
                    {
                        if (CheckCollisionRecs(boxes[b],lights[0].bounds)) DrawRectangle(boxes[b], Purple);

                        DrawRectangleLines((int)boxes[b].X, (int)boxes[b].Y, (int)boxes[b].Width, (int)boxes[b].Height, DarkBlue);
                    }

                    DrawText("(F1) Hide Shadow Volumes", 10, 50, 10, Green);
                }
                else
                {
                    DrawText("(F1) Show Shadow Volumes", 10, 50, 10, Green);
                }

                DrawFPS(screenWidth - 80, 10);
                DrawText("Drag to move light #1", 10, 10, 10, DarkGreen);
                DrawText("Right click to add new light", 10, 30, 10, DarkGreen);
            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(backgroundTexture);
        UnloadRenderTexture(lightMask);
        for (int i = 0; i < MAX_LIGHTS; i++)
        {
            if (lights[i].active) UnloadRenderTexture(lights[i].mask);
        }

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
