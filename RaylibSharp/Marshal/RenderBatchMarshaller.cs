namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

using RaylibSharp.GL;

[CustomMarshaller(typeof(RenderBatch), MarshalMode.ManagedToUnmanagedIn, typeof(RenderBatchMarshaller))]
[CustomMarshaller(typeof(RenderBatch), MarshalMode.ManagedToUnmanagedOut, typeof(RenderBatchMarshaller))]
static unsafe class RenderBatchMarshaller
{
    // TODO: Fix this as it is not working yet
    public static UnmanagedRenderBatch ConvertToUnmanaged(RenderBatch managed)
    {
        // UnmanagedVertexBuffer vertexBuffers = VertexBufferMarshaller.ConvertToUnmanaged(managed.VertexBuffer);
        // fixed (DrawCall* drawsPtr = managed.Draws)
        // fixed (UnmanagedVertexBuffer vertexBuffersPtr = vertexBuffers)
        // {
        //     return new()
        //     {
        //         BufferCount = managed.BufferCount,
        //         CurrentBuffer = managed.CurrentBuffer,
        //         VertexBuffer = vertexBuffersPtr,
        //         Draws = drawsPtr,
        //         CurrentDepth = managed.CurrentDepth,
        //         DrawCounter = managed.DrawCounter,
        //     };
        // }

        throw new NotImplementedException();
    }

    public static RenderBatch ConvertToManaged(UnmanagedRenderBatch unmanaged)
    {
        Span<DrawCall> draws = new(unmanaged.Draws, unmanaged.DrawCounter);
        Span<VertexBuffer> vertexBuffers = new(unmanaged.VertexBuffer, unmanaged.BufferCount);
        return new()
        {
            BufferCount = unmanaged.BufferCount,
            CurrentBuffer = unmanaged.CurrentBuffer,
            CurrentDepth = unmanaged.CurrentDepth,
            DrawCounter = unmanaged.DrawCounter,
            Draws = draws.ToArray(),
            VertexBuffer = vertexBuffers.ToArray(),
        };
    }
}
