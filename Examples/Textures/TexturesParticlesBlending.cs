using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class TexturesParticlesBlending : ExampleHelper
{

    const int MAX_PARTICLES = 200;

    // Particle structure with basic data
    struct Particle
    {
        public Vector2 Position;
        public Color Color;
        public float Alpha;
        public float Size;
        public float Rotation;
        public bool Active;        // NOTE: Use it to activate/deactive particle
    }

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - textures - particles blending");

        // Particles pool, reuse them!
        Particle[] mouseTail = new Particle[MAX_PARTICLES];

        // Initialize particles
        for (int i = 0; i < MAX_PARTICLES; i++)
        {
            mouseTail[i].Position = new(0, 0);
            mouseTail[i].Color = new Color(GetRandomValue(0, 255), GetRandomValue(0, 255), GetRandomValue(0, 255));
            mouseTail[i].Alpha = 1.0f;
            mouseTail[i].Size = GetRandomValue(1, 30) / 20.0f;
            mouseTail[i].Rotation = GetRandomValue(0, 360);
            mouseTail[i].Active = false;
        }

        float gravity = 3.0f;

        Texture smoke = LoadTexture("resources/spark_flame.png");

        BlendMode blending = BlendMode.Alpha;

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
                if (!mouseTail[i].Active)
                {
                    mouseTail[i].Active = true;
                    mouseTail[i].Alpha = 1.0f;
                    mouseTail[i].Position = GetMousePosition();
                    i = MAX_PARTICLES;
                }
            }

            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                if (mouseTail[i].Active)
                {
                    mouseTail[i].Position.Y += gravity / 2;
                    mouseTail[i].Alpha -= 0.005f;

                    if (mouseTail[i].Alpha <= 0.0f)
                    {
                        mouseTail[i].Active = false;
                    }

                    mouseTail[i].Rotation += 2.0f;
                }
            }

            if (IsKeyPressed(Key.Space))
            {
                if (blending == BlendMode.Alpha)
                {
                    blending = BlendMode.Additive;
                }
                else
                {
                    blending = BlendMode.Alpha;
                }
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(DarkGray);

                BeginBlendMode(blending);
                {
                    // Draw active particles
                    for (int i = 0; i < MAX_PARTICLES; i++)
                    {
                        if (mouseTail[i].Active)
                        {
                            DrawTexture(smoke, new(0.0f, 0.0f, smoke.Width, smoke.Height),
                                                               new(mouseTail[i].Position.X, mouseTail[i].Position.Y, smoke.Width * mouseTail[i].Size, smoke.Height * mouseTail[i].Size),
                                                               new((float)(smoke.Width * mouseTail[i].Size / 2.0f), (float)(smoke.Height * mouseTail[i].Size / 2.0f)), mouseTail[i].Rotation,
                                                               Fade(mouseTail[i].Color, mouseTail[i].Alpha));
                        }
                    }
                }
                EndBlendMode();

                DrawText("PRESS SPACE to CHANGE BLENDING MODE", 180, 20, 20, Black);

                if (blending == BlendMode.Alpha)
                {
                    DrawText("ALPHA BLENDING", 290, screenHeight - 40, 20, Black);
                }
                else
                {
                    DrawText("ADDITIVE BLENDING", 280, screenHeight - 40, 20, RayWhite);
                }
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(smoke);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
