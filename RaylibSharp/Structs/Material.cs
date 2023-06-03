namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Material, includes shader and maps </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Material
{
    /// <summary> Material shader </summary>
    public Shader /*Shader*/ Shader;
    /// <summary> Material maps array (MAX_MATERIAL_MAPS) </summary>
    public IntPtr /*MaterialMap **/ Maps;
    /// <summary> Material generic parameters (if required) </summary>
    public float[4] /*float[4]*/ Params;
}
#pragma warning restore CA1711,IDE0005

