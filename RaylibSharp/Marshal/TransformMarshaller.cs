namespace RaylibSharp;

static class TransformMarshaller
{
    public static UnmanagedTransform ConvertToUnmanaged(Transform managed)
    {
        return new UnmanagedTransform
        {
            Translation = managed.Translation,
            Rotation = managed.Rotation,
            Scale = managed.Scale,
        };
    }

    public static Transform ConvertToManaged(UnmanagedTransform unmanaged)
    {
        return new Transform
        {
            Translation = unmanaged.Translation,
            Rotation = unmanaged.Rotation,
            Scale = unmanaged.Scale,
        };
    }
}
