namespace RaylibSharp;

using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Vector2), MarshalMode.ManagedToUnmanagedIn, typeof(Vector2Marshaller))]
internal static unsafe class Vector2Marshaller
{
    public static UnmanagedVector2 ConvertToUnmanaged(Vector2 managed)
    {
        return new UnmanagedVector2()
        {
            X = managed.X,
            Y = managed.Y,
        };
    }
}

internal struct UnmanagedVector2
{
    public float X;
    public float Y;
}
