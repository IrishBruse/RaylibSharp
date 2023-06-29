namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedIn, typeof(FontMarshaller))]
[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedOut, typeof(FontMarshaller))]
internal static unsafe class FontMarshaller
{
    public static UnmanagedFont ConvertToUnmanaged(Font managed)
    {
        return new()
        {
            Basesize = managed.Basesize,
            Glyphcount = managed.Glyphcount,
            Glyphpadding = managed.Glyphpadding,
        };
    }

    public static Font ConvertToManaged(UnmanagedFont unmanaged)
    {
        return new()
        {
            Basesize = unmanaged.Basesize,
            Glyphcount = unmanaged.Glyphcount,
            Glyphpadding = unmanaged.Glyphpadding,
        };
    }
}
