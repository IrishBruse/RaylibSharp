namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

/// <summary> VrDeviceInfo, Head-Mounted-Display device parameters </summary>
public unsafe partial struct VrDeviceInfo
{
    /// <summary> Horizontal resolution in pixels </summary>
    public int HResolution;
    /// <summary> Vertical resolution in pixels </summary>
    public int VResolution;
    /// <summary> Horizontal size in meters </summary>
    public float HScreenSize;
    /// <summary> Vertical size in meters </summary>
    public float VScreenSize;
    /// <summary> Screen center in meters </summary>
    public float VScreenCenter;
    /// <summary> Distance between eye and display in meters </summary>
    public float EyeToScreenDistance;
    /// <summary> Lens separation distance in meters </summary>
    public float LensSeparationDistance;
    /// <summary> IPD (distance between pupils) in meters </summary>
    public float InterpupillaryDistance;
    /// <summary> Lens distortion constant parameters </summary>
    public Vector4 LensDistortionValues;
    /// <summary> Chromatic aberration correction parameters </summary>
    public Vector4 ChromaAbCorrection;
}

/// <summary> VrDeviceInfo, Head-Mounted-Display device parameters </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedVrDeviceInfo
{
    /// <summary> Horizontal resolution in pixels </summary>
    public int HResolution;
    /// <summary> Vertical resolution in pixels </summary>
    public int VResolution;
    /// <summary> Horizontal size in meters </summary>
    public float HScreenSize;
    /// <summary> Vertical size in meters </summary>
    public float VScreenSize;
    /// <summary> Screen center in meters </summary>
    public float VScreenCenter;
    /// <summary> Distance between eye and display in meters </summary>
    public float EyeToScreenDistance;
    /// <summary> Lens separation distance in meters </summary>
    public float LensSeparationDistance;
    /// <summary> IPD (distance between pupils) in meters </summary>
    public float InterpupillaryDistance;
    /// <summary> Lens distortion constant parameters </summary>
    public fixed float LensDistortionValues[4];
    /// <summary> Chromatic aberration correction parameters </summary>
    public fixed float ChromaAbCorrection[4];
}

#pragma warning restore CA1711,IDE0005
