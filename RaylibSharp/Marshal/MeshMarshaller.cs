namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Mesh), MarshalMode.ManagedToUnmanagedIn, typeof(MeshMarshaller))]
[CustomMarshaller(typeof(Mesh), MarshalMode.ManagedToUnmanagedOut, typeof(MeshMarshaller))]
internal static unsafe class MeshMarshaller
{
    public static UnmanagedMesh ConvertToUnmanaged(Mesh managed)
    {
        return new()
        {
            VertexCount = managed.VertexCount,
            TriangleCount = managed.TriangleCount,
        };
    }

    public static Mesh ConvertToManaged(UnmanagedMesh unmanaged)
    {
        return new()
        {
            VertexCount = unmanaged.VertexCount,
            TriangleCount = unmanaged.TriangleCount,
        };
    }
}
