namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> MaterialMap </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct MaterialMap
{
    /// <summary> Material map texture </summary>
    public Texture /* Texture2D */ Texture;
    /// <summary> Material map color </summary>
    public Color /* Color */ Color;
    /// <summary> Material map value </summary>
    public float /* float */ Value;
}

#pragma warning restore CA1711,IDE0005
