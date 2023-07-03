namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> System/Window config flags </summary>
public enum WindowFlag
{
    /// <summary> Set to try enabling V-Sync on GPU </summary>
    VsyncHint = 64,
    /// <summary> Set to run program in fullscreen </summary>
    FullscreenMode = 2,
    /// <summary> Set to allow resizable window </summary>
    WindowResizable = 4,
    /// <summary> Set to disable window decoration (frame and buttons) </summary>
    WindowUndecorated = 8,
    /// <summary> Set to hide window </summary>
    WindowHidden = 128,
    /// <summary> Set to minimize window (iconify) </summary>
    WindowMinimized = 512,
    /// <summary> Set to maximize window (expanded to monitor) </summary>
    WindowMaximized = 1024,
    /// <summary> Set to window non focused </summary>
    WindowUnfocused = 2048,
    /// <summary> Set to window always on top </summary>
    WindowTopmost = 4096,
    /// <summary> Set to allow windows running while minimized </summary>
    WindowAlwaysRun = 256,
    /// <summary> Set to allow transparent framebuffer </summary>
    WindowTransparent = 16,
    /// <summary> Set to support HighDPI </summary>
    WindowHighdpi = 8192,
    /// <summary> Set to support mouse passthrough, only supported when FLAG_WINDOW_UNDECORATED </summary>
    WindowMousePassthrough = 16384,
    /// <summary> Set to try enabling MSAA 4X </summary>
    Msaa4xHint = 32,
    /// <summary> Set to try enabling interlaced video format (for V3D) </summary>
    InterlacedHint = 65536,
}

#pragma warning restore CA1711
