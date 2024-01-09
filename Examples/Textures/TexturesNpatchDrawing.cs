using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesNpatchDrawing : ExampleHelper
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - N-patch drawing");

        // NOTE: Textures MUST be loaded after Window initialization (OpenGL context is required)
        Texture nPatchTexture = LoadTexture("resources/ninepatch_button.png");
        _ = new
        Vector2();
        Vector2 origin = new(0.0f, 0.0f);

        // Position and size of the n-patches
        Rectangle dstRec1 = new(480.0f, 160.0f, 32.0f, 32.0f);
        Rectangle dstRec2 = new(160.0f, 160.0f, 32.0f, 32.0f);
        Rectangle dstRecH = new(160.0f, 93.0f, 32.0f, 32.0f);
        Rectangle dstRecV = new(92.0f, 160.0f, 32.0f, 32.0f);

        // A 9-patch (NPATCH_NINE_PATCH) changes its sizes in both axis
        NPatchInfo ninePatchInfo1 = new(new(0.0f, 0.0f, 64.0f, 64.0f), 12, 40, 12, 12, NPatchLayout.NpatchNinePatch);
        NPatchInfo ninePatchInfo2 = new(new(0.0f, 128.0f, 64.0f, 64.0f), 16, 16, 16, 16, NPatchLayout.NpatchNinePatch);

        // A horizontal 3-patch (NPATCH_THREE_PATCH_HORIZONTAL) changes its sizes along the x axis only
        NPatchInfo h3PatchInfo = new(new(0.0f, 64.0f, 64.0f, 64.0f), 8, 8, 8, 8, NPatchLayout.NpatchThreePatchHorizontal);

        // A vertical 3-patch (NPATCH_THREE_PATCH_VERTICAL) changes its sizes along the y axis only
        NPatchInfo v3PatchInfo = new(new(0.0f, 192.0f, 64.0f, 64.0f), 6, 6, 6, 6, NPatchLayout.NpatchThreePatchVertical);

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            Vector2 mousePosition = GetMousePosition();

            // Resize the n-patches based on mouse position
            dstRec1.Width = mousePosition.X - dstRec1.X;
            dstRec1.Height = mousePosition.Y - dstRec1.Y;
            dstRec2.Width = mousePosition.X - dstRec2.X;
            dstRec2.Height = mousePosition.Y - dstRec2.Y;
            dstRecH.Width = mousePosition.X - dstRecH.X;
            dstRecV.Height = mousePosition.Y - dstRecV.Y;

            // Set a minimum width and/or height
            if (dstRec1.Width < 1.0f)
            {
                dstRec1.Width = 1.0f;
            }

            if (dstRec1.Width > 300.0f)
            {
                dstRec1.Width = 300.0f;
            }

            if (dstRec1.Height < 1.0f)
            {
                dstRec1.Height = 1.0f;
            }

            if (dstRec2.Width < 1.0f)
            {
                dstRec2.Width = 1.0f;
            }

            if (dstRec2.Width > 300.0f)
            {
                dstRec2.Width = 300.0f;
            }

            if (dstRec2.Height < 1.0f)
            {
                dstRec2.Height = 1.0f;
            }

            if (dstRecH.Width < 1.0f)
            {
                dstRecH.Width = 1.0f;
            }

            if (dstRecV.Height < 1.0f)
            {
                dstRecV.Height = 1.0f;
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // Draw the n-patches
                DrawTextureNPatch(nPatchTexture, ninePatchInfo2, dstRec2, origin, 0.0f, White);
                DrawTextureNPatch(nPatchTexture, ninePatchInfo1, dstRec1, origin, 0.0f, White);
                DrawTextureNPatch(nPatchTexture, h3PatchInfo, dstRecH, origin, 0.0f, White);
                DrawTextureNPatch(nPatchTexture, v3PatchInfo, dstRecV, origin, 0.0f, White);

                // Draw the source texture
                DrawRectangleLines(5, 88, 74, 266, Blue);
                DrawTexture(nPatchTexture, 10, 93, White);
                DrawText("TEXTURE", 15, 360, 10, DarkGray);

                DrawText("Move the mouse to stretch or shrink the n-patches", 10, 20, 20, DarkGray);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(nPatchTexture);       // Texture unloading

        CloseWindow();                // Close window and OpenGL context

        return 0;
    }
}
