using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class RaylibOpenglInterop : ExampleHelper 
{

    #if defined(PLATFORM_DESKTOP)
        #if defined(GRAPHICS_API_OPENGL_ES2)
private const int glGenVertexArrays = glGenVertexArraysOES;
private const int glBindVertexArray = glBindVertexArrayOES;
private const int glDeleteVertexArrays = glDeleteVertexArraysOES;
private const int GLSL_VERSION = 100;
        #else
            #if defined(__APPLE__)
            #else
            #endif
private const int GLSL_VERSION = 330;
        #endif
    #else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB
private const int GLSL_VERSION = 100;
    #endif

private const int MAX_PARTICLES = 1000;

    // Particle type
    typedef struct Particle {
        float x;
        float y;
        float period;
    } Particle;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib - point particles");

        Shader shader = LoadShader(TextFormat("resources/shaders/glsl%i/point_particle.vs", GLSL_VERSION),
                                   TextFormat("resources/shaders/glsl%i/point_particle.fs", GLSL_VERSION));

        int currentTimeLoc = GetShaderLocation(shader, "currentTime");
        int colorLoc = GetShaderLocation(shader, "color");

        // Initialize the vertex buffer for the particles and assign each particle random values
        Particle particles[MAX_PARTICLES] = new();

        for (int i = 0; i < MAX_PARTICLES; i++)
        {
            particles[i].X = (float)GetRandomValue(20, screenWidth - 20);
            particles[i].Y = (float)GetRandomValue(50, screenHeight - 20);

            // Give each particle a slightly different period. But don't spread it to much.
            // This way the particles line up every so often and you get a glimps of what is going on.
            particles[i].period = (float)GetRandomValue(10, 30)/10.0f;
        }

        // Create a plain OpenGL vertex buffer with the data and an vertex array object
        // that feeds the data from the buffer into the vertexPosition shader attribute.
        GLuint vao = 0;
        GLuint vbo = 0;
        glGenVertexArrays(1, &vao);
        glBindVertexArray(vao);
            glGenBuffers(1, &vbo);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);
            glBufferData(GL_ARRAY_BUFFER, MAX_PARTICLES*sizeof(Particle), particles, GL_STATIC_DRAW);
            // Note: LoadShader() automatically fetches the attribute index of "vertexPosition" and saves it in shader.locs[SHADER_LOC_VERTEX_POSITION]
            glVertexAttribPointer(shader.locs[SHADER_LOC_VERTEX_POSITION], 3, GL_FLOAT, GL_FALSE, 0, 0);
            glEnableVertexAttribArray(0);
            glBindBuffer(GL_ARRAY_BUFFER, 0);
        glBindVertexArray(0);

        // Allows the vertex shader to set the point size of each particle individually
        #ifndef GRAPHICS_API_OPENGL_ES2
        glEnable(GL_PROGRAM_POINT_SIZE);
        #endif

        SetTargetFPS(60);

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Draw
            BeginDrawing();{
                ClearBackground(White);

                DrawRectangle(10, 10, 210, 30, Maroon);
                DrawText(TextFormat("%zu particles in one vertex buffer", MAX_PARTICLES), 20, 20, 10, RayWhite);

                rlDrawRenderBatchActive();      // Draw iternal buffers data (previous draw calls)

                // Switch to plain OpenGL
                glUseProgram(shader.id);

                    glUniform1f(currentTimeLoc, GetTime());

                    Vector4 color = ColorNormalize((Color){ 255, 0, 0, 128 });
                    glUniform4fv(colorLoc, 1, (float *)&color);

                    // Get the current modelview and projection matrix so the particle system is displayed and transformed
                    Matrix modelViewProjection = MatrixMultiply(rlGetMatrixModelview(), rlGetMatrixProjection());

                    glUniformMatrix4fv(shader.locs[SHADER_LOC_MATRIX_MVP], 1, false, MatrixToFloat(modelViewProjection));

                    glBindVertexArray(vao);
                        glDrawArrays(GL_POINTS, 0, MAX_PARTICLES);
                    glBindVertexArray(0);

                glUseProgram(0);

                DrawFPS(screenWidth - 100, 10);

            }EndDrawing();
        }

        // De-Initialization
        glDeleteBuffers(1, &vbo);
        glDeleteVertexArrays(1, &vao);

        UnloadShader(shader);   // Unload shader

        CloseWindow();          // Close window and OpenGL context

        return 0;
    }
}
