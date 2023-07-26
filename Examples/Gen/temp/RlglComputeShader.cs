using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class RlglComputeShader : ExampleHelper 
{

    // IMPORTANT: This must match gol*.glsl GOL_WIDTH constant.
    // This must be a multiple of 16 (check golLogic compute dispatch).
private const int GOL_WIDTH = 768;

    // Maximum amount of queued draw commands (squares draw from mouse down events).
private const int MAX_BUFFERed_TRANSFERTS = 48;

    // Game Of Life Update Command
    typedef struct GolUpdateCmd {
        uint x;         // x coordinate of the gol command
        uint y;         // y coordinate of the gol command
        uint w;         // width of the filled zone
        uint enabled;   // whether to enable or disable zone
    } GolUpdateCmd;

    // Game Of Life Update Commands SSBO
    typedef struct GolUpdateSSBO {
        uint count;
        GolUpdateCmd commands[MAX_BUFFERed_TRANSFERTS];
    } GolUpdateSSBO;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        InitWindow(GOL_WIDTH, GOL_WIDTH, "RaylibSharp - rlgl - compute shader - game of life");

        const Vector2 resolution = new( GOL_WIDTH, GOL_WIDTH );
        uint brushSize = 8;

        // Game of Life logic compute shader
        char *golLogicCode = LoadFileText("resources/shaders/glsl430/gol.glsl");
        uint golLogicShader = rlCompileShader(golLogicCode, RL_COMPUTE_SHADER);
        uint golLogicProgram = rlLoadComputeShaderProgram(golLogicShader);
        UnloadFileText(golLogicCode);

        // Game of Life logic render shader
        Shader golRenderShader = LoadShader(null, "resources/shaders/glsl430/gol_render.glsl");
        int resUniformLoc = GetShaderLocation(golRenderShader, "resolution");

        // Game of Life transfert shader (CPU<.GPU download and upload)
        char *golTransfertCode = LoadFileText("resources/shaders/glsl430/gol_transfert.glsl");
        uint golTransfertShader = rlCompileShader(golTransfertCode, RL_COMPUTE_SHADER);
        uint golTransfertProgram = rlLoadComputeShaderProgram(golTransfertShader);
        UnloadFileText(golTransfertCode);

        // Load shader storage buffer object (SSBO), id returned
        uint ssboA = rlLoadShaderBuffer(GOL_WIDTH*GOL_WIDTH*sizeof(unsigned int), null, RL_DYNAMIC_COPY);
        uint ssboB = rlLoadShaderBuffer(GOL_WIDTH*GOL_WIDTH*sizeof(unsigned int), null, RL_DYNAMIC_COPY);
        uint ssboTransfert = rlLoadShaderBuffer(sizeof(GolUpdateSSBO), null, RL_DYNAMIC_COPY);

        GolUpdateSSBO transfertBuffer = new();

        // Create a white texture of the size of the window to update
        // each pixel of the window using the fragment shader: golRenderShader
        Image whiteImage = GenImageColor(GOL_WIDTH, GOL_WIDTH, White);
        Texture whiteTex = LoadTextureFromImage(whiteImage);
        UnloadImage(whiteImage);

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            brushSize += (int)GetMouseWheelMove();

            if ((IsMouseButtonDown(MouseButton.Left) || IsMouseButtonDown(MouseButton.Right))
                && (transfertBuffer.Count < MAX_BUFFERed_TRANSFERTS))
            {
                // Buffer a new command
                transfertBuffer.commands[transfertBuffer.Count].X = GetMouseX() - brushSize/2;
                transfertBuffer.commands[transfertBuffer.Count].Y = GetMouseY() - brushSize/2;
                transfertBuffer.commands[transfertBuffer.Count].w = brushSize;
                transfertBuffer.commands[transfertBuffer.Count].enabled = IsMouseButtonDown(MouseButton.Left);
                transfertBuffer.Count++;
            }
            else if (transfertBuffer.Count > 0)  // Process transfert buffer
            {
                // Send SSBO buffer to GPU
                rlUpdateShaderBuffer(ssboTransfert, &transfertBuffer, sizeof(GolUpdateSSBO), 0);

                // Process SSBO commands on GPU
                rlEnableShader(golTransfertProgram);
                rlBindShaderBuffer(ssboA, 1);
                rlBindShaderBuffer(ssboTransfert, 3);
                rlComputeShaderDispatch(transfertBuffer.Count, 1, 1); // Each GPU unit will process a command!
                rlDisableShader();

                transfertBuffer.Count = 0;
            }
            else
            {
                // Process game of life logic
                rlEnableShader(golLogicProgram);
                rlBindShaderBuffer(ssboA, 1);
                rlBindShaderBuffer(ssboB, 2);
                rlComputeShaderDispatch(GOL_WIDTH/16, GOL_WIDTH/16, 1);
                rlDisableShader();

                // ssboA <. ssboB
                int temp = ssboA;
                ssboA = ssboB;
                ssboB = temp;
            }

            rlBindShaderBuffer(ssboA, 1);
            SetShaderValue(golRenderShader, resUniformLoc, &resolution, SHADER_UNIFORM_VEC2);

            // Draw
            BeginDrawing();{

                ClearBackground(Blank);

                BeginShaderMode(golRenderShader);
                    DrawTexture(whiteTex, 0, 0, White);
                EndShaderMode();

                DrawRectangleLines(GetMouseX() - brushSize/2, GetMouseY() - brushSize/2, brushSize, brushSize, Red);

                DrawText("Use Mouse wheel to increase/decrease brush size", 10, 10, 20, White);
                DrawFPS(GetScreenWidth() - 100, 10);

            }EndDrawing();
        }

        // De-Initialization
        // Unload shader buffers objects.
        rlUnloadShaderBuffer(ssboA);
        rlUnloadShaderBuffer(ssboB);
        rlUnloadShaderBuffer(ssboTransfert);

        // Unload compute shader programs
        rlUnloadShaderProgram(golTransfertProgram);
        rlUnloadShaderProgram(golLogicProgram);

        UnloadTexture(whiteTex);            // Unload white texture
        UnloadShader(golRenderShader);      // Unload rendering fragment shader

        CloseWindow();                      // Close window and OpenGL context

        return 0;
    }
}
