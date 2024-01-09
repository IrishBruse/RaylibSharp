using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesMousePainting : ExampleHelper
{
    const int MAX_COLORS_COUNT = 23;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - mouse painting");

        // Colors to choose from
        Color[] colors = new Color[MAX_COLORS_COUNT]{
            RayWhite, Yellow, Gold, Orange, Pink, Red, Maroon, Green, Lime, DarkGreen,
            SkyBlue, Blue, DarkBlue, Purple, Violet, DarkPurple, Beige, Brown, DarkBrown,
            LightGray, Gray, DarkGray, Black };

        // Define colorsRecs data (for every rectangle)
        Rectangle[] colorsRecs = new Rectangle[MAX_COLORS_COUNT];

        for (int i = 0; i < MAX_COLORS_COUNT; i++)
        {
            colorsRecs[i].X = 10 + (30.0f * i) + (2 * i);
            colorsRecs[i].Y = 10;
            colorsRecs[i].Width = 30;
            colorsRecs[i].Height = 30;
        }

        int colorSelected = 0;
        int colorSelectedPrev = colorSelected;
        int colorMouseHover = 0;
        float brushSize = 20.0f;
        bool mouseWasPressed = false;

        Rectangle btnSaveRec = new(750, 10, 40, 30);
        bool showSaveMessage = false;
        int saveMessageCounter = 0;

        // Create a RenderTexture to use as a canvas
        RenderTexture target = LoadRenderTexture(screenWidth, screenHeight);

        // Clear render texture before entering the game loop
        BeginTextureMode(target);
        {
            ClearBackground(colors[0]);
        }
        EndTextureMode();

        SetTargetFPS(120);              // Set our game to run at 120 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            Vector2 mousePos = GetMousePosition();

            // Move between colors with keys
            if (IsKeyPressed(Key.Right))
            {
                colorSelected++;
            }
            else if (IsKeyPressed(Key.Left))
            {
                colorSelected--;
            }

            if (colorSelected >= MAX_COLORS_COUNT)
            {
                colorSelected = MAX_COLORS_COUNT - 1;
            }
            else if (colorSelected < 0)
            {
                colorSelected = 0;
            }

            // Choose color with mouse
            for (int i = 0; i < MAX_COLORS_COUNT; i++)
            {
                if (CheckCollisionPoint(mousePos, colorsRecs[i]))
                {
                    colorMouseHover = i;
                    break;
                }
                else
                {
                    colorMouseHover = -1;
                }
            }

            if ((colorMouseHover >= 0) && IsMouseButtonPressed(MouseButton.Left))
            {
                colorSelected = colorMouseHover;
                colorSelectedPrev = colorSelected;
            }

            // Change brush size
            brushSize += GetMouseWheelMove().Y * 5;
            if (brushSize < 2)
            {
                brushSize = 2;
            }

            if (brushSize > 50)
            {
                brushSize = 50;
            }

            if (IsKeyPressed(Key.C))
            {
                // Clear render texture to clear color
                BeginTextureMode(target);
                {
                    ClearBackground(colors[0]);
                }
                EndTextureMode();
            }

            if (IsMouseButtonDown(MouseButton.Left) || (GetGestureDetected() == Gesture.Drag))
            {
                // Paint circle into render texture
                // NOTE: To avoid discontinuous circles, we could store
                // previous-next mouse points and just draw a line using brush size
                BeginTextureMode(target);
                {
                    if (mousePos.Y > 50)
                    {
                        DrawCircle((int)mousePos.X, (int)mousePos.Y, brushSize, colors[colorSelected]);
                    }
                }
                EndTextureMode();
            }

            if (IsMouseButtonDown(MouseButton.Right))
            {
                if (!mouseWasPressed)
                {
                    colorSelectedPrev = colorSelected;
                    colorSelected = 0;
                }

                mouseWasPressed = true;

                // Erase circle from render texture
                BeginTextureMode(target);
                {
                    if (mousePos.Y > 50)
                    {
                        DrawCircle((int)mousePos.X, (int)mousePos.Y, brushSize, colors[0]);
                    }
                }
                EndTextureMode();
            }
            else if (IsMouseButtonReleased(MouseButton.Right) && mouseWasPressed)
            {
                colorSelected = colorSelectedPrev;
                mouseWasPressed = false;
            }

            bool btnSaveMouseHover;
            // Check mouse hover save button
            if (CheckCollisionPoint(mousePos, btnSaveRec))
            {
                btnSaveMouseHover = true;
            }
            else
            {
                btnSaveMouseHover = false;
            }

            // Image saving logic
            // NOTE: Saving painted texture to a default named image
            if ((btnSaveMouseHover && IsMouseButtonReleased(MouseButton.Left)) || IsKeyPressed(Key.S))
            {
                Image image = LoadImageFromTexture(target.Texture);
                ImageFlipVertical(ref image);
                ExportImage(image, "my_amazing_texture_painting.png");
                UnloadImage(image);
                showSaveMessage = true;
            }

            if (showSaveMessage)
            {
                // On saving, show a full screen message for 2 seconds
                saveMessageCounter++;
                if (saveMessageCounter > 240)
                {
                    showSaveMessage = false;
                    saveMessageCounter = 0;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                // NOTE: Render texture must be y-flipped due to default OpenGL coordinates (left-bottom)
                DrawTexture(target.Texture, new Rectangle(0, 0, target.Texture.Width, -target.Texture.Height), new(0, 0), White);

                // Draw drawing circle for reference
                if (mousePos.Y > 50)
                {
                    if (IsMouseButtonDown(MouseButton.Right))
                    {
                        DrawCircleLines((int)mousePos.X, (int)mousePos.Y, brushSize, Gray);
                    }
                    else
                    {
                        DrawCircle(GetMouseX(), GetMouseY(), brushSize, colors[colorSelected]);
                    }
                }

                // Draw top panel
                DrawRectangle(0, 0, GetScreenWidth(), 50, RayWhite);
                DrawLine(0, 50, GetScreenWidth(), 50, LightGray);

                // Draw color selection rectangles
                for (int i = 0; i < MAX_COLORS_COUNT; i++)
                {
                    DrawRectangle(colorsRecs[i], colors[i]);
                }

                DrawRectangleLines(10, 10, 30, 30, LightGray);

                if (colorMouseHover >= 0)
                {
                    DrawRectangle(colorsRecs[colorMouseHover], Fade(White, 0.6f));
                }

                DrawRectangleLines(new Rectangle(colorsRecs[colorSelected].X - 2, colorsRecs[colorSelected].Y - 2, colorsRecs[colorSelected].Width + 4, colorsRecs[colorSelected].Height + 4), 2, Black);
                // Draw save image button
                DrawRectangleLines(btnSaveRec, 2, btnSaveMouseHover ? Red : Black);
                DrawText("SAVE!", 755, 20, 10, btnSaveMouseHover ? Red : Black);

                // Draw save image message
                if (showSaveMessage)
                {
                    DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Fade(RayWhite, 0.8f));
                    DrawRectangle(0, 150, GetScreenWidth(), 80, Black);
                    DrawText("IMAGE SAVED:  my_amazing_texture_painting.png", 150, 180, 20, RayWhite);
                }

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadRenderTexture(target);    // Unload render texture

        CloseWindow();                  // Close window and OpenGL context

        return 0;
    }
}
