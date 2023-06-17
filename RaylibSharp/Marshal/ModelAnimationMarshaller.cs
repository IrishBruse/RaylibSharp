namespace RaylibSharp;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(ModelAnimation), MarshalMode.Default, typeof(ModelAnimationMarshaller))]
internal static unsafe class ModelAnimationMarshaller
{
    public static UnmanagedModelAnimation ConvertToUnmanaged(ModelAnimation managed)
    {
        throw new NotImplementedException();

    }

    public static ModelAnimation ConvertToManaged(UnmanagedModelAnimation unmanaged)
    {
        throw new NotImplementedException();
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedModelAnimation
{
    /// <summary> Number of bones </summary>
    public int Bonecount;
    /// <summary> Number of animation frames </summary>
    public int Framecount;
    /// <summary> Bones information (skeleton) </summary>
    public BoneInfo* Bones;
    /// <summary> Poses array by frame </summary>
    public Transform** Frameposes;
    /// <summary> Animation name </summary>
    public sbyte* Name;
}
