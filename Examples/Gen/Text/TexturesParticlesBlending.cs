using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesParticlesBlending : ExampleHelper 
{

private const int MAX_PARTICLES = 200;

    // Particle structure with basic data
    typedef struct {
        Vector2 position;
        Color color;
        float alpha;
        float size;
        float rotation;
        bool active;        // NOTE: Use it to activate/deactive particle
    } Particle;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - particles blending");

        // Particles pool, reuse them!
        Particle mouseTail[MAX_PARTICLES] = new();

        // Initialize particles
        for (int i = 0; i < MAX_PARTICLES; i++)
        {
            mouseTail[i].position = new( 0, 0 );
            mouseTail[i].color = Color.FromArgb(255, GetRandomValue(0, 255), GetRandomValue(0, 255), GetRandomValue(0, 255));
            mouseTail[i].alpha = 1.0f;
            mouseTail[i].size = (float)GetRandomValue(1, 30)/20.0f;
            mouseTail[i].rotation = (float)GetRandomValue(0, 360);
            mouseTail[i].active = false;
        }

        float gravity = 3.0f;

        Texture smoke = LoadTexture("resources/spark_flame.png");

        int blending = BLEND_ALPHA;

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update

            // Activate one particle every frame and Update active particles
            // NOTE: Particles initial position should be mouse position when activated
            // NOTE: Particles fall down with gravity and rotation... and disappear after 2 seconds (alpha = 0)
            // NOTE: When a particle disappears, active = false and it can be reused.
            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                if (!mouseTail[i].active)
                {
                    mouseTail[i].active = true;
                    mouseTail[i].alpha = 1.0f;
                    mouseTail[i].position = GetMousePosition();
                    i = MAX_PARTICLES;
                }
            }

            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                if (mouseTail[i].active)
                {
                    mouseTail[i].position.Y += gravity/2;
                    mouseTail[i].alpha -= 0.005f;

                    if (mouseTail[i].alpha <= 0.0f) mouseTail[i].active = false;

                    mouseTail[i].rotation += 2.0f;
                }
            }

            if (IsKeyPressed(Key.Space))
            {
                if (blending == BLEND_ALPHA) blending = BLEND_ADDITIVE;
                else blending = BLEND_ALPHA;
            }

            // Draw
            BeginDrawing();{

                ClearBackground(DarkGray);

                BeginBlendMode(blending);

                    // Draw active particles
                    for (int i = 0; i < MAX_PARTICLES; i++)
                    {
                        if (mouseTail[i].active) DrawTexture(smoke, new( 0.0f, 0.0f, (float)smoke.Width, (float)smoke.Height ),
                                                               new( mouseTail[i].position.X, mouseTail[i].position.Y, smoke.Width*mouseTail[i].size, smoke.Height*mouseTail[i].size ),
                                                               new( (float)(smoke.Width*mouseTail[i].size/2.0f), (float)(smoke.Height*mouseTail[i].size/2.0f) ), mouseTail[i].rotation,
                                                               Fade(mouseTail[i].color, mouseTail[i].alpha));
                    }

                EndBlendMode();

                DrawText("PRESS SPACE to CHANGE BLENDING MODE", 180, 20, 20, Black);

                if (blending == BLEND_ALPHA) DrawText("ALPHA BLENDING", 290, screenHeight - 40, 20, Black);
                else DrawText("ADDITIVE BLENDING", 280, screenHeight - 40, 20, RayWhite);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(smoke);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
