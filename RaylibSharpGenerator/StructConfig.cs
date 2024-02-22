namespace RaylibSharp.Generator;

public class StructConfig
{
    public string[] AdditionalProperties { get; set; } = [];
    public bool GenUnmanaged { get; set; } = true;
    public bool GenManaged { get; set; } = true;
    public bool UnmanagedAttribute { get; set; }
    public Dictionary<string, string> FunctionTypeConversion { get; set; } = new Dictionary<string, string>();
    public string[] Remove { get; set; } = [];
    public string[] UnmanagedRemove { get; set; } = [];

    public static readonly Dictionary<string, StructConfig> Data = new() {
        {
            "FilePathList",
            new StructConfig() {
                UnmanagedAttribute = true,
                AdditionalProperties = [
                    "internal sbyte** PathsPtr;"
                ]
            }
        },
        {
            "Shader",
            new StructConfig() {
                UnmanagedAttribute = true,
                GenUnmanaged = false,
            }
        },
        {
            "Font",
            new StructConfig() {
                UnmanagedAttribute = true
            }
        },
        {
            "Camera2D",
            new StructConfig() {
                GenUnmanaged = false
            }
        },
        {
            "Camera3D",
            new StructConfig() {
                GenUnmanaged = false
            }
        },
        {
            "Mesh",
            new StructConfig() {
                UnmanagedAttribute = true,
                GenManaged = false,
                GenUnmanaged = false
            }
        },
        {
            "Model",
            new StructConfig() {
                UnmanagedAttribute = true,
                Remove=[
                    "materialCount",
                    "meshCount",
                    "boneCount"
                ]
            }
        },
        {
            "Material",
            new StructConfig() {
                UnmanagedAttribute = true,
                UnmanagedRemove = [
                    "shader"
                ]
            }
        },
        {
            "ModelAnimation",
            new StructConfig() {
                UnmanagedAttribute = true
            }
        },
        {
            "Texture",
            new StructConfig() {
                GenUnmanaged = false
            }
        },
        {
            "Image",
            new StructConfig() {
                GenUnmanaged = false,
                FunctionTypeConversion = new() {
                    {"Format", "PixelFormat Format"}
                }
            }
        },
        {
            "MaterialMap",
            new StructConfig() {
                GenUnmanaged = false
            }
        },
        {
            "NPatchInfo",
            new StructConfig() {
                GenUnmanaged = false
            }
        },
        {
            "GlyphInfo",
            new StructConfig() {
                GenUnmanaged = false
            }
        },
        {
            "rlDrawCall",
            new StructConfig() {
                GenUnmanaged = false
            }
        }
    };
}
