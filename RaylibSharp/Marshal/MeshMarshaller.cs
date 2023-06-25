namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Mesh), MarshalMode.ManagedToUnmanagedIn, typeof(MeshMarshaller))]
[CustomMarshaller(typeof(Mesh), MarshalMode.ManagedToUnmanagedOut, typeof(MeshMarshaller))]
internal static unsafe class MeshMarshaller
{
    public static Mesh ConvertToManaged(UnmanagedMesh unmanaged)
    {
        return new();
    }

    public static UnmanagedMesh ConvertToUnmanaged(Mesh managed)
    {
        return new();
    }
}
