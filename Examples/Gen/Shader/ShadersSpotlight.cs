using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;
using RaylibSharp.GL;

using static RaylibSharp.Raylib;

public partial class ShadersSpotlight : ExampleHelper 
{

    #if defined(PLATFORM_DESKTOP)
private const int GLSL_VERSION = 330;
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

private const int MAX_SPOTS = 3;
private const int MAX_STARS = 400;

    // Spot data
    typedef struct Spot {
        Vector2 position;
        Vector2 speed;
        float inner;
        float radius;

        // Shader locations
        uint positionLoc;
        uint innerLoc;
        uint radiusLoc;
    } Spot;

    // Stars in the star field have a position and velocity
    typedef struct Star {
        Vector2 position;
        Vector2 speed;
    } Star;

    static static void UpdateStar(Star *s);
    static static void ResetStar(Star *s);

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - shaders - shader spotlight");
        HideCursor();

        Texture texRay = LoadTexture("resources/raysan.png");

        Star stars[MAX_STARS] = new();

        for (int n = 0; n < MAX_STARS; n++) ResetStar(ref stars[n]);

        // Progress all the stars on, so they don't all start in the centre
        for (int m = 0; m < screenWidth/2.0; m++)
        {
            for (int n = 0; n < MAX_STARS; n++) UpdateStar(ref stars[n]);
        }

        int frameCounter = 0;

        // Use default vert shader
        Shader shdrSpot = LoadShader(0, TextFormat("resources/shaders/glsl%i/spotlight.fs", GLSL_VERSION));

        // Get the locations of spots in the shader
        Spot spots[MAX_SPOTS];

        for (int i = 0; i < MAX_SPOTS; i++)
        {
            string posName = "spots[x].pos\0";
            string innerName = "spots[x].inner\0";
            string radiusName = "spots[x].Radius\0";

            posName[6] = '0' + i;
            innerName[6] = '0' + i;
            radiusName[6] = '0' + i;

            spots[i].positionLoc = GetShaderLocation(shdrSpot, posName);
            spots[i].innerLoc = GetShaderLocation(shdrSpot, innerName);
            spots[i].RadiusLoc = GetShaderLocation(shdrSpot, radiusName);

        }

        // Tell the shader how wide the screen is so we can have
        // a pitch black half and a dimly lit half.
        uint wLoc = GetShaderLocation(shdrSpot, "screenWidth");
        float sw = (float)GetScreenWidth();
        SetShaderValue(shdrSpot, wLoc, ref sw, SHADER_UNIFORM_FLOAT);

        // Randomize the locations and velocities of the spotlights
        // and initialize the shader locations
        for (int i = 0; i < MAX_SPOTS; i++)
        {
            spots[i].position.X = (float)GetRandomValue(64, screenWidth - 64);
            spots[i].position.Y = (float)GetRandomValue(64, screenHeight - 64);
            spots[i].speed = new( 0, 0 );

            while ((fabs(spots[i].speed.X) + fabs(spots[i].speed.Y)) < 2)
            {
                spots[i].speed.X = GetRandomValue(-400, 40) / 10.0f;
                spots[i].speed.Y = GetRandomValue(-400, 40) / 10.0f;
            }

            spots[i].inner = 28.0f * (i + 1);
            spots[i].Radius = 48.0f * (i + 1);

            SetShaderValue(shdrSpot, spots[i].positionLoc, ref spots[i].position.X, SHADER_UNIFORM_VEC2);
            SetShaderValue(shdrSpot, spots[i].innerLoc, ref spots[i].inner, SHADER_UNIFORM_FLOAT);
            SetShaderValue(shdrSpot, spots[i].RadiusLoc, ref spots[i].Radius, SHADER_UNIFORM_FLOAT);
        }

        SetTargetFPS(60);               // Set  to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            frameCounter++;

            // Move the stars, resetting them if the go offscreen
            for (int n = 0; n < MAX_STARS; n++) UpdateStar(ref stars[n]);

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

                    if (spots[i].position.X < 64) spots[i].speed.X = -spots[i].speed.X;
                    if (spots[i].position.X > (screenWidth - 64)) spots[i].speed.X = -spots[i].speed.X;
                    if (spots[i].position.Y < 64) spots[i].speed.Y = -spots[i].speed.Y;
                    if (spots[i].position.Y > (screenHeight - 64)) spots[i].speed.Y = -spots[i].speed.Y;
                }

                SetShaderValue(shdrSpot, spots[i].positionLoc, ref spots[i].position.X, SHADER_UNIFORM_VEC2);
            }

            // Draw
            BeginDrawing();{

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
                        (int)((screenWidth/2.0f) + cos((frameCounter + i*8)/51.45f)*(screenWidth/2.2f) - 32),
                        (int)((screenHeight/2.0f) + sin((frameCounter + i*8)/17.87f)*(screenHeight/4.2f)), White);
                }

                // Draw spot lights
                BeginShaderMode(shdrSpot);
                    // Instead of a blank rectangle you could render here
                    // a render texture of the full screen used to do screen
                    // scaling (slight adjustment to shader would be required
                    // to actually pay attention to the colour!)
                    DrawRectangle(0, 0, screenWidth, screenHeight, White);
                EndShaderMode();

                DrawFPS(10, 10);

                DrawText("Move the mouse!", 10, 30, 20, Green);
                DrawText("Pitch Black", (int)(screenWidth*0.2f), screenHeight/2, 20, Green);
                DrawText("Dark", (int)(screenWidth*.66f), screenHeight/2, 20, Green);

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texRay);
        UnloadShader(shdrSpot);

        CloseWindow();        // Close window and OpenGL context

        return 0;
    }

    static static void ResetStar(Star *s)
    {
        s.position = new( GetScreenWidth()/2.0f, GetScreenHeight()/2.0f );

        do
        {
            s.speed.X = (float)GetRandomValue(-1000, 1000)/100.0f;
            s.speed.Y = (float)GetRandomValue(-1000, 1000)/100.0f;

        } while (!(fabs(s.speed.X) + (fabs(s.speed.Y) > 1)));

        s.position = Vector2Add(s.position, Vector2Multiply(s.speed, new( 8.0f, 8.0f )));
    }

    static static void UpdateStar(Star *s)
    {
        s.position = Vector2Add(s.position, s.speed);

        if ((s.position.X < 0) || (s.position.X > GetScreenWidth()) ||
            (s.position.Y < 0) || (s.position.Y > GetScreenHeight()))
        {
            ResetStar(s);
        }
    }

}
