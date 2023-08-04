namespace RaylibSharp;

using RaylibSharp.GL;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(RenderBatch), MarshalMode.ManagedToUnmanagedIn, typeof(RenderBatchMarshaller))]
[CustomMarshaller(typeof(RenderBatch), MarshalMode.ManagedToUnmanagedOut, typeof(RenderBatchMarshaller))]
internal static unsafe class RenderBatchMarshaller
{
    // TODO: Fix this as it is not working yet
    public static UnmanagedRenderBatch ConvertToUnmanaged(RenderBatch managed)
    {
        throw new NotImplementedException();

        fixed (DrawCall* drawsPtr = managed.Draws)
        fixed (VertexBuffer* vertexBuffersPtr = managed.Vertexbuffer)
        {
            return new()
            {
                Buffercount = managed.Buffercount,
                Currentbuffer = managed.Currentbuffer,
                Currentdepth = managed.Currentdepth,
                Drawcounter = managed.Drawcounter,
                Draws = drawsPtr,
                // Vertexbuffer = vertexBuffersPtr,
            };
        }
    }

    public static RenderBatch ConvertToManaged(UnmanagedRenderBatch unmanaged)
    {
        throw new NotImplementedException();

        Span<DrawCall> draws = new(unmanaged.Draws, unmanaged.Drawcounter);
        Span<VertexBuffer> vertexBuffers = new(unmanaged.Vertexbuffer, unmanaged.Buffercount);
        return new()
        {
            Buffercount = unmanaged.Buffercount,
            Currentbuffer = unmanaged.Currentbuffer,
            Currentdepth = unmanaged.Currentdepth,
            Drawcounter = unmanaged.Drawcounter,
            Draws = draws.ToArray(),
            Vertexbuffer = vertexBuffers.ToArray(),
        };
    }
}
