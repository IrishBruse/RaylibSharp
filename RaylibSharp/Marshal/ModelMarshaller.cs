namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Model), MarshalMode.ManagedToUnmanagedIn, typeof(ModelMarshaller))]
[CustomMarshaller(typeof(Model), MarshalMode.ManagedToUnmanagedOut, typeof(ModelMarshaller))]
static unsafe class ModelMarshaller
{
    public static UnmanagedModel ConvertToUnmanaged(Model managed)
    {
        return new();
    }

    public static Model ConvertToManaged(UnmanagedModel unmanaged)
    {
        return new();
    }
}
