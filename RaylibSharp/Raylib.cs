namespace RaylibSharp;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Numerics;
using System.Drawing;

public static unsafe partial class Raylib
{
    /// <summary> Check if KEY_ESCAPE pressed or Close icon pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WindowShouldClose();

    /// <summary> Close window and unload OpenGL context </summary>
    [LibraryImport("raylib")]
    public static partial void CloseWindow();

    /// <summary> Check if window has been initialized successfully </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowReady();

    /// <summary> Check if window is currently fullscreen </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowFullscreen();

    /// <summary> Check if window is currently hidden (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowHidden();

    /// <summary> Check if window is currently minimized (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowMinimized();

    /// <summary> Check if window is currently maximized (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowMaximized();

    /// <summary> Check if window is currently focused (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowFocused();

    /// <summary> Check if window has been resized last frame </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowResized();

    /// <summary> Check if one specific window flag is enabled </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWindowState(WindowFlag flag);

    /// <summary> Set window configuration state using flags (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowState(WindowFlag flags);

    /// <summary> Clear window configuration state flags </summary>
    [LibraryImport("raylib")]
    public static partial void ClearWindowState(WindowFlag flags);

    /// <summary> Toggle window state: fullscreen/windowed (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void ToggleFullscreen();

    /// <summary> Set window state: maximized, if resizable (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void MaximizeWindow();

    /// <summary> Set window state: minimized, if resizable (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void MinimizeWindow();

    /// <summary> Set window state: not minimized/maximized (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void RestoreWindow();

    /// <summary> Set icon for window (single image, RGBA 32bit, only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowIcon(Image image);

    /// <summary> Set icon for window (multiple images, RGBA 32bit, only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowIcons(IntPtr images, int count);

    /// <summary> Set title for window (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowTitle([MarshalAs(UnmanagedType.LPStr)] string title);

    /// <summary> Set window position on screen (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowPosition(int x, int y);

    /// <summary> Set monitor for the current window (fullscreen mode) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowMonitor(int monitor);

    /// <summary> Set window minimum dimensions (for FLAG_WINDOW_RESIZABLE) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowMinSize(int width, int height);

    /// <summary> Set window dimensions </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowSize(int width, int height);

    /// <summary> Set window opacity [0.0f..1.0f] (only PLATFORM_DESKTOP) </summary>
    [LibraryImport("raylib")]
    public static partial void SetWindowOpacity(float opacity);

    /// <summary> Get native window handle </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr GetWindowHandle();

    /// <summary> Get current screen width </summary>
    [LibraryImport("raylib")]
    public static partial int GetScreenWidth();

    /// <summary> Get current screen height </summary>
    [LibraryImport("raylib")]
    public static partial int GetScreenHeight();

    /// <summary> Get current render width (it considers HiDPI) </summary>
    [LibraryImport("raylib")]
    public static partial int GetRenderWidth();

    /// <summary> Get current render height (it considers HiDPI) </summary>
    [LibraryImport("raylib")]
    public static partial int GetRenderHeight();

    /// <summary> Get number of connected monitors </summary>
    [LibraryImport("raylib")]
    public static partial int GetMonitorCount();

    /// <summary> Get current connected monitor </summary>
    [LibraryImport("raylib")]
    public static partial int GetCurrentMonitor();

    /// <summary> Get specified monitor position </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetMonitorPosition(int monitor);

    /// <summary> Get specified monitor width (current video mode used by monitor) </summary>
    [LibraryImport("raylib")]
    public static partial int GetMonitorWidth(int monitor);

    /// <summary> Get specified monitor height (current video mode used by monitor) </summary>
    [LibraryImport("raylib")]
    public static partial int GetMonitorHeight(int monitor);

    /// <summary> Get specified monitor physical width in millimetres </summary>
    [LibraryImport("raylib")]
    public static partial int GetMonitorPhysicalWidth(int monitor);

    /// <summary> Get specified monitor physical height in millimetres </summary>
    [LibraryImport("raylib")]
    public static partial int GetMonitorPhysicalHeight(int monitor);

    /// <summary> Get specified monitor refresh rate </summary>
    [LibraryImport("raylib")]
    public static partial int GetMonitorRefreshRate(int monitor);

    /// <summary> Get window position XY on monitor </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetWindowPosition();

    /// <summary> Get window scale DPI factor </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetWindowScaleDPI();

    /// <summary> Get the human-readable, UTF-8 encoded name of the primary monitor </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string GetMonitorName(int monitor);

    /// <summary> Set clipboard text content </summary>
    [LibraryImport("raylib")]
    public static partial void SetClipboardText([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get clipboard text content </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string GetClipboardText();

    /// <summary> Enable waiting for events on EndDrawing(), no automatic event polling </summary>
    [LibraryImport("raylib")]
    public static partial void EnableEventWaiting();

    /// <summary> Disable waiting for events on EndDrawing(), automatic events polling </summary>
    [LibraryImport("raylib")]
    public static partial void DisableEventWaiting();

    /// <summary> Swap back buffer with front buffer (screen drawing) </summary>
    [LibraryImport("raylib")]
    public static partial void SwapScreenBuffer();

    /// <summary> Register all input events </summary>
    [LibraryImport("raylib")]
    public static partial void PollInputEvents();

    /// <summary> Wait for some time (halt program execution) </summary>
    [LibraryImport("raylib")]
    public static partial void WaitTime(double seconds);

    /// <summary> Shows cursor </summary>
    [LibraryImport("raylib")]
    public static partial void ShowCursor();

    /// <summary> Hides cursor </summary>
    [LibraryImport("raylib")]
    public static partial void HideCursor();

    /// <summary> Check if cursor is not visible </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsCursorHidden();

    /// <summary> Enables cursor (unlock cursor) </summary>
    [LibraryImport("raylib")]
    public static partial void EnableCursor();

    /// <summary> Disables cursor (lock cursor) </summary>
    [LibraryImport("raylib")]
    public static partial void DisableCursor();

    /// <summary> Check if cursor is on the screen </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsCursorOnScreen();

    /// <summary> Set background color (framebuffer clear color) </summary>
    [LibraryImport("raylib")]
    public static partial void ClearBackground([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Setup canvas (framebuffer) to start drawing </summary>
    [LibraryImport("raylib")]
    public static partial void BeginDrawing();

    /// <summary> End canvas drawing and swap buffers (double buffering) </summary>
    [LibraryImport("raylib")]
    public static partial void EndDrawing();

    /// <summary> Begin 2D mode with custom camera (2D) </summary>
    [LibraryImport("raylib")]
    public static partial void BeginMode2D([MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Ends 2D mode with custom camera </summary>
    [LibraryImport("raylib")]
    public static partial void EndMode2D();

    /// <summary> Begin 3D mode with custom camera (3D) </summary>
    [LibraryImport("raylib")]
    public static partial void BeginMode3D([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Ends 3D mode and returns to default 2D orthographic mode </summary>
    [LibraryImport("raylib")]
    public static partial void EndMode3D();

    /// <summary> Begin drawing to render texture </summary>
    [LibraryImport("raylib")]
    public static partial void BeginTextureMode(RenderTexture target);

    /// <summary> Ends drawing to render texture </summary>
    [LibraryImport("raylib")]
    public static partial void EndTextureMode();

    /// <summary> Begin custom shader drawing </summary>
    [LibraryImport("raylib")]
    public static partial void BeginShaderMode(Shader shader);

    /// <summary> End custom shader drawing (use default shader) </summary>
    [LibraryImport("raylib")]
    public static partial void EndShaderMode();

    /// <summary> Begin blending mode (alpha, additive, multiplied, subtract, custom) </summary>
    [LibraryImport("raylib")]
    public static partial void BeginBlendMode(BlendMode mode);

    /// <summary> End blending mode (reset to default: alpha blending) </summary>
    [LibraryImport("raylib")]
    public static partial void EndBlendMode();

    /// <summary> Begin scissor mode (define screen area for following drawing) </summary>
    [LibraryImport("raylib")]
    public static partial void BeginScissorMode(int x, int y, int width, int height);

    /// <summary> End scissor mode </summary>
    [LibraryImport("raylib")]
    public static partial void EndScissorMode();

    /// <summary> Begin stereo rendering (requires VR simulator) </summary>
    [LibraryImport("raylib")]
    public static partial void BeginVrStereoMode(VrStereoConfig config);

    /// <summary> End stereo rendering (requires VR simulator) </summary>
    [LibraryImport("raylib")]
    public static partial void EndVrStereoMode();

    /// <summary> Load VR stereo config for VR simulator device parameters </summary>
    [LibraryImport("raylib")]
    public static partial VrStereoConfig LoadVrStereoConfig(VrDeviceInfo device);

    /// <summary> Unload VR stereo config </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadVrStereoConfig(VrStereoConfig config);

    /// <summary> Load shader from files and bind default locations </summary>
    [LibraryImport("raylib")]
    public static partial Shader LoadShader([MarshalAs(UnmanagedType.LPStr)] string vsFileName, [MarshalAs(UnmanagedType.LPStr)] string fsFileName);

    /// <summary> Load shader from code strings and bind default locations </summary>
    [LibraryImport("raylib")]
    public static partial Shader LoadShaderFromMemory([MarshalAs(UnmanagedType.LPStr)] string vsCode, [MarshalAs(UnmanagedType.LPStr)] string fsCode);

    /// <summary> Check if a shader is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsShaderReady(Shader shader);

    /// <summary> Get shader uniform location </summary>
    [LibraryImport("raylib")]
    public static partial int GetShaderLocation(Shader shader, [MarshalAs(UnmanagedType.LPStr)] string uniformName);

    /// <summary> Get shader attribute location </summary>
    [LibraryImport("raylib")]
    public static partial int GetShaderLocationAttrib(Shader shader, [MarshalAs(UnmanagedType.LPStr)] string attribName);

    /// <summary> Set shader uniform value </summary>
    [LibraryImport("raylib")]
    public static partial void SetShaderValue(Shader shader, int locIndex, IntPtr value, int uniformType);

    /// <summary> Set shader uniform value vector </summary>
    [LibraryImport("raylib")]
    public static partial void SetShaderValueV(Shader shader, int locIndex, IntPtr value, int uniformType, int count);

    /// <summary> Set shader uniform value (matrix 4x4) </summary>
    [LibraryImport("raylib")]
    public static partial void SetShaderValueMatrix(Shader shader, int locIndex, Matrix4x4 mat);

    /// <summary> Set shader uniform value for texture (sampler2d) </summary>
    [LibraryImport("raylib")]
    public static partial void SetShaderValueTexture(Shader shader, int locIndex, Texture texture);

    /// <summary> Unload shader from GPU memory (VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadShader(Shader shader);

    /// <summary> Get a ray trace from mouse position </summary>
    [LibraryImport("raylib")]
    public static partial Ray GetMouseRay(Vector2 mousePosition, [MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Get camera transform matrix (view matrix) </summary>
    [LibraryImport("raylib")]
    public static partial Matrix4x4 GetCameraMatrix([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Get camera 2d transform matrix </summary>
    [LibraryImport("raylib")]
    public static partial Matrix4x4 GetCameraMatrix2D([MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Get the screen space position for a 3d world space position </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetWorldToScreen(Vector3 position, [MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera);

    /// <summary> Get the world space position for a 2d camera screen space position </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetScreenToWorld2D(Vector2 position, [MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Get size position for a 3d world space position </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetWorldToScreenEx(Vector3 position, [MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, int width, int height);

    /// <summary> Get the screen space position for a 2d camera world space position </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetWorldToScreen2D(Vector2 position, [MarshalUsing(typeof(Camera2DMarshaller))] Camera2D camera);

    /// <summary> Set target FPS (maximum) </summary>
    [LibraryImport("raylib")]
    public static partial void SetTargetFPS(int fps);

    /// <summary> Get current FPS </summary>
    [LibraryImport("raylib")]
    public static partial int GetFPS();

    /// <summary> Get time in seconds for last frame drawn (delta time) </summary>
    [LibraryImport("raylib")]
    public static partial float GetFrameTime();

    /// <summary> Get elapsed time in seconds since InitWindow() </summary>
    [LibraryImport("raylib")]
    public static partial double GetTime();

    /// <summary> Takes a screenshot of current screen (filename extension defines format) </summary>
    [LibraryImport("raylib")]
    public static partial void TakeScreenshot([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Setup init configuration flags (view FLAGS) </summary>
    [LibraryImport("raylib")]
    public static partial void SetConfigFlags(WindowFlag flags);

    /// <summary> Set the current threshold (minimum) log level </summary>
    [LibraryImport("raylib")]
    public static partial void SetTraceLogLevel(int logLevel);

    /// <summary> Open URL with default system browser (if available) </summary>
    [LibraryImport("raylib")]
    public static partial void OpenURL([MarshalAs(UnmanagedType.LPStr)] string url);

    /// <summary> Set custom file binary data loader </summary>
    [LibraryImport("raylib")]
    public static partial void SetLoadFileDataCallback(LoadFileDataCallback callback);

    /// <summary> Set custom file binary data saver </summary>
    [LibraryImport("raylib")]
    public static partial void SetSaveFileDataCallback(SaveFileDataCallback callback);

    /// <summary> Set custom file text data loader </summary>
    [LibraryImport("raylib")]
    public static partial void SetLoadFileTextCallback(LoadFileTextCallback callback);

    /// <summary> Set custom file text data saver </summary>
    [LibraryImport("raylib")]
    public static partial void SetSaveFileTextCallback(SaveFileTextCallback callback);

    /// <summary> Load file data as byte array (read) </summary>
    [LibraryImport("raylib")]
    public static partial byte* LoadFileData([MarshalAs(UnmanagedType.LPStr)] string fileName, uint* bytesRead);

    /// <summary> Unload file data allocated by LoadFileData() </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadFileData(byte* data);

    /// <summary> Save data to file from byte array (write), returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SaveFileData([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr data, uint bytesToWrite);

    /// <summary> Export data to code (.h), returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportDataAsCode(byte[] data, uint size, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load text data from file (read), returns a '\0' terminated string </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadFileText([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Unload file text data allocated by LoadFileText() </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadFileText(IntPtr text);

    /// <summary> Get file length in bytes (NOTE: GetFileSize() conflicts with windows.h) </summary>
    [LibraryImport("raylib")]
    public static partial int GetFileLength([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Change working directory, return true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ChangeDirectory([MarshalAs(UnmanagedType.LPStr)] string dir);

    /// <summary> Check if a given path is a file or a directory </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsPathFile([MarshalAs(UnmanagedType.LPStr)] string path);

    /// <summary> Check if a file has been dropped into window </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsFileDropped();

    /// <summary> Load dropped filepaths </summary>
    [LibraryImport("raylib")]
    public static partial FilePathList LoadDroppedFiles();

    /// <summary> Unload dropped filepaths </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadDroppedFiles(FilePathList files);

    /// <summary> Get file modification time (last write time) </summary>
    [LibraryImport("raylib")]
    public static partial long GetFileModTime([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Check if a key has been pressed once </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyPressed(Key key);

    /// <summary> Check if a key is being pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyDown(Key key);

    /// <summary> Check if a key has been released once </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyReleased(Key key);

    /// <summary> Check if a key is NOT being pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsKeyUp(Key key);

    /// <summary> Set a custom key to exit program (default is ESC) </summary>
    [LibraryImport("raylib")]
    public static partial void SetExitKey(Key key);

    /// <summary> Get key pressed (keycode), call it multiple times for keys queued, returns 0 when the queue is empty </summary>
    [LibraryImport("raylib")]
    public static partial int GetKeyPressed();

    /// <summary> Get char pressed (unicode), call it multiple times for chars queued, returns 0 when the queue is empty </summary>
    [LibraryImport("raylib")]
    public static partial int GetCharPressed();

    /// <summary> Check if a gamepad is available </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadAvailable(int gamepad);

    /// <summary> Get gamepad internal name id </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string GetGamepadName(int gamepad);

    /// <summary> Check if a gamepad button has been pressed once </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonPressed(int gamepad, int button);

    /// <summary> Check if a gamepad button is being pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonDown(int gamepad, GamepadButton button);

    /// <summary> Check if a gamepad button has been released once </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonReleased(int gamepad, int button);

    /// <summary> Check if a gamepad button is NOT being pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGamepadButtonUp(int gamepad, int button);

    /// <summary> Get the last gamepad button pressed </summary>
    [LibraryImport("raylib")]
    public static partial GamepadButton GetGamepadButtonPressed();

    /// <summary> Get gamepad axis count for a gamepad </summary>
    [LibraryImport("raylib")]
    public static partial int GetGamepadAxisCount(int gamepad);

    /// <summary> Get axis movement value for a gamepad axis </summary>
    [LibraryImport("raylib")]
    public static partial float GetGamepadAxisMovement(int gamepad, GamepadAxis axis);

    /// <summary> Set internal gamepad mappings (SDL_GameControllerDB) </summary>
    [LibraryImport("raylib")]
    public static partial int SetGamepadMappings([MarshalAs(UnmanagedType.LPStr)] string mappings);

    /// <summary> Check if a mouse button has been pressed once </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonPressed(MouseButton button);

    /// <summary> Check if a mouse button is being pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonDown(MouseButton button);

    /// <summary> Check if a mouse button has been released once </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonReleased(MouseButton button);

    /// <summary> Check if a mouse button is NOT being pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseButtonUp(MouseButton button);

    /// <summary> Get mouse position X </summary>
    [LibraryImport("raylib")]
    public static partial int GetMouseX();

    /// <summary> Get mouse position Y </summary>
    [LibraryImport("raylib")]
    public static partial int GetMouseY();

    /// <summary> Get mouse position XY </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetMousePosition();

    /// <summary> Get mouse delta between frames </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetMouseDelta();

    /// <summary> Set mouse position XY </summary>
    [LibraryImport("raylib")]
    public static partial void SetMousePosition(int x, int y);

    /// <summary> Set mouse offset </summary>
    [LibraryImport("raylib")]
    public static partial void SetMouseOffset(int offsetX, int offsetY);

    /// <summary> Set mouse scaling </summary>
    [LibraryImport("raylib")]
    public static partial void SetMouseScale(float scaleX, float scaleY);

    /// <summary> Get mouse wheel movement for X or Y, whichever is larger </summary>
    [LibraryImport("raylib")]
    public static partial float GetMouseWheelMove();

    /// <summary> Get mouse wheel movement for both X and Y </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetMouseWheelMoveV();

    /// <summary> Set mouse cursor </summary>
    [LibraryImport("raylib")]
    public static partial void SetMouseCursor(int cursor);

    /// <summary> Get touch position X for touch point 0 (relative to screen size) </summary>
    [LibraryImport("raylib")]
    public static partial int GetTouchX();

    /// <summary> Get touch position Y for touch point 0 (relative to screen size) </summary>
    [LibraryImport("raylib")]
    public static partial int GetTouchY();

    /// <summary> Get touch position XY for a touch point index (relative to screen size) </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetTouchPosition(int index);

    /// <summary> Get touch point identifier for given index </summary>
    [LibraryImport("raylib")]
    public static partial int GetTouchPointId(int index);

    /// <summary> Get number of touch points </summary>
    [LibraryImport("raylib")]
    public static partial int GetTouchPointCount();

    /// <summary> Enable a set of gestures using flags </summary>
    [LibraryImport("raylib")]
    public static partial void SetGesturesEnabled(uint flags);

    /// <summary> Check if a gesture have been detected </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsGestureDetected(int gesture);

    /// <summary> Get latest detected gesture </summary>
    [LibraryImport("raylib")]
    public static partial int GetGestureDetected();

    /// <summary> Get gesture hold time in milliseconds </summary>
    [LibraryImport("raylib")]
    public static partial float GetGestureHoldDuration();

    /// <summary> Get gesture drag vector </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetGestureDragVector();

    /// <summary> Get gesture drag angle </summary>
    [LibraryImport("raylib")]
    public static partial float GetGestureDragAngle();

    /// <summary> Get gesture pinch delta </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 GetGesturePinchVector();

    /// <summary> Get gesture pinch angle </summary>
    [LibraryImport("raylib")]
    public static partial float GetGesturePinchAngle();

    /// <summary> Update camera position for selected mode </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateCamera(ref Camera3D camera, CameraMode mode);

    /// <summary> Update camera movement/rotation </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateCameraPro(ref Camera3D camera, Vector3 movement, Vector3 rotation, float zoom);

    /// <summary> Set texture and rectangle to be used on shapes drawing </summary>
    [LibraryImport("raylib")]
    public static partial void SetShapesTexture(Texture texture, RectangleF source);

    /// <summary> Draw a pixel </summary>
    [LibraryImport("raylib")]
    public static partial void DrawPixel(int posX, int posY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a pixel (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawPixelV(Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLine(int startPosX, int startPosY, int endPosX, int endPosY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLineV(Vector2 startPos, Vector2 endPos, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line defining thickness </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLineEx(Vector2 startPos, Vector2 endPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a line using cubic-bezier curves in-out </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLineBezier(Vector2 startPos, Vector2 endPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line using quadratic bezier curves with a control point </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLineBezierQuad(Vector2 startPos, Vector2 endPos, Vector2 controlPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line using cubic bezier curves with 2 control points </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLineBezierCubic(Vector2 startPos, Vector2 endPos, Vector2 startControlPos, Vector2 endControlPos, float thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw lines sequence </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLineStrip(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled circle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCircle(int centerX, int centerY, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a piece of a circle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCircleSector(Vector2 center, float radius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle sector outline </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCircleSectorLines(Vector2 center, float radius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a gradient-filled circle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCircleGradient(int centerX, int centerY, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color1, [MarshalUsing(typeof(ColorMarshaller))] Color color2);

    /// <summary> Draw a color-filled circle (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCircleV(Vector2 center, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle outline </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCircleLines(int centerX, int centerY, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ellipse </summary>
    [LibraryImport("raylib")]
    public static partial void DrawEllipse(int centerX, int centerY, float radiusH, float radiusV, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ellipse outline </summary>
    [LibraryImport("raylib")]
    public static partial void DrawEllipseLines(int centerX, int centerY, float radiusH, float radiusV, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ring </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRing(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw ring outline </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRingLines(Vector2 center, float innerRadius, float outerRadius, float startAngle, float endAngle, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangle(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleV(Vector2 position, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleRec(RectangleF rec, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled rectangle with pro parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectanglePro(RectangleF rec, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a vertical-gradient-filled rectangle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleGradientV(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color1, [MarshalUsing(typeof(ColorMarshaller))] Color color2);

    /// <summary> Draw a horizontal-gradient-filled rectangle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleGradientH(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color1, [MarshalUsing(typeof(ColorMarshaller))] Color color2);

    /// <summary> Draw a gradient-filled rectangle with custom vertex colors </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleGradientEx(RectangleF rec, [MarshalUsing(typeof(ColorMarshaller))] Color col1, [MarshalUsing(typeof(ColorMarshaller))] Color col2, [MarshalUsing(typeof(ColorMarshaller))] Color col3, [MarshalUsing(typeof(ColorMarshaller))] Color col4);

    /// <summary> Draw rectangle outline </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleLines(int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle outline with extended parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleLinesEx(RectangleF rec, float lineThick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle with rounded edges </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleRounded(RectangleF rec, float roundness, int segments, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle with rounded edges outline </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRectangleRoundedLines(RectangleF rec, float roundness, int segments, float lineThick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled triangle (vertex in counter-clockwise order!) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw triangle outline (vertex in counter-clockwise order!) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTriangleLines(Vector2 v1, Vector2 v2, Vector2 v3, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a triangle fan defined by points (first vertex is the center) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTriangleFan(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a triangle strip defined by points </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTriangleStrip(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a regular polygon (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawPoly(Vector2 center, int sides, float radius, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a polygon outline of n sides </summary>
    [LibraryImport("raylib")]
    public static partial void DrawPolyLines(Vector2 center, int sides, float radius, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a polygon outline of n sides with extended parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawPolyLinesEx(Vector2 center, int sides, float radius, float rotation, float lineThick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Check collision between two rectangles </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionRecs(RectangleF rec1, RectangleF rec2);

    /// <summary> Check collision between two circles </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionCircles(Vector2 center1, float radius1, Vector2 center2, float radius2);

    /// <summary> Check collision between circle and rectangle </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionCircleRec(Vector2 center, float radius, RectangleF rec);

    /// <summary> Check if point is inside rectangle </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointRec(Vector2 point, RectangleF rec);

    /// <summary> Check if point is inside circle </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointCircle(Vector2 point, Vector2 center, float radius);

    /// <summary> Check if point is inside a triangle </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointTriangle(Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3);

    /// <summary> Check if point is within a polygon described by array of vertices </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointPoly(Vector2 point, IntPtr points, int pointCount);

    /// <summary> Check the collision between two lines defined by two points each, returns collision point by reference </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionLines(Vector2 startPos1, Vector2 endPos1, Vector2 startPos2, Vector2 endPos2, IntPtr collisionPoint);

    /// <summary> Check if point belongs to line created between two points [p1] and [p2] with defined margin in pixels [threshold] </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionPointLine(Vector2 point, Vector2 p1, Vector2 p2, int threshold);

    /// <summary> Get collision rectangle for two rectangles collision </summary>
    [LibraryImport("raylib")]
    public static partial RectangleF GetCollisionRec(RectangleF rec1, RectangleF rec2);

    /// <summary> Load image from file into CPU memory (RAM) </summary>
    [LibraryImport("raylib")]
    public static partial Image LoadImage([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load image from RAW file data </summary>
    [LibraryImport("raylib")]
    public static partial Image LoadImageRaw([MarshalAs(UnmanagedType.LPStr)] string fileName, int width, int height, int format, int headerSize);

    /// <summary> Load image sequence from file (frames appended to image.data) </summary>
    [LibraryImport("raylib")]
    public static partial Image LoadImageAnim([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr frames);

    /// <summary> Load image from memory buffer, fileType refers to extension: i.e. '.png' </summary>
    [LibraryImport("raylib")]
    public static partial Image LoadImageFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, [MarshalAs(UnmanagedType.LPArray)] byte[] fileData, int dataSize);

    /// <summary> Load image from GPU texture data </summary>
    [LibraryImport("raylib")]
    public static partial Image LoadImageFromTexture(Texture texture);

    /// <summary> Load image from screen buffer and (screenshot) </summary>
    [LibraryImport("raylib")]
    public static partial Image LoadImageFromScreen();

    /// <summary> Check if an image is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsImageReady(Image image);

    /// <summary> Unload image from CPU memory (RAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadImage(Image image);

    /// <summary> Export image data to file, returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportImage(Image image, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Export image as code file defining an array of bytes, returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportImageAsCode(Image image, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Generate image: plain color </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageColor(int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Generate image: linear gradient, direction in degrees [0..360], 0=Vertical gradient </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageGradientLinear(int width, int height, int direction, [MarshalUsing(typeof(ColorMarshaller))] Color start, [MarshalUsing(typeof(ColorMarshaller))] Color end);

    /// <summary> Generate image: radial gradient </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageGradientRadial(int width, int height, float density, [MarshalUsing(typeof(ColorMarshaller))] Color inner, [MarshalUsing(typeof(ColorMarshaller))] Color outer);

    /// <summary> Generate image: square gradient </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageGradientSquare(int width, int height, float density, [MarshalUsing(typeof(ColorMarshaller))] Color inner, [MarshalUsing(typeof(ColorMarshaller))] Color outer);

    /// <summary> Generate image: checked </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageChecked(int width, int height, int checksX, int checksY, [MarshalUsing(typeof(ColorMarshaller))] Color col1, [MarshalUsing(typeof(ColorMarshaller))] Color col2);

    /// <summary> Generate image: white noise </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageWhiteNoise(int width, int height, float factor);

    /// <summary> Generate image: perlin noise </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImagePerlinNoise(int width, int height, int offsetX, int offsetY, float scale);

    /// <summary> Generate image: cellular algorithm, bigger tileSize means bigger cells </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageCellular(int width, int height, int tileSize);

    /// <summary> Generate image: grayscale image from text data </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageText(int width, int height, [MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Create an image duplicate (useful for transformations) </summary>
    [LibraryImport("raylib")]
    public static partial Image ImageCopy(Image image);

    /// <summary> Create an image from another image piece </summary>
    [LibraryImport("raylib")]
    public static partial Image ImageFromImage(Image image, RectangleF rec);

    /// <summary> Create an image from text (default font) </summary>
    [LibraryImport("raylib")]
    public static partial Image ImageText([MarshalAs(UnmanagedType.LPStr)] string text, int fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Create an image from text (custom sprite font) </summary>
    [LibraryImport("raylib")]
    public static partial Image ImageTextEx(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Convert image data to desired format </summary>
    [LibraryImport("raylib")]
    public static partial void ImageFormat(IntPtr image, int newFormat);

    /// <summary> Convert image to POT (power-of-two) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageToPOT(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color fill);

    /// <summary> Crop an image to a defined rectangle </summary>
    [LibraryImport("raylib")]
    public static partial void ImageCrop(IntPtr image, RectangleF crop);

    /// <summary> Crop image depending on alpha value </summary>
    [LibraryImport("raylib")]
    public static partial void ImageAlphaCrop(IntPtr image, float threshold);

    /// <summary> Clear alpha channel to desired color </summary>
    [LibraryImport("raylib")]
    public static partial void ImageAlphaClear(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color color, float threshold);

    /// <summary> Apply alpha mask to image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageAlphaMask(IntPtr image, Image alphaMask);

    /// <summary> Premultiply alpha channel </summary>
    [LibraryImport("raylib")]
    public static partial void ImageAlphaPremultiply(IntPtr image);

    /// <summary> Apply Gaussian blur using a box blur approximation </summary>
    [LibraryImport("raylib")]
    public static partial void ImageBlurGaussian(IntPtr image, int blurSize);

    /// <summary> Resize image (Bicubic scaling algorithm) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageResize(IntPtr image, int newWidth, int newHeight);

    /// <summary> Resize image (Nearest-Neighbor scaling algorithm) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageResizeNN(IntPtr image, int newWidth, int newHeight);

    /// <summary> Resize canvas and fill with color </summary>
    [LibraryImport("raylib")]
    public static partial void ImageResizeCanvas(IntPtr image, int newWidth, int newHeight, int offsetX, int offsetY, [MarshalUsing(typeof(ColorMarshaller))] Color fill);

    /// <summary> Compute all mipmap levels for a provided image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageMipmaps(IntPtr image);

    /// <summary> Dither image data to 16bpp or lower (Floyd-Steinberg dithering) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDither(IntPtr image, int rBpp, int gBpp, int bBpp, int aBpp);

    /// <summary> Flip image vertically </summary>
    [LibraryImport("raylib")]
    public static partial void ImageFlipVertical(IntPtr image);

    /// <summary> Flip image horizontally </summary>
    [LibraryImport("raylib")]
    public static partial void ImageFlipHorizontal(IntPtr image);

    /// <summary> Rotate image by input angle in degrees (-359 to 359)  </summary>
    [LibraryImport("raylib")]
    public static partial void ImageRotate(IntPtr image, int degrees);

    /// <summary> Rotate image clockwise 90deg </summary>
    [LibraryImport("raylib")]
    public static partial void ImageRotateCW(IntPtr image);

    /// <summary> Rotate image counter-clockwise 90deg </summary>
    [LibraryImport("raylib")]
    public static partial void ImageRotateCCW(IntPtr image);

    /// <summary> Modify image color: tint </summary>
    [LibraryImport("raylib")]
    public static partial void ImageColorTint(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Modify image color: invert </summary>
    [LibraryImport("raylib")]
    public static partial void ImageColorInvert(IntPtr image);

    /// <summary> Modify image color: grayscale </summary>
    [LibraryImport("raylib")]
    public static partial void ImageColorGrayscale(IntPtr image);

    /// <summary> Modify image color: contrast (-100 to 100) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageColorContrast(IntPtr image, float contrast);

    /// <summary> Modify image color: brightness (-255 to 255) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageColorBrightness(IntPtr image, int brightness);

    /// <summary> Modify image color: replace color </summary>
    [LibraryImport("raylib")]
    public static partial void ImageColorReplace(IntPtr image, [MarshalUsing(typeof(ColorMarshaller))] Color color, [MarshalUsing(typeof(ColorMarshaller))] Color replace);

    /// <summary> Load color data from image as a Color array (RGBA - 32bit) </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadImageColors(Image image);

    /// <summary> Load colors palette from image as a Color array (RGBA - 32bit) </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadImagePalette(Image image, int maxPaletteSize, IntPtr colorCount);

    /// <summary> Unload color data loaded with LoadImageColors() </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadImageColors(IntPtr colors);

    /// <summary> Unload colors palette loaded with LoadImagePalette() </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadImagePalette(IntPtr colors);

    /// <summary> Get image alpha border rectangle </summary>
    [LibraryImport("raylib")]
    public static partial RectangleF GetImageAlphaBorder(Image image, float threshold);

    /// <summary> Get image pixel color at (x, y) position </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color GetImageColor(Image image, int x, int y);

    /// <summary> Clear image background with given color </summary>
    [LibraryImport("raylib")]
    public static partial void ImageClearBackground(IntPtr dst, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw pixel within an image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawPixel(IntPtr dst, int posX, int posY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw pixel within an image (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawPixelV(IntPtr dst, Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line within an image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawLine(IntPtr dst, int startPosX, int startPosY, int endPosX, int endPosY, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw line within an image (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawLineV(IntPtr dst, Vector2 start, Vector2 end, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a filled circle within an image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawCircle(IntPtr dst, int centerX, int centerY, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a filled circle within an image (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawCircleV(IntPtr dst, Vector2 center, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle outline within an image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawCircleLines(IntPtr dst, int centerX, int centerY, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw circle outline within an image (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawCircleLinesV(IntPtr dst, Vector2 center, int radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle within an image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawRectangle(IntPtr dst, int posX, int posY, int width, int height, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle within an image (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawRectangleV(IntPtr dst, Vector2 position, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle within an image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawRectangleRec(IntPtr dst, RectangleF rec, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw rectangle lines within an image </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawRectangleLines(IntPtr dst, RectangleF rec, int thick, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a source image within a destination image (tint applied to source) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDraw(IntPtr dst, Image src, RectangleF srcRec, RectangleF dstRec, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw text (using default font) within an image (destination) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawText(IntPtr dst, [MarshalAs(UnmanagedType.LPStr)] string text, int posX, int posY, int fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw text (custom sprite font) within an image (destination) </summary>
    [LibraryImport("raylib")]
    public static partial void ImageDrawTextEx(IntPtr dst, Font font, [MarshalAs(UnmanagedType.LPStr)] string text, Vector2 position, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Load texture from file into GPU memory (VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial Texture LoadTexture([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load texture from image data </summary>
    [LibraryImport("raylib")]
    public static partial Texture LoadTextureFromImage(Image image);

    /// <summary> Load cubemap from image, multiple image cubemap layouts supported </summary>
    [LibraryImport("raylib")]
    public static partial Texture LoadTextureCubemap(Image image, int layout);

    /// <summary> Load texture for rendering (framebuffer) </summary>
    [LibraryImport("raylib")]
    public static partial RenderTexture LoadRenderTexture(int width, int height);

    /// <summary> Check if a texture is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsTextureReady(Texture texture);

    /// <summary> Unload texture from GPU memory (VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadTexture(Texture texture);

    /// <summary> Check if a render texture is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsRenderTextureReady(RenderTexture target);

    /// <summary> Unload render texture from GPU memory (VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadRenderTexture(RenderTexture target);

    /// <summary> Update GPU texture with new data </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateTexture(Texture texture, IntPtr pixels);

    /// <summary> Update GPU texture rectangle with new data </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateTextureRec(Texture texture, RectangleF rec, IntPtr pixels);

    /// <summary> Generate GPU mipmaps for a texture </summary>
    [LibraryImport("raylib")]
    public static partial void GenTextureMipmaps(IntPtr texture);

    /// <summary> Set texture scaling filter mode </summary>
    [LibraryImport("raylib")]
    public static partial void SetTextureFilter(Texture texture, int filter);

    /// <summary> Set texture wrapping mode </summary>
    [LibraryImport("raylib")]
    public static partial void SetTextureWrap(Texture texture, int wrap);

    /// <summary> Draw a Texture2D </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTexture(Texture texture, int posX, int posY, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a Texture2D with position defined as Vector2 </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextureV(Texture texture, Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a Texture2D with extended parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextureEx(Texture texture, Vector2 position, float rotation, float scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a part of a texture defined by a rectangle </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextureRec(Texture texture, RectangleF source, Vector2 position, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a part of a texture defined by a rectangle with 'pro' parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTexturePro(Texture texture, RectangleF source, RectangleF dest, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draws a texture (or part of it) that stretches or shrinks nicely </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextureNPatch(Texture texture, NPatchInfo nPatchInfo, RectangleF dest, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Get color with alpha applied, alpha goes from 0.0f to 1.0f </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color Fade([MarshalUsing(typeof(ColorMarshaller))] Color color, float alpha);

    /// <summary> Get hexadecimal value for a Color </summary>
    [LibraryImport("raylib")]
    public static partial int ColorToInt([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Get Color normalized as float [0..1] </summary>
    [LibraryImport("raylib")]
    public static partial Vector4 ColorNormalize([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Get Color from normalized values [0..1] </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorFromNormalized(Vector4 normalized);

    /// <summary> Get HSV values for a Color, hue [0..360], saturation/value [0..1] </summary>
    [LibraryImport("raylib")]
    public static partial Vector3 ColorToHSV([MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Get a Color from HSV values, hue [0..360], saturation/value [0..1] </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorFromHSV(float hue, float saturation, float value);

    /// <summary> Get color multiplied with another color </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorTint([MarshalUsing(typeof(ColorMarshaller))] Color color, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Get color with brightness correction, brightness factor goes from -1.0f to 1.0f </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorBrightness([MarshalUsing(typeof(ColorMarshaller))] Color color, float factor);

    /// <summary> Get color with contrast correction, contrast values between -1.0f and 1.0f </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorContrast([MarshalUsing(typeof(ColorMarshaller))] Color color, float contrast);

    /// <summary> Get color with alpha applied, alpha goes from 0.0f to 1.0f </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorAlpha([MarshalUsing(typeof(ColorMarshaller))] Color color, float alpha);

    /// <summary> Get src alpha-blended into dst color with tint </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color ColorAlphaBlend([MarshalUsing(typeof(ColorMarshaller))] Color dst, [MarshalUsing(typeof(ColorMarshaller))] Color src, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Get Color structure from hexadecimal value </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color GetColor(uint hexValue);

    /// <summary> Get Color from a source pixel pointer of certain format </summary>
    [LibraryImport("raylib")]
    [return: MarshalUsing(typeof(ColorMarshaller))]
    public static partial Color GetPixelColor(IntPtr srcPtr, int format);

    /// <summary> Set color formatted into destination pixel pointer </summary>
    [LibraryImport("raylib")]
    public static partial void SetPixelColor(IntPtr dstPtr, [MarshalUsing(typeof(ColorMarshaller))] Color color, int format);

    /// <summary> Get pixel data size in bytes for certain format </summary>
    [LibraryImport("raylib")]
    public static partial int GetPixelDataSize(int width, int height, int format);

    /// <summary> Get the default Font </summary>
    [LibraryImport("raylib")]
    public static partial Font GetFontDefault();

    /// <summary> Load font from file into GPU memory (VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial Font LoadFont([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load font from file with extended parameters, use NULL for fontChars and 0 for glyphCount to load the default character set </summary>
    [LibraryImport("raylib")]
    public static partial Font LoadFontEx([MarshalAs(UnmanagedType.LPStr)] string fileName, int fontSize, IntPtr fontChars, int glyphCount);

    /// <summary> Load font from Image (XNA style) </summary>
    [LibraryImport("raylib")]
    public static partial Font LoadFontFromImage(Image image, [MarshalUsing(typeof(ColorMarshaller))] Color key, int firstChar);

    /// <summary> Load font from memory buffer, fileType refers to extension: i.e. '.ttf' </summary>
    [LibraryImport("raylib")]
    public static partial Font LoadFontFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, [MarshalAs(UnmanagedType.LPArray)] byte[] fileData, int dataSize, int fontSize, IntPtr fontChars, int glyphCount);

    /// <summary> Check if a font is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsFontReady(Font font);

    /// <summary> Load font data for further use </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadFontData(byte[] fileData, int dataSize, int fontSize, IntPtr fontChars, int glyphCount, int type);

    /// <summary> Generate image font atlas using chars info </summary>
    [LibraryImport("raylib")]
    public static partial Image GenImageFontAtlas(IntPtr chars, IntPtr recs, int glyphCount, int fontSize, int padding, int packMethod);

    /// <summary> Unload font chars info data (RAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadFontData(IntPtr chars, int glyphCount);

    /// <summary> Unload font from GPU memory (VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadFont(Font font);

    /// <summary> Export font as code file, returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportFontAsCode(Font font, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Draw current FPS </summary>
    [LibraryImport("raylib")]
    public static partial void DrawFPS(int posX, int posY);

    /// <summary> Draw text (using default font) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawText([MarshalAs(UnmanagedType.LPStr)] string text, int posX, int posY, int fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw text using font and additional parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextEx(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, Vector2 position, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw text using Font and pro parameters (rotation) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextPro(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, Vector2 position, Vector2 origin, float rotation, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw one character (codepoint) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextCodepoint(Font font, int codepoint, Vector2 position, float fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw multiple character (codepoint) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTextCodepoints(Font font, IntPtr codepoints, int count, Vector2 position, float fontSize, float spacing, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Measure string width for default font </summary>
    [LibraryImport("raylib")]
    public static partial int MeasureText([MarshalAs(UnmanagedType.LPStr)] string text, int fontSize);

    /// <summary> Measure string size for Font </summary>
    [LibraryImport("raylib")]
    public static partial Vector2 MeasureTextEx(Font font, [MarshalAs(UnmanagedType.LPStr)] string text, float fontSize, float spacing);

    /// <summary> Get glyph index position in font for a codepoint (unicode character), fallback to '?' if not found </summary>
    [LibraryImport("raylib")]
    public static partial int GetGlyphIndex(Font font, int codepoint);

    /// <summary> Get glyph font info data for a codepoint (unicode character), fallback to '?' if not found </summary>
    [LibraryImport("raylib")]
    public static partial GlyphInfo GetGlyphInfo(Font font, int codepoint);

    /// <summary> Get glyph rectangle in font atlas for a codepoint (unicode character), fallback to '?' if not found </summary>
    [LibraryImport("raylib")]
    public static partial RectangleF GetGlyphAtlasRec(Font font, int codepoint);

    /// <summary> Load UTF-8 text encoded from codepoints array </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadUTF8(IntPtr codepoints, int length);

    /// <summary> Unload UTF-8 text encoded from codepoints array </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadUTF8(IntPtr text);

    /// <summary> Load all codepoints from a UTF-8 text string, codepoints count returned by parameter </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadCodepoints([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr count);

    /// <summary> Unload codepoints data from memory </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadCodepoints(IntPtr codepoints);

    /// <summary> Get total number of codepoints in a UTF-8 encoded string </summary>
    [LibraryImport("raylib")]
    public static partial int GetCodepointCount([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get next codepoint in a UTF-8 encoded string, 0x3f('?') is returned on failure </summary>
    [LibraryImport("raylib")]
    public static partial int GetCodepoint([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr codepointSize);

    /// <summary> Get next codepoint in a UTF-8 encoded string, 0x3f('?') is returned on failure </summary>
    [LibraryImport("raylib")]
    public static partial int GetCodepointNext([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr codepointSize);

    /// <summary> Get previous codepoint in a UTF-8 encoded string, 0x3f('?') is returned on failure </summary>
    [LibraryImport("raylib")]
    public static partial int GetCodepointPrevious([MarshalAs(UnmanagedType.LPStr)] string text, IntPtr codepointSize);

    /// <summary> Encode one codepoint into UTF-8 byte array (array length returned as parameter) </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string CodepointToUTF8(int codepoint, IntPtr utf8Size);

    /// <summary> Copy one string to another, returns bytes copied </summary>
    [LibraryImport("raylib")]
    public static partial int TextCopy(IntPtr dst, [MarshalAs(UnmanagedType.LPStr)] string src);

    /// <summary> Get text length, checks for '\0' ending </summary>
    [LibraryImport("raylib")]
    public static partial uint TextLength([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get a piece of a text string </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextSubtext([MarshalAs(UnmanagedType.LPStr)] string text, int position, int length);

    /// <summary> Replace text string (WARNING: memory must be freed!) </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr TextReplace(IntPtr text, [MarshalAs(UnmanagedType.LPStr)] string replace, [MarshalAs(UnmanagedType.LPStr)] string by);

    /// <summary> Insert text in a position (WARNING: memory must be freed!) </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr TextInsert([MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.LPStr)] string insert, int position);

    /// <summary> Join text strings with delimiter </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextJoin(IntPtr textList, int count, [MarshalAs(UnmanagedType.LPStr)] string delimiter);

    /// <summary> Split text into multiple strings </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr TextSplit([MarshalAs(UnmanagedType.LPStr)] string text, char delimiter, IntPtr count);

    /// <summary> Append text at specific position and move cursor! </summary>
    [LibraryImport("raylib")]
    public static partial void TextAppend(IntPtr text, [MarshalAs(UnmanagedType.LPStr)] string append, IntPtr position);

    /// <summary> Find first text occurrence within a string </summary>
    [LibraryImport("raylib")]
    public static partial int TextFindIndex([MarshalAs(UnmanagedType.LPStr)] string text, [MarshalAs(UnmanagedType.LPStr)] string find);

    /// <summary> Get upper case version of provided string </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextToUpper([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get lower case version of provided string </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextToLower([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get Pascal case notation version of provided string </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string TextToPascal([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Get integer value from text (negative values not supported) </summary>
    [LibraryImport("raylib")]
    public static partial int TextToInteger([MarshalAs(UnmanagedType.LPStr)] string text);

    /// <summary> Draw a line in 3D world space </summary>
    [LibraryImport("raylib")]
    public static partial void DrawLine3D(Vector3 startPos, Vector3 endPos, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a point in 3D space, actually a small line </summary>
    [LibraryImport("raylib")]
    public static partial void DrawPoint3D(Vector3 position, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a circle in 3D world space </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCircle3D(Vector3 center, float radius, Vector3 rotationAxis, float rotationAngle, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a color-filled triangle (vertex in counter-clockwise order!) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTriangle3D(Vector3 v1, Vector3 v2, Vector3 v3, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a triangle strip defined by points </summary>
    [LibraryImport("raylib")]
    public static partial void DrawTriangleStrip3D(IntPtr points, int pointCount, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCube(Vector3 position, float width, float height, float length, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCubeV(Vector3 position, Vector3 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube wires </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCubeWires(Vector3 position, float width, float height, float length, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw cube wires (Vector version) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCubeWiresV(Vector3 position, Vector3 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw sphere </summary>
    [LibraryImport("raylib")]
    public static partial void DrawSphere(Vector3 centerPos, float radius, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw sphere with extended parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawSphereEx(Vector3 centerPos, float radius, int rings, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw sphere wires </summary>
    [LibraryImport("raylib")]
    public static partial void DrawSphereWires(Vector3 centerPos, float radius, int rings, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder/cone </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCylinder(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder with base at startPos and top at endPos </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCylinderEx(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder/cone wires </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCylinderWires(Vector3 position, float radiusTop, float radiusBottom, float height, int slices, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a cylinder wires with base at startPos and top at endPos </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCylinderWiresEx(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a capsule with the center of its sphere caps at startPos and endPos </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCapsule(Vector3 startPos, Vector3 endPos, float radius, int slices, int rings, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw capsule wireframe with the center of its sphere caps at startPos and endPos </summary>
    [LibraryImport("raylib")]
    public static partial void DrawCapsuleWires(Vector3 startPos, Vector3 endPos, float radius, int slices, int rings, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a plane XZ </summary>
    [LibraryImport("raylib")]
    public static partial void DrawPlane(Vector3 centerPos, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a ray line </summary>
    [LibraryImport("raylib")]
    public static partial void DrawRay(Ray ray, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a grid (centered at (0, 0, 0)) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawGrid(int slices, float spacing);

    /// <summary> Load model from files (meshes and materials) </summary>
    [LibraryImport("raylib")]
    public static partial Model LoadModel([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load model from generated mesh (default material) </summary>
    [LibraryImport("raylib")]
    public static partial Model LoadModelFromMesh(Mesh mesh);

    /// <summary> Check if a model is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsModelReady(Model model);

    /// <summary> Unload model (including meshes) from memory (RAM and/or VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadModel(Model model);

    /// <summary> Compute model bounding box limits (considers all meshes) </summary>
    [LibraryImport("raylib")]
    public static partial BoundingBox GetModelBoundingBox(Model model);

    /// <summary> Draw a model (with texture if set) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawModel(Model model, Vector3 position, float scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a model with extended parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawModelEx(Model model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a model wires (with texture if set) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawModelWires(Model model, Vector3 position, float scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a model wires (with texture if set) with extended parameters </summary>
    [LibraryImport("raylib")]
    public static partial void DrawModelWiresEx(Model model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw bounding box (wires) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawBoundingBox(BoundingBox box, [MarshalUsing(typeof(ColorMarshaller))] Color color);

    /// <summary> Draw a billboard texture </summary>
    [LibraryImport("raylib")]
    public static partial void DrawBillboard([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, Texture texture, Vector3 position, float size, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a billboard texture defined by source </summary>
    [LibraryImport("raylib")]
    public static partial void DrawBillboardRec([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, Texture texture, RectangleF source, Vector3 position, Vector2 size, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Draw a billboard texture defined by source and rotation </summary>
    [LibraryImport("raylib")]
    public static partial void DrawBillboardPro([MarshalUsing(typeof(Camera3DMarshaller))] Camera3D camera, Texture texture, RectangleF source, Vector3 position, Vector3 up, Vector2 size, Vector2 origin, float rotation, [MarshalUsing(typeof(ColorMarshaller))] Color tint);

    /// <summary> Upload mesh vertex data in GPU and provide VAO/VBO ids </summary>
    [LibraryImport("raylib")]
    public static partial void UploadMesh(IntPtr mesh, [MarshalAs(UnmanagedType.I1)] bool dynamic);

    /// <summary> Update mesh vertex data in GPU for a specific buffer index </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateMeshBuffer(Mesh mesh, int index, IntPtr data, int dataSize, int offset);

    /// <summary> Unload mesh data from CPU and GPU </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadMesh(Mesh mesh);

    /// <summary> Draw a 3d mesh with material and transform </summary>
    [LibraryImport("raylib")]
    public static partial void DrawMesh(Mesh mesh, Material material, Matrix4x4 transform);

    /// <summary> Draw multiple mesh instances with material and different transforms </summary>
    [LibraryImport("raylib")]
    public static partial void DrawMeshInstanced(Mesh mesh, Material material, IntPtr transforms, int instances);

    /// <summary> Export mesh data to file, returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportMesh(Mesh mesh, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Compute mesh bounding box limits </summary>
    [LibraryImport("raylib")]
    public static partial BoundingBox GetMeshBoundingBox(Mesh mesh);

    /// <summary> Compute mesh tangents </summary>
    [LibraryImport("raylib")]
    public static partial void GenMeshTangents(IntPtr mesh);

    /// <summary> Generate polygonal mesh </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshPoly(int sides, float radius);

    /// <summary> Generate plane mesh (with subdivisions) </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshPlane(float width, float length, int resX, int resZ);

    /// <summary> Generate cuboid mesh </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshCube(float width, float height, float length);

    /// <summary> Generate sphere mesh (standard sphere) </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshSphere(float radius, int rings, int slices);

    /// <summary> Generate half-sphere mesh (no bottom cap) </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshHemiSphere(float radius, int rings, int slices);

    /// <summary> Generate cylinder mesh </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshCylinder(float radius, float height, int slices);

    /// <summary> Generate cone/pyramid mesh </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshCone(float radius, float height, int slices);

    /// <summary> Generate torus mesh </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshTorus(float radius, float size, int radSeg, int sides);

    /// <summary> Generate trefoil knot mesh </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshKnot(float radius, float size, int radSeg, int sides);

    /// <summary> Generate heightmap mesh from image data </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshHeightmap(Image heightmap, Vector3 size);

    /// <summary> Generate cubes-based map mesh from image data </summary>
    [LibraryImport("raylib")]
    public static partial Mesh GenMeshCubicmap(Image cubicmap, Vector3 cubeSize);

    /// <summary> Load materials from model file </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadMaterials([MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr materialCount);

    /// <summary> Load default material (Supports: DIFFUSE, SPECULAR, NORMAL maps) </summary>
    [LibraryImport("raylib")]
    public static partial Material LoadMaterialDefault();

    /// <summary> Check if a material is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMaterialReady(Material material);

    /// <summary> Unload material from GPU memory (VRAM) </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadMaterial(Material material);

    /// <summary> Set texture for a material map type (MATERIAL_MAP_DIFFUSE, MATERIAL_MAP_SPECULAR...) </summary>
    [LibraryImport("raylib")]
    public static partial void SetMaterialTexture(IntPtr material, int mapType, Texture texture);

    /// <summary> Set material for a mesh </summary>
    [LibraryImport("raylib")]
    public static partial void SetModelMeshMaterial(IntPtr model, int meshId, int materialId);

    /// <summary> Load model animations from file </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadModelAnimations([MarshalAs(UnmanagedType.LPStr)] string fileName, uint* animCount);

    /// <summary> Update model animation pose </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateModelAnimation(Model model, ModelAnimation anim, int frame);

    /// <summary> Unload animation data </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadModelAnimation(ModelAnimation anim);

    /// <summary> Unload animation array data </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadModelAnimations(IntPtr animations, uint count);

    /// <summary> Check model animation skeleton match </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsModelAnimationValid(Model model, ModelAnimation anim);

    /// <summary> Check collision between two spheres </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionSpheres(Vector3 center1, float radius1, Vector3 center2, float radius2);

    /// <summary> Check collision between two bounding boxes </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionBoxes(BoundingBox box1, BoundingBox box2);

    /// <summary> Check collision between box and sphere </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckCollisionBoxSphere(BoundingBox box, Vector3 center, float radius);

    /// <summary> Get collision info between ray and sphere </summary>
    [LibraryImport("raylib")]
    public static partial RayCollision GetRayCollisionSphere(Ray ray, Vector3 center, float radius);

    /// <summary> Get collision info between ray and box </summary>
    [LibraryImport("raylib")]
    public static partial RayCollision GetRayCollisionBox(Ray ray, BoundingBox box);

    /// <summary> Get collision info between ray and mesh </summary>
    [LibraryImport("raylib")]
    public static partial RayCollision GetRayCollisionMesh(Ray ray, Mesh mesh, Matrix4x4 transform);

    /// <summary> Get collision info between ray and triangle </summary>
    [LibraryImport("raylib")]
    public static partial RayCollision GetRayCollisionTriangle(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3);

    /// <summary> Get collision info between ray and quad </summary>
    [LibraryImport("raylib")]
    public static partial RayCollision GetRayCollisionQuad(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4);

    /// <summary> Initialize audio device and context </summary>
    [LibraryImport("raylib")]
    public static partial void InitAudioDevice();

    /// <summary> Close the audio device and context </summary>
    [LibraryImport("raylib")]
    public static partial void CloseAudioDevice();

    /// <summary> Check if audio device has been initialized successfully </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioDeviceReady();

    /// <summary> Set master volume (listener) </summary>
    [LibraryImport("raylib")]
    public static partial void SetMasterVolume(float volume);

    /// <summary> Load wave data from file </summary>
    [LibraryImport("raylib")]
    public static partial Wave LoadWave([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load wave from memory buffer, fileType refers to extension: i.e. '.wav' </summary>
    [LibraryImport("raylib")]
    public static partial Wave LoadWaveFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, byte[] fileData, int dataSize);

    /// <summary> Checks if wave data is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsWaveReady(Wave wave);

    /// <summary> Load sound from file </summary>
    [LibraryImport("raylib")]
    public static partial Sound LoadSound([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load sound from wave data </summary>
    [LibraryImport("raylib")]
    public static partial Sound LoadSoundFromWave(Wave wave);

    /// <summary> Checks if a sound is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsSoundReady(Sound sound);

    /// <summary> Update sound buffer with new data </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateSound(Sound sound, IntPtr data, int sampleCount);

    /// <summary> Unload wave data </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadWave(Wave wave);

    /// <summary> Unload sound </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadSound(Sound sound);

    /// <summary> Export wave data to file, returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportWave(Wave wave, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Export wave sample data to code (.h), returns true on success </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ExportWaveAsCode(Wave wave, [MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Play a sound </summary>
    [LibraryImport("raylib")]
    public static partial void PlaySound(Sound sound);

    /// <summary> Stop playing a sound </summary>
    [LibraryImport("raylib")]
    public static partial void StopSound(Sound sound);

    /// <summary> Pause a sound </summary>
    [LibraryImport("raylib")]
    public static partial void PauseSound(Sound sound);

    /// <summary> Resume a paused sound </summary>
    [LibraryImport("raylib")]
    public static partial void ResumeSound(Sound sound);

    /// <summary> Check if a sound is currently playing </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsSoundPlaying(Sound sound);

    /// <summary> Set volume for a sound (1.0 is max level) </summary>
    [LibraryImport("raylib")]
    public static partial void SetSoundVolume(Sound sound, float volume);

    /// <summary> Set pitch for a sound (1.0 is base level) </summary>
    [LibraryImport("raylib")]
    public static partial void SetSoundPitch(Sound sound, float pitch);

    /// <summary> Set pan for a sound (0.5 is center) </summary>
    [LibraryImport("raylib")]
    public static partial void SetSoundPan(Sound sound, float pan);

    /// <summary> Copy a wave to a new wave </summary>
    [LibraryImport("raylib")]
    public static partial Wave WaveCopy(Wave wave);

    /// <summary> Crop a wave to defined samples range </summary>
    [LibraryImport("raylib")]
    public static partial void WaveCrop(IntPtr wave, int initSample, int finalSample);

    /// <summary> Convert wave data to desired format </summary>
    [LibraryImport("raylib")]
    public static partial void WaveFormat(IntPtr wave, int sampleRate, int sampleSize, int channels);

    /// <summary> Load samples data from wave as a 32bit float data array </summary>
    [LibraryImport("raylib")]
    public static partial IntPtr LoadWaveSamples(Wave wave);

    /// <summary> Unload samples data loaded with LoadWaveSamples() </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadWaveSamples(IntPtr samples);

    /// <summary> Load music stream from file </summary>
    [LibraryImport("raylib")]
    public static partial Music LoadMusicStream([MarshalAs(UnmanagedType.LPStr)] string fileName);

    /// <summary> Load music stream from data </summary>
    [LibraryImport("raylib")]
    public static partial Music LoadMusicStreamFromMemory([MarshalAs(UnmanagedType.LPStr)] string fileType, byte[] data, int dataSize);

    /// <summary> Checks if a music stream is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMusicReady(Music music);

    /// <summary> Unload music stream </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadMusicStream(Music music);

    /// <summary> Start music playing </summary>
    [LibraryImport("raylib")]
    public static partial void PlayMusicStream(Music music);

    /// <summary> Check if music is playing </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMusicStreamPlaying(Music music);

    /// <summary> Updates buffers for music streaming </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateMusicStream(Music music);

    /// <summary> Stop music playing </summary>
    [LibraryImport("raylib")]
    public static partial void StopMusicStream(Music music);

    /// <summary> Pause music playing </summary>
    [LibraryImport("raylib")]
    public static partial void PauseMusicStream(Music music);

    /// <summary> Resume playing paused music </summary>
    [LibraryImport("raylib")]
    public static partial void ResumeMusicStream(Music music);

    /// <summary> Seek music to a position (in seconds) </summary>
    [LibraryImport("raylib")]
    public static partial void SeekMusicStream(Music music, float position);

    /// <summary> Set volume for music (1.0 is max level) </summary>
    [LibraryImport("raylib")]
    public static partial void SetMusicVolume(Music music, float volume);

    /// <summary> Set pitch for a music (1.0 is base level) </summary>
    [LibraryImport("raylib")]
    public static partial void SetMusicPitch(Music music, float pitch);

    /// <summary> Set pan for a music (0.5 is center) </summary>
    [LibraryImport("raylib")]
    public static partial void SetMusicPan(Music music, float pan);

    /// <summary> Get music time length (in seconds) </summary>
    [LibraryImport("raylib")]
    public static partial float GetMusicTimeLength(Music music);

    /// <summary> Get current music time played (in seconds) </summary>
    [LibraryImport("raylib")]
    public static partial float GetMusicTimePlayed(Music music);

    /// <summary> Load audio stream (to stream raw audio pcm data) </summary>
    [LibraryImport("raylib")]
    public static partial AudioStream LoadAudioStream(uint sampleRate, uint sampleSize, uint channels);

    /// <summary> Checks if an audio stream is ready </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioStreamReady(AudioStream stream);

    /// <summary> Unload audio stream and free memory </summary>
    [LibraryImport("raylib")]
    public static partial void UnloadAudioStream(AudioStream stream);

    /// <summary> Update audio stream buffers with data </summary>
    [LibraryImport("raylib")]
    public static partial void UpdateAudioStream(AudioStream stream, IntPtr data, int frameCount);

    /// <summary> Check if any audio stream buffers requires refill </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioStreamProcessed(AudioStream stream);

    /// <summary> Play audio stream </summary>
    [LibraryImport("raylib")]
    public static partial void PlayAudioStream(AudioStream stream);

    /// <summary> Pause audio stream </summary>
    [LibraryImport("raylib")]
    public static partial void PauseAudioStream(AudioStream stream);

    /// <summary> Resume audio stream </summary>
    [LibraryImport("raylib")]
    public static partial void ResumeAudioStream(AudioStream stream);

    /// <summary> Check if audio stream is playing </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsAudioStreamPlaying(AudioStream stream);

    /// <summary> Stop audio stream </summary>
    [LibraryImport("raylib")]
    public static partial void StopAudioStream(AudioStream stream);

    /// <summary> Set volume for audio stream (1.0 is max level) </summary>
    [LibraryImport("raylib")]
    public static partial void SetAudioStreamVolume(AudioStream stream, float volume);

    /// <summary> Set pitch for audio stream (1.0 is base level) </summary>
    [LibraryImport("raylib")]
    public static partial void SetAudioStreamPitch(AudioStream stream, float pitch);

    /// <summary> Set pan for audio stream (0.5 is centered) </summary>
    [LibraryImport("raylib")]
    public static partial void SetAudioStreamPan(AudioStream stream, float pan);

    /// <summary> Default size for new audio streams </summary>
    [LibraryImport("raylib")]
    public static partial void SetAudioStreamBufferSizeDefault(int size);

    /// <summary> Audio thread callback to request new data </summary>
    [LibraryImport("raylib")]
    public static partial void SetAudioStreamCallback(AudioStream stream, AudioCallback callback);

    /// <summary> Attach audio stream processor to stream </summary>
    [LibraryImport("raylib")]
    public static partial void AttachAudioStreamProcessor(AudioStream stream, AudioCallback processor);

    /// <summary> Detach audio stream processor from stream </summary>
    [LibraryImport("raylib")]
    public static partial void DetachAudioStreamProcessor(AudioStream stream, AudioCallback processor);

    /// <summary> Attach audio stream processor to the entire audio pipeline </summary>
    [LibraryImport("raylib")]
    public static partial void AttachAudioMixedProcessor(AudioCallback processor);

    /// <summary> Detach audio stream processor from the entire audio pipeline </summary>
    [LibraryImport("raylib")]
    public static partial void DetachAudioMixedProcessor(AudioCallback processor);

}

