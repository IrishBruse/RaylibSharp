namespace RaylibSharp.GL;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Dynamic vertex buffers (position + texcoords + colors + indices arrays) </summary>
public unsafe partial struct VertexBuffer
{
    /// <summary> Number of elements in the buffer (QUADS) </summary>
    public int ElementCount;
    /// <summary> Vertex position (XYZ - 3 components per vertex) (shader-location = 0) </summary>
    public float[] Vertices;
    /// <summary> Vertex texture coordinates (UV - 2 components per vertex) (shader-location = 1) </summary>
    public float[] Texcoords;
    /// <summary> Vertex colors (RGBA - 4 components per vertex) (shader-location = 3) </summary>
    public byte[] Colors;
    /// <summary> Vertex indices (in case vertex data comes indexed) (6 indices per quad) </summary>
    public short[] Indices;
    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint VaoId;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId0;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId1;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId2;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId3;
}

/// <summary> Dynamic vertex buffers (position + texcoords + colors + indices arrays) </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedVertexBuffer
{
    /// <summary> Number of elements in the buffer (QUADS) </summary>
    public int ElementCount;
    /// <summary> Vertex position (XYZ - 3 components per vertex) (shader-location = 0) </summary>
    public float* Vertices;
    /// <summary> Vertex texture coordinates (UV - 2 components per vertex) (shader-location = 1) </summary>
    public float* Texcoords;
    /// <summary> Vertex colors (RGBA - 4 components per vertex) (shader-location = 3) </summary>
    public byte* Colors;
    /// <summary> Vertex indices (in case vertex data comes indexed) (6 indices per quad) </summary>
    public short* Indices;
    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint VaoId;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId0;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId1;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId2;
    /// <summary> OpenGL Vertex Buffer Objects id (4 types of vertex data) </summary>
    public uint VboId3;
}

#pragma warning restore CA1711,IDE0005
