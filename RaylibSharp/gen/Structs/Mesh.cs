namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Mesh, vertex data and vao/vbo </summary>
[NativeMarshalling(typeof(MeshMarshaller))]
public unsafe partial struct Mesh
{
    /// <summary> Number of vertices stored in arrays </summary>
    public int VertexCount;
    /// <summary> Number of triangles stored (indexed or not) </summary>
    public int TriangleCount;
    /// <summary> Vertex position (XYZ - 3 components per vertex) (shader-location = 0) </summary>
    public float[] Vertices;
    /// <summary> Vertex texture coordinates (UV - 2 components per vertex) (shader-location = 1) </summary>
    public float[] Texcoords;
    /// <summary> Vertex texture second coordinates (UV - 2 components per vertex) (shader-location = 5) </summary>
    public float[] Texcoords2;
    /// <summary> Vertex normals (XYZ - 3 components per vertex) (shader-location = 2) </summary>
    public float[] Normals;
    /// <summary> Vertex tangents (XYZW - 4 components per vertex) (shader-location = 4) </summary>
    public float[] Tangents;
    /// <summary> Vertex colors (RGBA - 4 components per vertex) (shader-location = 3) </summary>
    public byte[] Colors;
    /// <summary> Vertex indices (in case vertex data comes indexed) </summary>
    public short[] Indices;
    /// <summary> Animated vertex positions (after bones transformations) </summary>
    public float[] AnimVertices;
    /// <summary> Animated normals (after bones transformations) </summary>
    public float[] AnimNormals;
    /// <summary> Vertex bone ids, max 255 bone ids, up to 4 bones influence by vertex (skinning) </summary>
    public byte[] BoneIds;
    /// <summary> Vertex bone weight, up to 4 bones influence by vertex (skinning) </summary>
    public float[] BoneWeights;
    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint VaoId;
    /// <summary> OpenGL Vertex Buffer Objects id (default vertex data) </summary>
    public uint[] VboId;
}

/// <summary> Mesh, vertex data and vao/vbo </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedMesh
{
    /// <summary> Number of vertices stored in arrays </summary>
    public int VertexCount;
    /// <summary> Number of triangles stored (indexed or not) </summary>
    public int TriangleCount;
    /// <summary> Vertex position (XYZ - 3 components per vertex) (shader-location = 0) </summary>
    public float* Vertices;
    /// <summary> Vertex texture coordinates (UV - 2 components per vertex) (shader-location = 1) </summary>
    public float* Texcoords;
    /// <summary> Vertex texture second coordinates (UV - 2 components per vertex) (shader-location = 5) </summary>
    public float* Texcoords2;
    /// <summary> Vertex normals (XYZ - 3 components per vertex) (shader-location = 2) </summary>
    public float* Normals;
    /// <summary> Vertex tangents (XYZW - 4 components per vertex) (shader-location = 4) </summary>
    public float* Tangents;
    /// <summary> Vertex colors (RGBA - 4 components per vertex) (shader-location = 3) </summary>
    public byte* Colors;
    /// <summary> Vertex indices (in case vertex data comes indexed) </summary>
    public short* Indices;
    /// <summary> Animated vertex positions (after bones transformations) </summary>
    public float* AnimVertices;
    /// <summary> Animated normals (after bones transformations) </summary>
    public float* AnimNormals;
    /// <summary> Vertex bone ids, max 255 bone ids, up to 4 bones influence by vertex (skinning) </summary>
    public byte* BoneIds;
    /// <summary> Vertex bone weight, up to 4 bones influence by vertex (skinning) </summary>
    public float* BoneWeights;
    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint VaoId;
    /// <summary> OpenGL Vertex Buffer Objects id (default vertex data) </summary>
    public uint* VboId;
}

#pragma warning restore CA1711,IDE0005
