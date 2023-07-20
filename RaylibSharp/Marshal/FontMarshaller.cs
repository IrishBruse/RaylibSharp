namespace RaylibSharp;

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedIn, typeof(FontMarshaller))]
[CustomMarshaller(typeof(Font), MarshalMode.ManagedToUnmanagedOut, typeof(FontMarshaller))]
internal static unsafe class FontMarshaller
{
    public static UnmanagedFont ConvertToUnmanaged(Font managed)
    {
        fixed (RectangleF* recsPtr = managed.Recs)
        fixed (GlyphInfo* glyphPtr = managed.Glyphs)
        {
            return new()
            {
                Basesize = managed.Basesize,
                Glyphcount = managed.Glyphcount,
                Glyphpadding = managed.Glyphpadding,
                Texture = managed.Texture,
                Glyphs = glyphPtr,
                Recs = recsPtr,
            };
        }
    }

    public static Font ConvertToManaged(UnmanagedFont unmanaged)
    {
        Span<GlyphInfo> glyphs = new(unmanaged.Glyphs, unmanaged.Glyphcount);
        Span<RectangleF> recs = new(unmanaged.Recs, unmanaged.Glyphcount);
        return new()
        {
            Basesize = unmanaged.Basesize,
            Glyphcount = unmanaged.Glyphcount,
            Glyphpadding = unmanaged.Glyphpadding,
            Texture = unmanaged.Texture,
            Glyphs = glyphs.ToArray(),
            Recs = recs.ToArray(),
        };
    }
}
