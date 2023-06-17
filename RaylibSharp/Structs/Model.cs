namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Model, meshes, materials and animation data </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Model
{
    /// <summary> Local transform matrix </summary>
    public Matrix4x4 Transform;
    /// <summary> Number of meshes </summary>
    public int Meshcount;
    /// <summary> Number of materials </summary>
    public int Materialcount;
    /// <summary> Meshes array </summary>
    public Mesh* Meshes;
    /// <summary> Materials array </summary>
    public Material* Materials;
    /// <summary> Mesh material number </summary>
    public int* Meshmaterial;
    /// <summary> Number of bones </summary>
    public int Bonecount;
    /// <summary> Bones information (skeleton) </summary>
    public BoneInfo* Bones;
    /// <summary> Bones base transformation (pose) </summary>
    public Transform* Bindpose;
}

#pragma warning restore CA1711,IDE0005
