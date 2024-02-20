namespace RaylibSharp;

using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Material), MarshalMode.ManagedToUnmanagedIn, typeof(MaterialMarshaller))]
[CustomMarshaller(typeof(Material), MarshalMode.ManagedToUnmanagedOut, typeof(MaterialMarshaller))]
static unsafe class MaterialMarshaller
{
    public static UnmanagedMaterial ConvertToUnmanaged(Material managed)
    {
        UnmanagedMaterial res;
        GCHandle vertices = GCHandle.Alloc(managed.Maps, GCHandleType.Pinned);

        res = new()
        {
            Shader = ShaderMarshaller.ConvertToUnmanaged(managed.Shader),
            Maps = (MaterialMap*)vertices.AddrOfPinnedObject(),
        };

        res.Params[0] = managed.Params[0];
        res.Params[1] = managed.Params[1];
        res.Params[2] = managed.Params[2];
        res.Params[3] = managed.Params[3];

        return res;
    }

    public static Material ConvertToManaged(UnmanagedMaterial unmanaged)
    {
        Material ret = new();
        ret.Shader = ShaderMarshaller.ConvertToManaged(unmanaged.Shader);

        if (unmanaged.Params != null)
        {
            ret.Params = new Vector4(unmanaged.Params[0], unmanaged.Params[1], unmanaged.Params[2], unmanaged.Params[3]);
        }

        if (unmanaged.Maps != null)
        {
            ret.Maps = new Span<MaterialMap>(unmanaged.Maps, 12).ToArray();
        }

        return ret;
    }
}
