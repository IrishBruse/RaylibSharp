namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Gesture </summary>
public enum Gesture
{
    /// <summary> No gesture </summary>
    None = 0,
    /// <summary> Tap gesture </summary>
    Tap = 1,
    /// <summary> Double tap gesture </summary>
    Doubletap = 2,
    /// <summary> Hold gesture </summary>
    Hold = 4,
    /// <summary> Drag gesture </summary>
    Drag = 8,
    /// <summary> Swipe right gesture </summary>
    SwipeRight = 16,
    /// <summary> Swipe left gesture </summary>
    SwipeLeft = 32,
    /// <summary> Swipe up gesture </summary>
    SwipeUp = 64,
    /// <summary> Swipe down gesture </summary>
    SwipeDown = 128,
    /// <summary> Pinch in gesture </summary>
    PinchIn = 256,
    /// <summary> Pinch out gesture </summary>
    PinchOut = 512,
}
#pragma warning restore CA1711
