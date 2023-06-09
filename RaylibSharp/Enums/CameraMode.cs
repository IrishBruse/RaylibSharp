namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Camera system modes </summary>
public enum CameraMode
{
    /// <summary> Custom camera </summary>
    CameraCustom = 0,
    /// <summary> Free camera </summary>
    CameraFree = 1,
    /// <summary> Orbital camera </summary>
    CameraOrbital = 2,
    /// <summary> First person camera </summary>
    CameraFirstPerson = 3,
    /// <summary> Third person camera </summary>
    CameraThirdPerson = 4,
}
#pragma warning restore CA1711
