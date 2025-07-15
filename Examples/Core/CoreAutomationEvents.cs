/*******************************************************************************************
*
*   raylib [core] example - automation events
*
*   Example originally created with raylib 5.0, last time updated with raylib 5.0
*
*   Example based on 2d_camera_platformer example by arvyy (@arvyy)
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2023 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreAutomationEvents : ExampleHelper
{
    const int GRAVITY = 400;
    const float PLAYER_JUMP_SPD = 350.0f;
    const float PLAYER_HOR_SPD = 200.0f;

    const int MAX_ENVIRONMENT_ELEMENTS = 5;

    struct Player(Vector2 position, float speed, bool canJump) {
        public Vector2 Position = position;
        public float Speed = speed;
        public bool CanJump = canJump;
    }

    struct EnvElement(Rectangle rect, int blocking, Color color) {
        public Rectangle Rect = rect;
        public int Blocking = blocking;
        public Color Color = color;
    }


    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - automation events");

        // Define player
        Player player = new();
        player.Position = new(400, 280);
        player.Speed = 0;
        player.CanJump = false;

        // Define environment elements (platforms)
        EnvElement[] envElements = new EnvElement[MAX_ENVIRONMENT_ELEMENTS]{
            new(new(0, 0, 1000, 400), false, LIGHTGRAY),
            new(new(0, 400, 1000, 200), true, GRAY),
            new(new(300, 200, 400, 10), true, GRAY),
            new(new(250, 300, 100, 10), true, GRAY),
            new(new(650, 300, 100, 10), true, GRAY)
        };

        // Define camera
        Camera2D camera = new();
        camera.Target = player.Position;
        camera.Offset = new(screenWidth/2.0f, screenHeight/2.0f);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        // Automation events
        AutomationEventList aelist = LoadAutomationEventList(0);  // Initialize list of automation events to record new events
        SetAutomationEventList(&aelist);
        bool eventRecording = false;
        bool eventPlaying = false;

        uint frameCounter = 0;
        uint playFrameCounter = 0;
        uint currentPlayFrame = 0;

        SetTargetFPS(60);
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            //----------------------------------------------------------------------------------
            float deltaTime = 0.015f;//GetFrameTime();

            // Dropped files logic
            //----------------------------------------------------------------------------------
            if (IsFileDropped())
            {
                FilePathList droppedFiles = LoadDroppedFiles();

                // Supports loading .rgs style files (text or binary) and .png style palette images
                if (IsFileExtension(droppedFiles.Paths[0], ".txt;.rae"))
                {
                    UnloadAutomationEventList(aelist);
                    aelist = LoadAutomationEventList(droppedFiles.Paths[0]);

                    eventRecording = false;

                    // Reset scene state to play
                    eventPlaying = true;
                    playFrameCounter = 0;
                    currentPlayFrame = 0;

                    player.Position = new(400, 280);
                    player.Speed = 0;
                    player.CanJump = false;

                    camera.Target = player.Position;
                    camera.Offset = new(screenWidth/2.0f, screenHeight/2.0f);
                    camera.Rotation = 0.0f;
                    camera.Zoom = 1.0f;
                }

                UnloadDroppedFiles(droppedFiles);   // Unload filepaths from memory
            }
            //----------------------------------------------------------------------------------

            // Update player
            //----------------------------------------------------------------------------------
            if (IsKeyDown(Key.Left)) player.Position.X -= PLAYER_HOR_SPD*deltaTime;
            if (IsKeyDown(Key.Right)) player.Position.X += PLAYER_HOR_SPD*deltaTime;
            if (IsKeyDown(Key.Space) && player.CanJump)
            {
                player.Speed = -PLAYER_JUMP_SPD;
                player.CanJump = false;
            }

            int hitObstacle = 0;
            for (int i = 0; i < MAX_ENVIRONMENT_ELEMENTS; i++)
            {
                EnvElement *element = &envElements[i];
                Vector2 *p = &(player.Position);
                if (element.blocking &&
                    element.Rect.X <= p.X &&
                    element.Rect.X + element.Rect.Width >= p.X &&
                    element.Rect.Y >= p.Y &&
                    element.Rect.Y <= p.Y + player.Speed*deltaTime)
                {
                    hitObstacle = 1;
                    player.Speed = 0.0f;
                    p.Y = element.Rect.Y;
                }
            }

            if (!hitObstacle)
            {
                player.Position.Y += player.Speed*deltaTime;
                player.Speed += GRAVITY*deltaTime;
                player.CanJump = false;
            }
            else player.CanJump = true;

            if (IsKeyPressed(Key.R))
            {
                // Reset game state
                player.Position = new(400, 280);
                player.Speed = 0;
                player.CanJump = false;

                camera.Target = player.Position;
                camera.Offset = new(screenWidth/2.0f, screenHeight/2.0f);
                camera.Rotation = 0.0f;
                camera.Zoom = 1.0f;
            }
            //----------------------------------------------------------------------------------

            // Events playing
            // NOTE: Logic must be before Camera update because it depends on mouse-wheel value,
            // that can be set by the played event... but some other inputs could be affected
            //----------------------------------------------------------------------------------
            if (eventPlaying)
            {
                // NOTE: Multiple events could be executed in a single frame
                while (playFrameCounter == aelist.events[currentPlayFrame].frame)
                {
                    PlayAutomationEvent(aelist.events[currentPlayFrame]);
                    currentPlayFrame++;

                    if (currentPlayFrame == aelist.Count)
                    {
                        eventPlaying = false;
                        currentPlayFrame = 0;
                        playFrameCounter = 0;

                        TraceLog(TraceLogLevel.Info, "FINISH PLAYING!");
                        break;
                    }
                }

                playFrameCounter++;
            }
            //----------------------------------------------------------------------------------

            // Update camera
            //----------------------------------------------------------------------------------
            camera.Target = player.Position;
            camera.Offset = new(screenWidth/2.0f, screenHeight/2.0f);
            float minX = 1000, minY = 1000, maxX = -1000, maxY = -1000;

            // WARNING: On event replay, mouse-wheel internal value is set
            camera.Zoom += ((float)GetMouseWheelMove()*0.05f);
            if (camera.Zoom > 3.0f) camera.Zoom = 3.0f;
            else if (camera.Zoom < 0.25f) camera.Zoom = 0.25f;

            for (int i = 0; i < MAX_ENVIRONMENT_ELEMENTS; i++)
            {
                EnvElement *element = &envElements[i];
                minX = fminf(element.Rect.X, minX);
                maxX = fmaxf(element.Rect.X + element.Rect.Width, maxX);
                minY = fminf(element.Rect.Y, minY);
                maxY = fmaxf(element.Rect.Y + element.Rect.Height, maxY);
            }

            Vector2 max = GetWorldToScreen2D(new(maxX, maxY), camera);
            Vector2 min = GetWorldToScreen2D(new(minX, minY), camera);

            if (max.X < screenWidth) camera.Offset.X = screenWidth - (max.X - screenWidth/2);
            if (max.Y < screenHeight) camera.Offset.Y = screenHeight - (max.Y - screenHeight/2);
            if (min.X > 0) camera.Offset.X = screenWidth/2 - min.X;
            if (min.Y > 0) camera.Offset.Y = screenHeight/2 - min.Y;
            //----------------------------------------------------------------------------------

            // Events management
            if (IsKeyPressed(Key.S))    // Toggle events recording
            {
                if (!eventPlaying)
                {
                    if (eventRecording)
                    {
                        StopAutomationEventRecording();
                        eventRecording = false;

                        ExportAutomationEventList(aelist, "automation.rae");

                        TraceLog(TraceLogLevel.Info, "RECORDED FRAMES: %i", aelist.Count);
                    }
                    else
                    {
                        SetAutomationEventBaseFrame(180);
                        StartAutomationEventRecording();
                        eventRecording = true;
                    }
                }
            }
            else if (IsKeyPressed(Key.A)) // Toggle events playing (WARNING: Starts next frame)
            {
                if (!eventRecording && (aelist.Count > 0))
                {
                    // Reset scene state to play
                    eventPlaying = true;
                    playFrameCounter = 0;
                    currentPlayFrame = 0;

                    player.Position = new(400, 280);
                    player.Speed = 0;
                    player.CanJump = false;

                    camera.Target = player.Position;
                    camera.Offset = new(screenWidth/2.0f, screenHeight/2.0f);
                    camera.Rotation = 0.0f;
                    camera.Zoom = 1.0f;
                }
            }

            if (eventRecording || eventPlaying) frameCounter++;
            else frameCounter = 0;
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(LIGHTGRAY);

                BeginMode2D(camera);

                    // Draw environment elements
                    for (int i = 0; i < MAX_ENVIRONMENT_ELEMENTS; i++)
                    {
                        DrawRectangleRec(envElements[i].Rect, envElements[i].Color);
                    }

                    // Draw player rectangle
                    DrawRectangleRec(new(player.Position.X - 20, player.Position.Y - 40, 40, 40), RED);

                EndMode2D();

                // Draw game controls
                DrawRectangle(10, 10, 290, 145, Fade(SKYBLUE, 0.5f));
                DrawRectangleLines(10, 10, 290, 145, Fade(BLUE, 0.8f));

                DrawText("Controls:", 20, 20, 10, BLACK);
                DrawText("- RIGHT | LEFT: Player movement", 30, 40, 10, DARKGRAY);
                DrawText("- SPACE: Player jump", 30, 60, 10, DARKGRAY);
                DrawText("- R: Reset game state", 30, 80, 10, DARKGRAY);

                DrawText("- S: START/STOP RECORDING INPUT EVENTS", 30, 110, 10, BLACK);
                DrawText("- A: REPLAY LAST RECORDED INPUT EVENTS", 30, 130, 10, BLACK);

                // Draw automation events recording indicator
                if (eventRecording)
                {
                    DrawRectangle(10, 160, 290, 30, Fade(RED, 0.3f));
                    DrawRectangleLines(10, 160, 290, 30, Fade(MAROON, 0.8f));
                    DrawCircle(30, 175, 10, MAROON);

                    if (((frameCounter/15)%2) == 1) DrawText(TextFormat("RECORDING EVENTS... [%i]", aelist.Count), 50, 170, 10, MAROON);
                }
                else if (eventPlaying)
                {
                    DrawRectangle(10, 160, 290, 30, Fade(LIME, 0.3f));
                    DrawRectangleLines(10, 160, 290, 30, Fade(DARKGREEN, 0.8f));
                    DrawTriangle(new(20, 155 + 10), new(20, 155 + 30), new(40, 155 + 20), DARKGREEN);

                    if (((frameCounter/15)%2) == 1) DrawText(TextFormat("PLAYING RECORDED EVENTS... [%i]", currentPlayFrame), 50, 170, 10, DARKGREEN);
                }


            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

