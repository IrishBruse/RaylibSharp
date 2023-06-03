namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Model, meshes, materials and animation data </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Model
{
    /// <summary> Local transform matrix </summary>
    public Matrix /*Matrix*/ Transform;
    /// <summary> Number of meshes </summary>
    public int /*int*/ Meshcount;
    /// <summary> Number of materials </summary>
    public int /*int*/ Materialcount;
    /// <summary> Meshes array </summary>
    public IntPtr /*Mesh **/ Meshes;
    /// <summary> Materials array </summary>
    public IntPtr /*Material **/ Materials;
    /// <summary> Mesh material number </summary>
    public IntPtr /*int **/ Meshmaterial;
    /// <summary> Number of bones </summary>
    public int /*int*/ Bonecount;
    /// <summary> Bones information (skeleton) </summary>
    public IntPtr /*BoneInfo **/ Bones;
    /// <summary> Bones base transformation (pose) </summary>
    public IntPtr /*Transform **/ Bindpose;
}
#pragma warning restore CA1711,IDE0005

