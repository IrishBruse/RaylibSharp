namespace RaylibSharp;

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Color), MarshalMode.ManagedToUnmanagedIn, typeof(ColorMarshaller))]
[CustomMarshaller(typeof(Color), MarshalMode.ManagedToUnmanagedOut, typeof(ColorMarshaller))]
internal static unsafe class ColorMarshaller
{
    internal struct UnmanagedColor
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }

    public static UnmanagedColor ConvertToUnmanaged(Color managed)
    {
        return new UnmanagedColor()
        {
            R = managed.R,
            G = managed.G,
            B = managed.B,
            A = managed.A
        };
    }

    public static Color ConvertToManaged(UnmanagedColor unmanaged)
    {
        return Color.FromArgb(unmanaged.R, unmanaged.G, unmanaged.B, unmanaged.A);
    }
}
