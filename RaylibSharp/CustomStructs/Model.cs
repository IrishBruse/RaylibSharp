namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Numerics;
using System.Runtime.InteropServices;

/// <summary> Model, meshes, materials and animation data </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Model
{
    /// <summary> Local transform matrix </summary>
    public Matrix4x4 Transform;

    /// <summary> Number of meshes </summary>
    public int MeshCount;

    /// <summary> Number of materials </summary>
    public int MaterialCount;

    internal Mesh* meshes;

    internal Material* materials;

    /// <summary> Mesh material number </summary>
    public int* MeshMaterial;

    /// <summary> Number of bones </summary>
    public int BoneCount;

    internal UnmanagedBoneInfo* bones;

    /// <summary> Bones base transformation (pose) </summary>
    public UnmanagedTransform* BindPose;

    // --------------------------------------------

    /// <summary> Materials array </summary>
    public readonly Span<Material> Materials
    {
        get
        {
            return new(materials, MaterialCount);
        }
    }

    /// <summary> Bones information (skeleton) </summary>
    public readonly Span<UnmanagedBoneInfo> Bones
    {
        get
        {
            return new(bones, BoneCount);
        }
    }

    /// <summary> Meshes array </summary>
    public readonly Span<Mesh> Meshes
    {
        get
        {
            return new(meshes, MeshCount);
        }
    }
}

#pragma warning restore CA1711,IDE0005
