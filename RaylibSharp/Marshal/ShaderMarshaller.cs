namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(Shader), MarshalMode.ManagedToUnmanagedIn, typeof(ShaderMarshaller))]
[CustomMarshaller(typeof(Shader), MarshalMode.ManagedToUnmanagedOut, typeof(ShaderMarshaller))]
internal static unsafe class ShaderMarshaller
{
    // https://github.com/raysan5/raylib/blob/334e96d470c5cb09d154e1c849d04adb721a7a8c/src/rlgl.h#L227C13-L227C53
    private const int RL_MAX_SHADER_LOCATIONS = 32;

    public static UnmanagedShader ConvertToUnmanaged(Shader managed)
    {
        fixed (int* variable = managed.Locs)
        {
            int* array = variable;
            return new()
            {
                Id = managed.Id,
                Locs = array,
            };
        }
    }

    public static Shader ConvertToManaged(UnmanagedShader unmanaged)
    {
        Span<int> locs = new(unmanaged.Locs, RL_MAX_SHADER_LOCATIONS);
        return new()
        {
            Id = unmanaged.Id,
            Locs = locs.ToArray(),
        };
    }
}
