using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersJuliaSet : ExampleHelper
{
    // A few good julia sets
    private static readonly Vector2[] pointsOfInterest =
    {
        new(-0.348827f,0.607167f),
        new(-0.786268f,0.169728f),
        new(-0.8f,0.156f),
        new(0.285f,0.0f),
        new(-0.835f,-0.2321f),
        new(-0.70176f,-0.3842f),
    };

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        //SetConfigFlags(FLAG_WINDOW_HIGHDPI);
        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - julia sets");

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Load julia set shader
        // NOTE: Defining 0 (null) for vertex shader forces usage of internal default vertex shader
        Shader shader = LoadShader(null, $"resources/shaders/glsl{glslVersion}/julia_set.fs");

        // Create a RenderTexture to be used for render to texture
        RenderTexture target = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());

        // c constant to use in z^2 + c
        Vector2 c = pointsOfInterest[0];

        // Offset and zoom to draw the julia set at. (centered on screen and default size)
        Vector2 offset = new(-(float)GetScreenWidth() / 2, -(float)GetScreenHeight() / 2);
        float zoom = 1.0f;

        Vector2 offsetSpeed = new(0.0f, 0.0f);

        // Get variable (uniform) locations on the shader to connect with the program
        // NOTE: If uniform variable could not be found in the shader, function returns -1
        int cLoc = GetShaderLocation(shader, "c");
        int zoomLoc = GetShaderLocation(shader, "zoom");
        int offsetLoc = GetShaderLocation(shader, "offset");

        // Tell the shader what the screen dimensions, zoom, offset and c are
        Vector2 screenDims = new(GetScreenWidth(), GetScreenHeight());
        SetShaderValue(shader, GetShaderLocation(shader, "screenDims"), ref screenDims, ShaderUniformDataType.ShaderUniformVec2);

        SetShaderValue(shader, cLoc, ref c, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(shader, zoomLoc, ref zoom, ShaderUniformDataType.ShaderUniformFloat);
        SetShaderValue(shader, offsetLoc, ref offset, ShaderUniformDataType.ShaderUniformVec2);

        int incrementSpeed = 0; // Multiplier of speed to change c value
        bool showControls = true; // Show controls
        bool pause = false; // Pause animation

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            // Press [1 - 6] to reset c to a point of interest
            if (IsKeyPressed(Key.One) ||
                IsKeyPressed(Key.Two) ||
                IsKeyPressed(Key.Three) ||
                IsKeyPressed(Key.Four) ||
                IsKeyPressed(Key.Five) ||
                IsKeyPressed(Key.Six))
            {
                if (IsKeyPressed(Key.One))
                {
                    c = pointsOfInterest[0];
                }
                else if (IsKeyPressed(Key.Two))
                {
                    c = pointsOfInterest[1];
                }
                else if (IsKeyPressed(Key.Three))
                {
                    c = pointsOfInterest[2];
                }
                else if (IsKeyPressed(Key.Four))
                {
                    c = pointsOfInterest[3];
                }
                else if (IsKeyPressed(Key.Five))
                {
                    c = pointsOfInterest[4];
                }
                else if (IsKeyPressed(Key.Six))
                {
                    c = pointsOfInterest[5];
                }

                SetShaderValue(shader, cLoc, ref c, ShaderUniformDataType.ShaderUniformVec2);
            }

            if (IsKeyPressed(Key.Space))
            {
                pause = !pause; // Pause animation (c change)
            }

            if (IsKeyPressed(Key.F1))
            {
                showControls = !showControls; // Toggle whether or not to show controls
            }

            if (!pause)
            {
                if (IsKeyPressed(Key.Right))
                {
                    incrementSpeed++;
                }
                else if (IsKeyPressed(Key.Left))
                {
                    incrementSpeed--;
                }

                // TODO: The idea is to zoom and move around with mouse
                // Probably offset movement should be proportional to zoom level
                if (IsMouseButtonDown(MouseButton.Left) || IsMouseButtonDown(MouseButton.Right))
                {
                    if (IsMouseButtonDown(MouseButton.Left))
                    {
                        zoom += zoom * 0.003f;
                    }

                    if (IsMouseButtonDown(MouseButton.Right))
                    {
                        zoom -= zoom * 0.003f;
                    }

                    Vector2 mousePos = GetMousePosition();

                    offsetSpeed.X = mousePos.X - ((float)screenWidth / 2);
                    offsetSpeed.Y = mousePos.Y - ((float)screenHeight / 2);

                    // Slowly move camera to targetOffset
                    offset[0] += GetFrameTime() * offsetSpeed.X * 0.8f;
                    offset[1] += GetFrameTime() * offsetSpeed.Y * 0.8f;
                }
                else
                {
                    offsetSpeed = new(0.0f, 0.0f);
                }

                SetShaderValue(shader, zoomLoc, ref zoom, ShaderUniformDataType.ShaderUniformFloat);
                SetShaderValue(shader, offsetLoc, ref offset, ShaderUniformDataType.ShaderUniformVec2);

                // Increment c value with time
                float amount = GetFrameTime() * incrementSpeed * 0.0005f;
                c[0] += amount;
                c[1] += amount;

                SetShaderValue(shader, cLoc, ref c, ShaderUniformDataType.ShaderUniformVec2);
            }

            // Draw
            // Using a render texture to draw Julia set
            BeginTextureMode(target);
            {       // Enable drawing to texture
                ClearBackground(Black); // Clear the render texture

                // Draw a rectangle in shader mode to be used as shader canvas
                // NOTE: RectangleF uses font white character texture coordinates,
                // so shader can not be applied here directly because input vertexTexCoord
                // do not represent full screen coordinates (space where want to apply shader)
                DrawRectangle(0, 0, GetScreenWidth(), GetScreenHeight(), Black);
            }
            EndTextureMode();

            BeginDrawing();
            {
                ClearBackground(Black); // Clear screen background

                // Draw the saved texture and rendered julia set with shader
                // NOTE: We do not invert texture on Y, already considered inside shader
                BeginShaderMode(shader);
                {
                    // WARNING: If FLAG_WINDOW_HIGHDPI is enabled, HighDPI monitor scaling should be considered
                    // when rendering the RenderTexture to fit in the HighDPI scaled Window
                    DrawTexture(target.Texture, new(0.0f, 0.0f), 0.0f, 1.0f, White);
                }
                EndShaderMode();

                if (showControls)
                {
                    DrawText("Press Mouse buttons right/left to zoom in/out and move", 10, 15, 10, RayWhite);
                    DrawText("Press Key.F1 to toggle these controls", 10, 30, 10, RayWhite);
                    DrawText("Press KEYS [1 - 6] to change point of interest", 10, 45, 10, RayWhite);
                    DrawText("Press Key.Left | Key.Right to change speed", 10, 60, 10, RayWhite);
                    DrawText("Press Key.Space to pause movement animation", 10, 75, 10, RayWhite);
                }
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadShader(shader); // Unload shader
        UnloadRenderTexture(target); // Unload render texture

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
