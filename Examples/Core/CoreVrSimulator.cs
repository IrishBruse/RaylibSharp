/*******************************************************************************************
*
*   raylib [core] example - VR Simulator (Oculus Rift CV1 parameters)
*
*   Example originally created with raylib 2.5, last time updated with raylib 4.0
*
*   Example licensed under an unmodified zlib/libpng license, which is an OSI-certified,
*   BSD-like license that allows static linking with closed source software
*
*   Copyright (c) 2017-2024 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static RaylibSharp.Raylib;
using RaylibSharp;

public partial class CoreVrSimulator : ExampleHelper
{
    #if PLATFORM_DESKTOP
    const int GLSL_VERSION = 330;
    #else   // PLATFORM_ANDROID, PLATFORM_WEB
    const int GLSL_VERSION = 100;
    #endif

    //------------------------------------------------------------------------------------
    // Program main entry point
    //------------------------------------------------------------------------------------
    public static int Example()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        // NOTE: screenWidth/screenHeight should match VR device aspect ratio
        InitWindow(screenWidth, screenHeight, "RaylibSharp [core] example - vr simulator");

        // VR device parameters definition
        VrDeviceInfo device = new () {
            // Oculus Rift CV1 parameters for simulator
            HResolution = 2160,                 // Horizontal resolution in pixels
            VResolution = 1200,                 // Vertical resolution in pixels
            HScreenSize = 0.133793f,            // Horizontal size in meters
            VScreenSize = 0.0669f,              // Vertical size in meters
            EyeToScreenDistance = 0.041f,       // Distance between eye and display in meters
            LensSeparationDistance = 0.07f,     // Lens separation distance in meters
            InterpupillaryDistance = 0.07f,     // IPD (distance between pupils) in meters
        };

            // NOTE: CV1 uses fresnel-hybrid-asymmetric lenses with specific compute shaders
            // Following parameters are just an approximation to CV1 distortion stereo rendering
        device.LensDistortionValues[0] = 1.0f;     // Lens distortion constant parameter 0
        device.LensDistortionValues[1] = 0.22f;    // Lens distortion constant parameter 1
        device.LensDistortionValues[2] = 0.24f;    // Lens distortion constant parameter 2
        device.LensDistortionValues[3] = 0.0f;     // Lens distortion constant parameter 3
        device.ChromaAbCorrection[0] = 0.996f;     // Chromatic aberration correction parameter 0
        device.ChromaAbCorrection[1] = -0.004f;    // Chromatic aberration correction parameter 1
        device.ChromaAbCorrection[2] = 1.014f;     // Chromatic aberration correction parameter 2
        device.ChromaAbCorrection[3] = 0.0f;       // Chromatic aberration correction parameter 3


        // Load VR stereo config for VR device parameteres (Oculus Rift CV1 parameters)
        VrStereoConfig config = LoadVrStereoConfig(device);

        // Distortion shader (uses device lens distortion and chroma)
        Shader distortion = LoadShader(null, TextFormat("resources/distortion%i.fs", GLSL_VERSION));

        // Update distortion shader with lens and distortion-scale parameters
        SetShaderValue(distortion, GetShaderLocation(distortion, "leftLensCenter"),
                       config.LeftLensCenter, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "rightLensCenter"),
                       config.RightLensCenter, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "leftScreenCenter"),
                       config.LeftScreenCenter, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "rightScreenCenter"),
                       config.RightScreenCenter, ShaderUniformDataType.ShaderUniformVec2);

        SetShaderValue(distortion, GetShaderLocation(distortion, "scale"),
                       config.Scale, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "scaleIn"),
                       config.ScaleIn, ShaderUniformDataType.ShaderUniformVec2);
        SetShaderValue(distortion, GetShaderLocation(distortion, "deviceWarpParam"),
                       device.LensDistortionValues, ShaderUniformDataType.ShaderUniformVec4);
        SetShaderValue(distortion, GetShaderLocation(distortion, "chromaAbParam"),
                       device.ChromaAbCorrection, ShaderUniformDataType.ShaderUniformVec4);

        // Initialize framebuffer for stereo rendering
        // NOTE: Screen size should match HMD aspect ratio
        RenderTexture2D target = LoadRenderTexture(device.HResolution, device.VResolution);

        // The target's height is flipped (in the source Rectangle), due to OpenGL reasons
        Rectangle sourceRec = new(0.0f, 0.0f, (float)target.Texture.Width, -(float)target.Texture.Height);
        Rectangle destRec = new(0.0f, 0.0f, (float)GetScreenWidth(), (float)GetScreenHeight());

        // Define the camera to look into our 3d world
        Camera camera = new();
        camera.Position = (Vector3)new(5.0f, 2.0f, 5.0f);    // Camera position
        camera.Target = (Vector3)new(0.0f, 2.0f, 0.0f);      // Camera looking at point
        camera.Up = (Vector3)new(0.0f, 1.0f, 0.0f);          // Camera up vector
        camera.Fovy = 60.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);

        DisableCursor();                    // Limit cursor to relative movement inside the window

        SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            UpdateCamera(ref camera, CameraMode.FirstPerson);
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginTextureMode(target);
                ClearBackground(RAYWHITE);
                BeginVrStereoMode(config);
                    BeginMode3D(camera);

                        DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, RED);
                        DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, MAROON);
                        DrawGrid(40, 1.0f);

                    EndMode3D();
                EndVrStereoMode();
            EndTextureMode();

            BeginDrawing();
                ClearBackground(RAYWHITE);
                BeginShaderMode(distortion);
                    DrawTexturePro(target.Texture, sourceRec, destRec, new(0.0f, 0.0f), 0.0f, WHITE);
                EndShaderMode();
                DrawFPS(10, 10);
            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        UnloadVrStereoConfig(config);   // Unload stereo config

        UnloadRenderTexture(target);    // Unload stereo render fbo
        UnloadShader(distortion);       // Unload distortion shader

        CloseWindow();                  // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}

