namespace RaylibSharp;

static unsafe class MeshMarshaller
{
    //     public static UnmanagedMesh ConvertToUnmanaged(Mesh managed)
    //     {
    //         int vertCount = managed.Vertices.Length;

    //         UnmanagedMesh res = new();
    //         res.VertexCount = vertCount;
    //         res.TriangleCount = managed.TriangleCount;
    //         res.VaoId = managed.VaoId;

    //         res.Vertices = Raylib.Allocate(managed.Vertices);

    //         GCHandle texcoords = GCHandle.Alloc(managed.Texcoords, GCHandleType.Pinned);
    //         res.Texcoords = (float*)texcoords.AddrOfPinnedObject();
    //         texcoords.Free();

    //         GCHandle texcoords2 = GCHandle.Alloc(managed.Texcoords2, GCHandleType.Pinned);
    //         res.Texcoords2 = (float*)texcoords2.AddrOfPinnedObject();
    //         texcoords2.Free();

    //         GCHandle normals = GCHandle.Alloc(managed.Normals, GCHandleType.Pinned);
    //         res.Normals = (float*)normals.AddrOfPinnedObject();
    //         normals.Free();

    //         GCHandle tangents = GCHandle.Alloc(managed.Tangents, GCHandleType.Pinned);
    //         res.Tangents = (float*)tangents.AddrOfPinnedObject();
    //         tangents.Free();

    //         GCHandle colors = GCHandle.Alloc(managed.Colors, GCHandleType.Pinned);
    //         res.Colors = (byte*)colors.AddrOfPinnedObject();
    //         colors.Free();

    //         GCHandle indices = GCHandle.Alloc(managed.Indices, GCHandleType.Pinned);
    //         res.Indices = (short*)indices.AddrOfPinnedObject();
    //         indices.Free();

    //         GCHandle animVertices = GCHandle.Alloc(managed.AnimVertices, GCHandleType.Pinned);
    //         res.AnimVertices = (float*)animVertices.AddrOfPinnedObject();
    //         animVertices.Free();

    //         GCHandle animNormals = GCHandle.Alloc(managed.AnimNormals, GCHandleType.Pinned);
    //         res.AnimNormals = (float*)animNormals.AddrOfPinnedObject();
    //         animNormals.Free();

    //         GCHandle boneIds = GCHandle.Alloc(managed.BoneIds, GCHandleType.Pinned);
    //         res.BoneIds = (byte*)boneIds.AddrOfPinnedObject();
    //         boneIds.Free();

    //         GCHandle boneWeights = GCHandle.Alloc(managed.BoneWeights, GCHandleType.Pinned);
    //         res.BoneWeights = (float*)boneWeights.AddrOfPinnedObject();
    //         boneWeights.Free();

    //         GCHandle vboId = GCHandle.Alloc(managed.VboId, GCHandleType.Pinned);
    //         res.VboId = (uint*)vboId.AddrOfPinnedObject();
    //         vboId.Free();

    //         return res;
    //     }

    // #pragma warning disable IDE0011
    //     public static Mesh ConvertToManaged(UnmanagedMesh unmanaged)
    //     {
    //         int triangleCount = unmanaged.TriangleCount;
    //         int vertexCount = unmanaged.VertexCount;

    //         Mesh res = new();

    //         res.TriangleCount = triangleCount;
    //         res.VaoId = unmanaged.VaoId;
    //         if (unmanaged.AnimVertices != null) { res.AnimVertices = new Span<float>(unmanaged.AnimVertices, vertexCount).ToArray(); }
    //         if (unmanaged.AnimNormals != null) { res.AnimNormals = new Span<float>(unmanaged.AnimNormals, vertexCount).ToArray(); }
    //         if (unmanaged.BoneIds != null) { res.BoneIds = new Span<byte>(unmanaged.BoneIds, vertexCount).ToArray(); }
    //         if (unmanaged.BoneWeights != null) { res.BoneWeights = new Span<float>(unmanaged.BoneWeights, vertexCount).ToArray(); }
    //         if (unmanaged.Colors != null) { res.Colors = new Span<Color>(unmanaged.Colors, vertexCount).ToArray(); }
    //         if (unmanaged.Texcoords != null) { res.Texcoords = new Span<Vector2>(unmanaged.Texcoords, vertexCount).ToArray(); }
    //         if (unmanaged.Texcoords2 != null) { res.Texcoords2 = new Span<Vector2>(unmanaged.Texcoords2, vertexCount).ToArray(); }
    //         if (unmanaged.Indices != null) { res.Indices = new Span<short>(unmanaged.Indices, vertexCount).ToArray(); }
    //         if (unmanaged.Normals != null) { res.Normals = new Span<Vector3>(unmanaged.Normals, vertexCount).ToArray(); }
    //         if (unmanaged.Tangents != null) { res.Tangents = new Span<Vector4>(unmanaged.Tangents, vertexCount).ToArray(); }
    //         if (unmanaged.VboId != null) { res.VboId = new Span<uint>(unmanaged.VboId, 7).ToArray(); } // https://github.com/raysan5/raylib/blob/c57b8d5a6abbf210fece6b44dcdadd112a7e072e/src/config.h#L218
    //         if (unmanaged.Vertices != null) { res.Vertices = new Span<Vector3>(unmanaged.Vertices, vertexCount).ToArray(); }

    //         return res;
    //     }
    // #pragma warning restore
}
