namespace Raylib;

#pragma warning disable CA1711

/// <summary> System/Window config flags </summary>
public enum ConfigFlags
{
    /// <summary> Set to try enabling V-Sync on GPU </summary>
    FlagVsyncHint = 64,
    /// <summary> Set to run program in fullscreen </summary>
    FlagFullscreenMode = 2,
    /// <summary> Set to allow resizable window </summary>
    FlagWindowResizable = 4,
    /// <summary> Set to disable window decoration (frame and buttons) </summary>
    FlagWindowUndecorated = 8,
    /// <summary> Set to hide window </summary>
    FlagWindowHidden = 128,
    /// <summary> Set to minimize window (iconify) </summary>
    FlagWindowMinimized = 512,
    /// <summary> Set to maximize window (expanded to monitor) </summary>
    FlagWindowMaximized = 1024,
    /// <summary> Set to window non focused </summary>
    FlagWindowUnfocused = 2048,
    /// <summary> Set to window always on top </summary>
    FlagWindowTopmost = 4096,
    /// <summary> Set to allow windows running while minimized </summary>
    FlagWindowAlwaysRun = 256,
    /// <summary> Set to allow transparent framebuffer </summary>
    FlagWindowTransparent = 16,
    /// <summary> Set to support HighDPI </summary>
    FlagWindowHighdpi = 8192,
    /// <summary> Set to support mouse passthrough, only supported when FLAG_WINDOW_UNDECORATED </summary>
    FlagWindowMousePassthrough = 16384,
    /// <summary> Set to try enabling MSAA 4X </summary>
    FlagMsaa4xHint = 32,
    /// <summary> Set to try enabling interlaced video format (for V3D) </summary>
    FlagInterlacedHint = 65536,
}
#pragma warning restore CA1711
