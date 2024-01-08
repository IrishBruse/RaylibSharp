namespace RaylibSharp;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(FilePathList), MarshalMode.ManagedToUnmanagedIn, typeof(FilePathListMarshaller))]
[CustomMarshaller(typeof(FilePathList), MarshalMode.ManagedToUnmanagedOut, typeof(FilePathListMarshaller))]
static unsafe class FilePathListMarshaller
{
    public static UnmanagedFilePathList ConvertToUnmanaged(FilePathList managed)
    {
        return new()
        {
            Count = managed.Count,
            Capacity = managed.Capacity,
            Paths = managed.PathsPtr,
        };
    }

    public static FilePathList ConvertToManaged(UnmanagedFilePathList unmanaged)
    {
        List<string> paths = new();

        for (int i = 0; i < unmanaged.Count; i++)
        {
            paths.Add(Marshal.PtrToStringAnsi((nint)unmanaged.Paths[i])!);
        }

        return new()
        {
            Count = unmanaged.Count,
            Capacity = unmanaged.Capacity,
            Paths = paths.ToArray(),
            PathsPtr = unmanaged.Paths,
        };
    }
}
