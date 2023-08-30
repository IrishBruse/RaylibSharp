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
                BaseSize = managed.BaseSize,
                GlyphCount = managed.GlyphCount,
                GlyphPadding = managed.GlyphPadding,
                Texture = managed.Texture,
                Glyphs = glyphPtr,
                Recs = recsPtr,
            };
        }
    }

    public static Font ConvertToManaged(UnmanagedFont unmanaged)
    {
        Span<GlyphInfo> glyphs = new(unmanaged.Glyphs, unmanaged.GlyphCount);
        Span<RectangleF> recs = new(unmanaged.Recs, unmanaged.GlyphCount);
        return new()
        {
            BaseSize = unmanaged.BaseSize,
            GlyphCount = unmanaged.GlyphCount,
            GlyphPadding = unmanaged.GlyphPadding,
            Texture = unmanaged.Texture,
            Glyphs = glyphs.ToArray(),
            Recs = recs.ToArray(),
        };
    }
}
