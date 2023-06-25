namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Model, meshes, materials and animation data </summary>
[NativeMarshalling(typeof(ModelMarshaller))]
public unsafe partial struct Model
{
    /// <summary> Local transform matrix </summary>
    public Matrix4x4 Transform;
    /// <summary> Number of meshes </summary>
    public int Meshcount;
    /// <summary> Number of materials </summary>
    public int Materialcount;
    /// <summary> Meshes array </summary>
    public Mesh[] Meshes;
    /// <summary> Materials array </summary>
    public Material[] Materials;
    /// <summary> Mesh material number </summary>
    public int[] Meshmaterial;
    /// <summary> Number of bones </summary>
    public int Bonecount;
    /// <summary> Bones information (skeleton) </summary>
    public BoneInfo[] Bones;
    /// <summary> Bones base transformation (pose) </summary>
    public Transform[] Bindpose;
}

/// <summary> Model, meshes, materials and animation data </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedModel
{
    /// <summary> Local transform matrix </summary>
    public Matrix4x4 Transform;
    /// <summary> Number of meshes </summary>
    public int Meshcount;
    /// <summary> Number of materials </summary>
    public int Materialcount;
    /// <summary> Meshes array </summary>
    public UnmanagedMesh* Meshes;
    /// <summary> Materials array </summary>
    public UnmanagedMaterial* Materials;
    /// <summary> Mesh material number </summary>
    public int* Meshmaterial;
    /// <summary> Number of bones </summary>
    public int Bonecount;
    /// <summary> Bones information (skeleton) </summary>
    public UnmanagedBoneInfo* Bones;
    /// <summary> Bones base transformation (pose) </summary>
    public UnmanagedTransform* Bindpose;
}

#pragma warning restore CA1711,IDE0005
