using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ShapesEasingsRectangleArray : ExampleHelper
{

    private const int RECS_WIDTH = 50;
    private const int RECS_HEIGHT = 50;

    private const int MAX_RECS_X = 800 / RECS_WIDTH;
    private const int MAX_RECS_Y = 450 / RECS_HEIGHT;

    private const int PLAY_TIME_IN_FRAMES = 240;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shapes - easings rectangle array");

        RectangleF[] recs = new RectangleF[MAX_RECS_X * MAX_RECS_Y];

        for (int y = 0; y < MAX_RECS_Y; y++)
        {
            for (int x = 0; x < MAX_RECS_X; x++)
            {
                recs[(y * MAX_RECS_X) + x].X = (RECS_WIDTH / 2.0f) + (RECS_WIDTH * x);
                recs[(y * MAX_RECS_X) + x].Y = (RECS_HEIGHT / 2.0f) + (RECS_HEIGHT * y);
                recs[(y * MAX_RECS_X) + x].Width = RECS_WIDTH;
                recs[(y * MAX_RECS_X) + x].Height = RECS_HEIGHT;
            }
        }

        float rotation = 0.0f;
        int framesCounter = 0;
        int state = 0; // Rectangles animation state: 0-Playing, 1-Finished

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            if (state == 0)
            {
                framesCounter++;

                for (int i = 0; i < MAX_RECS_X * MAX_RECS_Y; i++)
                {
                    recs[i].Height = EaseCircOut(framesCounter, RECS_HEIGHT, -RECS_HEIGHT, PLAY_TIME_IN_FRAMES);
                    recs[i].Width = EaseCircOut(framesCounter, RECS_WIDTH, -RECS_WIDTH, PLAY_TIME_IN_FRAMES);

                    if (recs[i].Height < 0)
                    {
                        recs[i].Height = 0;
                    }

                    if (recs[i].Width < 0)
                    {
                        recs[i].Width = 0;
                    }

                    if ((recs[i].Height == 0) && (recs[i].Width == 0))
                    {
                        state = 1; // Finish playing
                    }

                    rotation = EaseLinearIn(framesCounter, 0.0f, 360.0f, PLAY_TIME_IN_FRAMES);
                }
            }
            else if ((state == 1) && IsKeyPressed(Key.Space))
            {
                // When animation has finished, press space to restart
                framesCounter = 0;

                for (int i = 0; i < MAX_RECS_X * MAX_RECS_Y; i++)
                {
                    recs[i].Height = RECS_HEIGHT;
                    recs[i].Width = RECS_WIDTH;
                }

                state = 0;
            }

            // Draw
            BeginDrawing();
            {
                ClearBackground(RayWhite);

                if (state == 0)
                {
                    for (int i = 0; i < MAX_RECS_X * MAX_RECS_Y; i++)
                    {
                        DrawRectangle(recs[i], new(recs[i].Width / 2, recs[i].Height / 2), rotation, Red);
                    }
                }
                else if (state == 1)
                {
                    DrawText("PRESS [SPACE] TO PLAY AGAIN!", 240, 200, 20, Gray);
                }
            }
            EndDrawing();
        }

        // De-Initialization
        CloseWindow(); // Close window and OpenGL context

        return 0;
    }
}
