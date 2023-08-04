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
    public int BufferCount;
    /// <summary> Current buffer tracking in case of multi-buffering </summary>
    public int CurrentBuffer;
    /// <summary> Dynamic buffer(s) for vertex data </summary>
    public VertexBuffer[] VertexBuffer;
    /// <summary> Draw calls array, depends on textureId </summary>
    public DrawCall[] Draws;
    /// <summary> Draw calls counter </summary>
    public int DrawCounter;
    /// <summary> Current depth value for next draw </summary>
    public float CurrentDepth;
}

/// <summary> rlRenderBatch type </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedRenderBatch
{
    /// <summary> Number of vertex buffers (multi-buffering support) </summary>
    public int BufferCount;
    /// <summary> Current buffer tracking in case of multi-buffering </summary>
    public int CurrentBuffer;
    /// <summary> Dynamic buffer(s) for vertex data </summary>
    public UnmanagedVertexBuffer* VertexBuffer;
    /// <summary> Draw calls array, depends on textureId </summary>
    public DrawCall* Draws;
    /// <summary> Draw calls counter </summary>
    public int DrawCounter;
    /// <summary> Current depth value for next draw </summary>
    public float CurrentDepth;
}

#pragma warning restore CA1711,IDE0005
