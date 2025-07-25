namespace RaylibSharp;

using System;

static unsafe class BoneInfoMarshaller
{
    public static UnmanagedBoneInfo ConvertToUnmanaged(BoneInfo managed)
    {
        UnmanagedBoneInfo unmanaged = default;
        // Copy managed.Name to unmanaged.Name (fixed buffer)
        char* dst = unmanaged.Name;
        int len = Math.Min(managed.Name?.Length ?? 0, 32);
        for (int i = 0; i < len; i++)
            dst[i] = managed.Name![i];
        for (int i = len; i < 32; i++)
            dst[i] = '\0';
        unmanaged.Parent = managed.Parent;
        return unmanaged;
    }

    public static BoneInfo ConvertToManaged(UnmanagedBoneInfo unmanaged)
    {
        string name = new string(unmanaged.Name, 0, 32).TrimEnd('\0');
        return new BoneInfo
        {
            Name = name,
            Parent = unmanaged.Parent,
        };
    }
}
