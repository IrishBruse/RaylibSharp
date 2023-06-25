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
    public int Vertexcount;
    /// <summary> Number of triangles stored (indexed or not) </summary>
    public int Trianglecount;
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
    public float[] Animvertices;
    /// <summary> Animated normals (after bones transformations) </summary>
    public float[] Animnormals;
    /// <summary> Vertex bone ids, max 255 bone ids, up to 4 bones influence by vertex (skinning) </summary>
    public byte[] Boneids;
    /// <summary> Vertex bone weight, up to 4 bones influence by vertex (skinning) </summary>
    public float[] Boneweights;
    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint Vaoid;
    /// <summary> OpenGL Vertex Buffer Objects id (default vertex data) </summary>
    public uint[] Vboid;
}

/// <summary> Mesh, vertex data and vao/vbo </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedMesh
{
    /// <summary> Number of vertices stored in arrays </summary>
    public int Vertexcount;
    /// <summary> Number of triangles stored (indexed or not) </summary>
    public int Trianglecount;
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
    public float* Animvertices;
    /// <summary> Animated normals (after bones transformations) </summary>
    public float* Animnormals;
    /// <summary> Vertex bone ids, max 255 bone ids, up to 4 bones influence by vertex (skinning) </summary>
    public byte* Boneids;
    /// <summary> Vertex bone weight, up to 4 bones influence by vertex (skinning) </summary>
    public float* Boneweights;
    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint Vaoid;
    /// <summary> OpenGL Vertex Buffer Objects id (default vertex data) </summary>
    public uint* Vboid;
}

#pragma warning restore CA1711,IDE0005
