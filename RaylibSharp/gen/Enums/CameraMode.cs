namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Camera system modes </summary>
public enum CameraMode
{
    /// <summary> Camera custom, controlled by user (UpdateCamera() does nothing) </summary>
    Custom = 0,
    /// <summary> Camera free mode </summary>
    Free = 1,
    /// <summary> Camera orbital, around target, zoom supported </summary>
    Orbital = 2,
    /// <summary> Camera first person </summary>
    FirstPerson = 3,
    /// <summary> Camera third person </summary>
    ThirdPerson = 4,
}

#pragma warning restore CA1711
