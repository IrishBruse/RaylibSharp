namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Mesh, vertex data and vao/vbo </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Mesh
{
    /// <summary> Number of vertices stored in arrays </summary>
    public int /*int*/ Vertexcount;
    /// <summary> Number of triangles stored (indexed or not) </summary>
    public int /*int*/ Trianglecount;
    /// <summary> Vertex position (XYZ - 3 components per vertex) (shader-location = 0) </summary>
    public IntPtr /*float **/ Vertices;
    /// <summary> Vertex texture coordinates (UV - 2 components per vertex) (shader-location = 1) </summary>
    public IntPtr /*float **/ Texcoords;
    /// <summary> Vertex texture second coordinates (UV - 2 components per vertex) (shader-location = 5) </summary>
    public IntPtr /*float **/ Texcoords2;
    /// <summary> Vertex normals (XYZ - 3 components per vertex) (shader-location = 2) </summary>
    public IntPtr /*float **/ Normals;
    /// <summary> Vertex tangents (XYZW - 4 components per vertex) (shader-location = 4) </summary>
    public IntPtr /*float **/ Tangents;
    /// <summary> Vertex colors (RGBA - 4 components per vertex) (shader-location = 3) </summary>
    public uchar * /*unsigned char **/ Colors;
    /// <summary> Vertex indices (in case vertex data comes indexed) </summary>
    public ushort * /*unsigned short **/ Indices;
    /// <summary> Animated vertex positions (after bones transformations) </summary>
    public IntPtr /*float **/ Animvertices;
    /// <summary> Animated normals (after bones transformations) </summary>
    public IntPtr /*float **/ Animnormals;
    /// <summary> Vertex bone ids, max 255 bone ids, up to 4 bones influence by vertex (skinning) </summary>
    public uchar * /*unsigned char **/ Boneids;
    /// <summary> Vertex bone weight, up to 4 bones influence by vertex (skinning) </summary>
    public IntPtr /*float **/ Boneweights;
    /// <summary> OpenGL Vertex Array Object id </summary>
    public uint /*unsigned int*/ Vaoid;
    /// <summary> OpenGL Vertex Buffer Objects id (default vertex data) </summary>
    public uint * /*unsigned int **/ Vboid;
}
#pragma warning restore CA1711,IDE0005

