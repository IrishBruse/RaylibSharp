namespace RaylibSharp.GL;

using System.Runtime.InteropServices;

/// <summary> A multi-OpenGL abstraction layer with an immediate-mode style API </summary>
public static unsafe partial class RLGL
{
    const string LIB = "raylib";

    /// <summary> Load a render batch system </summary>
    public static RenderBatch LoadRenderBatch(int numBuffers, int bufferElements)
    {
        throw new NotImplementedException();
    }

    /// <summary> Load a render batch system </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadRenderBatch")]
    private static partial UnmanagedRenderBatch _LoadRenderBatch(int numBuffers, int bufferElements);

}
