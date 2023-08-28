using System;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShadersSpotlight : ExampleHelper
{
    private const int MAX_SPOTS = 3;
    private const int MAX_STARS = 400;

    // Spot data
    private class Spot
    {
        public Vector2 position;
        public Vector2 speed;
        public float inner;
        public float radius;

        // Shader locations
        public int positionLoc;
        public int innerLoc;
        public int radiusLoc;
    }

    // Stars in the star field have a position and velocity
    private struct Star
    {
        public Vector2 position;
        public Vector2 speed;
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - shader spotlight");
        HideCursor();

        Texture texRay = LoadTexture("resources/raysan.png");

        Star[] stars = new Star[MAX_STARS];

        for (int n = 0; n < MAX_STARS; n++)
        {
            ResetStar(stars[n]);
        }

        // Progress all the stars on, so they don't all start in the centre
        for (int m = 0; m < screenWidth / 2.0; m++)
        {
            for (int n = 0; n < MAX_STARS; n++)
            {
                UpdateStar(stars[n]);
            }
        }

        int frameCounter = 0;

        int glslVersion = Environment.OSVersion.Platform == PlatformID.Other ? 100 : 330;

        // Use default vert shader
        Shader shdrSpot = LoadShader(null, $"resources/shaders/glsl{glslVersion}/spotlight.fs");

        // Get the locations of spots in the shader
        Spot[] spots = new Spot[MAX_SPOTS];

        for (int i = 0; i < MAX_SPOTS; i++)
        {
            char x = (char)('0' + i);
            string posName = $"spots[{x}].pos";
            string innerName = $"spots[{x}].inner";
            string radiusName = $"spots[{x}].Radius";

            spots[i].positionLoc = GetShaderLocation(shdrSpot, posName);
            spots[i].innerLoc = GetShaderLocation(shdrSpot, innerName);
            spots[i].radiusLoc = GetShaderLocation(shdrSpot, radiusName);
        }

        // Tell the shader how wide the screen is so we can have
        // a pitch black half and a dimly lit half.
        int wLoc = GetShaderLocation(shdrSpot, "screenWidth");
        float sw = GetScreenWidth();
        SetShaderValue(shdrSpot, wLoc, ref sw, ShaderUniformDataType.ShaderUniformFloat);

        // Randomize the locations and velocities of the spotlights
        // and initialize the shader locations
        for (int i = 0; i < MAX_SPOTS; i++)
        {
            spots[i].position.X = GetRandomValue(64, screenWidth - 64);
            spots[i].position.Y = GetRandomValue(64, screenHeight - 64);
            spots[i].speed = new(0, 0);

            while ((MathF.Abs(spots[i].speed.X) + MathF.Abs(spots[i].speed.Y)) < 2)
            {
                spots[i].speed.X = GetRandomValue(-400, 40) / 10.0f;
                spots[i].speed.Y = GetRandomValue(-400, 40) / 10.0f;
            }

            spots[i].inner = 28.0f * (i + 1);
            spots[i].radius = 48.0f * (i + 1);

            SetShaderValue(shdrSpot, spots[i].positionLoc, ref spots[i].position.X, ShaderUniformDataType.ShaderUniformVec2);
            SetShaderValue(shdrSpot, spots[i].innerLoc, ref spots[i].inner, ShaderUniformDataType.ShaderUniformFloat);
            SetShaderValue(shdrSpot, spots[i].radiusLoc, ref spots[i].radius, ShaderUniformDataType.ShaderUniformFloat);
        }

        SetTargetFPS(60);               // Set  to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            frameCounter++;

            // Move the stars, resetting them if the go offscreen
            for (int n = 0; n < MAX_STARS; n++)
            {
                UpdateStar(stars[n]);
            }

            // Update the spots, send them to the shader
            for (int i = 0; i < MAX_SPOTS; i++)
            {
                if (i == 0)
                {
                    Vector2 mp = GetMousePosition();
                    spots[i].position.X = mp.X;
                    spots[i].position.Y = screenHeight - mp.Y;
                }
                else
                {
                    spots[i].position.X += spots[i].speed.X;
                    spots[i].position.Y += spots[i].speed.Y;

                    if (spots[i].position.X < 64)
                    {
                        spots[i].speed.X = -spots[i].speed.X;
                    }

                    if (spots[i].position.X > (screenWidth - 64))
                    {
                        spots[i].speed.X = -spots[i].speed.X;
                    }

                    if (spots[i].position.Y < 64)
                    {
                        spots[i].speed.Y = -spots[i].speed.Y;
                    }

                    if (spots[i].position.Y > (screenHeight - 64))
                    {
                        spots[i].speed.Y = -spots[i].speed.Y;
                    }
                }

                SetShaderValue(shdrSpot, spots[i].positionLoc, ref spots[i].position.X, ShaderUniformDataType.ShaderUniformVec2);
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(DarkBlue);

                // Draw stars and bobs
                for (int n = 0; n < MAX_STARS; n++)
                {
                    // Single pixel is just too small these days!
                    DrawRectangle((int)stars[n].position.X, (int)stars[n].position.Y, 2, 2, White);
                }

                for (int i = 0; i < 16; i++)
                {
                    DrawTexture(texRay,
                        (int)((screenWidth / 2.0f) + (MathF.Cos((frameCounter + (i * 8)) / 51.45f) * (screenWidth / 2.2f)) - 32),
                        (int)((screenHeight / 2.0f) + (MathF.Sin((frameCounter + (i * 8)) / 17.87f) * (screenHeight / 4.2f))), White);
                }

                // Draw spot lights
                BeginShaderMode(shdrSpot);
                {
                    // Instead of a blank rectangle you could render here
                    // a render texture of the full screen used to do screen
                    // scaling (slight adjustment to shader would be required
                    // to actually pay attention to the colour!)
                    DrawRectangle(0, 0, screenWidth, screenHeight, White);
                }
                EndShaderMode();

                DrawFPS(10, 10);

                DrawText("Move the mouse!", 10, 30, 20, Green);
                DrawText("Pitch Black", (int)(screenWidth * 0.2f), screenHeight / 2, 20, Green);
                DrawText("Dark", (int)(screenWidth * .66f), screenHeight / 2, 20, Green);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texRay);
        UnloadShader(shdrSpot);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    private static void ResetStar(Star s)
    {
        s.position = new(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);

        do
        {
            s.speed.X = GetRandomValue(-1000, 1000) / 100.0f;
            s.speed.Y = GetRandomValue(-1000, 1000) / 100.0f;

        } while (!(MathF.Abs(s.speed.X) + MathF.Abs(s.speed.Y) > 1));

        s.position = Vector2.Add(s.position, Vector2.Multiply(s.speed, new Vector2(8.0f, 8.0f)));
    }

    private static void UpdateStar(Star s)
    {
        s.position = Vector2.Add(s.position, s.speed);

        if ((s.position.X < 0) || (s.position.X > GetScreenWidth()) ||
            (s.position.Y < 0) || (s.position.Y > GetScreenHeight()))
        {
            ResetStar(s);
        }
    }

}
