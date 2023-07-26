using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class ModelsMeshGeneration : ExampleHelper 
{

private const int NUM_MODELS = 9;

    static Mesh GenMeshCustom(void);    // Generate a simple triangle mesh from code

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "RaylibSharp - models - mesh generation");

        // We generate a checked image for texturing
        Image checked = GenImageChecked(2, 2, 1, 1, Red, Green);
        Texture texture = LoadTextureFromImage(checked);
        UnloadImage(checked);

        Model models[NUM_MODELS] = new();

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
        for (int i = 0; i < NUM_MODELS; i++) models[i].Materials[0].Maps[MaterialMapIndex.Albedo].texture = texture;

        // Define the camera to look into our 3d world
        Camera camera = new( new(5.0f,5.0f, 5.0f ), { 0.0f, 0.0f, 0.0f), new(0.0f,1.0f, 0.0f), 45.0f, 0 };

        // Model drawing position
        Vector3 position = new( 0.0f, 0.0f, 0.0f );

        int currentModel = 0;

        SetTargetFPS(60);               // Set our game to run at 60 frames-per-second

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.Orbital);

            if (IsMouseButtonPressed(MouseButton.Left))
            {
                currentModel = (currentModel + 1)%NUM_MODELS; // Cycle between the textures
            }

            if (IsKeyPressed(Key.Right))
            {
                currentModel++;
                if (currentModel >= NUM_MODELS) currentModel = 0;
            }
            else if (IsKeyPressed(Key.Left))
            {
                currentModel--;
                if (currentModel < 0) currentModel = NUM_MODELS - 1;
            }

            // Draw
            BeginDrawing();{

                ClearBackground(RayWhite);

                BeginMode3D(camera);{

                   DrawModel(models[currentModel], position, 1.0f, White);
                   DrawGrid(10, 1.0);

                }EndMode3D();

                DrawRectangle(30, 400, 310, 30, Fade(SkyBlue, 0.5f));
                DrawRectangleLines(30, 400, 310, 30, Fade(DarkBlue, 0.5f));
                DrawText("MOUSE LEFT BUTTON to CYCLE PROCEDURAL MODELS", 40, 410, 10, Blue);

                switch(currentModel)
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

            }EndDrawing();
        }

        // De-Initialization
        UnloadTexture(texture); // Unload texture

        // Unload models data (GPU VRAM)
        for (int i = 0; i < NUM_MODELS; i++) UnloadModel(models[i]);

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }

    // Generate a simple triangle mesh from code
    static Mesh GenMeshCustom(void)
    {
        Mesh mesh = new();
        mesh.triangleCount = 1;
        mesh.vertexCount = mesh.triangleCount*3;
        mesh.vertices = (float *)MemAlloc(mesh.vertexCount*3*sizeof(float));    // 3 vertices, 3 coordinates each (x, y, z)
        mesh.texcoords = (float *)MemAlloc(mesh.vertexCount*2*sizeof(float));   // 3 vertices, 2 coordinates each (x, y)
        mesh.normals = (float *)MemAlloc(mesh.vertexCount*3*sizeof(float));     // 3 vertices, 3 coordinates each (x, y, z)

        // Vertex at (0, 0, 0)
        mesh.vertices[0] = 0;
        mesh.vertices[1] = 0;
        mesh.vertices[2] = 0;
        mesh.normals[0] = 0;
        mesh.normals[1] = 1;
        mesh.normals[2] = 0;
        mesh.texcoords[0] = 0;
        mesh.texcoords[1] = 0;

        // Vertex at (1, 0, 2)
        mesh.vertices[3] = 1;
        mesh.vertices[4] = 0;
        mesh.vertices[5] = 2;
        mesh.normals[3] = 0;
        mesh.normals[4] = 1;
        mesh.normals[5] = 0;
        mesh.texcoords[2] = 0.5f;
        mesh.texcoords[3] = 1.0f;

        // Vertex at (2, 0, 0)
        mesh.vertices[6] = 2;
        mesh.vertices[7] = 0;
        mesh.vertices[8] = 0;
        mesh.normals[6] = 0;
        mesh.normals[7] = 1;
        mesh.normals[8] = 0;
        mesh.texcoords[4] = 1;
        mesh.texcoords[5] =0;

        // Upload mesh data from CPU (RAM) to GPU (VRAM) memory
        UploadMesh(&mesh, false);

        return mesh;
    }
}
