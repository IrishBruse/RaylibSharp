using System.Numerics;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsMeshGeneration : ExampleHelper
{
    const int NUM_MODELS = 9;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - mesh generation");

        // We generate a checked image for texturing
        Image check = GenImageChecked(2, 2, 1, 1, Red, Green);
        Texture texture = LoadTextureFromImage(check);
        UnloadImage(check);

        Model[] models = new Model[NUM_MODELS];

        models[0] = LoadModelFromMesh(GenMeshPlane(2, 2, 5, 5));
        models[1] = LoadModelFromMesh(GenMeshCube(2.0f, 1.0f, 2.0f));
        models[2] = LoadModelFromMesh(GenMeshSphere(2, 32, 32));
        models[3] = LoadModelFromMesh(GenMeshHemiSphere(2, 16, 16));
        models[4] = LoadModelFromMesh(GenMeshCylinder(1, 2, 16));
        models[5] = LoadModelFromMesh(GenMeshTorus(0.25f, 4.0f, 16, 32));
        models[6] = LoadModelFromMesh(GenMeshKnot(1.0f, 2.0f, 16, 128));
        models[7] = LoadModelFromMesh(GenMeshPoly(5, 2.0f));
        models[8] = LoadModelFromMesh(GenMeshCustom());

        // Generated meshes could be exported as .obj files
        //ExportMesh(models[0].Meshes[0], "plane.obj");
        //ExportMesh(models[1].Meshes[0], "cube.obj");
        //ExportMesh(models[2].Meshes[0], "sphere.obj");
        //ExportMesh(models[3].Meshes[0], "hemisphere.obj");
        //ExportMesh(models[4].Meshes[0], "cylinder.obj");
        //ExportMesh(models[5].Meshes[0], "torus.obj");
        //ExportMesh(models[6].Meshes[0], "knot.obj");
        //ExportMesh(models[7].Meshes[0], "poly.obj");
        //ExportMesh(models[8].Meshes[0], "custom.obj");

        // Set checked texture as default diffuse component for all models material
        for (int i = 0; i < NUM_MODELS; i++)
        {
            models[i].Materials[0].Maps[(int)MaterialMapIndex.Albedo].Texture = texture;
        }

        // Define the camera to look into our 3d woRLGL.d
        Camera3D camera = new();
        camera.Position = new(10.0f, 10.0f, 10.0f); // Camera3D position
        camera.Target = new(0.0f, 0.0f, 0.0f); // Camera3D looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f); // Camera3D up vector (rotation towards target)
        camera.Fovy = 45.0f; // Camera3D field-of-view Y
        camera.Projection = CameraProjection.Perspective; // Camera3D projection type

        // Model drawing position
        Vector3 position = new(0.0f, 0.0f, 0.0f);

        int currentModel = 0;

        SetTargetFPS(60); // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            if (IsMouseButtonPressed(MouseButton.Left))
            {
                currentModel = (currentModel + 1) % NUM_MODELS; // Cycle between the textures
            }

            if (IsKeyPressed(Key.Right))
            {
                currentModel++;
                if (currentModel >= NUM_MODELS)
                {
                    currentModel = 0;
                }
            }
            else if (IsKeyPressed(Key.Left))
            {
                currentModel--;
                if (currentModel < 0)
                {
                    currentModel = NUM_MODELS - 1;
                }
            }

            // Draw
            BeginDrawing();
            {

                ClearBackground(RayWhite);

                BeginMode3D(camera);
                {

                    DrawModel(models[currentModel], position, 1.0f, White);
                    DrawGrid(10, 1.0f);

                }
                EndMode3D();

                DrawRectangle(30, 400, 310, 30, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(30, 400, 310, 30, Fade(DarkBlue, 0.5f));
                DrawText("MOUSE LEFT BUTTON to CYCLE PROCEDURAL MODELS", 40, 410, 10, Blue);

                switch (currentModel)
                {
                    case 0: DrawText("PLANE", 680, 10, 20, DarkBlue); break;
                    case 1: DrawText("CUBE", 680, 10, 20, DarkBlue); break;
                    case 2: DrawText("SPHERE", 680, 10, 20, DarkBlue); break;
                    case 3: DrawText("HEMISPHERE", 640, 10, 20, DarkBlue); break;
                    case 4: DrawText("CYLINDER", 680, 10, 20, DarkBlue); break;
                    case 5: DrawText("TORUS", 680, 10, 20, DarkBlue); break;
                    case 6: DrawText("KNOT", 680, 10, 20, DarkBlue); break;
                    case 7: DrawText("POLY", 680, 10, 20, DarkBlue); break;
                    case 8: DrawText("Custom (triangle)", 580, 10, 20, DarkBlue); break;
                    default: break;
                }

            }
            EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture); // Unload texture

        // Unload models data (GPU VRAM)
        for (int i = 0; i < NUM_MODELS; i++)
        {
            UnloadModel(models[i]);
        }

        CloseWindow(); // Close window and OpenGL context

        return 0;
    }

    // Generate a simple triangle mesh from code
    static Mesh GenMeshCustom()
    {
        int triangleCount = 1;
        Vector3[] Vertices = new Vector3[triangleCount * 3]; // 3 vertices, 3 coordinates each (x, y, z)
        Vector2[] TexCoords = new Vector2[Vertices.Length]; // 3 vertices, 2 coordinates each (x, y)
        Vector3[] Normals = new Vector3[Vertices.Length]; // 3 vertices, 3 coordinates each (x, y, z)

        // Vertex at (0, 0, 0)
        Vertices[0] = new(0);
        Normals[0] = Vector3.UnitY;
        TexCoords[0] = Vector2.Zero;

        // Vertex at (1, 0, 2)
        Vertices[1] = new(1, 0, 2);
        Normals[1] = Vector3.UnitY;
        TexCoords[1] = new(0.5f, 1f);

        // Vertex at (2, 0, 0)
        Vertices[2] = new(2, 0, 0);
        Normals[2] = Vector3.UnitY;
        TexCoords[2] = new(1f, 0f);


        Mesh mesh = new()
        {
            Vertices = Vertices,
            Normals = Normals,
            TexCoords = TexCoords,
        };
        mesh.TriangleCount = 1;

        // Upload mesh data from CPU (RAM) to GPU (VRAM) memory
        UploadMesh(ref mesh, false);

        return mesh;
    }
}
