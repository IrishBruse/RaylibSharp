namespace RaylibSharp;

using System.Runtime.InteropServices.Marshalling;

[CustomMarshaller(typeof(ModelAnimation), MarshalMode.ManagedToUnmanagedIn, typeof(ModelAnimationMarshaller))]
[CustomMarshaller(typeof(ModelAnimation), MarshalMode.ManagedToUnmanagedOut, typeof(ModelAnimationMarshaller))]
[CustomMarshaller(typeof(ModelAnimation), MarshalMode.ElementOut, typeof(Out))]
internal static unsafe class ModelAnimationMarshaller
{
    public static UnmanagedModelAnimation ConvertToUnmanaged(ModelAnimation managed)
    {
        throw new NotImplementedException();
    }

    public static ModelAnimation ConvertToManaged(UnmanagedModelAnimation unmanaged)
    {
        throw new NotImplementedException();
    }

    internal static class Out
    {
        public static ModelAnimation ConvertToManaged(UnmanagedColor unmanaged)
        {
            throw new NotImplementedException();
        }

        public static void Free(UnmanagedColor unmanaged)
        {
            throw new NotImplementedException();
        }
    }
}
