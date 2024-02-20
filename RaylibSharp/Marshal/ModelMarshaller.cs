namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Model), MarshalMode.ManagedToUnmanagedIn, typeof(ModelMarshaller))]
[CustomMarshaller(typeof(Model), MarshalMode.ManagedToUnmanagedOut, typeof(ModelMarshaller))]
static unsafe class ModelMarshaller
{
    public static UnmanagedModel ConvertToUnmanaged(Model managed)
    {
        fixed (Mesh* meshes = managed.Meshes)
        {
            return new()
            {
                Transform = managed.Transform,
                MeshCount = managed.Meshes.Length,
                MaterialCount = managed.Materials.Length,
                BoneCount = managed.Bones.Length,
                Meshes = meshes,
                // Materials = managed.Materials,
                // MeshMaterial = managed.MeshMaterial,
                // Bones = managed.Bones,
                // BindPose = managed.BindPose,
            };
        }
    }

    public static Model ConvertToManaged(UnmanagedModel unmanaged)
    {
        return new();
    }
}
