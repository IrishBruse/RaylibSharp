namespace Raylib;

#pragma warning disable CA1711

/// <summary> Gamepad axis </summary>
public enum GamepadAxis
{
    /// <summary> Gamepad left stick X axis </summary>
    LeftX = 0,
    /// <summary> Gamepad left stick Y axis </summary>
    LeftY = 1,
    /// <summary> Gamepad right stick X axis </summary>
    RightX = 2,
    /// <summary> Gamepad right stick Y axis </summary>
    RightY = 3,
    /// <summary> Gamepad back trigger left, pressure level: [1..-1] </summary>
    LeftTrigger = 4,
    /// <summary> Gamepad back trigger right, pressure level: [1..-1] </summary>
    RightTrigger = 5,
}
#pragma warning restore CA1711
