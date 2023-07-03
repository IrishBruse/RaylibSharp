namespace RaylibSharp.Generator;

public record StructConfig(string[] AdditionalProperties, bool GenUnmanaged = true, bool GenManaged = true, bool UnmanagedAttribute = false, bool UseAsClass = false)
{
}
