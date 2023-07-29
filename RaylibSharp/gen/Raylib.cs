namespace RaylibSharp;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Numerics;
using System.Drawing;

public static unsafe partial class Raylib
{
    /// <summary> Check if KEY_ESCAPE pressed or Close icon pressed </summary>
    [LibraryImport(LIB, EntryPoint = "WindowShouldClose")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WindowShouldClose();

    /// <summary> Close window and unload OpenGL context </summary>
    [LibraryImport(LIB, EntryPoint = "CloseWindow")]
    public static partial void CloseWindow();

    /// <summary> Check if window has been initialized successfully </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowReady();

    /// <summary> Check if window is currently fullscreen </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowFullscreen")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowFullscreen();

    /// <summary> Check if window is currently hidden (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowHidden")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowHidden();

    /// <summary> Check if window is currently minimized (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowMinimized")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowMinimized();

    /// <summary> Check if window is currently maximized (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowMaximized")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowMaximized();

    /// <summary> Check if window is currently focused (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowFocused")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowFocused();

    /// <summary> Check if window has been resized last frame </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowResized")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowResized();

    /// <summary> Check if one specific window flag is enabled </summary>
    [LibraryImport(LIB, EntryPoint = "IsWindowState")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowState(WindowFlag flag);

    /// <summary> Set window configuration state using flags (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowState")]
    public static partial void SetWindowState(WindowFlag flags);

    /// <summary> Clear window configuration state flags </summary>
    [LibraryImport(LIB, EntryPoint = "ClearWindowState")]
    public static partial void ClearWindowState(WindowFlag flags);

    /// <summary> Toggle window state: fullscreen/windowed (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "ToggleFullscreen")]
    public static partial void ToggleFullscreen();

    /// <summary> Set window state: maximized, if resizable (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "MaximizeWindow")]
    public static partial void MaximizeWindow();

    /// <summary> Set window state: minimized, if resizable (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "MinimizeWindow")]
    public static partial void MinimizeWindow();

    /// <summary> Set window state: not minimized/maximized (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "RestoreWindow")]
    public static partial void RestoreWindow();

    /// <summary> Set icon for window (single image, RGBA 32bit, only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowIcon")]
    public static partial void SetWindowIcon(Image image);

    /// <summary> Set icon for window (multiple images, RGBA 32bit, only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowIcons")]
    public static partial void SetWindowIcons(IntPtr images, int count);

    /// <summary> Set title for window (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowTitle")]
    public static partial void SetWindowTitle([MarshalAs(UnmanagedType.LPStr)] string title);

    /// <summary> Set window position on screen (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowPosition")]
    public static partial void SetWindowPosition(int x, int y);

    /// <summary> Set monitor for the current window (fullscreen mode) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowMonitor")]
    public static partial void SetWindowMonitor(int monitor);

    /// <summary> Set window minimum dimensions (for FLAG_WINDOW_RESIZABLE) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowMinSize")]
    public static partial void SetWindowMinSize(int width, int height);

    /// <summary> Set window dimensions </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowSize")]
    public static partial void SetWindowSize(int width, int height);

    /// <summary> Set window opacity [0.0f..1.0f] (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowOpacity")]
    public static partial void SetWindowOpacity(float opacity);

    /// <summary> Set window focused (only PLATFORM_DESKTOP) </summary>
    [LibraryImport(LIB, EntryPoint = "SetWindowFocused")]
    public static partial void SetWindowFocused();

    /// <summary> Get native window handle </summary>
    [LibraryImport(LIB, EntryPoint = "GetWindowHandle")]
    public static partial IntPtr GetWindowHandle();

    /// <summary> Get current screen width </summary>
    [LibraryImport(LIB, EntryPoint = "GetScreenWidth")]
    public static partial int GetScreenWidth();

    /// <summary> Get current screen height </summary>
    [LibraryImport(LIB, EntryPoint = "GetScreenHeight")]
    public static partial int GetScreenHeight();

    /// <summary> Get current render width (it considers HiDPI) </summary>
    [LibraryImport(LIB, EntryPoint = "GetRenderWidth")]
    public static partial int GetRenderWidth();

    /// <summary> Get current render height (it considers HiDPI) </summary>
    [LibraryImport(LIB, EntryPoint = "GetRenderHeight")]
    public static partial int GetRenderHeight();

    /// <summary> Get number of connected monitors </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorCount")]
    public static partial int GetMonitorCount();

    /// <summary> Get current connected monitor </summary>
    [LibraryImport(LIB, EntryPoint = "GetCurrentMonitor")]
    public static partial int GetCurrentMonitor();

    /// <summary> Get specified monitor position </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorPosition")]
    public static partial Vector2 GetMonitorPosition(int monitor);

    /// <summary> Get specified monitor width (current video mode used by monitor) </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorWidth")]
    public static partial int GetMonitorWidth(int monitor);

    /// <summary> Get specified monitor height (current video mode used by monitor) </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorHeight")]
    public static partial int GetMonitorHeight(int monitor);

    /// <summary> Get specified monitor physical width in millimetres </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorPhysicalWidth")]
    public static partial int GetMonitorPhysicalWidth(int monitor);

    /// <summary> Get specified monitor physical height in millimetres </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorPhysicalHeight")]
    public static partial int GetMonitorPhysicalHeight(int monitor);

    /// <summary> Get specified monitor refresh rate </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorRefreshRate")]
    public static partial int GetMonitorRefreshRate(int monitor);

    /// <summary> Get window position XY on monitor </summary>
    [LibraryImport(LIB, EntryPoint = "GetWindowPosition")]
    public static partial Vector2 GetWindowPosition();

    /// <summary> Get window scale DPI factor </summary>
    [LibraryImport(LIB, EntryPoint = "GetWindowScaleDPI")]
    public static partial Vector2 GetWindowScaleDPI();

    /// <summary> Get the human-readable, UTF-8 encoded name of the primary monitor </summary>
    [LibraryImport(LIB, EntryPoint = "GetMonitorName")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string GetMonitorName(int monitor);

    /// <summary> Set clipboard text content </summary>
    [LibraryImport(LIB, EntryPoint = "SetClipboardText")]
    public static partial void SetClipboardText([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get clipboard text content </summary>
    [LibraryImport(LIB, EntryPoint = "GetClipboardText")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string GetClipboardText();

    /// <summary> Enable waiting for events on EndDrawing(), no automatic event polling </summary>
    [LibraryImport(LIB, EntryPoint = "EnableEventWaiting")]
    public static partial void EnableEventWaiting();

    /// <summary> Disable waiting for events on EndDrawing(), automatic events polling </summary>
    [LibraryImport(LIB, EntryPoint = "DisableEventWaiting")]
    public static partial void DisableEventWaiting();

    /// <summary> Swap back buffer with front buffer (screen drawing) </summary>
    [LibraryImport(LIB, EntryPoint = "SwapScreenBuffer")]
    public static partial void SwapScreenBuffer();

    /// <summary> Register all input events </summary>
    [LibraryImport(LIB, EntryPoint = "PollInputEvents")]
    public static partial void PollInputEvents();

    /// <summary> Wait for some time (halt program execution) </summary>
    [LibraryImport(LIB, EntryPoint = "WaitTime")]
    public static partial void WaitTime(double seconds);

    /// <summary> Shows cursor </summary>
    [LibraryImport(LIB, EntryPoint = "ShowCursor")]
    public static partial void ShowCursor();

    /// <summary> Hides cursor </summary>
    [LibraryImport(LIB, EntryPoint = "HideCursor")]
    public static partial void HideCursor();

    /// <summary> Check if cursor is not visible </summary>
    [LibraryImport(LIB, EntryPoint = "IsCursorHidden")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsCursorHidden();

    /// <summary> Enables cursor (unlock cursor) </summary>
    [LibraryImport(LIB, EntryPoint = "EnableCursor")]
    public static partial void EnableCursor();

    /// <summary> Disables cursor (lock cursor) </summary>
    [LibraryImport(LIB, EntryPoint = "DisableCursor")]
    public static partial void DisableCursor();

    /// <summary> Check if cursor is on the screen </summary>
    [LibraryImport(LIB, EntryPoint = "IsCursorOnScreen")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsCursorOnScreen();

    /// <summary> Set background color (framebuffer clear color) </summary>
    [LibraryImport(LIB, EntryPoint = "ClearBackground")]
    public static partial void ClearBackground([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Setup canvas (framebuffer) to start drawing </summary>
    [LibraryImport(LIB, EntryPoint = "BeginDrawing")]
    public static partial void BeginDrawing();

    /// <summary> End canvas drawing and swap buffers (double buffering) </summary>
    [LibraryImport(LIB, EntryPoint = "EndDrawing")]
    public static partial void EndDrawing();

    /// <summary> Begin 2D mode with custom camera (2D) </summary>
    [LibraryImport(LIB, EntryPoint = "BeginMode2D")]
    public static partial void BeginMode2D([MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Ends 2D mode with custom camera </summary>
    [LibraryImport(LIB, EntryPoint = "EndMode2D")]
    public static partial void EndMode2D();

    /// <summary> Begin 3D mode with custom camera (3D) </summary>
    [LibraryImport(LIB, EntryPoint = "BeginMode3D")]
    public static partial void BeginMode3D([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Ends 3D mode and returns to default 2D orthographic mode </summary>
    [LibraryImport(LIB, EntryPoint = "EndMode3D")]
    public static partial void EndMode3D();

    /// <summary> Begin drawing to render texture </summary>
    [LibraryImport(LIB, EntryPoint = "BeginTextureMode")]
    public static partial void BeginTextureMode(RenderTexture target);

    /// <summary> Ends drawing to render texture </summary>
    [LibraryImport(LIB, EntryPoint = "EndTextureMode")]
    public static partial void EndTextureMode();

    /// <summary> Begin custom shader drawing </summary>
    [LibraryImport(LIB, EntryPoint = "BeginShaderMode")]
    public static partial void BeginShaderMode(Shader shader);

    /// <summary> End custom shader drawing (use default shader) </summary>
    [LibraryImport(LIB, EntryPoint = "EndShaderMode")]
    public static partial void EndShaderMode();

    /// <summary> Begin blending mode (alpha, additive, multiplied, subtract, custom) </summary>
    [LibraryImport(LIB, EntryPoint = "BeginBlendMode")]
    public static partial void BeginBlendMode(BlendMode mode);

    /// <summary> End blending mode (reset to default: alpha blending) </summary>
    [LibraryImport(LIB, EntryPoint = "EndBlendMode")]
    public static partial void EndBlendMode();

    /// <summary> Begin scissor mode (define screen area for following drawing) </summary>
    [LibraryImport(LIB, EntryPoint = "BeginScissorMode")]
    public static partial void BeginScissorMode(int x, int y, int width, int height);

    /// <summary> End scissor mode </summary>
    [LibraryImport(LIB, EntryPoint = "EndScissorMode")]
    public static partial void EndScissorMode();

    /// <summary> Begin stereo rendering (requires VR simulator) </summary>
    [LibraryImport(LIB, EntryPoint = "BeginVrStereoMode")]
    public static partial void BeginVrStereoMode(VrStereoConfig config);

    /// <summary> End stereo rendering (requires VR simulator) </summary>
    [LibraryImport(LIB, EntryPoint = "EndVrStereoMode")]
    public static partial void EndVrStereoMode();

    /// <summary> Load VR stereo config for VR simulator device parameters </summary>
    [LibraryImport(LIB, EntryPoint = "LoadVrStereoConfig")]
    public static partial VrStereoConfig LoadVrStereoConfig(VrDeviceInfo device);

    /// <summary> Unload VR stereo config </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadVrStereoConfig")]
    public static partial void UnloadVrStereoConfig(VrStereoConfig config);

    /// <summary> Load shader from files and bind default locations </summary>
    [LibraryImport(LIB, EntryPoint = "LoadShader")]
    public static partial Shader LoadShader([MarshalAs(UnmanagedType.LPStr)] string? vsFileName, [MarshalAs(UnmanagedType.LPStr)] string? fsFileName);

    /// <summary> Load shader from code strings and bind default locations </summary>
    [LibraryImport(LIB, EntryPoint = "LoadShaderFromMemory")]
    public static partial Shader LoadShaderFromMemory([MarshalAs(UnmanagedType.LPStr)] string vsCode, [MarshalAs(UnmanagedType.LPStr)] string fsCode);

    /// <summary> Check if a shader is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsShaderReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsShaderReady(Shader shader);

    /// <summary> Get shader uniform location </summary>
    [LibraryImport(LIB, EntryPoint = "GetShaderLocation")]
    public static partial int GetShaderLocation(Shader shader, [MarshalAs(UnmanagedType.LPStr)] string uniformName);

    /// <summary> Get shader attribute location </summary>
    [LibraryImport(LIB, EntryPoint = "GetShaderLocationAttrib")]
    public static partial int GetShaderLocationAttrib(Shader shader, [MarshalAs(UnmanagedType.LPStr)] string attribName);

    /// <summary> Set shader uniform value </summary>
    [LibraryImport(LIB, EntryPoint = "SetShaderValue")]
    public static partial void SetShaderValue(Shader shader, int locIndex, IntPtr value, ShaderUniformDataType uniformType);

    /// <summary> Set shader uniform value vector </summary>
    [LibraryImport(LIB, EntryPoint = "SetShaderValueV")]
    public static partial void SetShaderValue(Shader shader, int locIndex, IntPtr value, int uniformType, int count);

    /// <summary> Set shader uniform value (matrix 4x4) </summary>
    [LibraryImport(LIB, EntryPoint = "SetShaderValueMatrix")]
    public static partial void SetShaderValueMatrix(Shader shader, int locIndex, Matrix4x4 mat);

    /// <summary> Set shader uniform value for texture (sampler2d) </summary>
    [LibraryImport(LIB, EntryPoint = "SetShaderValueTexture")]
    public static partial void SetShaderValueTexture(Shader shader, int locIndex, Texture texture);

    /// <summary> Unload shader from GPU memory (VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadShader")]
    public static partial void UnloadShader(Shader shader);

    /// <summary> Get a ray trace from mouse position </summary>
    [LibraryImport(LIB, EntryPoint = "GetMouseRay")]
    public static partial Ray GetMouseRay(Vector2 mousePosition, [MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Get camera transform matrix (view matrix) </summary>
    [LibraryImport(LIB, EntryPoint = "GetCameraMatrix")]
    public static partial Matrix4x4 GetCameraMatrix([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Get camera 2d transform matrix </summary>
    [LibraryImport(LIB, EntryPoint = "GetCameraMatrix2D")]
    public static partial Matrix4x4 GetCameraMatrix2D([MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Get the screen space position for a 3d world space position </summary>
    [LibraryImport(LIB, EntryPoint = "GetWorldToScreen")]
    public static partial Vector2 GetWorldToScreen(Vector3 position, [MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Get the world space position for a 2d camera screen space position </summary>
    [LibraryImport(LIB, EntryPoint = "GetScreenToWorld2D")]
    public static partial Vector2 GetScreenToWorld2D(Vector2 position, [MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Get size position for a 3d world space position </summary>
    [LibraryImport(LIB, EntryPoint = "GetWorldToScreenEx")]
    public static partial Vector2 GetWorldToScreen(Vector3 position, [MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, int width, int height);

    /// <summary> Get the screen space position for a 2d camera world space position </summary>
    [LibraryImport(LIB, EntryPoint = "GetWorldToScreen2D")]
    public static partial Vector2 GetWorldToScreen2D(Vector2 position, [MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Set target FPS (maximum) </summary>
    [LibraryImport(LIB, EntryPoint = "SetTargetFPS")]
    public static partial void SetTargetFPS(int fps);

    /// <summary> Get current FPS </summary>
    [LibraryImport(LIB, EntryPoint = "GetFPS")]
    public static partial int GetFPS();

    /// <summary> Get time in seconds for last frame drawn (delta time) </summary>
    [LibraryImport(LIB, EntryPoint = "GetFrameTime")]
    public static partial float GetFrameTime();

    /// <summary> Get elapsed time in seconds since InitWindow() </summary>
    [LibraryImport(LIB, EntryPoint = "GetTime")]
    public static partial double GetTime();

    /// <summary> Takes a screenshot of current screen (filename extension defines format) </summary>
    [LibraryImport(LIB, EntryPoint = "TakeScreenshot")]
    public static partial void TakeScreenshot([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Setup init configuration flags (view FLAGS) </summary>
    [LibraryImport(LIB, EntryPoint = "SetConfigFlags")]
    public static partial void SetConfigFlags(WindowFlag flags);

    /// <summary> Set the current threshold (minimum) log level </summary>
    [LibraryImport(LIB, EntryPoint = "SetTraceLogLevel")]
    public static partial void SetTraceLogLevel(TraceLogLevel logLevel);

    /// <summary> Open URL with default system browser (if available) </summary>
    [LibraryImport(LIB, EntryPoint = "OpenURL")]
    public static partial void OpenURL([MarshalAs(UnmanagedType.LPStr)] string url);

    /// <summary> Set custom file binary data loader </summary>
    [LibraryImport(LIB, EntryPoint = "SetLoadFileDataCallback")]
    public static partial void SetLoadFileDataCallback(LoadFileDataCallback callback);

    /// <summary> Set custom file binary data saver </summary>
    [LibraryImport(LIB, EntryPoint = "SetSaveFileDataCallback")]
    public static partial void SetSaveFileDataCallback(SaveFileDataCallback callback);

    /// <summary> Set custom file text data loader </summary>
    [LibraryImport(LIB, EntryPoint = "SetLoadFileTextCallback")]
    public static partial void SetLoadFileTextCallback(LoadFileTextCallback callback);

    /// <summary> Set custom file text data saver </summary>
    [LibraryImport(LIB, EntryPoint = "SetSaveFileTextCallback")]
    public static partial void SetSaveFileTextCallback(SaveFileTextCallback callback);

    /// <summary> Change working directory, return true on success </summary>
    [LibraryImport(LIB, EntryPoint = "ChangeDirectory")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ChangeDirectory([MarshalAs(UnmanagedType.LPStr)] string dir);

    /// <summary> Check if a given path is a file or a directory </summary>
    [LibraryImport(LIB, EntryPoint = "IsPathFile")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsPathFile([MarshalAs(UnmanagedType.LPStr)] string path);

    /// <summary> Check if a file has been dropped into window </summary>
    [LibraryImport(LIB, EntryPoint = "IsFileDropped")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsFileDropped();

    /// <summary> Load dropped filepaths </summary>
    [LibraryImport(LIB, EntryPoint = "LoadDroppedFiles")]
    public static partial FilePathList LoadDroppedFiles();

    /// <summary> Unload dropped filepaths </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadDroppedFiles")]
    public static partial void UnloadDroppedFiles(FilePathList files);

    /// <summary> Get file modification time (last write time) </summary>
    [LibraryImport(LIB, EntryPoint = "GetFileModTime")]
    public static partial long GetFileModTime([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Check if a key has been pressed once </summary>
    [LibraryImport(LIB, EntryPoint = "IsKeyPressed")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyPressed(Key key);

    /// <summary> Check if a key is being pressed </summary>
    [LibraryImport(LIB, EntryPoint = "IsKeyDown")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyDown(Key key);

    /// <summary> Check if a key has been released once </summary>
    [LibraryImport(LIB, EntryPoint = "IsKeyReleased")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyReleased(Key key);

    /// <summary> Check if a key is NOT being pressed </summary>
    [LibraryImport(LIB, EntryPoint = "IsKeyUp")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyUp(Key key);

    /// <summary> Set a custom key to exit program (default is ESC) </summary>
    [LibraryImport(LIB, EntryPoint = "SetExitKey")]
    public static partial void SetExitKey(Key key);

    /// <summary> Get key pressed (keycode), call it multiple times for keys queued, returns 0 when the queue is empty </summary>
    [LibraryImport(LIB, EntryPoint = "GetKeyPressed")]
    public static partial int GetKeyPressed();

    /// <summary> Get char pressed (unicode), call it multiple times for chars queued, returns 0 when the queue is empty </summary>
    [LibraryImport(LIB, EntryPoint = "GetCharPressed")]
    public static partial int GetCharPressed();

    /// <summary> Check if a gamepad is available </summary>
    [LibraryImport(LIB, EntryPoint = "IsGamepadAvailable")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadAvailable(int gamepad);

    /// <summary> Get gamepad internal name id </summary>
    [LibraryImport(LIB, EntryPoint = "GetGamepadName")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string GetGamepadName(int gamepad);

    /// <summary> Check if a gamepad button has been pressed once </summary>
    [LibraryImport(LIB, EntryPoint = "IsGamepadButtonPressed")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonPressed(int gamepad, int button);

    /// <summary> Check if a gamepad button is being pressed </summary>
    [LibraryImport(LIB, EntryPoint = "IsGamepadButtonDown")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonDown(int gamepad, GamepadButton button);

    /// <summary> Check if a gamepad button has been released once </summary>
    [LibraryImport(LIB, EntryPoint = "IsGamepadButtonReleased")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonReleased(int gamepad, int button);

    /// <summary> Check if a gamepad button is NOT being pressed </summary>
    [LibraryImport(LIB, EntryPoint = "IsGamepadButtonUp")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonUp(int gamepad, int button);

    /// <summary> Get the last gamepad button pressed </summary>
    [LibraryImport(LIB, EntryPoint = "GetGamepadButtonPressed")]
    public static partial GamepadButton GetGamepadButtonPressed();

    /// <summary> Get gamepad axis count for a gamepad </summary>
    [LibraryImport(LIB, EntryPoint = "GetGamepadAxisCount")]
    public static partial int GetGamepadAxisCount(int gamepad);

    /// <summary> Get axis movement value for a gamepad axis </summary>
    [LibraryImport(LIB, EntryPoint = "GetGamepadAxisMovement")]
    public static partial float GetGamepadAxisMovement(int gamepad, GamepadAxis axis);

    /// <summary> Set internal gamepad mappings (SDL_GameControllerDB) </summary>
    [LibraryImport(LIB, EntryPoint = "SetGamepadMappings")]
    public static partial int SetGamepadMappings([MarshalAs(UnmanagedType.LPStr)] string mappings);

    /// <summary> Check if a mouse button has been pressed once </summary>
    [LibraryImport(LIB, EntryPoint = "IsMouseButtonPressed")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonPressed(MouseButton button);

    /// <summary> Check if a mouse button is being pressed </summary>
    [LibraryImport(LIB, EntryPoint = "IsMouseButtonDown")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonDown(MouseButton button);

    /// <summary> Check if a mouse button has been released once </summary>
    [LibraryImport(LIB, EntryPoint = "IsMouseButtonReleased")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonReleased(MouseButton button);

    /// <summary> Check if a mouse button is NOT being pressed </summary>
    [LibraryImport(LIB, EntryPoint = "IsMouseButtonUp")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonUp(MouseButton button);

    /// <summary> Get mouse position X </summary>
    [LibraryImport(LIB, EntryPoint = "GetMouseX")]
    public static partial int GetMouseX();

    /// <summary> Get mouse position Y </summary>
    [LibraryImport(LIB, EntryPoint = "GetMouseY")]
    public static partial int GetMouseY();

    /// <summary> Get mouse position XY </summary>
    [LibraryImport(LIB, EntryPoint = "GetMousePosition")]
    public static partial Vector2 GetMousePosition();

    /// <summary> Get mouse delta between frames </summary>
    [LibraryImport(LIB, EntryPoint = "GetMouseDelta")]
    public static partial Vector2 GetMouseDelta();

    /// <summary> Set mouse position XY </summary>
    [LibraryImport(LIB, EntryPoint = "SetMousePosition")]
    public static partial void SetMousePosition(int x, int y);

    /// <summary> Set mouse offset </summary>
    [LibraryImport(LIB, EntryPoint = "SetMouseOffset")]
    public static partial void SetMouseOffset(int offsetX, int offsetY);

    /// <summary> Set mouse scaling </summary>
    [LibraryImport(LIB, EntryPoint = "SetMouseScale")]
    public static partial void SetMouseScale(float scaleX, float scaleY);

    /// <summary> Get mouse wheel movement for both X and Y </summary>
    [LibraryImport(LIB, EntryPoint = "GetMouseWheelMoveV")]
    public static partial Vector2 GetMouseWheelMove();

    /// <summary> Set mouse cursor </summary>
    [LibraryImport(LIB, EntryPoint = "SetMouseCursor")]
    public static partial void SetMouseCursor(MouseCursor cursor);

    /// <summary> Get touch position X for touch point 0 (relative to screen size) </summary>
    [LibraryImport(LIB, EntryPoint = "GetTouchX")]
    public static partial int GetTouchX();

    /// <summary> Get touch position Y for touch point 0 (relative to screen size) </summary>
    [LibraryImport(LIB, EntryPoint = "GetTouchY")]
    public static partial int GetTouchY();

    /// <summary> Get touch position XY for a touch point index (relative to screen size) </summary>
    [LibraryImport(LIB, EntryPoint = "GetTouchPosition")]
    public static partial Vector2 GetTouchPosition(int index);

    /// <summary> Get touch point identifier for given index </summary>
    [LibraryImport(LIB, EntryPoint = "GetTouchPointId")]
    public static partial int GetTouchPointId(int index);

    /// <summary> Get number of touch points </summary>
    [LibraryImport(LIB, EntryPoint = "GetTouchPointCount")]
    public static partial int GetTouchPointCount();

    /// <summary> Enable a set of gestures using flags </summary>
    [LibraryImport(LIB, EntryPoint = "SetGesturesEnabled")]
    public static partial void SetGesturesEnabled(uint flags);

    /// <summary> Check if a gesture have been detected </summary>
    [LibraryImport(LIB, EntryPoint = "IsGestureDetected")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGestureDetected(int gesture);

    /// <summary> Get latest detected gesture </summary>
    [LibraryImport(LIB, EntryPoint = "GetGestureDetected")]
    public static partial Gesture GetGestureDetected();

    /// <summary> Get gesture hold time in milliseconds </summary>
    [LibraryImport(LIB, EntryPoint = "GetGestureHoldDuration")]
    public static partial float GetGestureHoldDuration();

    /// <summary> Get gesture drag vector </summary>
    [LibraryImport(LIB, EntryPoint = "GetGestureDragVector")]
    public static partial Vector2 GetGestureDragVector();

    /// <summary> Get gesture drag angle </summary>
    [LibraryImport(LIB, EntryPoint = "GetGestureDragAngle")]
    public static partial float GetGestureDragAngle();

    /// <summary> Get gesture pinch delta </summary>
    [LibraryImport(LIB, EntryPoint = "GetGesturePinchVector")]
    public static partial Vector2 GetGesturePinchVector();

    /// <summary> Get gesture pinch angle </summary>
    [LibraryImport(LIB, EntryPoint = "GetGesturePinchAngle")]
    public static partial float GetGesturePinchAngle();

    /// <summary> Update camera position for selected mode </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateCamera")]
    public static partial void UpdateCamera(ref Camera3D camera, CameraMode mode);

    /// <summary> Update camera movement/rotation </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateCameraPro")]
    public static partial void UpdateCamera(ref Camera3D camera, Vector3 movement, Vector3 rotation, float zoom);

    /// <summary> Set texture and rectangle to be used on shapes drawing </summary>
    [LibraryImport(LIB, EntryPoint = "SetShapesTexture")]
    public static partial void SetShapesTexture(Texture texture, RectangleF source);

    /// <summary> Draw a pixel </summary>
    [LibraryImport(LIB, EntryPoint = "DrawPixel")]
    public static partial void DrawPixel(int posX, int posY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a pixel (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawPixelV")]
    public static partial void DrawPixel(Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLine")]
    public static partial void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLineV")]
    public static partial void DrawLine(Vector2 startPos, Vector2 endPos, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line defining thickness </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLineEx")]
    public static partial void DrawLine(Vector2 startPos, Vector2 endPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line using cubic-bezier curves in-out </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLineBezier")]
    public static partial void DrawLineBezier(Vector2 startPos, Vector2 endPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line using quadratic bezier curves with a control point </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLineBezierQuad")]
    public static partial void DrawLineBezierQuad(Vector2 startPos, Vector2 endPos, Vector2 controlPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line using cubic bezier curves with 2 control points </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLineBezierCubic")]
    public static partial void DrawLineBezierCubic(Vector2 startPos, Vector2 endPos, Vector2 startControlPos, Vector2 endControlPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw lines sequence </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLineStrip")]
    public static partial void DrawLineStrip(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled circle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCircle")]
    public static partial void DrawCircle(int centerX, int centerY, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a piece of a circle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCircleSector")]
    public static partial void DrawCircleSector(Vector2 center, float radius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle sector outline </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCircleSectorLines")]
    public static partial void DrawCircleSectorLines(Vector2 center, float radius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a gradient-filled circle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCircleGradient")]
    public static partial void DrawCircleGradient(int centerX, int centerY, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color1, [MarshalUsing(typeof(ColorMarshaller))] Color color2);

    /// <summary> Draw a color-filled circle (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCircleV")]
    public static partial void DrawCircle(Vector2 center, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle outline </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCircleLines")]
    public static partial void DrawCircleLines(int centerX, int centerY, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ellipse </summary>
    [LibraryImport(LIB, EntryPoint = "DrawEllipse")]
    public static partial void DrawEllipse(int centerX, int centerY, float radiusH, float radiusV, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ellipse outline </summary>
    [LibraryImport(LIB, EntryPoint = "DrawEllipseLines")]
    public static partial void DrawEllipseLines(int centerX, int centerY, float radiusH, float radiusV, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ring </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRing")]
    public static partial void DrawRing(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ring outline </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRingLines")]
    public static partial void DrawRingLines(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangle")]
    public static partial void DrawRectangle(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleV")]
    public static partial void DrawRectangle(Vector2 position, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleRec")]
    public static partial void DrawRectangle(RectangleF rec, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle with pro parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectanglePro")]
    public static partial void DrawRectangle(RectangleF rec, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a vertical-gradient-filled rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleGradientV")]
    public static partial void DrawRectangleGradient(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color1, [MarshalUsing(typeof(ColorMarshaller))] Color color2);

    /// <summary> Draw a horizontal-gradient-filled rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleGradientH")]
    public static partial void DrawRectangleGradientH(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color1, [MarshalUsing(typeof(ColorMarshaller))] Color color2);

    /// <summary> Draw a gradient-filled rectangle with custom vertex colors </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleGradientEx")]
    public static partial void DrawRectangleGradient(RectangleF rec, [MarshalUsing(typeof(ColorMarshaller))] Color col1, [MarshalUsing(typeof(ColorMarshaller))] Color col2, [MarshalUsing(typeof(ColorMarshaller))] Color col3, [MarshalUsing(typeof(ColorMarshaller))] Color col4);

    /// <summary> Draw rectangle outline </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleLines")]
    public static partial void DrawRectangleLines(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle outline with extended parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleLinesEx")]
    public static partial void DrawRectangleLines(RectangleF rec, float lineThick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle with rounded edges </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleRounded")]
    public static partial void DrawRectangleRounded(RectangleF rec, float roundness, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle with rounded edges outline </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRectangleRoundedLines")]
    public static partial void DrawRectangleRoundedLines(RectangleF rec, float roundness, int segments, float lineThick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled triangle (vertex in counter-clockwise order!) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTriangle")]
    public static partial void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw triangle outline (vertex in counter-clockwise order!) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTriangleLines")]
    public static partial void DrawTriangleLines(Vector2 v1, Vector2 v2, Vector2 v3, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a triangle fan defined by points (first vertex is the center) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTriangleFan")]
    public static partial void DrawTriangleFan(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a triangle strip defined by points </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTriangleStrip")]
    public static partial void DrawTriangleStrip(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a regular polygon (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawPoly")]
    public static partial void DrawPoly(Vector2 center, int sides, float radius, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a polygon outline of n sides </summary>
    [LibraryImport(LIB, EntryPoint = "DrawPolyLines")]
    public static partial void DrawPolyLines(Vector2 center, int sides, float radius, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a polygon outline of n sides with extended parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawPolyLinesEx")]
    public static partial void DrawPolyLines(Vector2 center, int sides, float radius, float rotation, float lineThick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Check collision between two rectangles </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionRecs")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionRecs(RectangleF rec1, RectangleF rec2);

    /// <summary> Check collision between two circles </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionCircles")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionCircles(Vector2 center1, float radius1, Vector2 center2, float radius2);

    /// <summary> Check collision between circle and rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionCircleRec")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionCircle(Vector2 center, float radius, RectangleF rec);

    /// <summary> Check if point is inside rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionPointRec")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPoint(Vector2 point, RectangleF rec);

    /// <summary> Check if point is inside circle </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionPointCircle")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointCircle(Vector2 point, Vector2 center, float radius);

    /// <summary> Check if point is inside a triangle </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionPointTriangle")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3);

    /// <summary> Check if point is within a polygon described by array of vertices </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionPointPoly")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointPoly(Vector2 point, IntPtr points, int pointCount);

    /// <summary> Check the collision between two lines defined by two points each, returns collision point by reference </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionLines")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionLines(Vector2 startPos1, Vector2 endPos1, Vector2 startPos2, Vector2 endPos2, IntPtr collisionPoint);

    /// <summary> Check if point belongs to line created between two points [p1] and [p2] with defined margin in pixels [threshold] </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionPointLine")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointLine(Vector2 point, Vector2 p1, Vector2 p2, int threshold);

    /// <summary> Get collision rectangle for two rectangles collision </summary>
    [LibraryImport(LIB, EntryPoint = "GetCollisionRec")]
    public static partial RectangleF GetCollision(RectangleF rec1, RectangleF rec2);

    /// <summary> Load image from file into CPU memory (RAM) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImage")]
    public static partial Image LoadImage([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load image from RAW file data </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageRaw")]
    public static partial Image LoadImageRaw([MarshalAs(UnmanagedType.LPStr)] string fileName, int width, int height, int format, int headerSize);

    /// <summary> Load image sequence from file (frames appended to image.data) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageAnim")]
    public static partial Image LoadImageAnim([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr frames);

    /// <summary> Load image from memory buffer, fileType refers to extension: i.e. '.png' </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageFromMemory")]
    public static partial Image LoadImageFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, [MarshalAs(UnmanagedType.LPArray)] byte[] fileData, int dataSize);

    /// <summary> Load image from GPU texture data </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageFromTexture")]
    public static partial Image LoadImageFromTexture(Texture texture);

    /// <summary> Load image from screen buffer and (screenshot) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageFromScreen")]
    public static partial Image LoadImageFromScreen();

    /// <summary> Check if an image is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsImageReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsImageReady(Image image);

    /// <summary> Unload image from CPU memory (RAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadImage")]
    public static partial void UnloadImage(Image image);

    /// <summary> Export image data to file, returns true on success </summary>
    [LibraryImport(LIB, EntryPoint = "ExportImage")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportImage(Image image, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Export image to memory buffer </summary>
    [LibraryImport(LIB, EntryPoint = "ExportImageToMemory")]
    public static partial byte* ExportImageToMemory(Image image, [MarshalAs(UnmanagedType.LPStr)] string fileType, IntPtr fileSize);

    /// <summary> Export image as code file defining an array of bytes, returns true on success </summary>
    [LibraryImport(LIB, EntryPoint = "ExportImageAsCode")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportImageAsCode(Image image, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Generate image: plain color </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageColor")]
    public static partial Image GenImageColor(int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Generate image: linear gradient, direction in degrees [0..360], 0=Vertical gradient </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageGradientLinear")]
    public static partial Image GenImageGradientLinear(int width, int height, int direction, [MarshalUsing(typeof(ColorMarshaller))] Color start, [MarshalUsing(typeof(ColorMarshaller))] Color end);

    /// <summary> Generate image: radial gradient </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageGradientRadial")]
    public static partial Image GenImageGradientRadial(int width, int height, float density, [MarshalUsing(typeof(ColorMarshaller))] Color inner, [MarshalUsing(typeof(ColorMarshaller))] Color outer);

    /// <summary> Generate image: square gradient </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageGradientSquare")]
    public static partial Image GenImageGradientSquare(int width, int height, float density, [MarshalUsing(typeof(ColorMarshaller))] Color inner, [MarshalUsing(typeof(ColorMarshaller))] Color outer);

    /// <summary> Generate image: checked </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageChecked")]
    public static partial Image GenImageChecked(int width, int height, int checksX, int checksY, [MarshalUsing(typeof(ColorMarshaller))] Color col1, [MarshalUsing(typeof(ColorMarshaller))] Color col2);

    /// <summary> Generate image: white noise </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageWhiteNoise")]
    public static partial Image GenImageWhiteNoise(int width, int height, float factor);

    /// <summary> Generate image: perlin noise </summary>
    [LibraryImport(LIB, EntryPoint = "GenImagePerlinNoise")]
    public static partial Image GenImagePerlinNoise(int width, int height, int offsetX, int offsetY, float scale);

    /// <summary> Generate image: cellular algorithm, bigger tileSize means bigger cells </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageCellular")]
    public static partial Image GenImageCellular(int width, int height, int tileSize);

    /// <summary> Generate image: grayscale image from text data </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageText")]
    public static partial Image GenImageText(int width, int height, [MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Create an image duplicate (useful for transformations) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageCopy")]
    public static partial Image ImageCopy(Image image);

    /// <summary> Create an image from another image piece </summary>
    [LibraryImport(LIB, EntryPoint = "ImageFromImage")]
    public static partial Image ImageFromImage(Image image, RectangleF rec);

    /// <summary> Create an image from text (default font) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageText")]
    public static partial Image ImageText([MarshalAs(UnmanagedType.LPStr)] string text, int fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Create an image from text (custom sprite font) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageTextEx")]
    public static partial Image ImageText(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Convert image data to desired format </summary>
    [LibraryImport(LIB, EntryPoint = "ImageFormat")]
    public static partial void ImageFormat(IntPtr image, int newFormat);

    /// <summary> Convert image to POT (power-of-two) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageToPOT")]
    public static partial void ImageToPOT(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color fill);

    /// <summary> Crop an image to a defined rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "ImageCrop")]
    public static partial void ImageCrop(IntPtr image, RectangleF crop);

    /// <summary> Crop image depending on alpha value </summary>
    [LibraryImport(LIB, EntryPoint = "ImageAlphaCrop")]
    public static partial void ImageAlphaCrop(IntPtr image, float threshold);

    /// <summary> Clear alpha channel to desired color </summary>
    [LibraryImport(LIB, EntryPoint = "ImageAlphaClear")]
    public static partial void ImageAlphaClear(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color color, float threshold);

    /// <summary> Apply alpha mask to image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageAlphaMask")]
    public static partial void ImageAlphaMask(IntPtr image, Image alphaMask);

    /// <summary> Premultiply alpha channel </summary>
    [LibraryImport(LIB, EntryPoint = "ImageAlphaPremultiply")]
    public static partial void ImageAlphaPremultiply(IntPtr image);

    /// <summary> Apply Gaussian blur using a box blur approximation </summary>
    [LibraryImport(LIB, EntryPoint = "ImageBlurGaussian")]
    public static partial void ImageBlurGaussian(IntPtr image, int blurSize);

    /// <summary> Resize image (Bicubic scaling algorithm) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageResize")]
    public static partial void ImageResize(IntPtr image, int newWidth, int newHeight);

    /// <summary> Resize image (Nearest-Neighbor scaling algorithm) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageResizeNN")]
    public static partial void ImageResizeNN(IntPtr image, int newWidth, int newHeight);

    /// <summary> Resize canvas and fill with color </summary>
    [LibraryImport(LIB, EntryPoint = "ImageResizeCanvas")]
    public static partial void ImageResizeCanvas(IntPtr image, int newWidth, int newHeight, int offsetX, int offsetY, [MarshalUsing(typeof(ColorMarshaller))] Color fill);

    /// <summary> Compute all mipmap levels for a provided image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageMipmaps")]
    public static partial void ImageMipmaps(IntPtr image);

    /// <summary> Dither image data to 16bpp or lower (Floyd-Steinberg dithering) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDither")]
    public static partial void ImageDither(IntPtr image, int rBpp, int gBpp, int bBpp, int aBpp);

    /// <summary> Flip image vertically </summary>
    [LibraryImport(LIB, EntryPoint = "ImageFlipVertical")]
    public static partial void ImageFlipVertical(IntPtr image);

    /// <summary> Flip image horizontally </summary>
    [LibraryImport(LIB, EntryPoint = "ImageFlipHorizontal")]
    public static partial void ImageFlipHorizontal(IntPtr image);

    /// <summary> Rotate image by input angle in degrees (-359 to 359) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageRotate")]
    public static partial void ImageRotate(IntPtr image, int degrees);

    /// <summary> Rotate image clockwise 90deg </summary>
    [LibraryImport(LIB, EntryPoint = "ImageRotateCW")]
    public static partial void ImageRotateCW(IntPtr image);

    /// <summary> Rotate image counter-clockwise 90deg </summary>
    [LibraryImport(LIB, EntryPoint = "ImageRotateCCW")]
    public static partial void ImageRotateCCW(IntPtr image);

    /// <summary> Modify image color: tint </summary>
    [LibraryImport(LIB, EntryPoint = "ImageColorTint")]
    public static partial void ImageColorTint(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Modify image color: invert </summary>
    [LibraryImport(LIB, EntryPoint = "ImageColorInvert")]
    public static partial void ImageColorInvert(IntPtr image);

    /// <summary> Modify image color: grayscale </summary>
    [LibraryImport(LIB, EntryPoint = "ImageColorGrayscale")]
    public static partial void ImageColorGrayscale(IntPtr image);

    /// <summary> Modify image color: contrast (-100 to 100) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageColorContrast")]
    public static partial void ImageColorContrast(IntPtr image, float contrast);

    /// <summary> Modify image color: brightness (-255 to 255) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageColorBrightness")]
    public static partial void ImageColorBrightness(IntPtr image, int brightness);

    /// <summary> Modify image color: replace color </summary>
    [LibraryImport(LIB, EntryPoint = "ImageColorReplace")]
    public static partial void ImageColorReplace(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color color, [MarshalUsing(typeof(ColorMarshaller))] Color replace);

    /// <summary> Load color data from image as a Color array (RGBA - 32bit) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageColors")]
    public static partial IntPtr LoadImageColors(Image image);

    /// <summary> Load colors palette from image as a Color array (RGBA - 32bit) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImagePalette")]
    public static partial IntPtr LoadImagePalette(Image image, int maxPaletteSize, IntPtr colorCount);

    /// <summary> Unload color data loaded with LoadImageColors() </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadImageColors")]
    public static partial void UnloadImageColors(IntPtr colors);

    /// <summary> Unload colors palette loaded with LoadImagePalette() </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadImagePalette")]
    public static partial void UnloadImagePalette(IntPtr colors);

    /// <summary> Get image alpha border rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "GetImageAlphaBorder")]
    public static partial RectangleF GetImageAlphaBorder(Image image, float threshold);

    /// <summary> Get image pixel color at (x, y) position </summary>
    [LibraryImport(LIB, EntryPoint = "GetImageColor")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color GetImageColor(Image image, int x, int y);

    /// <summary> Clear image background with given color </summary>
    [LibraryImport(LIB, EntryPoint = "ImageClearBackground")]
    public static partial void ImageClearBackground(IntPtr dst, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw pixel within an image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawPixel")]
    public static partial void ImageDrawPixel(IntPtr dst, int posX, int posY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw pixel within an image (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawPixelV")]
    public static partial void ImageDrawPixel(IntPtr dst, Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line within an image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawLine")]
    public static partial void ImageDrawLine(IntPtr dst, int startPosX, int startPosY, int endPosX, int endPosY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line within an image (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawLineV")]
    public static partial void ImageDrawLine(IntPtr dst, Vector2 start, Vector2 end, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a filled circle within an image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawCircle")]
    public static partial void ImageDrawCircle(IntPtr dst, int centerX, int centerY, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a filled circle within an image (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawCircleV")]
    public static partial void ImageDrawCircle(IntPtr dst, Vector2 center, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle outline within an image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawCircleLines")]
    public static partial void ImageDrawCircleLines(IntPtr dst, int centerX, int centerY, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle outline within an image (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawCircleLinesV")]
    public static partial void ImageDrawCircleLines(IntPtr dst, Vector2 center, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle within an image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawRectangle")]
    public static partial void ImageDrawRectangle(IntPtr dst, int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle within an image (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawRectangleV")]
    public static partial void ImageDrawRectangle(IntPtr dst, Vector2 position, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle within an image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawRectangleRec")]
    public static partial void ImageDrawRectangle(IntPtr dst, RectangleF rec, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle lines within an image </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawRectangleLines")]
    public static partial void ImageDrawRectangleLines(IntPtr dst, RectangleF rec, int thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a source image within a destination image (tint applied to source) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDraw")]
    public static partial void ImageDraw(IntPtr dst, Image src, RectangleF srcRec, RectangleF dstRec, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw text (using default font) within an image (destination) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawText")]
    public static partial void ImageDrawText(IntPtr dst, [MarshalAs(UnmanagedType.LPStr)] string text, int posX, int posY, int fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw text (custom sprite font) within an image (destination) </summary>
    [LibraryImport(LIB, EntryPoint = "ImageDrawTextEx")]
    public static partial void ImageDrawText(IntPtr dst, Font font, [MarshalAs(UnmanagedType.LPStr)] string text, Vector2 position, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Load texture from file into GPU memory (VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadTexture")]
    public static partial Texture LoadTexture([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load texture from image data </summary>
    [LibraryImport(LIB, EntryPoint = "LoadTextureFromImage")]
    public static partial Texture LoadTextureFromImage(Image image);

    /// <summary> Load cubemap from image, multiple image cubemap layouts supported </summary>
    [LibraryImport(LIB, EntryPoint = "LoadTextureCubemap")]
    public static partial Texture LoadTextureCubemap(Image image, int layout);

    /// <summary> Load texture for rendering (framebuffer) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadRenderTexture")]
    public static partial RenderTexture LoadRenderTexture(int width, int height);

    /// <summary> Check if a texture is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsTextureReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsTextureReady(Texture texture);

    /// <summary> Unload texture from GPU memory (VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadTexture")]
    public static partial void UnloadTexture(Texture texture);

    /// <summary> Check if a render texture is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsRenderTextureReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsRenderTextureReady(RenderTexture target);

    /// <summary> Unload render texture from GPU memory (VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadRenderTexture")]
    public static partial void UnloadRenderTexture(RenderTexture target);

    /// <summary> Update GPU texture with new data </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateTexture")]
    public static partial void UpdateTexture(Texture texture, IntPtr pixels);

    /// <summary> Update GPU texture rectangle with new data </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateTextureRec")]
    public static partial void UpdateTexture(Texture texture, RectangleF rec, IntPtr pixels);

    /// <summary> Generate GPU mipmaps for a texture </summary>
    [LibraryImport(LIB, EntryPoint = "GenTextureMipmaps")]
    public static partial void GenTextureMipmaps(IntPtr texture);

    /// <summary> Set texture scaling filter mode </summary>
    [LibraryImport(LIB, EntryPoint = "SetTextureFilter")]
    public static partial void SetTextureFilter(Texture texture, TextureFilter filter);

    /// <summary> Set texture wrapping mode </summary>
    [LibraryImport(LIB, EntryPoint = "SetTextureWrap")]
    public static partial void SetTextureWrap(Texture texture, int wrap);

    /// <summary> Draw a Texture2D </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTexture")]
    public static partial void DrawTexture(Texture texture, int posX, int posY, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a Texture2D with position defined as Vector2 </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextureV")]
    public static partial void DrawTexture(Texture texture, Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a Texture2D with extended parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextureEx")]
    public static partial void DrawTexture(Texture texture, Vector2 position, float rotation, float scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a part of a texture defined by a rectangle </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextureRec")]
    public static partial void DrawTexture(Texture texture, RectangleF source, Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a part of a texture defined by a rectangle with 'pro' parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTexturePro")]
    public static partial void DrawTexture(Texture texture, RectangleF source, RectangleF dest, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draws a texture (or part of it) that stretches or shrinks nicely </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextureNPatch")]
    public static partial void DrawTextureNPatch(Texture texture, NPatchInfo nPatchInfo, RectangleF dest, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Get color with alpha applied, alpha goes from 0.0f to 1.0f </summary>
    [LibraryImport(LIB, EntryPoint = "Fade")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color Fade([MarshalUsing(typeof(ColorMarshaller))] Color color, float alpha);

    /// <summary> Get hexadecimal value for a Color </summary>
    [LibraryImport(LIB, EntryPoint = "ColorToInt")]
    public static partial int ColorToInt([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Get Color normalized as float [0..1] </summary>
    [LibraryImport(LIB, EntryPoint = "ColorNormalize")]
    public static partial Vector4 ColorNormalize([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Get Color from normalized values [0..1] </summary>
    [LibraryImport(LIB, EntryPoint = "ColorFromNormalized")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorFromNormalized(Vector4 normalized);

    /// <summary> Get HSV values for a Color, hue [0..360], saturation/value [0..1] </summary>
    [LibraryImport(LIB, EntryPoint = "ColorToHSV")]
    public static partial Vector3 ColorToHSV([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Get a Color from HSV values, hue [0..360], saturation/value [0..1] </summary>
    [LibraryImport(LIB, EntryPoint = "ColorFromHSV")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorFromHSV(float hue, float saturation, float value);

    /// <summary> Get color multiplied with another color </summary>
    [LibraryImport(LIB, EntryPoint = "ColorTint")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorTint([MarshalUsing(typeof(ColorMarshaller))] Color color, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Get color with brightness correction, brightness factor goes from -1.0f to 1.0f </summary>
    [LibraryImport(LIB, EntryPoint = "ColorBrightness")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorBrightness([MarshalUsing(typeof(ColorMarshaller))] Color color, float factor);

    /// <summary> Get color with contrast correction, contrast values between -1.0f and 1.0f </summary>
    [LibraryImport(LIB, EntryPoint = "ColorContrast")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorContrast([MarshalUsing(typeof(ColorMarshaller))] Color color, float contrast);

    /// <summary> Get color with alpha applied, alpha goes from 0.0f to 1.0f </summary>
    [LibraryImport(LIB, EntryPoint = "ColorAlpha")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorAlpha([MarshalUsing(typeof(ColorMarshaller))] Color color, float alpha);

    /// <summary> Get src alpha-blended into dst color with tint </summary>
    [LibraryImport(LIB, EntryPoint = "ColorAlphaBlend")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorAlphaBlend([MarshalUsing(typeof(ColorMarshaller))] Color dst, [MarshalUsing(typeof(ColorMarshaller))] Color src, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Get Color structure from hexadecimal value </summary>
    [LibraryImport(LIB, EntryPoint = "GetColor")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color GetColor(uint hexValue);

    /// <summary> Get Color from a source pixel pointer of certain format </summary>
    [LibraryImport(LIB, EntryPoint = "GetPixelColor")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color GetPixelColor(IntPtr srcPtr, int format);

    /// <summary> Set color formatted into destination pixel pointer </summary>
    [LibraryImport(LIB, EntryPoint = "SetPixelColor")]
    public static partial void SetPixelColor(IntPtr dstPtr, [MarshalUsing(typeof(ColorMarshaller))] Color color, int format);

    /// <summary> Get pixel data size in bytes for certain format </summary>
    [LibraryImport(LIB, EntryPoint = "GetPixelDataSize")]
    public static partial int GetPixelDataSize(int width, int height, int format);

    /// <summary> Get the default Font </summary>
    [LibraryImport(LIB, EntryPoint = "GetFontDefault")]
    public static partial Font GetFontDefault();

    /// <summary> Load font from file into GPU memory (VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadFont")]
    public static partial Font LoadFont([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load font from file with extended parameters, use NULL for fontChars and 0 for glyphCount to load the default character set </summary>
    [LibraryImport(LIB, EntryPoint = "LoadFontEx")]
    public static partial Font LoadFont([MarshalAs(UnmanagedType.LPStr)] string fileName, int fontSize, IntPtr fontChars, int glyphCount);

    /// <summary> Load font from Image (XNA style) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadFontFromImage")]
    public static partial Font LoadFontFromImage(Image image, [MarshalUsing(typeof(ColorMarshaller))] Color key, int firstChar);

    /// <summary> Load font from memory buffer, fileType refers to extension: i.e. '.ttf' </summary>
    [LibraryImport(LIB, EntryPoint = "LoadFontFromMemory")]
    public static partial Font LoadFontFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, [MarshalAs(UnmanagedType.LPArray)] byte[] fileData, int dataSize, int fontSize, IntPtr fontChars, int glyphCount);

    /// <summary> Check if a font is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsFontReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsFontReady(Font font);

    /// <summary> Load font data for further use </summary>
    [LibraryImport(LIB, EntryPoint = "LoadFontData")]
    public static partial IntPtr LoadFontData(byte[] fileData, int dataSize, int fontSize, IntPtr fontChars, int glyphCount, int type);

    /// <summary> Generate image font atlas using chars info </summary>
    [LibraryImport(LIB, EntryPoint = "GenImageFontAtlas")]
    public static partial Image GenImageFontAtlas(IntPtr chars, IntPtr recs, int glyphCount, int fontSize, int padding, int packMethod);

    /// <summary> Unload font chars info data (RAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadFontData")]
    public static partial void UnloadFontData(IntPtr chars, int glyphCount);

    /// <summary> Unload font from GPU memory (VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadFont")]
    public static partial void UnloadFont(Font font);

    /// <summary> Export font as code file, returns true on success </summary>
    [LibraryImport(LIB, EntryPoint = "ExportFontAsCode")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportFontAsCode(Font font, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Draw current FPS </summary>
    [LibraryImport(LIB, EntryPoint = "DrawFPS")]
    public static partial void DrawFPS(int posX, int posY);

    /// <summary> Draw text (using default font) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawText")]
    public static partial void DrawText([MarshalAs(UnmanagedType.LPStr)] string text, int posX, int posY, int fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw text using font and additional parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextEx")]
    public static partial void DrawText(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, Vector2 position, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw text using Font and pro parameters (rotation) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextPro")]
    public static partial void DrawText(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, Vector2 position, Vector2 origin, float rotation, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw one character (codepoint) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextCodepoint")]
    public static partial void DrawTextCodepoint(Font font, int codepoint, Vector2 position, float fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw multiple character (codepoint) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTextCodepoints")]
    public static partial void DrawTextCodepoints(Font font, IntPtr codepoints, int count, Vector2 position, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Set vertical line spacing when drawing with line-breaks </summary>
    [LibraryImport(LIB, EntryPoint = "SetTextLineSpacing")]
    public static partial void SetTextLineSpacing(int spacing);

    /// <summary> Measure string width for default font </summary>
    [LibraryImport(LIB, EntryPoint = "MeasureText")]
    public static partial int MeasureText([MarshalAs(UnmanagedType.LPStr)] string text, int fontSize);

    /// <summary> Measure string size for Font </summary>
    [LibraryImport(LIB, EntryPoint = "MeasureTextEx")]
    public static partial Vector2 MeasureText(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, float fontSize, float spacing);

    /// <summary> Get glyph index position in font for a codepoint (unicode character), fallback to '?' if not found </summary>
    [LibraryImport(LIB, EntryPoint = "GetGlyphIndex")]
    public static partial int GetGlyphIndex(Font font, int codepoint);

    /// <summary> Get glyph font info data for a codepoint (unicode character), fallback to '?' if not found </summary>
    [LibraryImport(LIB, EntryPoint = "GetGlyphInfo")]
    public static partial GlyphInfo GetGlyphInfo(Font font, int codepoint);

    /// <summary> Get glyph rectangle in font atlas for a codepoint (unicode character), fallback to '?' if not found </summary>
    [LibraryImport(LIB, EntryPoint = "GetGlyphAtlasRec")]
    public static partial RectangleF GetGlyphAtlas(Font font, int codepoint);

    /// <summary> Load UTF-8 text encoded from codepoints array </summary>
    [LibraryImport(LIB, EntryPoint = "LoadUTF8")]
    public static partial IntPtr LoadUTF8(IntPtr codepoints, int length);

    /// <summary> Unload UTF-8 text encoded from codepoints array </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadUTF8")]
    public static partial void UnloadUTF8(IntPtr text);

    /// <summary> Load all codepoints from a UTF-8 text string, codepoints count returned by parameter </summary>
    [LibraryImport(LIB, EntryPoint = "LoadCodepoints")]
    public static partial IntPtr LoadCodepoints([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr count);

    /// <summary> Unload codepoints data from memory </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadCodepoints")]
    public static partial void UnloadCodepoints(IntPtr codepoints);

    /// <summary> Get total number of codepoints in a UTF-8 encoded string </summary>
    [LibraryImport(LIB, EntryPoint = "GetCodepointCount")]
    public static partial int GetCodepointCount([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get next codepoint in a UTF-8 encoded string, 0x3f('?') is returned on failure </summary>
    [LibraryImport(LIB, EntryPoint = "GetCodepoint")]
    public static partial int GetCodepoint([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr codepointSize);

    /// <summary> Get next codepoint in a UTF-8 encoded string, 0x3f('?') is returned on failure </summary>
    [LibraryImport(LIB, EntryPoint = "GetCodepointNext")]
    public static partial int GetCodepointNext([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr codepointSize);

    /// <summary> Get previous codepoint in a UTF-8 encoded string, 0x3f('?') is returned on failure </summary>
    [LibraryImport(LIB, EntryPoint = "GetCodepointPrevious")]
    public static partial int GetCodepointPrevious([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr codepointSize);

    /// <summary> Encode one codepoint into UTF-8 byte array (array length returned as parameter) </summary>
    [LibraryImport(LIB, EntryPoint = "CodepointToUTF8")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string CodepointToUTF8(int codepoint, IntPtr utf8Size);

    /// <summary> Copy one string to another, returns bytes copied </summary>
    [LibraryImport(LIB, EntryPoint = "TextCopy")]
    public static partial int TextCopy(IntPtr dst, [MarshalAs(UnmanagedType.LPStr)] string src);

    /// <summary> Get text length, checks for '\0' ending </summary>
    [LibraryImport(LIB, EntryPoint = "TextLength")]
    public static partial uint TextLength([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get a piece of a text string </summary>
    [LibraryImport(LIB, EntryPoint = "TextSubtext")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextSubtext([MarshalAs(UnmanagedType.LPStr)] string text, int position, int length);

    /// <summary> Replace text string (WARNING: memory must be freed!) </summary>
    [LibraryImport(LIB, EntryPoint = "TextReplace")]
    public static partial IntPtr TextReplace(IntPtr text, [MarshalAs(UnmanagedType.LPStr)] string replace, [MarshalAs(UnmanagedType.LPStr)] string by);

    /// <summary> Insert text in a position (WARNING: memory must be freed!) </summary>
    [LibraryImport(LIB, EntryPoint = "TextInsert")]
    public static partial IntPtr TextInsert([MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.LPStr)] string insert, int position);

    /// <summary> Join text strings with delimiter </summary>
    [LibraryImport(LIB, EntryPoint = "TextJoin")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextJoin(IntPtr textList, int count, [MarshalAs(UnmanagedType.LPStr)] string delimiter);

    /// <summary> Split text into multiple strings </summary>
    [LibraryImport(LIB, EntryPoint = "TextSplit")]
    public static partial IntPtr TextSplit([MarshalAs(UnmanagedType.LPStr)] string text, char delimiter, IntPtr count);

    /// <summary> Append text at specific position and move cursor! </summary>
    [LibraryImport(LIB, EntryPoint = "TextAppend")]
    public static partial void TextAppend(IntPtr text, [MarshalAs(UnmanagedType.LPStr)] string append, IntPtr position);

    /// <summary> Find first text occurrence within a string </summary>
    [LibraryImport(LIB, EntryPoint = "TextFindIndex")]
    public static partial int TextFindIndex([MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.LPStr)] string find);

    /// <summary> Get upper case version of provided string </summary>
    [LibraryImport(LIB, EntryPoint = "TextToUpper")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextToUpper([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get lower case version of provided string </summary>
    [LibraryImport(LIB, EntryPoint = "TextToLower")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextToLower([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get Pascal case notation version of provided string </summary>
    [LibraryImport(LIB, EntryPoint = "TextToPascal")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextToPascal([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get integer value from text (negative values not supported) </summary>
    [LibraryImport(LIB, EntryPoint = "TextToInteger")]
    public static partial int TextToInteger([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Draw a line in 3D world space </summary>
    [LibraryImport(LIB, EntryPoint = "DrawLine3D")]
    public static partial void DrawLine3D(Vector3 startPos, Vector3 endPos, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a point in 3D space, actually a small line </summary>
    [LibraryImport(LIB, EntryPoint = "DrawPoint3D")]
    public static partial void DrawPoint3D(Vector3 position, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a circle in 3D world space </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCircle3D")]
    public static partial void DrawCircle3D(Vector3 center, float radius, Vector3 rotationAxis, float rotationAngle, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled triangle (vertex in counter-clockwise order!) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTriangle3D")]
    public static partial void DrawTriangle3D(Vector3 v1, Vector3 v2, Vector3 v3, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a triangle strip defined by points </summary>
    [LibraryImport(LIB, EntryPoint = "DrawTriangleStrip3D")]
    public static partial void DrawTriangleStrip3D(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCube")]
    public static partial void DrawCube(Vector3 position, float width, float height, float length, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCubeV")]
    public static partial void DrawCube(Vector3 position, Vector3 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube wires </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCubeWires")]
    public static partial void DrawCubeWires(Vector3 position, float width, float height, float length, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube wires (Vector version) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCubeWiresV")]
    public static partial void DrawCubeWires(Vector3 position, Vector3 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw sphere </summary>
    [LibraryImport(LIB, EntryPoint = "DrawSphere")]
    public static partial void DrawSphere(Vector3 centerPos, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw sphere with extended parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawSphereEx")]
    public static partial void DrawSphere(Vector3 centerPos, float radius, int rings, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw sphere wires </summary>
    [LibraryImport(LIB, EntryPoint = "DrawSphereWires")]
    public static partial void DrawSphereWires(Vector3 centerPos, float radius, int rings, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder/cone </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCylinder")]
    public static partial void DrawCylinder(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder with base at startPos and top at endPos </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCylinderEx")]
    public static partial void DrawCylinder(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder/cone wires </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCylinderWires")]
    public static partial void DrawCylinderWires(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder wires with base at startPos and top at endPos </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCylinderWiresEx")]
    public static partial void DrawCylinderWires(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a capsule with the center of its sphere caps at startPos and endPos </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCapsule")]
    public static partial void DrawCapsule(Vector3 startPos, Vector3 endPos, float radius, int slices, int rings, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw capsule wireframe with the center of its sphere caps at startPos and endPos </summary>
    [LibraryImport(LIB, EntryPoint = "DrawCapsuleWires")]
    public static partial void DrawCapsuleWires(Vector3 startPos, Vector3 endPos, float radius, int slices, int rings, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a plane XZ </summary>
    [LibraryImport(LIB, EntryPoint = "DrawPlane")]
    public static partial void DrawPlane(Vector3 centerPos, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a ray line </summary>
    [LibraryImport(LIB, EntryPoint = "DrawRay")]
    public static partial void DrawRay(Ray ray, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a grid (centered at (0, 0, 0)) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawGrid")]
    public static partial void DrawGrid(int slices, float spacing);

    /// <summary> Load model from files (meshes and materials) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadModel")]
    public static partial Model LoadModel([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load model from generated mesh (default material) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadModelFromMesh")]
    public static partial Model LoadModelFromMesh(Mesh mesh);

    /// <summary> Check if a model is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsModelReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsModelReady(Model model);

    /// <summary> Unload model (including meshes) from memory (RAM and/or VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadModel")]
    public static partial void UnloadModel(Model model);

    /// <summary> Compute model bounding box limits (considers all meshes) </summary>
    [LibraryImport(LIB, EntryPoint = "GetModelBoundingBox")]
    public static partial BoundingBox GetModelBoundingBox(Model model);

    /// <summary> Draw a model (with texture if set) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawModel")]
    public static partial void DrawModel(Model model, Vector3 position, float scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a model with extended parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawModelEx")]
    public static partial void DrawModel(Model model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a model wires (with texture if set) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawModelWires")]
    public static partial void DrawModelWires(Model model, Vector3 position, float scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a model wires (with texture if set) with extended parameters </summary>
    [LibraryImport(LIB, EntryPoint = "DrawModelWiresEx")]
    public static partial void DrawModelWires(Model model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw bounding box (wires) </summary>
    [LibraryImport(LIB, EntryPoint = "DrawBoundingBox")]
    public static partial void DrawBoundingBox(BoundingBox box, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a billboard texture </summary>
    [LibraryImport(LIB, EntryPoint = "DrawBillboard")]
    public static partial void DrawBillboard([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, Texture texture, Vector3 position, float size, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a billboard texture defined by source </summary>
    [LibraryImport(LIB, EntryPoint = "DrawBillboardRec")]
    public static partial void DrawBillboard([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, Texture texture, RectangleF source, Vector3 position, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a billboard texture defined by source and rotation </summary>
    [LibraryImport(LIB, EntryPoint = "DrawBillboardPro")]
    public static partial void DrawBillboard([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, Texture texture, RectangleF source, Vector3 position, Vector3 up, Vector2 size, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Upload mesh vertex data in GPU and provide VAO/VBO ids </summary>
    [LibraryImport(LIB, EntryPoint = "UploadMesh")]
    public static partial void UploadMesh(IntPtr mesh, [MarshalAs(UnmanagedType.I1)] bool dynamic);

    /// <summary> Update mesh vertex data in GPU for a specific buffer index </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateMeshBuffer")]
    public static partial void UpdateMeshBuffer(Mesh mesh, int index, IntPtr data, int dataSize, int offset);

    /// <summary> Unload mesh data from CPU and GPU </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadMesh")]
    public static partial void UnloadMesh(Mesh mesh);

    /// <summary> Draw a 3d mesh with material and transform </summary>
    [LibraryImport(LIB, EntryPoint = "DrawMesh")]
    public static partial void DrawMesh(Mesh mesh, Material material, Matrix4x4 transform);

    /// <summary> Draw multiple mesh instances with material and different transforms </summary>
    [LibraryImport(LIB, EntryPoint = "DrawMeshInstanced")]
    public static partial void DrawMeshInstanced(Mesh mesh, Material material, IntPtr transforms, int instances);

    /// <summary> Export mesh data to file, returns true on success </summary>
    [LibraryImport(LIB, EntryPoint = "ExportMesh")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportMesh(Mesh mesh, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Compute mesh bounding box limits </summary>
    [LibraryImport(LIB, EntryPoint = "GetMeshBoundingBox")]
    public static partial BoundingBox GetMeshBoundingBox(Mesh mesh);

    /// <summary> Compute mesh tangents </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshTangents")]
    public static partial void GenMeshTangents(IntPtr mesh);

    /// <summary> Generate polygonal mesh </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshPoly")]
    public static partial Mesh GenMeshPoly(int sides, float radius);

    /// <summary> Generate plane mesh (with subdivisions) </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshPlane")]
    public static partial Mesh GenMeshPlane(float width, float length, int resX, int resZ);

    /// <summary> Generate cuboid mesh </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshCube")]
    public static partial Mesh GenMeshCube(float width, float height, float length);

    /// <summary> Generate sphere mesh (standard sphere) </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshSphere")]
    public static partial Mesh GenMeshSphere(float radius, int rings, int slices);

    /// <summary> Generate half-sphere mesh (no bottom cap) </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshHemiSphere")]
    public static partial Mesh GenMeshHemiSphere(float radius, int rings, int slices);

    /// <summary> Generate cylinder mesh </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshCylinder")]
    public static partial Mesh GenMeshCylinder(float radius, float height, int slices);

    /// <summary> Generate cone/pyramid mesh </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshCone")]
    public static partial Mesh GenMeshCone(float radius, float height, int slices);

    /// <summary> Generate torus mesh </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshTorus")]
    public static partial Mesh GenMeshTorus(float radius, float size, int radSeg, int sides);

    /// <summary> Generate trefoil knot mesh </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshKnot")]
    public static partial Mesh GenMeshKnot(float radius, float size, int radSeg, int sides);

    /// <summary> Generate heightmap mesh from image data </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshHeightmap")]
    public static partial Mesh GenMeshHeightmap(Image heightmap, Vector3 size);

    /// <summary> Generate cubes-based map mesh from image data </summary>
    [LibraryImport(LIB, EntryPoint = "GenMeshCubicmap")]
    public static partial Mesh GenMeshCubicmap(Image cubicmap, Vector3 cubeSize);

    /// <summary> Load materials from model file </summary>
    [LibraryImport(LIB, EntryPoint = "LoadMaterials")]
    public static partial IntPtr LoadMaterials([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr materialCount);

    /// <summary> Load default material (Supports: DIFFUSE, SPECULAR, NORMAL maps) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadMaterialDefault")]
    public static partial Material LoadMaterialDefault();

    /// <summary> Check if a material is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsMaterialReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMaterialReady(Material material);

    /// <summary> Unload material from GPU memory (VRAM) </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadMaterial")]
    public static partial void UnloadMaterial(Material material);

    /// <summary> Set texture for a material map type (MATERIAL_MAP_DIFFUSE, MATERIAL_MAP_SPECULAR...) </summary>
    [LibraryImport(LIB, EntryPoint = "SetMaterialTexture")]
    public static partial void SetMaterialTexture(IntPtr material, int mapType, Texture texture);

    /// <summary> Set material for a mesh </summary>
    [LibraryImport(LIB, EntryPoint = "SetModelMeshMaterial")]
    public static partial void SetModelMeshMaterial(IntPtr model, int meshId, int materialId);

    /// <summary> Load model animations from file </summary>
    [LibraryImport(LIB, EntryPoint = "LoadModelAnimations")]
    public static partial IntPtr LoadModelAnimations([MarshalAs(UnmanagedType.LPStr)] string fileName, uint* animCount);

    /// <summary> Update model animation pose </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateModelAnimation")]
    public static partial void UpdateModelAnimation(Model model, ModelAnimation anim, int frame);

    /// <summary> Unload animation data </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadModelAnimation")]
    public static partial void UnloadModelAnimation(ModelAnimation anim);

    /// <summary> Unload animation array data </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadModelAnimations")]
    public static partial void UnloadModelAnimations(IntPtr animations, uint count);

    /// <summary> Check model animation skeleton match </summary>
    [LibraryImport(LIB, EntryPoint = "IsModelAnimationValid")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsModelAnimationValid(Model model, ModelAnimation anim);

    /// <summary> Check collision between two spheres </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionSpheres")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionSpheres(Vector3 center1, float radius1, Vector3 center2, float radius2);

    /// <summary> Check collision between two bounding boxes </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionBoxes")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionBoxes(BoundingBox box1, BoundingBox box2);

    /// <summary> Check collision between box and sphere </summary>
    [LibraryImport(LIB, EntryPoint = "CheckCollisionBoxSphere")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionBoxSphere(BoundingBox box, Vector3 center, float radius);

    /// <summary> Get collision info between ray and sphere </summary>
    [LibraryImport(LIB, EntryPoint = "GetRayCollisionSphere")]
    public static partial RayCollision GetRayCollisionSphere(Ray ray, Vector3 center, float radius);

    /// <summary> Get collision info between ray and box </summary>
    [LibraryImport(LIB, EntryPoint = "GetRayCollisionBox")]
    public static partial RayCollision GetRayCollisionBox(Ray ray, BoundingBox box);

    /// <summary> Get collision info between ray and mesh </summary>
    [LibraryImport(LIB, EntryPoint = "GetRayCollisionMesh")]
    public static partial RayCollision GetRayCollisionMesh(Ray ray, Mesh mesh, Matrix4x4 transform);

    /// <summary> Get collision info between ray and triangle </summary>
    [LibraryImport(LIB, EntryPoint = "GetRayCollisionTriangle")]
    public static partial RayCollision GetRayCollisionTriangle(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3);

    /// <summary> Get collision info between ray and quad </summary>
    [LibraryImport(LIB, EntryPoint = "GetRayCollisionQuad")]
    public static partial RayCollision GetRayCollisionQuad(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4);

    /// <summary> Initialize audio device and context </summary>
    [LibraryImport(LIB, EntryPoint = "InitAudioDevice")]
    public static partial void InitAudioDevice();

    /// <summary> Close the audio device and context </summary>
    [LibraryImport(LIB, EntryPoint = "CloseAudioDevice")]
    public static partial void CloseAudioDevice();

    /// <summary> Check if audio device has been initialized successfully </summary>
    [LibraryImport(LIB, EntryPoint = "IsAudioDeviceReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioDeviceReady();

    /// <summary> Set master volume (listener) </summary>
    [LibraryImport(LIB, EntryPoint = "SetMasterVolume")]
    public static partial void SetMasterVolume(float volume);

    /// <summary> Load wave data from file </summary>
    [LibraryImport(LIB, EntryPoint = "LoadWave")]
    public static partial Wave LoadWave([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load wave from memory buffer, fileType refers to extension: i.e. '.wav' </summary>
    [LibraryImport(LIB, EntryPoint = "LoadWaveFromMemory")]
    public static partial Wave LoadWaveFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, byte[] fileData, int dataSize);

    /// <summary> Checks if wave data is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsWaveReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWaveReady(Wave wave);

    /// <summary> Load sound from file </summary>
    [LibraryImport(LIB, EntryPoint = "LoadSound")]
    public static partial Sound LoadSound([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load sound from wave data </summary>
    [LibraryImport(LIB, EntryPoint = "LoadSoundFromWave")]
    public static partial Sound LoadSoundFromWave(Wave wave);

    /// <summary> Checks if a sound is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsSoundReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsSoundReady(Sound sound);

    /// <summary> Update sound buffer with new data </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateSound")]
    public static partial void UpdateSound(Sound sound, IntPtr data, int sampleCount);

    /// <summary> Unload wave data </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadWave")]
    public static partial void UnloadWave(Wave wave);

    /// <summary> Unload sound </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadSound")]
    public static partial void UnloadSound(Sound sound);

    /// <summary> Export wave data to file, returns true on success </summary>
    [LibraryImport(LIB, EntryPoint = "ExportWave")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportWave(Wave wave, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Export wave sample data to code (.h), returns true on success </summary>
    [LibraryImport(LIB, EntryPoint = "ExportWaveAsCode")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportWaveAsCode(Wave wave, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Play a sound </summary>
    [LibraryImport(LIB, EntryPoint = "PlaySound")]
    public static partial void PlaySound(Sound sound);

    /// <summary> Stop playing a sound </summary>
    [LibraryImport(LIB, EntryPoint = "StopSound")]
    public static partial void StopSound(Sound sound);

    /// <summary> Pause a sound </summary>
    [LibraryImport(LIB, EntryPoint = "PauseSound")]
    public static partial void PauseSound(Sound sound);

    /// <summary> Resume a paused sound </summary>
    [LibraryImport(LIB, EntryPoint = "ResumeSound")]
    public static partial void ResumeSound(Sound sound);

    /// <summary> Check if a sound is currently playing </summary>
    [LibraryImport(LIB, EntryPoint = "IsSoundPlaying")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsSoundPlaying(Sound sound);

    /// <summary> Set volume for a sound (1.0 is max level) </summary>
    [LibraryImport(LIB, EntryPoint = "SetSoundVolume")]
    public static partial void SetSoundVolume(Sound sound, float volume);

    /// <summary> Set pitch for a sound (1.0 is base level) </summary>
    [LibraryImport(LIB, EntryPoint = "SetSoundPitch")]
    public static partial void SetSoundPitch(Sound sound, float pitch);

    /// <summary> Set pan for a sound (0.5 is center) </summary>
    [LibraryImport(LIB, EntryPoint = "SetSoundPan")]
    public static partial void SetSoundPan(Sound sound, float pan);

    /// <summary> Copy a wave to a new wave </summary>
    [LibraryImport(LIB, EntryPoint = "WaveCopy")]
    public static partial Wave WaveCopy(Wave wave);

    /// <summary> Crop a wave to defined samples range </summary>
    [LibraryImport(LIB, EntryPoint = "WaveCrop")]
    public static partial void WaveCrop(IntPtr wave, int initSample, int finalSample);

    /// <summary> Convert wave data to desired format </summary>
    [LibraryImport(LIB, EntryPoint = "WaveFormat")]
    public static partial void WaveFormat(IntPtr wave, int sampleRate, int sampleSize, int channels);

    /// <summary> Load samples data from wave as a 32bit float data array </summary>
    [LibraryImport(LIB, EntryPoint = "LoadWaveSamples")]
    public static partial IntPtr LoadWaveSamples(Wave wave);

    /// <summary> Unload samples data loaded with LoadWaveSamples() </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadWaveSamples")]
    public static partial void UnloadWaveSamples(IntPtr samples);

    /// <summary> Load music stream from file </summary>
    [LibraryImport(LIB, EntryPoint = "LoadMusicStream")]
    public static partial Music LoadMusicStream([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load music stream from data </summary>
    [LibraryImport(LIB, EntryPoint = "LoadMusicStreamFromMemory")]
    public static partial Music LoadMusicStreamFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, byte[] data, int dataSize);

    /// <summary> Checks if a music stream is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsMusicReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMusicReady(Music music);

    /// <summary> Unload music stream </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadMusicStream")]
    public static partial void UnloadMusicStream(Music music);

    /// <summary> Start music playing </summary>
    [LibraryImport(LIB, EntryPoint = "PlayMusicStream")]
    public static partial void PlayMusicStream(Music music);

    /// <summary> Check if music is playing </summary>
    [LibraryImport(LIB, EntryPoint = "IsMusicStreamPlaying")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMusicStreamPlaying(Music music);

    /// <summary> Updates buffers for music streaming </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateMusicStream")]
    public static partial void UpdateMusicStream(Music music);

    /// <summary> Stop music playing </summary>
    [LibraryImport(LIB, EntryPoint = "StopMusicStream")]
    public static partial void StopMusicStream(Music music);

    /// <summary> Pause music playing </summary>
    [LibraryImport(LIB, EntryPoint = "PauseMusicStream")]
    public static partial void PauseMusicStream(Music music);

    /// <summary> Resume playing paused music </summary>
    [LibraryImport(LIB, EntryPoint = "ResumeMusicStream")]
    public static partial void ResumeMusicStream(Music music);

    /// <summary> Seek music to a position (in seconds) </summary>
    [LibraryImport(LIB, EntryPoint = "SeekMusicStream")]
    public static partial void SeekMusicStream(Music music, float position);

    /// <summary> Set volume for music (1.0 is max level) </summary>
    [LibraryImport(LIB, EntryPoint = "SetMusicVolume")]
    public static partial void SetMusicVolume(Music music, float volume);

    /// <summary> Set pitch for a music (1.0 is base level) </summary>
    [LibraryImport(LIB, EntryPoint = "SetMusicPitch")]
    public static partial void SetMusicPitch(Music music, float pitch);

    /// <summary> Set pan for a music (0.5 is center) </summary>
    [LibraryImport(LIB, EntryPoint = "SetMusicPan")]
    public static partial void SetMusicPan(Music music, float pan);

    /// <summary> Get music time length (in seconds) </summary>
    [LibraryImport(LIB, EntryPoint = "GetMusicTimeLength")]
    public static partial float GetMusicTimeLength(Music music);

    /// <summary> Get current music time played (in seconds) </summary>
    [LibraryImport(LIB, EntryPoint = "GetMusicTimePlayed")]
    public static partial float GetMusicTimePlayed(Music music);

    /// <summary> Load audio stream (to stream raw audio pcm data) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadAudioStream")]
    public static partial AudioStream LoadAudioStream(uint sampleRate, uint sampleSize, uint channels);

    /// <summary> Checks if an audio stream is ready </summary>
    [LibraryImport(LIB, EntryPoint = "IsAudioStreamReady")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioStreamReady(AudioStream stream);

    /// <summary> Unload audio stream and free memory </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadAudioStream")]
    public static partial void UnloadAudioStream(AudioStream stream);

    /// <summary> Update audio stream buffers with data </summary>
    [LibraryImport(LIB, EntryPoint = "UpdateAudioStream")]
    public static partial void UpdateAudioStream(AudioStream stream, IntPtr data, int frameCount);

    /// <summary> Check if any audio stream buffers requires refill </summary>
    [LibraryImport(LIB, EntryPoint = "IsAudioStreamProcessed")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioStreamProcessed(AudioStream stream);

    /// <summary> Play audio stream </summary>
    [LibraryImport(LIB, EntryPoint = "PlayAudioStream")]
    public static partial void PlayAudioStream(AudioStream stream);

    /// <summary> Pause audio stream </summary>
    [LibraryImport(LIB, EntryPoint = "PauseAudioStream")]
    public static partial void PauseAudioStream(AudioStream stream);

    /// <summary> Resume audio stream </summary>
    [LibraryImport(LIB, EntryPoint = "ResumeAudioStream")]
    public static partial void ResumeAudioStream(AudioStream stream);

    /// <summary> Check if audio stream is playing </summary>
    [LibraryImport(LIB, EntryPoint = "IsAudioStreamPlaying")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioStreamPlaying(AudioStream stream);

    /// <summary> Stop audio stream </summary>
    [LibraryImport(LIB, EntryPoint = "StopAudioStream")]
    public static partial void StopAudioStream(AudioStream stream);

    /// <summary> Set volume for audio stream (1.0 is max level) </summary>
    [LibraryImport(LIB, EntryPoint = "SetAudioStreamVolume")]
    public static partial void SetAudioStreamVolume(AudioStream stream, float volume);

    /// <summary> Set pitch for audio stream (1.0 is base level) </summary>
    [LibraryImport(LIB, EntryPoint = "SetAudioStreamPitch")]
    public static partial void SetAudioStreamPitch(AudioStream stream, float pitch);

    /// <summary> Set pan for audio stream (0.5 is centered) </summary>
    [LibraryImport(LIB, EntryPoint = "SetAudioStreamPan")]
    public static partial void SetAudioStreamPan(AudioStream stream, float pan);

    /// <summary> Default size for new audio streams </summary>
    [LibraryImport(LIB, EntryPoint = "SetAudioStreamBufferSizeDefault")]
    public static partial void SetAudioStreamBufferSizeDefault(int size);

    /// <summary> Audio thread callback to request new data </summary>
    [LibraryImport(LIB, EntryPoint = "SetAudioStreamCallback")]
    public static partial void SetAudioStreamCallback(AudioStream stream, AudioCallback callback);

    /// <summary> Attach audio stream processor to stream </summary>
    [LibraryImport(LIB, EntryPoint = "AttachAudioStreamProcessor")]
    public static partial void AttachAudioStreamProcessor(AudioStream stream, AudioCallback processor);

    /// <summary> Detach audio stream processor from stream </summary>
    [LibraryImport(LIB, EntryPoint = "DetachAudioStreamProcessor")]
    public static partial void DetachAudioStreamProcessor(AudioStream stream, AudioCallback processor);

    /// <summary> Attach audio stream processor to the entire audio pipeline </summary>
    [LibraryImport(LIB, EntryPoint = "AttachAudioMixedProcessor")]
    public static partial void AttachAudioMixedProcessor(AudioCallback processor);

    /// <summary> Detach audio stream processor from the entire audio pipeline </summary>
    [LibraryImport(LIB, EntryPoint = "DetachAudioMixedProcessor")]
    public static partial void DetachAudioMixedProcessor(AudioCallback processor);

}

