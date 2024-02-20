namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Camera, defines position/orientation in 3d space </summary>
public unsafe partial struct Camera3D
{
    /// <summary> Camera position </summary>
    public Vector3 Position;
    /// <summary> Camera target it looks-at </summary>
    public Vector3 Target;
    /// <summary> Camera up vector (rotation over its axis) </summary>
    public Vector3 Up;
    /// <summary> Camera field-of-view aperture in Y (degrees) in perspective, used as near plane width in orthographic </summary>
    public float Fovy;
    /// <summary> Camera projection: CAMERA_PERSPECTIVE or CAMERA_ORTHOGRAPHIC </summary>
    public CameraProjection Projection;
}

#pragma warning restore CA1711,IDE0005
