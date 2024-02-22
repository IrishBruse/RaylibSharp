namespace RaylibSharp;

using System;
using System.Numerics;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006, CA1708

/// <summary> Mesh, vertex data and vao/vbo </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct Mesh : IDisposable
{
    /// <summary>  Number of vertices stored in arrays </summary>
    public int VertexCount;

    /// <summary> Number of triangles stored (indexed or not) </summary>
    public int TriangleCount;

    // Vertex attributes data

    /// <summary> Vertex position (XYZ - 3 components per vertex) (shader-location = 0) </summary>
    public float* vertices;

    /// <summary> Vertex texture coordinates (UV - 2 components per vertex) (shader-location = 1) </summary>
    public float* texCoords;

    /// <summary> Vertex second texture coordinates (useful for lightmaps) (shader-location = 5) </summary>
    public float* texCoords2;

    /// <summary> Vertex normals (XYZ - 3 components per vertex) (shader-location = 2) </summary>
    public float* normals;

    /// <summary> Vertex tangents (XYZW - 4 components per vertex) (shader-location = 4) </summary>
    public float* tangents;

    /// <summary> Vertex colors (RGBA - 4 components per vertex) (shader-location = 3) </summary>
    public byte* colors;

    /// <summary> Vertex indices (in case vertex data comes indexed) </summary>
    public ushort* indices;

    // Animation vertex data

    /// <summary> Animated vertex positions (after bones transformations) </summary>
    public float* animVertices;

    /// <summary> Animated normals (after bones transformations) </summary>
    public float* animNormals;

    /// <summary> Vertex bone ids, up to 4 bones influence by vertex (skinning) </summary>
    public byte* boneIds;

    /// <summary> Vertex bone weight, up to 4 bones influence by vertex (skinning) </summary>
    public float* boneWeights;

    // OpenGL identifiers

    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint vaoId;

    /// <summary> OpenGL Vertex Buffer Objects id (default vertex data, uint[]) </summary>
    public uint* vboId;

    /// <inheritdoc cref="vboId" />
    public readonly Span<uint> VboId => new(vboId, Raylib.MAX_MESH_VERTEX_BUFFERS);

    /// <summary> Vertex position (XYZ - 3 components per vertex) (shader-location = 0) </summary>
    public Vector3[] Vertices
    {
        set => Copy(value, ref vertices);
    }

    /// <inheritdoc cref="texCoords" />
    public Vector2[] TexCoords
    {
        set => Copy(value, ref texCoords);
    }

    /// <inheritdoc cref="texCoords2" />
    public Vector2[] TexCoords2
    {
        set => Copy(value, ref texCoords2);
    }

    /// <inheritdoc cref="normals" />
    public Vector3[] Normals
    {
        set => Copy(value, ref normals);
    }

    /// <inheritdoc cref="tangents" />
    public Vector4[] Tangents
    {
        set => Copy(value, ref normals);
    }

    /// <inheritdoc cref="colors" />
    public Color[] Colors
    {
        set => Copy(value, ref colors);
    }

    /// <inheritdoc cref="indices" />
    public ushort[] Indices
    {
        set => Copy(value, ref indices);
    }

    readonly void Copy<Src, Dst>(Src[] src, ref Dst* dst) where Src : unmanaged where Dst : unmanaged
    {
        if (colors != null)
        {
            Raylib.MemFree((nint)colors);
        }

        dst = (Dst*)Raylib.Allocate<Src>(src.Length);

        fixed (Src* srcPtr = src)
        {
            int sizeInBytes = src.Length * sizeof(Src);
            Buffer.MemoryCopy(srcPtr, dst, sizeInBytes, sizeInBytes);
        }
    }

    /// <summary> Unload mesh data from CPU and GPU </summary>
    public readonly void Dispose()
    {
        Raylib.UnloadMesh(this);
        GC.SuppressFinalize(this);
    }
}

#pragma warning restore IDE1006, CA1708
