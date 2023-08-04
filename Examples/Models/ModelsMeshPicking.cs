using System.Drawing;
using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsMeshPicking : ExampleHelper
{
    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - mesh picking");

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(20.0f, 20.0f, 20.0f); // Camera3D position
        camera.Target = new(0.0f, 8.0f, 0.0f);      // Camera3D looking at point
        camera.Up = new(0.0f, 1.6f, 0.0f);          // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera3D projection type
        _ = new
        Ray();        // Picking ray

        Model tower = LoadModel("resources/models/obj/turret.obj");                 // Load OBJ model
        Texture texture = LoadTexture("resources/models/obj/turret_diffuse.png"); // Load model texture
        tower.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;            // Set model diffuse texture

        Vector3 towerPos = new(0.0f, 0.0f, 0.0f);                        // Set model position
        BoundingBox towerBBox = GetMeshBoundingBox(tower.Meshes[0]);    // Get mesh bounding box

        // Ground quad
        Vector3 g0 = new(-50.0f, 0.0f, -50.0f);
        Vector3 g1 = new(-50.0f, 0.0f, 50.0f);
        Vector3 g2 = new(50.0f, 0.0f, 50.0f);
        Vector3 g3 = new(50.0f, 0.0f, -50.0f);

        // Test triangle
        Vector3 ta = new(-25.0f, 0.5f, 0.0f);
        Vector3 tb = new(-4.0f, 2.5f, 1.0f);
        Vector3 tc = new(-8.0f, 6.5f, 0.0f);

        Vector3 bary = new(0.0f, 0.0f, 0.0f);

        // Test sphere
        Vector3 sp = new(-30.0f, 5.0f, 5.0f);
        float sr = 4.0f;

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            if (IsCursorHidden())
            {
                UpdateCamera(ref camera, CameraMode.FirstPerson);          // Update camera
            }

            // Toggle camera controls
            if (IsMouseButtonPressed(MouseButton.Right))
            {
                if (IsCursorHidden())
                {
                    EnableCursor();
                }
                else
                {
                    DisableCursor();
                }
            }

            // Display information about closest hit
            RayCollision collision = new();
            string hitObjectName = "None";
            collision.Distance = float.MaxValue;
            collision.Hit = false;
            Color cursorColor = White;

            // Get ray and test against objects
            Ray ray = GetMouseRay(GetMousePosition(), camera);

            // Check ray collision against ground quad
            RayCollision groundHitInfo = GetRayCollisionQuad(ray, g0, g1, g2, g3);

            if (groundHitInfo.Hit && (groundHitInfo.Distance < collision.Distance))
            {
                collision = groundHitInfo;
                cursorColor = Green;
                hitObjectName = "Ground";
            }

            // Check ray collision against test triangle
            RayCollision triHitInfo = GetRayCollisionTriangle(ray, ta, tb, tc);

            if (triHitInfo.Hit && (triHitInfo.Distance < collision.Distance))
            {
                collision = triHitInfo;
                cursorColor = Purple;
                hitObjectName = "Triangle";

                bary = Vector3Barycenter(collision.Point, ta, tb, tc);
            }

            // Check ray collision against test sphere
            RayCollision sphereHitInfo = GetRayCollisionSphere(ray, sp, sr);

            if (sphereHitInfo.Hit && (sphereHitInfo.Distance < collision.Distance))
            {
                collision = sphereHitInfo;
                cursorColor = Orange;
                hitObjectName = "Sphere";
            }

            // Check ray collision against bounding box first, before trying the full ray-mesh test
            RayCollision boxHitInfo = GetRayCollisionBox(ray, towerBBox);

            if (boxHitInfo.Hit && (boxHitInfo.Distance < collision.Distance))
            {
                collision = boxHitInfo;
                cursorColor = Orange;
                hitObjectName = "Box";

                // Check ray collision against model meshes
                RayCollision meshHitInfo = new();
                for (int m = 0; m < tower.MeshCount; m++)
                {
                    // NOTE: We consider the model.transform for the collision check but
                    // it can be checked against any transform Matrix, used when checking against same
                    // model drawn multiple times with multiple transforms
                    meshHitInfo = GetRayCollisionMesh(ray, tower.Meshes[m], tower.Transform);
                    if (meshHitInfo.Hit)
                    {
                        // Save the closest hit mesh
                        if ((!collision.Hit) || (collision.Distance > meshHitInfo.Distance))
                        {
                            collision = meshHitInfo;
                        }

                        break;  // Stop once one mesh collision is detected, the colliding mesh is m
                    }
                }

                if (meshHitInfo.Hit)
                {
                    collision = meshHitInfo;
                    cursorColor = Orange;
                    hitObjectName = "Mesh";
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    // Draw the tower
                    // WARNING: If scale is different than 1.0f,
                    // not considered by GetRayCollisionModel()
                    DrawModel(tower, towerPos, 1.0f, White);

                    // Draw the test triangle
                    DrawLine3D(ta, tb, Purple);
                    DrawLine3D(tb, tc, Purple);
                    DrawLine3D(tc, ta, Purple);

                    // Draw the test sphere
                    DrawSphereWires(sp, sr, 8, 8, Purple);

                    // Draw the mesh bbox if we hit it
                    if (boxHitInfo.Hit)
                    {
                        DrawBoundingBox(towerBBox, Lime);
                    }

                    // If we hit something, draw the cursor at the hit point
                    if (collision.Hit)
                    {
                        DrawCube(collision.Point, 0.3f, 0.3f, 0.3f, cursorColor);
                        DrawCubeWires(collision.Point, 0.3f, 0.3f, 0.3f, Red);

                        Vector3 normalEnd;
                        normalEnd.X = collision.Point.X + collision.Normal.X;
                        normalEnd.Y = collision.Point.Y + collision.Normal.Y;
                        normalEnd.Z = collision.Point.Z + collision.Normal.Z;

                        DrawLine3D(collision.Point, normalEnd, Red);
                    }

                    DrawRay(ray, Maroon);

                    DrawGrid(10, 10.0f);

                }
                EndMode3D();

                // Draw some debug GUI text
                DrawText("Hit Object: " + hitObjectName, 10, 50, 10, Black);

                if (collision.Hit)
                {
                    int ypos = 70;

                    DrawText("Distance: " + collision.Distance.ToString("0.000"), 10, ypos, 10, Black);
                    DrawText($"Hit Pos: {collision.Point.X:000.00} {collision.Point.Y:000.00} {collision.Point.Z:000.00}", 10, ypos + 15, 10, Black);
                    DrawText($"Hit Norm: {collision.Point.X:000.00} {collision.Point.Y:000.00} {collision.Point.Z:000.00}", 10, ypos + 30, 10, Black);

                    if (triHitInfo.Hit && hitObjectName == "Triangle")
                    {
                        DrawText($"Barycenter: {bary.X:000.00} {bary.Y:000.00} {bary.Z:000.00}", 10, ypos + 45, 10, Black);
                    }
                }

                DrawText("Right click mouse to toggle camera controls", 10, 430, 10, Gray);

                DrawText("(c) Turret 3D model by Alberto Cano", screenWidth - 200, screenHeight - 20, 10, Gray);

                DrawFPS(10, 10);

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadModel(tower);         // Unload model
        UnloadTexture(texture);     // Unload texture

        CloseWindow();              // Close window and OpenGL context

        return 0;
    }
}
