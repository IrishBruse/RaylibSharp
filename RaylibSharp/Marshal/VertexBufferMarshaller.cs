namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

using RaylibSharp.GL;

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
                ElementCount = managed.ElementCount,
                Vertices = verticesPtr,
                Texcoords = texcoordsPtr,
                Colors = colorsPtr,
                Indices = indicesPtr,
                VaoId = managed.VaoId,
                VboId0 = managed.VboId0,
                VboId1 = managed.VboId1,
                VboId2 = managed.VboId2,
                VboId3 = managed.VboId3,
            };
        }
    }

    public static VertexBuffer ConvertToManaged(UnmanagedVertexBuffer unmanaged)
    {
        // https://github.com/raysan5/raylib/blob/d3ea64983212f7451a9cfbf644da8a5c43dbc706/src/rlgl.h#L2535
        return new()
        {
            ElementCount = unmanaged.ElementCount,
            Vertices = new Span<float>(unmanaged.Vertices, unmanaged.ElementCount * 3 * 4).ToArray(),
            Texcoords = new Span<float>(unmanaged.Texcoords, unmanaged.ElementCount * 2 * 4).ToArray(),
            Colors = new Span<byte>(unmanaged.Colors, unmanaged.ElementCount * 4 * 4).ToArray(),
            Indices = new Span<short>(unmanaged.Indices, unmanaged.ElementCount * 6).ToArray(),
            VaoId = unmanaged.VaoId,
            VboId0 = unmanaged.VboId0,
            VboId1 = unmanaged.VboId1,
            VboId2 = unmanaged.VboId2,
            VboId3 = unmanaged.VboId3,
        };
    }
}
