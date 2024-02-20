namespace RaylibSharp;

using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Matrix4x4), MarshalMode.ManagedToUnmanagedIn, typeof(Matrix4x4Marshaller))]
[CustomMarshaller(typeof(Matrix4x4), MarshalMode.ManagedToUnmanagedOut, typeof(Matrix4x4Marshaller))]
static unsafe class Matrix4x4Marshaller
{
    static readonly Matrix4x4 HorizontalFlip = Matrix4x4.CreateScale(new Vector3(-1, -1, 1));

    public static Matrix4x4 ConvertToUnmanaged(Matrix4x4 managed)
    {
        return Matrix4x4.Transpose(managed * HorizontalFlip);
    }

    public static Matrix4x4 ConvertToManaged(Matrix4x4 unmanaged)
    {
        return Matrix4x4.Transpose(unmanaged * HorizontalFlip);
    }
}
