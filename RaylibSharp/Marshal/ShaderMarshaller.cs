namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Shader), MarshalMode.ManagedToUnmanagedIn, typeof(ShaderMarshaller))]
[CustomMarshaller(typeof(Shader), MarshalMode.ManagedToUnmanagedOut, typeof(ShaderMarshaller))]
internal static unsafe class ShaderMarshaller
{
    public static UnmanagedShader ConvertToUnmanaged(Shader managed)
    {
        return new();
    }

    public static Shader ConvertToManaged(UnmanagedShader unmanaged)
    {
        return new();
    }
}
