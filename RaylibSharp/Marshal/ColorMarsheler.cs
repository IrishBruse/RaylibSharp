namespace Raylib;

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Color), MarshalMode.ManagedToUnmanagedIn, typeof(ColorMarshaller))]
internal static unsafe class ColorMarshaller
{
    internal struct ErrorDataUnmanaged
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }

    public static ErrorDataUnmanaged ConvertToUnmanaged(Color managed)
    {
        return new ErrorDataUnmanaged() { R = managed.R, G = managed.G, B = managed.B, A = managed.A };
    }

    public static Color ConvertToManaged(nint unmanaged)
    {
        throw new NotImplementedException();
    }
}
