namespace RaylibSharp.GL;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> of those state-change happens (this is done in core module) </summary>
public unsafe partial struct DrawCall
{
    /// <summary> Drawing mode: LINES, TRIANGLES, QUADS </summary>
    public int Mode;
    /// <summary> Number of vertex of the draw </summary>
    public int VertexCount;
    /// <summary> Number of vertex required for index alignment (LINES, TRIANGLES) </summary>
    public int VertexAlignment;
    /// <summary> Texture id to be used on the draw -> Use to create new draw call if changes </summary>
    public uint TextureId;
}

#pragma warning restore CA1711,IDE0005
