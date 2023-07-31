namespace RaylibSharp.GL;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> rlRenderBatch type </summary>
public unsafe partial struct RenderBatch
{
    /// <summary> Number of vertex buffers (multi-buffering support) </summary>
    public int Buffercount;
    /// <summary> Current buffer tracking in case of multi-buffering </summary>
    public int Currentbuffer;
    /// <summary> Dynamic buffer(s) for vertex data </summary>
    public VertexBuffer[] Vertexbuffer;
    /// <summary> Draw calls array, depends on textureId </summary>
    public DrawCall[] Draws;
    /// <summary> Draw calls counter </summary>
    public int Drawcounter;
    /// <summary> Current depth value for next draw </summary>
    public float Currentdepth;
}

/// <summary> rlRenderBatch type </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedRenderBatch
{
    /// <summary> Number of vertex buffers (multi-buffering support) </summary>
    public int Buffercount;
    /// <summary> Current buffer tracking in case of multi-buffering </summary>
    public int Currentbuffer;
    /// <summary> Dynamic buffer(s) for vertex data </summary>
    public UnmanagedVertexBuffer* Vertexbuffer;
    /// <summary> Draw calls array, depends on textureId </summary>
    public DrawCall* Draws;
    /// <summary> Draw calls counter </summary>
    public int Drawcounter;
    /// <summary> Current depth value for next draw </summary>
    public float Currentdepth;
}

#pragma warning restore CA1711,IDE0005
