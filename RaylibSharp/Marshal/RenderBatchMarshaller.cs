namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

using RaylibSharp.GL;

[CustomMarshaller(typeof(RenderBatch), MarshalMode.ManagedToUnmanagedIn, typeof(RenderBatchMarshaller))]
[CustomMarshaller(typeof(RenderBatch), MarshalMode.ManagedToUnmanagedOut, typeof(RenderBatchMarshaller))]
internal static unsafe class RenderBatchMarshaller
{
    // TODO: Fix this as it is not working yet
    public static UnmanagedRenderBatch ConvertToUnmanaged(RenderBatch managed)
    {
        fixed (DrawCall* drawsPtr = managed.Draws)
        fixed (VertexBuffer* vertexBuffersPtr = managed.VertexBuffer)
        {
            return new()
            {
                BufferCount = managed.BufferCount,
                CurrentBuffer = managed.CurrentBuffer,
                CurrentDepth = managed.CurrentDepth,
                DrawCounter = managed.DrawCounter,
                Draws = drawsPtr,
                // Vertexbuffer = vertexBuffersPtr,
            };
        }
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
