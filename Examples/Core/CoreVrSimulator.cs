using System.Numerics;
using System.Drawing;

using RaylibSharp;

using static RaylibSharp.Raylib;
using Camera = RaylibSharp.Camera3D;

public class CoreVrSimulator : ExampleHelper
{
    private static readonly int GLSL_VERSION = 330;

    // Program main entry point
    public static int Example()
    {
        // Initialization
        const int screenWidth = 800;
        const int screenHeight = 450;

        // NOTE: screenWidth/screenHeight should match VR device aspect ratio
        InitWindow(screenWidth, screenHeight, "RaylibSharp - Core - vr simulator");

        // VR device parameters definition
        VrDeviceInfo device = new()
        {
            // Oculus Rift CV1 parameters for simulator
            Hresolution = 2160,                 // Horizontal resolution in pixels
            Vresolution = 1200,                 // Vertical resolution in pixels
            Hscreensize = 0.133793f,            // Horizontal size in meters
            Vscreensize = 0.0669f,              // Vertical size in meters
            Vscreencenter = 0.04678f,           // Screen center in meters
            Eyetoscreendistance = 0.041f,       // Distance between eye and display in meters
            Lensseparationdistance = 0.07f,     // Lens separation distance in meters
            Interpupillarydistance = 0.07f,     // IPD (distance between pupils) in meters

            // NOTE: CV1 uses fresnel-hybrid-asymmetric lenses with specific compute shaders
            // Following parameters are just an approximation to CV1 distortion stereo rendering
            Lensdistortionvalues = new(1.0f, 0.22f, 0.24f, 0.0f), // Lens distortion constant parameters
            Chromaabcorrection = new(0.996f, -0.004f, 1.014f, 0.0f), // Lens distortion constant parameters
        };

        // Load VR stereo config for VR device parameteres (Oculus Rift CV1 parameters)
        VrStereoConfig config = LoadVrStereoConfig(device);

        // Distortion shader (uses device lens distortion and chroma)
        Shader distortion = LoadShader(null, $"resources/distortion{GLSL_VERSION}.fs");

        // Update distortion shader with lens and distortion-scale parameters
        SetShaderValue(distortion, GetShaderLocation(distortion, "leftLensCenter"), ref config.Leftlenscenter, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "rightLensCenter"), ref config.Rightlenscenter, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "leftScreenCenter"), ref config.Leftscreencenter, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "rightScreenCenter"), ref config.Rightscreencenter, ShaderUniformDataType.ShaderUniformVec2);

        SetShaderValue(distortion, GetShaderLocation(distortion, "scale"), ref config.Scale, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "scaleIn"), ref config.Scalein, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "deviceWarpParam"), ref device.Lensdistortionvalues, ShaderUniformDataType.ShaderUniformVec4);
        SetShaderValue(distortion, GetShaderLocation(distortion, "chromaAbParam"), ref device.Chromaabcorrection, ShaderUniformDataType.ShaderUniformVec4);

        // Initialize framebuffer for stereo rendering
        // NOTE: Screen size should match HMD aspect ratio
        RenderTexture target = LoadRenderTexture(device.Hresolution, device.Vresolution);

        // The target's height is flipped (in the source Rectangle), due to OpenGL reasons
        RectangleF sourceRec = new(0.0f, 0.0f, target.Texture.Width, -(float)target.Texture.Height);
        RectangleF destRec = new(0.0f, 0.0f, GetScreenWidth(), GetScreenHeight());

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = new(5.0f, 2.0f, 5.0f);    // Camera position
        camera.Target = new(0.0f, 2.0f, 0.0f);      // Camera looking at point
        camera.Up = new(0.0f, 1.0f, 0.0f);          // Camera up vector
        camera.Fovy = 60.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(90);                   // Set our game to run at 90 frames-per-second

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            UpdateCamera(ref camera, CameraMode.FirstPerson);

            // Draw
            BeginTextureMode(target);
            ClearBackground(RayWhite);
            BeginVrStereoMode(config);
            BeginMode3D(camera);
            {
                DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, Red);
                DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, Maroon);
                DrawGrid(40, 1.0f);
            }
            EndMode3D();
            EndVrStereoMode();
            EndTextureMode();

            BeginDrawing();
            {
                ClearBackground(RayWhite);
                BeginShaderMode(distortion);
                {
                    DrawTexture(target.Texture, sourceRec, destRec, new(0.0f, 0.0f), 0.0f, White);
                }
                EndShaderMode();
                DrawFPS(10, 10);
            }
            EndDrawing();
        }

        // De-Initialization
        UnloadVrStereoConfig(config);   // Unload stereo config

        UnloadRenderTexture(target);    // Unload stereo render fbo
        UnloadShader(distortion);       // Unload distortion shader

        CloseWindow();

        return 0;
    }
}
