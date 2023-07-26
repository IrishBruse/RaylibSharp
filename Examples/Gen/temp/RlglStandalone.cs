using System.Numerics;
using System.Drawing;
using System;

using RaylibSharp;

using static RaylibSharp.Raylib;

public partial class RlglStandalone : ExampleHelper 
{

    // NOTE: rlgl can be configured just re-defining the following values:
    //#define RL_DEFAULT_BATCH_BUFFER_ELEMENTS   8192    // Default internal render batch elements limits
    //#define RL_DEFAULT_BATCH_BUFFERS              1    // Default number of batch buffers (multi-buffering)
    //#define RL_DEFAULT_BATCH_DRAWCALLS          256    // Default number of batch draw calls (by state changes: mode, texture)
    //#define RL_DEFAULT_BATCH_MAX_TEXTURE_UNITS    4    // Maximum number of textures units that can be activated on batch drawing (SetShaderValueTexture())
    //#define RL_MAX_MATRIX_STACK_SIZE             32    // Maximum size of internal Matrix stack
    //#define RL_MAX_SHADER_LOCATIONS              32    // Maximum number of shader locations supported
    //#define RL_CULL_DISTANCE_NEAR              0.01    // Default projection matrix near cull distance
    //#define RL_CULL_DISTANCE_FAR             1000.0    // Default projection matrix far cull distance





    #if defined(__EMSCRIPTEN__)

    #endif

private const int Red = (Color){;
private const int RayWhite = (Color){;
private const int DarkGray = (Color){;

    // Structures Definition
    // Color, 4 components, R8G8B8A8 (32bit)
    typedef struct Color {
        unsigned char r;        // Color red value
        unsigned char g;        // Color green value
        unsigned char b;        // Color blue value
        unsigned char a;        // Color alpha value
    } Color;

    // Camera type, defines a camera position/orientation in 3d space
    typedef struct Camera {
        Vector3 position;       // Camera position
        Vector3 target;         // Camera target it looks-at
        Vector3 up;             // Camera up vector (rotation over its axis)
        float fovy;             // Camera field-of-view apperture in Y (degrees) in perspective, used as near plane width in orthographic
        int projection;         // Camera projection: CameraProjection.Perspective or CameraProjection.Orthographic
    } Camera;

    // Module specific Functions Declaration
    static static void ErrorCallback(int error, string description);
    static static void KeyCallback(GLFWwindow *window, int key, int scancode, int action, int mods);

    // Drawing functions (uses rlgl functionality)
    static static void DrawGrid(int slices, float spacing);
    static static void DrawCube(Vector3 position, float width, float height, float length, Color color);
    static static void DrawCubeWires(Vector3 position, float width, float height, float length, Color color);
    static static void DrawRectangle(Vector2 position, Vector2 size, Color color);

    // NOTE: We use raymath to get this functionality but it could be implemented in this module
    //static Matrix MatrixIdentity(void);
    //static Matrix MatrixOrtho(double left, double right, double bottom, double top, double near, double far);
    //static Matrix MatrixPerspective(double fovy, double aspect, double near, double far);
    //static Matrix MatrixLookAt(Vector3 eye, Vector3 target, Vector3 up);

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        // GLFW3 Initialization + OpenGL 3.3 Context + Extensions
        glfwSetErrorCallback(ErrorCallback);

        if (!glfwInit())
        {
            printf("GLFW3: Can not initialize GLFW\n");
            return 1;
        }
        else printf("GLFW3: GLFW initialized successfully\n");

        glfwWindowHint(GLFW_SAMPLES, 4);
        glfwWindowHint(GLFW_DEPTH_BITS, 16);
        glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
        glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
        glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
        //glfwWindowHint(GLFW_OPENGL_DEBUG_CONTEXT, GL_TRUE);
    #if defined(__APPLE__)
        glfwWindowHint( GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE );
    #endif

        GLFWwindow *window = glfwCreateWindow(screenWidth, screenHeight, "rlgl standalone", null, null);

        if (!window)
        {
            glfwTerminate();
            return 2;
        }
        else printf("GLFW3: Window created successfully\n");

        glfwSetWindowPos(window, 200, 200);

        glfwSetKeyCallback(window, KeyCallback);

        glfwMakeContextCurrent(window);
        glfwSwapInterval(0);

        // Load OpenGL 3.3 supported extensions
        rlLoadExtensions(glfwGetProcAddress);

        // Initialize OpenGL context (states and resources)
        rlglInit(screenWidth, screenHeight);

        // Initialize viewport and internal projection/modelview matrices
        rlViewport(0, 0, screenWidth, screenHeight);
        rlMatrixMode(RL_PROJECTION);                        // Switch to PROJECTION matrix
        rlLoadIdentity();                                   // Reset current matrix (PROJECTION)
        rlOrtho(0, screenWidth, screenHeight, 0, 0.0f, 1.0f); // Orthographic projection with top-left corner at (0,0)
        rlMatrixMode(RL_MODELVIEW);                         // Switch back to MODELVIEW matrix
        rlLoadIdentity();                                   // Reset current matrix (MODELVIEW)

        rlClearColor(245, 245, 245, 255);                   // Define clear color
        rlEnableDepthTest();                                // Enable DEPTH_TEST for 3D

        Camera camera = new();
        camera.Position = (Vector3)new(5.0f,5.0f, 5.0f);    // Camera position
        camera.Target = (Vector3)new(0.0f,0.0f, 0.0f);      // Camera looking at point
        camera.Up = (Vector3)new(0.0f,1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.Fovy = 45.0f;                                // Camera field-of-view Y

        Vector3 cubePosition = new( 0.0f, 0.0f, 0.0f );        // Cube default position (center)

        // Main game loop
        while (!glfwWindowShouldClose(window))
        {
            // Update
            //camera.Position.X += 0.01f;

            // Draw
            rlClearScreenBuffers();             // Clear current framebuffer

                // Draw '3D' elements in the scene
                // Calculate projection matrix (from perspective) and view matrix from camera look at
                Matrix matProj = MatrixPerspective((double)(camera.Fovy*DEG2RAD), (double)screenWidth/(double)screenHeight, 0.01, 1000.0);
                Matrix matView = MatrixLookAt(camera.Position, camera.Target, camera.Up);

                rlSetMatrixModelview(matView);    // Set internal modelview matrix (default shader)
                rlSetMatrixProjection(matProj);   // Set internal projection matrix (default shader)

                DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, Red);
                DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, RayWhite);
                DrawGrid(10, 1.0f);

                // Draw internal render batch buffers (3D data)
                rlDrawRenderBatchActive();

                // Draw '2D' elements in the scene (GUI)

    #if defined(RLGL_SET_MATRIX_MANUALLY)
                matProj = MatrixOrtho(0.0, screenWidth, screenHeight, 0.0, 0.0, 1.0);
                matView = MatrixIdentity();

                rlSetMatrixModelview(matView);    // Set internal modelview matrix (default shader)
                rlSetMatrixProjection(matProj);   // Set internal projection matrix (default shader)

    #else   // Let rlgl generate and multiply matrix internally

                rlMatrixMode(RL_PROJECTION);                            // Enable internal projection matrix
                rlLoadIdentity();                                       // Reset internal projection matrix
                rlOrtho(0.0, screenWidth, screenHeight, 0.0, 0.0, 1.0); // Recalculate internal projection matrix
                rlMatrixMode(RL_MODELVIEW);                             // Enable internal modelview matrix
                rlLoadIdentity();                                       // Reset internal modelview matrix
    #endif
                DrawRectangle(new( 10.0f, 10.0f ), new( 780.0f, 20.0f ), DarkGray);

                // Draw internal render batch buffers (2D data)
                rlDrawRenderBatchActive();

            glfwSwapBuffers(window);
            glfwPollEvents();
        }

        // De-Initialization
        rlglClose();                    // Unload rlgl internal buffers and default shader/texture

        glfwDestroyWindow(window);      // Close window
        glfwTerminate();                // Free GLFW3 resources

        return 0;
    }

    // Module specific Functions Definitions

    // GLFW3: Error callback
    static static void ErrorCallback(int error, string description)
    {
        fprintf(stderr, "%s", description);
    }

    // GLFW3: Keyboard callback
    static static void KeyCallback(GLFWwindow *window, int key, int scancode, int action, int mods)
    {
        if (key == GLFW_Key.Escape && action == GLFW_PRESS)
        {
            glfwSetWindowShouldClose(window, GL_TRUE);
        }
    }

    // Draw rectangle using rlgl OpenGL 1.1 style coding (translated to OpenGL 3.3 internally)
    static static void DrawRectangle(Vector2 position, Vector2 size, Color color)
    {
        rlBegin(RL_TRIANGLES);
            rlColor4ub(color.r, color.g, color.b, color.a);

            rlVertex2f(position.X, position.Y);
            rlVertex2f(position.X, position.Y + size.Y);
            rlVertex2f(position.X + size.X, position.Y + size.Y);

            rlVertex2f(position.X, position.Y);
            rlVertex2f(position.X + size.X, position.Y + size.Y);
            rlVertex2f(position.X + size.X, position.Y);
        rlEnd();
    }

    // Draw a grid centered at (0, 0, 0)
    static static void DrawGrid(int slices, float spacing)
    {
        int halfSlices = slices / 2;

        rlBegin(RL_LINES);
            for (int i = -halfSlices; i <= halfSlices; i++)
            {
                if (i == 0)
                {
                    rlColor3f(0.5f, 0.5f, 0.5f);
                    rlColor3f(0.5f, 0.5f, 0.5f);
                    rlColor3f(0.5f, 0.5f, 0.5f);
                    rlColor3f(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    rlColor3f(0.75f, 0.75f, 0.75f);
                    rlColor3f(0.75f, 0.75f, 0.75f);
                    rlColor3f(0.75f, 0.75f, 0.75f);
                    rlColor3f(0.75f, 0.75f, 0.75f);
                }

                rlVertex3f((float)i*spacing, 0.0f, (float)-halfSlices*spacing);
                rlVertex3f((float)i*spacing, 0.0f, (float)halfSlices*spacing);

                rlVertex3f((float)-halfSlices*spacing, 0.0f, (float)i*spacing);
                rlVertex3f((float)halfSlices*spacing, 0.0f, (float)i*spacing);
            }
        rlEnd();
    }

    // Draw cube
    // NOTE: Cube position is the center position
    static static void DrawCube(Vector3 position, float width, float height, float length, Color color)
    {
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        rlPushMatrix();

            // NOTE: Be careful! Function order matters (rotate . scale . translate)
            rlTranslatef(position.X, position.Y, position.Z);
            //rlScalef(2.0f, 2.0f, 2.0f);
            //rlRotatef(45, 0, 1, 0);

            rlBegin(RL_TRIANGLES);
                rlColor4ub(color.r, color.g, color.b, color.a);

                // Front Face -----------------------------------------------------
                rlVertex3f(x-width/2, y-height/2, z+length/2);  // Bottom Left
                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Bottom Right
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Top Left

                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Top Right
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Top Left
                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Bottom Right

                // Back Face ------------------------------------------------------
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Bottom Left
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Left
                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Bottom Right

                rlVertex3f(x+width/2, y+height/2, z-length/2);  // Top Right
                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Bottom Right
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Left

                // Top Face -------------------------------------------------------
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Left
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Bottom Left
                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Bottom Right

                rlVertex3f(x+width/2, y+height/2, z-length/2);  // Top Right
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Left
                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Bottom Right

                // Bottom Face ----------------------------------------------------
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Top Left
                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Bottom Right
                rlVertex3f(x-width/2, y-height/2, z+length/2);  // Bottom Left

                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Top Right
                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Bottom Right
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Top Left

                // Right face -----------------------------------------------------
                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Bottom Right
                rlVertex3f(x+width/2, y+height/2, z-length/2);  // Top Right
                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Top Left

                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Bottom Left
                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Bottom Right
                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Top Left

                // Left Face ------------------------------------------------------
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Bottom Right
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Top Left
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Right

                rlVertex3f(x-width/2, y-height/2, z+length/2);  // Bottom Left
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Top Left
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Bottom Right
            rlEnd();
        rlPopMatrix();
    }

    // Draw cube wires
    static static void DrawCubeWires(Vector3 position, float width, float height, float length, Color color)
    {
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        rlPushMatrix();

            rlTranslatef(position.X, position.Y, position.Z);
            //rlRotatef(45, 0, 1, 0);

            rlBegin(RL_LINES);
                rlColor4ub(color.r, color.g, color.b, color.a);

                // Front Face -----------------------------------------------------
                // Bottom Line
                rlVertex3f(x-width/2, y-height/2, z+length/2);  // Bottom Left
                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Bottom Right

                // Left Line
                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Bottom Right
                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Top Right

                // Top Line
                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Top Right
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Top Left

                // Right Line
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Top Left
                rlVertex3f(x-width/2, y-height/2, z+length/2);  // Bottom Left

                // Back Face ------------------------------------------------------
                // Bottom Line
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Bottom Left
                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Bottom Right

                // Left Line
                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Bottom Right
                rlVertex3f(x+width/2, y+height/2, z-length/2);  // Top Right

                // Top Line
                rlVertex3f(x+width/2, y+height/2, z-length/2);  // Top Right
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Left

                // Right Line
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Left
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Bottom Left

                // Top Face -------------------------------------------------------
                // Left Line
                rlVertex3f(x-width/2, y+height/2, z+length/2);  // Top Left Front
                rlVertex3f(x-width/2, y+height/2, z-length/2);  // Top Left Back

                // Right Line
                rlVertex3f(x+width/2, y+height/2, z+length/2);  // Top Right Front
                rlVertex3f(x+width/2, y+height/2, z-length/2);  // Top Right Back

                // Bottom Face  ---------------------------------------------------
                // Left Line
                rlVertex3f(x-width/2, y-height/2, z+length/2);  // Top Left Front
                rlVertex3f(x-width/2, y-height/2, z-length/2);  // Top Left Back

                // Right Line
                rlVertex3f(x+width/2, y-height/2, z+length/2);  // Top Right Front
                rlVertex3f(x+width/2, y-height/2, z-length/2);  // Top Right Back
            rlEnd();
        rlPopMatrix();
    }
}
