namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Camera2D), MarshalMode.ManagedToUnmanagedIn, typeof(Camera2DMarshaller))]
[CustomMarshaller(typeof(Camera2D), MarshalMode.ManagedToUnmanagedOut, typeof(Camera2DMarshaller))]
internal static unsafe class Camera2DMarshaller
{
    public static UnmanagedCamera2D ConvertToUnmanaged(Camera2D managed)
    {
        return new()
        {
            Offset = managed.Offset,
            Rotation = managed.Rotation,
            Target = managed.Target,
            Zoom = managed.Zoom,
        };
    }

    public static Camera2D ConvertToManaged(UnmanagedCamera2D unmanaged)
    {
        return new()
        {
            Offset = unmanaged.Offset,
            Rotation = unmanaged.Rotation,
            Target = unmanaged.Target,
            Zoom = unmanaged.Zoom,
        };
    }
}
