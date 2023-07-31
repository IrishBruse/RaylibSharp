namespace RaylibSharp;

using RaylibSharp.GL;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(VertexBuffer), MarshalMode.ManagedToUnmanagedIn, typeof(VertexBufferMarshaller))]
[CustomMarshaller(typeof(VertexBuffer), MarshalMode.ManagedToUnmanagedOut, typeof(VertexBufferMarshaller))]
// TODO: might not be fully accurate yet improve implementation
internal static unsafe class VertexBufferMarshaller
{
    public static UnmanagedVertexBuffer ConvertToUnmanaged(VertexBuffer managed)
    {
        fixed (float* verticesPtr = managed.Vertices)
        fixed (float* texcoordsPtr = managed.Texcoords)
        fixed (byte* colorsPtr = managed.Colors)
        fixed (short* indicesPtr = managed.Indices)
        {
            return new()
            {
                Elementcount = managed.Elementcount,
                Vertices = verticesPtr,
                Texcoords = texcoordsPtr,
                Colors = colorsPtr,
                Indices = indicesPtr,
                Vaoid = managed.Vaoid,
                Vboid0 = managed.Vboid0,
                Vboid1 = managed.Vboid1,
                Vboid2 = managed.Vboid2,
                Vboid3 = managed.Vboid3,
            };
        }
    }

    public static VertexBuffer ConvertToManaged(UnmanagedVertexBuffer unmanaged)
    {
        // https://github.com/raysan5/raylib/blob/d3ea64983212f7451a9cfbf644da8a5c43dbc706/src/rlgl.h#L2535
        return new()
        {
            Elementcount = unmanaged.Elementcount,
            Vertices = new Span<float>(unmanaged.Vertices, unmanaged.Elementcount * 3 * 4).ToArray(),
            Texcoords = new Span<float>(unmanaged.Texcoords, unmanaged.Elementcount * 2 * 4).ToArray(),
            Colors = new Span<byte>(unmanaged.Colors, unmanaged.Elementcount * 4 * 4).ToArray(),
            Indices = new Span<short>(unmanaged.Indices, unmanaged.Elementcount * 6).ToArray(),
            Vaoid = unmanaged.Vaoid,
            Vboid0 = unmanaged.Vboid0,
            Vboid1 = unmanaged.Vboid1,
            Vboid2 = unmanaged.Vboid2,
            Vboid3 = unmanaged.Vboid3,
        };
    }
}
