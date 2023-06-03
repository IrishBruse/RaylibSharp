namespace Raylib;

#pragma warning disable CA1711

/// <summary> Gamepad buttons </summary>
public enum GamepadButton
{
    /// <summary> Unknown button, just for error checking </summary>
    Unknown = 0,
    /// <summary> Gamepad left DPAD up button </summary>
    LeftFaceUp = 1,
    /// <summary> Gamepad left DPAD right button </summary>
    LeftFaceRight = 2,
    /// <summary> Gamepad left DPAD down button </summary>
    LeftFaceDown = 3,
    /// <summary> Gamepad left DPAD left button </summary>
    LeftFaceLeft = 4,
    /// <summary> Gamepad right button up (i.e. PS3: Triangle, Xbox: Y) </summary>
    RightFaceUp = 5,
    /// <summary> Gamepad right button right (i.e. PS3: Square, Xbox: X) </summary>
    RightFaceRight = 6,
    /// <summary> Gamepad right button down (i.e. PS3: Cross, Xbox: A) </summary>
    RightFaceDown = 7,
    /// <summary> Gamepad right button left (i.e. PS3: Circle, Xbox: B) </summary>
    RightFaceLeft = 8,
    /// <summary> Gamepad top/back trigger left (first), it could be a trailing button </summary>
    LeftTrigger1 = 9,
    /// <summary> Gamepad top/back trigger left (second), it could be a trailing button </summary>
    LeftTrigger2 = 10,
    /// <summary> Gamepad top/back trigger right (one), it could be a trailing button </summary>
    RightTrigger1 = 11,
    /// <summary> Gamepad top/back trigger right (second), it could be a trailing button </summary>
    RightTrigger2 = 12,
    /// <summary> Gamepad center buttons, left one (i.e. PS3: Select) </summary>
    MiddleLeft = 13,
    /// <summary> Gamepad center buttons, middle one (i.e. PS3: PS, Xbox: XBOX) </summary>
    Middle = 14,
    /// <summary> Gamepad center buttons, right one (i.e. PS3: Start) </summary>
    MiddleRight = 15,
    /// <summary> Gamepad joystick pressed button left </summary>
    LeftThumb = 16,
    /// <summary> Gamepad joystick pressed button right </summary>
    RightThumb = 17,
}
#pragma warning restore CA1711

