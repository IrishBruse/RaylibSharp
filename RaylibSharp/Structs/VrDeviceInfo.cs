namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> VrDeviceInfo, Head-Mounted-Display device parameters </summary>
public unsafe partial struct VrDeviceInfo
{
    /// <summary> Horizontal resolution in pixels </summary>
    public int Hresolution;
    /// <summary> Vertical resolution in pixels </summary>
    public int Vresolution;
    /// <summary> Horizontal size in meters </summary>
    public float Hscreensize;
    /// <summary> Vertical size in meters </summary>
    public float Vscreensize;
    /// <summary> Screen center in meters </summary>
    public float Vscreencenter;
    /// <summary> Distance between eye and display in meters </summary>
    public float Eyetoscreendistance;
    /// <summary> Lens separation distance in meters </summary>
    public float Lensseparationdistance;
    /// <summary> IPD (distance between pupils) in meters </summary>
    public float Interpupillarydistance;
    /// <summary> Lens distortion constant parameters </summary>
    public Vector4 Lensdistortionvalues;
    /// <summary> Chromatic aberration correction parameters </summary>
    public Vector4 Chromaabcorrection;
}

/// <summary> VrDeviceInfo, Head-Mounted-Display device parameters </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedVrDeviceInfo
{
    /// <summary> Horizontal resolution in pixels </summary>
    public int Hresolution;
    /// <summary> Vertical resolution in pixels </summary>
    public int Vresolution;
    /// <summary> Horizontal size in meters </summary>
    public float Hscreensize;
    /// <summary> Vertical size in meters </summary>
    public float Vscreensize;
    /// <summary> Screen center in meters </summary>
    public float Vscreencenter;
    /// <summary> Distance between eye and display in meters </summary>
    public float Eyetoscreendistance;
    /// <summary> Lens separation distance in meters </summary>
    public float Lensseparationdistance;
    /// <summary> IPD (distance between pupils) in meters </summary>
    public float Interpupillarydistance;
    /// <summary> Lens distortion constant parameters </summary>
    public fixed float Lensdistortionvalues[4];
    /// <summary> Chromatic aberration correction parameters </summary>
    public fixed float Chromaabcorrection[4];
}

#pragma warning restore CA1711,IDE0005
