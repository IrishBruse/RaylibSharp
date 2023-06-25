namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Material), MarshalMode.ManagedToUnmanagedIn, typeof(MaterialMarshaller))]
[CustomMarshaller(typeof(Material), MarshalMode.ManagedToUnmanagedOut, typeof(MaterialMarshaller))]
internal static unsafe class MaterialMarshaller
{
    public static Material ConvertToManaged(UnmanagedMaterial unmanaged)
    {
        return new();
    }

    public static UnmanagedMaterial ConvertToUnmanaged(Material managed)
    {
        return new();
    }
}
