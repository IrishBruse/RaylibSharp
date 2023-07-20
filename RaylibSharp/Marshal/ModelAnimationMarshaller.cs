namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(ModelAnimation), MarshalMode.ManagedToUnmanagedIn, typeof(ModelAnimationMarshaller))]
[CustomMarshaller(typeof(ModelAnimation), MarshalMode.ManagedToUnmanagedOut, typeof(ModelAnimationMarshaller))]
internal static unsafe class ModelAnimationMarshaller
{
    public static UnmanagedModelAnimation ConvertToUnmanaged(ModelAnimation managed)
    {
        return new();
    }

    public static ModelAnimation ConvertToManaged(UnmanagedModelAnimation unmanaged)
    {
        return new();
    }
}
