namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Material), MarshalMode.ManagedToUnmanagedIn, typeof(MaterialMarshaller))]
[CustomMarshaller(typeof(Material), MarshalMode.ManagedToUnmanagedOut, typeof(MaterialMarshaller))]
static unsafe class MaterialMarshaller
{
    public static UnmanagedMaterial ConvertToUnmanaged(Material managed)
    {
        throw new NotImplementedException();
    }

    public static Material ConvertToManaged(UnmanagedMaterial unmanaged)
    {
        throw new NotImplementedException();
    }
}
