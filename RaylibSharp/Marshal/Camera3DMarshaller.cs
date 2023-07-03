namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Camera3D), MarshalMode.ManagedToUnmanagedIn, typeof(Camera3DMarshaller))]
[CustomMarshaller(typeof(Camera3D), MarshalMode.ManagedToUnmanagedOut, typeof(Camera3DMarshaller))]
internal static unsafe class Camera3DMarshaller
{
    public static UnmanagedCamera3D ConvertToUnmanaged(Camera3D managed)
    {
        return new()
        {
            Fovy = managed.Fovy,
            Position = managed.Position,
            Projection = (int)managed.Projection,
            Target = managed.Target,
            Up = managed.Up,
        };
    }

    public static Camera3D ConvertToManaged(UnmanagedCamera3D unmanaged)
    {
        return new()
        {
            Fovy = unmanaged.Fovy,
            Position = unmanaged.Position,
            Projection = (CameraProjection)unmanaged.Projection,
            Target = unmanaged.Target,
            Up = unmanaged.Up,
        };
    }
}
