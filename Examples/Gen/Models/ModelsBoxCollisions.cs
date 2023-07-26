using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsBoxCollisions : ExampleHelper 
{

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - box collisions");

        // Define the camera to look into our 3d world
        Camera camera = new( new(0.0f,10.0f, 10.0f ), { 0.0f, 0.0f, 0.0f), new(0.0f,1.0f, 0.0f), 45.0f, 0 };

        Vector3 playerPosition = new( 0.0f, 1.0f, 2.0f );
        Vector3 playerSize = new( 1.0f, 2.0f, 1.0f );
        Color playerColor = Green;

        Vector3 enemyBoxPos = new( -4.0f, 1.0f, 0.0f );
        Vector3 enemyBoxSize = new( 2.0f, 2.0f, 2.0f );

        Vector3 enemySpherePos = new( 4.0f, 0.0f, 0.0f );
        float enemySphereSize = 1.5f;

        bool collision = false;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update

            // Move player
            if (IsKeyDown(Key.Right)) playerPosition.X += 0.2f;
            else if (IsKeyDown(Key.Left)) playerPosition.X -= 0.2f;
            else if (IsKeyDown(Key.Down)) playerPosition.Z += 0.2f;
            else if (IsKeyDown(Key.Up)) playerPosition.Z -= 0.2f;

            collision = false;

            // Check collisions player vs enemy-box
            if (CheckCollisionBoxes(
                (BoundingBox){(Vector3){ playerPosition.X - playerSize.X/2,
                                         playerPosition.Y - playerSize.Y/2,
                                         playerPosition.Z - playerSize.Z/2 },
                              (Vector3){ playerPosition.X + playerSize.X/2,
                                         playerPosition.Y + playerSize.Y/2,
                                         playerPosition.Z + playerSize.Z/2 }},
                (BoundingBox){(Vector3){ enemyBoxPos.X - enemyBoxSize.X/2,
                                         enemyBoxPos.Y - enemyBoxSize.Y/2,
                                         enemyBoxPos.Z - enemyBoxSize.Z/2 },
                              (Vector3){ enemyBoxPos.X + enemyBoxSize.X/2,
                                         enemyBoxPos.Y + enemyBoxSize.Y/2,
                                         enemyBoxPos.Z + enemyBoxSize.Z/2 }})) collision = true;

            // Check collisions player vs enemy-sphere
            if (CheckCollisionBoxSphere(
                (BoundingBox){(Vector3){ playerPosition.X - playerSize.X/2,
                                         playerPosition.Y - playerSize.Y/2,
                                         playerPosition.Z - playerSize.Z/2 },
                              (Vector3){ playerPosition.X + playerSize.X/2,
                                         playerPosition.Y + playerSize.Y/2,
                                         playerPosition.Z + playerSize.Z/2 }},
                enemySpherePos, enemySphereSize)) collision = true;

            if (collision) playerColor = Red;
            else playerColor = Green;

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                    // Draw enemy-box
                    DrawCube(enemyBoxPos, enemyBoxSize.X, enemyBoxSize.Y, enemyBoxSize.Z, Gray);
                    DrawCubeWires(enemyBoxPos, enemyBoxSize.X, enemyBoxSize.Y, enemyBoxSize.Z, DarkGray);

                    // Draw enemy-sphere
                    DrawSphere(enemySpherePos, enemySphereSize, Gray);
                    DrawSphereWires(enemySpherePos, enemySphereSize, 16, 16, DarkGray);

                    // Draw player
                    DrawCube(playerPosition, playerSize, playerColor);

                    DrawGrid(10, 1.0f);        // Draw a grid

                }EndMode3D();

                DrawText("Move player with cursors to collide", 220, 40, 20, Gray);

                DrawFPS(10, 10);

            }EndDrawing();
        }

        // De-Initialization
        CloseWindow();        // Close window and OpenGL context

        return 0;
    }
}
